
using OutSystems.ExternalLibraries.SDK;
using System.Collections.Generic;
using DoiTLean.FuzzyWuzzy.Structures;

namespace DoiTLean.FuzzyWuzzy
{
    /// <summary>
    /// Fuzzy string matching like a boss. It uses Levenshtein Distance to calculate the differences between sequences in a simple-to-use package
    /// </summary>
    [OSInterface(Description = "Fuzzy string matching like a boss. It uses Levenshtein Distance to calculate the differences between sequences in a simple-to-use package", IconResourceName = "DoiTLean.FuzzyWuzzy.resources.odc_fuzzy_wuzzy.png", Name = "FuzzyWuzzy")]
    public interface IFuzzyWuzzy
    {




        /// <summary>
        /// Fuzz.Ratio(&quot;mysmilarstring&quot;,&quot;myawfullysimilarstirng&quot;)
        /// 72
        /// Fuzz.Ratio(&quot;mysmilarstring&quot;,&quot;mysimilarstring&quot;)
        /// 97
        /// </summary>
        /// <param name="String1"></param>
        /// <param name="String2"></param>
        /// <param name="Ratio"></param>
        [OSAction]
        void Ratio(string String1, string String2, out int Ratio);

        /// <summary>
        /// Fuzz.PartialRatio(&quot;similar&quot;, &quot;somewhresimlrbetweenthisstring&quot;)
        /// 71
        /// </summary>
        /// <param name="String1"></param>
        /// <param name="String2"></param>
        /// <param name=Ratio"></param>
        [OSAction] 
        void PartialRatio(string String1, string String2, out int Ratio);

        /// <summary>
        /// Fuzz.TokenSortRatio(&quot;order words out of&quot;,&quot;  words out of order&quot;)
        /// 100
        /// </summary>
        /// <param name="String1"></param>
        /// <param name="String2"></param>
        /// <param name="Ratio"></param>
        void TokenSortRatio(string String1, string String2, out int Ratio);

        /// <summary>
        /// Fuzz.PartialTokenSortRatio(&quot;order words out of&quot;,&quot;  words out of order&quot;)
        /// 100
        /// </summary>
        /// <param name="String1"></param>
        /// <param name="String2"></param>
        /// <param name="Ratio"></param>
        void PartialTokenSortRatio(string String1, string String2, out int Ratio);

        /// <summary>
        /// Fuzz.TokenSetRatio(&quot;fuzzy was a bear&quot;, &quot;fuzzy fuzzy fuzzy bear&quot;)
        /// 100
        /// </summary>
        /// <param name="String1"></param>
        /// <param name="String2"></param>
        /// <param name="Ratio"></param>
        void TokenSetRatio(string String1, string String2, out int Ratio);

        /// <summary>
        /// Fuzz.PartialTokenSetRatio(&quot;fuzzy was a bear&quot;, &quot;fuzzy fuzzy fuzzy bear&quot;)
        /// 100
        /// </summary>
        /// <param name="String1"></param>
        /// <param name="String2"></param>
        /// <param name="Ratio"></param>
        void PartialTokenSetRatio(string String1, string String2, out int Ratio);

        /// <summary>
        /// Fuzz.TokenInitialismRatio(&quot;NASA&quot;, &quot;National Aeronautics and Space Administration&quot;);
        /// 89
        /// Fuzz.TokenInitialismRatio(&quot;NASA&quot;, &quot;National Aeronautics Space Administration&quot;);
        /// 100
        /// Fuzz.TokenInitialismRatio(&quot;NASA&quot;, &quot;National Aeronautics Space Administration, Kennedy Space Center, Cape Canaveral, Florida 32899&quot;);
        /// 53
        /// </summary>
        /// <param name="String1"></param>
        /// <param name="String2"></param>
        /// <param name="Ratio"></param>
        void TokenInitialismRatio(string String1, string String2, out int Ratio);

        /// <summary>
        /// Fuzz.PartialTokenInitialismRatio(&quot;NASA&quot;, &quot;National Aeronautics Space Administration, Kennedy Space Center, Cape Canaveral, Florida 32899&quot;);
        /// 100
        /// </summary>
        /// <param name="String1"></param>
        /// <param name="String2"></param>
        /// <param name="Ratio"></param>
        void PartialTokenInitialismRatio(string String1, string String2, out int Ratio);

        /// <summary>
        /// Fuzz.TokenAbbreviationRatio(&quot;bl 420&quot;, &quot;Baseline section 420&quot;);
        /// 40
        /// </summary>
        /// <param name="String1"></param>
        /// <param name="String2"></param>
        /// <param name="Ratio"></param>
        void TokenAbbreviationRatio(string String1, string String2, out int Ratio);

