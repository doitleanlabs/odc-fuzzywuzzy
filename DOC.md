# FuzzyWuzzy — OutSystems ODC External Library

Fuzzy string matching for OutSystems Developer Cloud (ODC) apps, based on Levenshtein distance
(FuzzySharp / SeatGeek FuzzyWuzzy algorithm). This document describes the library from the
**OutSystems consumer** point of view — installing it and calling its actions from a Service
Studio module. For the .NET source code, see [README.md](README.md).

## Installing

1. In ODC Portal, open your application (or a shared module) and go to **Logic > External
   Libraries**.
2. Upload the packaged `ODC-FuzzyWuzzy.zip` (see release assets).
3. Once installed, an `FuzzyWuzzy` External Logic interface becomes available in Service Studio,
   with the actions and structures listed below.

## Structures

### `TextRecord`

Single candidate string, used as input to the `Process_*` actions.

| Field | Type | Description |
|-------|------|--------------|
| `Text` | Text | The candidate string. |

Example: `{ "Text": "Dallas Cowboys" }`

### `ResultRecord`

Single match result, returned by the `Process_*` actions.

| Field | Type | Description |
|-------|------|--------------|
| `String` | Text | The matched candidate string. |
| `Score` | Integer | Similarity score, 0-100 (100 = identical). |
| `Index` | Integer | Position of the match in the input `TextRecord` list. |

Example: `{ "String": "Dallas Cowboys", "Score": 90, "Index": 3 }`

## Actions

All actions below are exposed under the `FuzzyWuzzy` interface.

### Ratio actions

Each of these compares two strings and returns a similarity score (0-100) via the `Ratio` output
parameter. They differ in how they normalize/tokenize the input before comparing:

| Action | Behavior | Example | Result |
|--------|----------|---------|--------|
| `Ratio` | Plain Levenshtein ratio | `Ratio("mysmilarstring", "myawfullysimilarstirng")` | `72` |
| `PartialRatio` | Best matching substring | `PartialRatio("similar", "somewhresimlrbetweenthisstring")` | `71` |
| `TokenSortRatio` | Tokenizes, sorts words, then compares | `TokenSortRatio("order words out of", "words out of order")` | `100` |
| `PartialTokenSortRatio` | Same as above, partial match | `PartialTokenSortRatio("order words out of", "words out of order")` | `100` |
| `TokenSetRatio` | Compares as unordered sets of tokens | `TokenSetRatio("fuzzy was a bear", "fuzzy fuzzy fuzzy bear")` | `100` |
| `PartialTokenSetRatio` | Same as above, partial match | `PartialTokenSetRatio("fuzzy was a bear", "fuzzy fuzzy fuzzy bear")` | `100` |
| `TokenInitialismRatio` | Compares an initialism (e.g. acronym) against a full phrase | `TokenInitialismRatio("NASA", "National Aeronautics Space Administration")` | `100` |
| `PartialTokenInitialismRatio` | Same as above, partial match | `PartialTokenInitialismRatio("NASA", "National Aeronautics Space Administration, Kennedy Space Center, Cape Canaveral, Florida 32899")` | `100` |
| `TokenAbbreviationRatio` | Compares an abbreviation against a full phrase | `TokenAbbreviationRatio("bl 420", "Baseline section 420")` | `40` |
| `PartialTokenAbbreviationRatio` | Same as above, partial match | `PartialTokenAbbreviationRatio("bl 420", "Baseline section 420")` | `50` |
| `WeightedRatio` | Weighted combination of the above, generally the best all-round choice | `WeightedRatio("The quick brown fox jimps ofver the small lazy dog", "the quick brown fox jumps over the small lazy dog")` | `95` |

**Inputs:** `String1` (Text), `String2` (Text)
**Output:** `Ratio` (Integer)

If either input is empty, the underlying algorithm still runs and returns whatever score that
comparison implies (e.g. comparing against an empty string typically yields a low score) — these
actions do not special-case empty inputs.

### Process actions

Compare one search string against a list of `TextRecord` candidates.

#### `Process_ExtractOne`

Returns the single best match.

**Inputs:** `String` (Text), `Strings` (List of `TextRecord`)
**Output:** `Result` (`ResultRecord`)

Example:
```json
{
  "String": "cowboys",
  "Strings": [
    { "Text": "Atlanta Falcons" },
    { "Text": "New York Jets" },
    { "Text": "New York Giants" },
    { "Text": "Dallas Cowboys" }
  ]
}
```
Result: `{ "String": "Dallas Cowboys", "Score": 90, "Index": 3 }`

If `String` is empty or `Strings` is empty, `Result` comes back as a default/empty `ResultRecord`
(`String: ""`, `Score: 0`, `Index: 0`) — no error is raised.

#### `Process_ExtractTop`

Returns up to `Limit` matches with `Score >= Cutoff`.

**Inputs:** `String` (Text), `Strings` (List of `TextRecord`), `Limit` (Integer), `Cutoff` (Integer)
**Output:** `Result` (List of `ResultRecord`)

Example: `Process_ExtractTop("goolge", [google, bing, facebook, linkedin, twitter, googleplus,
bingnews, plexoogl], Limit=3, Cutoff=0)` →
`[{google,83,0}, {googleplus,75,5}, {plexoogl,43,7}]`

#### `Process_ExtractAll`

Returns every candidate with `Score >= Cutoff`, in input order.

**Inputs:** `String` (Text), `Strings` (List of `TextRecord`), `Cutoff` (Integer)
**Output:** `Result` (List of `ResultRecord`)

Example with `Cutoff=40`: `Process_ExtractAll("goolge", [google, bing, facebook, linkedin,
twitter, googleplus, bingnews, plexoogl], Cutoff=40)` →
`[{google,83,0}, {googleplus,75,5}, {plexoogl,43,7}]`

#### `Process_ExtractSorted`

Like `Process_ExtractAll`, but the results are sorted by `Score` descending.

**Inputs:** `String` (Text), `Strings` (List of `TextRecord`), `Cutoff` (Integer)
**Output:** `Result` (List of `ResultRecord`)

For all `Process_*` actions, if `String` is empty/null or `Strings` is null/empty, `Result` comes
back as an **empty list** rather than an error.

## Error handling

These actions do not throw exceptions for the common "nothing to compare" cases described above
(empty search string, empty/missing candidate list) — they return an empty result instead, so no
`try/catch` is required in your OutSystems logic for those cases. Any other unexpected error
(e.g. running out of memory on a very large `Strings` list) will surface as a standard OutSystems
server error for the action call.
