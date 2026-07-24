using DoiTLean.FuzzyWuzzy.Structures;
using System.Collections.Generic;
using System.Linq;
using FuzzySharp;
using FuzzySharp.Extractor;



namespace DoiTLean.FuzzyWuzzy {
    /// <summary>
    ///  Fuzzy string matching like a boss. It uses Levenshtein Distance to calculate the differences between sequences in a simple-to-use package
    /// </summary>
    public class FuzzyWuzzy : IFuzzyWuzzy {


        /// <summary>
        /// Converts the OutSystems record list into the plain string list FuzzySharp expects.
        /// Missing/null Text fields become empty strings so indexes still line up with the input.
        /// </summary>
        private static List<string> ToElementList(List<TextRecord> Strings)
        {
            if (Strings == null)
            {
                return new List<string>();
            }

            return Strings.Select(rec => rec.Text ?? string.Empty).ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="String"></param>
        /// <param name="Strings"></param>
        /// <param name="Limit"></param>
        /// <param name="Result"></param>
        /// <param name="Cutoff"></param>
        public void Process_ExtractTop(string String, List<TextRecord> Strings, int Limit, int Cutoff, out List<ResultRecord> Result)
        {
            Result = new List<ResultRecord>();

            // Nothing to search against: return an empty result instead of crashing across the OutSystems boundary.
            if (string.IsNullOrEmpty(String) || Strings == null || Strings.Count == 0)
            {
                return;
            }

            List<string> elements = ToElementList(Strings);

            IEnumerable<ExtractedResult<string>> results = FuzzySharp.Process.ExtractTop(String, elements, limit: Limit, cutoff: Cutoff);

            foreach (ExtractedResult<string> res in results)
            {
                Result.Add(new ResultRecord(res.Value, res.Score, res.Index));
            }

        } // Process_ExtractTop

        /// <summary>
        ///
        /// </summary>
        /// <param name="String"></param>
        /// <param name="Strings"></param>
        /// <param name="Result"></param>
        /// <param name="Cutoff"></param>
        public void Process_ExtractAll(string String, List<TextRecord> Strings, int Cutoff, out List<ResultRecord> Result)
        {
            Result = new List<ResultRecord>();

            if (string.IsNullOrEmpty(String) || Strings == null || Strings.Count == 0)
            {
                return;
            }

            List<string> elements = ToElementList(Strings);

            IEnumerable<ExtractedResult<string>> results = FuzzySharp.Process.ExtractAll(String, elements, cutoff: Cutoff);

            foreach (ExtractedResult<string> res in results)
            {
                Result.Add(new ResultRecord(res.Value, res.Score, res.Index));
            }
        } // Process_ExtractAll

        /// <summary>
        ///
        /// </summary>
        /// <param name="String"></param>
        /// <param name="Strings"></param>
        /// <param name="Result"></param>
        /// <param name="Cutoff"></param>
        public void Process_ExtractSorted(string String, List<TextRecord> Strings, int Cutoff, out List<ResultRecord> Result)
        {
            Result = new List<ResultRecord>();

            if (string.IsNullOrEmpty(String) || Strings == null || Strings.Count == 0)
            {
                return;
            }

            List<string> elements = ToElementList(Strings);

            IEnumerable<ExtractedResult<string>> results = FuzzySharp.Process.ExtractSorted(String, elements, cutoff: Cutoff);

            foreach (ExtractedResult<string> res in results)
            {
                Result.Add(new ResultRecord(res.Value, res.Score, res.Index));
            }
        } // Proce_ExtractSorted


        /// <summary>
        /// 
        /// </summary>
        /// <param name="String1"></param>
        /// <param name="String2"></param>
        /// <param name="Ratio"></param>
        public void Ratio(string String1, string String2, out int Ratio)
        {
            Ratio = Fuzz.Ratio(String1, String2);
        } // Ratio

        /// <summary>
        /// 
        /// </summary>
        /// <param name="String1"></param>
        /// <param name="String2"></param>
        /// <param name="Ratio"></param>
        public void PartialRatio(string String1, string String2, out int Ratio)
        {
            Ratio = Fuzz.PartialRatio(String1, String2);
        } // PartialRatio

        /// <summary>
        /// 
        /// </summary>
        /// <param name="String1"></param>
        /// <param name="String2"></param>
        /// <param name="Ratio"></param>
        public void TokenSortRatio(string String1, string String2, out int Ratio)
        {
            Ratio = Fuzz.TokenSortRatio(String1, String2);
        } // TokenSortRatio

        /// <summary>
        /// 
        /// </summary>
        /// <param name="String1"></param>
        /// <param name="String2"></param>
        /// <param name="Ratio"></param>
        public void PartialTokenSortRatio(string String1, string String2, out int Ratio)
        {
            Ratio = Fuzz.PartialTokenSortRatio(String1, String2);
        } // PartialTokenSortRatio

        /// <summary>
        /// 
        /// </summary>
        /// <param name="String1"></param>
        /// <param name="String2"></param>
        /// <param name="Ratio"></param>
        public void TokenSetRatio(string String1, string String2, out int Ratio)
        {
            Ratio = Fuzz.TokenSetRatio(String1, String2);
        } // TokenSetRatio

        /// <summary>
        /// 
        /// </summary>
        /// <param name="String1"></param>
        /// <param name="String2"></param>
        /// <param name="Ratio"></param>
        public void PartialTokenSetRatio(string String1, string String2, out int Ratio)
        {
            Ratio = Fuzz.PartialTokenSetRatio(String1, String2);
        } // PartialTokenSetRatio

        /// <summary>
        /// 
        /// </summary>
        /// <param name="String1"></param>
        /// <param name="String2"></param>
        /// <param name="Ratio"></param>
        public void TokenInitialismRatio(string String1, string String2, out int Ratio)
        {
            Ratio = Fuzz.TokenInitialismRatio(String1, String2);
        } // TokenInitialismRatio

        /// <summary>
        /// 
        /// </summary>
        /// <param name="String1"></param>
        /// <param name="String2"></param>
        /// <param name="Ratio"></param>
        public void PartialTokenInitialismRatio(string String1, string String2, out int Ratio)
        {
            Ratio = Fuzz.PartialTokenInitialismRatio(String1, String2);
        } // PartialTokenInitialismRatio

        /// <summary>
        /// 
        /// </summary>
        /// <param name="String1"></param>
        /// <param name="String2"></param>
        /// <param name="Ratio"></param>
        public void TokenAbbreviationRatio(string String1, string String2, out int Ratio)
        {
            Ratio = Fuzz.TokenAbbreviationRatio(String1, String2);
        } // TokenAbbreviationRatio

        /// <summary>
        /// 
        /// </summary>
        /// <param name="String1"></param>
        /// <param name="String2"></param>
        /// <param name="Ratio"></param>
        public void PartialTokenAbbreviationRatio(string String1, string String2, out int Ratio)
        {
            Ratio = Fuzz.PartialTokenAbbreviationRatio(String1, String2);
        } // PartialTokenAbbreviationRatio

        /// <summary>
        /// 
        /// </summary>
        /// <param name="String1"></param>
        /// <param name="String2"></param>
        /// <param name="Ratio"></param>
        public void WeightedRatio(string String1, string String2, out int Ratio)
        {
            Ratio = Fuzz.WeightedRatio(String1, String2);
        } // WeightedRatio

        /// <summary>
        /// Returns a default (empty) ResultRecord when there is nothing to match against,
        /// since FuzzySharp.Process.ExtractOne returns null in that case and would otherwise
        /// throw a NullReferenceException across the OutSystems boundary.
        /// </summary>
        /// <param name="String"></param>
        /// <param name="Strings"></param>
        /// <param name="Result"></param>
        public void Process_ExtractOne(string String, List<TextRecord> Strings, out ResultRecord Result)
        {
            Result = new ResultRecord();

            if (string.IsNullOrEmpty(String) || Strings == null || Strings.Count == 0)
            {
                return;
            }

            List<string> elements = ToElementList(Strings);

            ExtractedResult<string> result = FuzzySharp.Process.ExtractOne(String, elements);
            if (result == null)
            {
                return;
            }

            Result.String = result.Value;
            Result.Score = result.Score;
            Result.Index = result.Index;

        } // Process_ExtractOne




    } // FuzzyWuzzy


 }