        /// <summary>
        /// Fuzz.PartialTokenAbbreviationRatio(&quot;bl 420&quot;, &quot;Baseline section 420&quot;);
        /// 50
        /// </summary>
        /// <param name="String1"></param>
        /// <param name="String2"></param>
        /// <param name="Ratio"></param>
        void PartialTokenAbbreviationRatio(string String1, string String2, out int Ratio);

        /// <summary>
        /// Fuzz.WeightedRatio(&quot;The quick brown fox jimps ofver the small lazy dog&quot;, &quot;the quick brown fox jumps over the small lazy dog&quot;)
        /// 95
        /// </summary>
        /// <param name="String1"></param>
        /// <param name="String2"></param>
        /// <param name="Ratio"></param>
        void WeightedRatio(string String1, string String2, out int Ratio);

        /// <summary>
        /// Process.ExtractOne(&quot;cowboys&quot;, new[] { &quot;Atlanta Falcons&quot;, &quot;New York Jets&quot;, &quot;New York Giants&quot;, &quot;Dallas Cowboys&quot;})
        /// (string: Dallas Cowboys, score: 90, index: 3)
        /// </summary>
        /// <param name="String"></param>
        /// <param name="Strings"></param>
        /// <param name="Result"></param>
        void Process_ExtractOne(string String, List<TextRecord> Strings, out ResultRecord Result);

        /// <summary>
        /// Process.ExtractTop(&quot;goolge&quot;, new[] { &quot;google&quot;, &quot;bing&quot;, &quot;facebook&quot;, &quot;linkedin&quot;, &quot;twitter&quot;, &quot;googleplus&quot;, &quot;bingnews&quot;, &quot;plexoogl&quot; }, limit: 3);
        /// [(string: google, score: 83, index: 0), (string: googleplus, score: 75, index: 5), (string: plexoogl, score: 43, index: 7)]
        /// </summary>
        /// <param name="String"></param>
        /// <param name="Strings"></param>
        /// <param name="Limit"></param>
        /// <param name="Cutoff"></param>
        /// <param name="Result"></param>
        void Process_ExtractTop(string String, List<TextRecord> Strings, int Limit, int Cutoff, out List<ResultRecord> Result);

        /// <summary>
        /// Process.ExtractAll(&quot;goolge&quot;, new [] {&quot;google&quot;, &quot;bing&quot;, &quot;facebook&quot;, &quot;linkedin&quot;, &quot;twitter&quot;, &quot;googleplus&quot;, &quot;bingnews&quot;, &quot;plexoogl&quot; })
        /// [(string: google, score: 83, index: 0), (string: bing, score: 22, index: 1), (string: facebook, score: 29, index: 2), (string: linkedin, score: 29, index: 3), (string: twitter, score: 15, index: 4), (string: googleplus, score: 75, index: 5), (string: bingnews, score: 29, index: 6), (string: plexoogl, score: 43, index: 7)]
        /// // score cutoff
        /// Process.ExtractAll(&quot;goolge&quot;, new[] { &quot;google&quot;, &quot;bing&quot;, &quot;facebook&quot;, &quot;linkedin&quot;, &quot;twitter&quot;, &quot;googleplus&quot;, &quot;bingnews&quot;, &quot;plexoogl&quot; }, cutoff: 40)
        /// [(string: google, score: 83, index: 0), (string: googleplus, score: 75, index: 5), (string: plexoogl, score: 43, index: 7)]
        /// </summary>
        /// <param name="String"></param>
        /// <param name="Strings"></param>
        /// <param name="Cutoff"></param>
        /// <param name="Result"></param>
        void Process_ExtractAll(string String, List<TextRecord> Strings, int Cutoff, out List<ResultRecord> Result);

        /// <summary>
        /// Process.ExtractSorted(&quot;goolge&quot;, new [] {&quot;google&quot;, &quot;bing&quot;, &quot;facebook&quot;, &quot;linkedin&quot;, &quot;twitter&quot;, &quot;googleplus&quot;, &quot;bingnews&quot;, &quot;plexoogl&quot; })
        /// [(string: google, score: 83, index: 0), (string: googleplus, score: 75, index: 5), (string: plexoogl, score: 43, index: 7), (string: facebook, score: 29, index: 2), (string: linkedin, score: 29, index: 3), (string: bingnews, score: 29, index: 6), (string: bing, score: 22, index: 1), (string: twitter, score: 15, index: 4)]
        /// </summary>
        /// <param name="String"></param>
        /// <param name="Strings"></param>
        /// <param name="Cutoff"></param>
        /// <param name="Result"></param>
        void Process_ExtractSorted(string String, List<TextRecord> Strings, int Cutoff, out List<ResultRecord> Result);

    }
}