# odc-fuzzywuzzy

.NET External Library that exposes fuzzy string matching (Levenshtein distance based) as
OutSystems Developer Cloud (ODC) External Logic actions. Wraps
[FuzzySharp](https://github.com/JakeBayer/FuzzySharp) — the .NET port of Python's
[FuzzyWuzzy](https://github.com/seatgeek/fuzzywuzzy) by SeatGeek.

For the OutSystems-consumer view of this library (actions, inputs/outputs, JSON examples), see
[DOC.md](DOC.md).

## Requirements

- .NET SDK 10.0 (`dotnet --list-sdks` to confirm it's installed)
- Linux x64 target for packaging (`generate_upload_package.ps1` publishes `linux-x64`,
  self-contained `false` — a matching .NET runtime must exist on the ODC target)

## Repository structure

```
DoiTLean.FuzzyWuzzy/              Library project (the External Library itself)
  IFuzzyWuzzy.cs                  OSInterface — one action per exposed method
  FuzzyWuzzy.cs                   OSInterface implementation
  Structures/
    TextRecord.cs                 OSStructure — single input item for Process_* actions
    ResultRecord.cs                OSStructure — single match result
  generate_upload_package.ps1     Publish + zip packaging script
DoiTLean.FuzzyWuzzy.UnitTests/    NUnit test project
Dist/                             Packaged .zip output (generated, not source)
```

## Build & test

```bash
dotnet build DoiTLean.FuzzyWuzzy/DoiTLean.FuzzyWuzzy.sln
dotnet test DoiTLean.FuzzyWuzzy/DoiTLean.FuzzyWuzzy.sln
```

Both should report 0 warnings / all tests passing before merging changes.

## Packaging / publishing

From `DoiTLean.FuzzyWuzzy/`, run (PowerShell):

```powershell
./generate_upload_package.ps1
```

This runs `dotnet publish -c Release -r linux-x64 --self-contained false` and compresses the
publish output into `../Dist/ODC-FuzzyWuzzy.zip`. That zip is what gets uploaded as an External
Library asset in ODC (see [DOC.md](DOC.md) for the consumer-side install steps).

If PowerShell (`pwsh`) isn't available in your environment, the equivalent commands are:

```bash
dotnet publish DoiTLean.FuzzyWuzzy/DoiTLean.FuzzyWuzzy.csproj -c Release -r linux-x64 --self-contained false
cd DoiTLean.FuzzyWuzzy/bin/Release/net10.0/linux-x64/publish
zip -r ../../../../../Dist/ODC-FuzzyWuzzy.zip .
```

## Core concepts

- **`IFuzzyWuzzy`** — the `[OSInterface]` contract. Every method meant to be callable from
  OutSystems must carry `[OSAction]`; a method without it compiles fine but is silently invisible
  to ODC.
- **Ratio methods** (`Ratio`, `PartialRatio`, `TokenSortRatio`, `WeightedRatio`, ...) — compare two
  strings and return a 0-100 similarity score. Thin wrappers over `FuzzySharp.Fuzz`.
- **`Process_*` methods** (`Process_ExtractOne/Top/All/Sorted`) — compare one search string against
  a list of `TextRecord` candidates and return the best match(es) as `ResultRecord`(s). Thin
  wrappers over `FuzzySharp.Process`.
- **Null/empty safety** — all `Process_*` actions treat a missing/empty search string or candidate
  list as "no match" (empty list / default `ResultRecord`) rather than throwing, since an unhandled
  exception here would surface as an opaque runtime error inside an OutSystems app.

## Known limitations

- `Process_*` actions only guard the top-level `String`/`Strings` inputs; they do not validate
  individual `TextRecord.Text` values beyond treating a null `Text` as an empty string.
- Packaging targets `linux-x64` only; adjust the `-r` (runtime identifier) in
  `generate_upload_package.ps1` if you need a different target platform.

## OutSystems Links

- [ODC Documentation](https://success.outsystems.com/documentation/outsystems_developer_cloud/)
- [ODC External Logic](https://success.outsystems.com/documentation/outsystems_developer_cloud/building_apps/extend_your_apps_with_external_logic/)
- [ODC External Libraries SDK](https://success.outsystems.com/documentation/outsystems_developer_cloud/building_apps/extend_your_apps_with_external_logic/external_libraries_sdk_readme/)
- [FuzzyWuzzy](https://github.com/seatgeek/fuzzywuzzy)
- [Levenshtein Distance](https://en.wikipedia.org/wiki/Levenshtein_distance)
