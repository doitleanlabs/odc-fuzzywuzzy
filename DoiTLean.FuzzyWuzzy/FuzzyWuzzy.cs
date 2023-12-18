using System;
using System.IO;
using DoiTLean.FuzzyWuzzy.Structures;
using System.Collections.Generic;
using System.Diagnostics;
using FuzzySharp;
using FuzzySharp.Extractor;



namespace DoiTLean.FuzzyWuzzy {
    /// <summary>
    ///  Fuzzy string matching like a boss. It uses Levenshtein Distance to calculate the differences between sequences in a simple-to-use package
    /// </summary>
    public class FuzzyWuzzy : IFuzzyWuzzy {


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

            List<string> elements = new List<string>();
            foreach (TextRecord CurrentRec in Strings)
            {
                elements.Add(CurrentRec.Text.ToString());
            }
  
            IEnumerable<ExtractedResult<string>> results = FuzzySharp.Process.ExtractTop(String, elements, limit: Limit, cutoff: Cutoff);

            foreach (ExtractedResult<string> res in results)
            {
                ResultRecord record = new ResultRecord();

                record.Index = res.Index;
                record.Score = res.Score;
                record.String = res.Value;

                Result.Add(record);
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

            List<string> elements = new List<string>();
            foreach (TextRecord CurrentRec in Strings)
            {
                elements.Add(CurrentRec.Text.ToString());
            }
            
            IEnumerable<ExtractedResult<string>> results = FuzzySharp.Process.ExtractAll(String, elements, cutoff: Cutoff);

            foreach (ExtractedResult<string> res in results)
            {
                ResultRecord record = new ResultRecord();

                record.Index = res.Index;
                record.Score = res.Score;
                record.String = res.Value;

                Result.Add(record);
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

            List<string> elements = new List<string>();
            foreach (TextRecord CurrentRec in Strings)
            {
                elements.Add(CurrentRec.Text.ToString());
            }

            IEnumerable<ExtractedResult<string>> results = FuzzySharp.Process.ExtractSorted(String, elements, cutoff: Cutoff);

            foreach (ExtractedResult<string> res in results)
            {
                ResultRecord record = new ResultRecord();

                record.Index = res.Index;
                record.Score = res.Score;
                record.String = res.Value;

                Result.Add(record);
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
        /// 
        /// </summary>
        /// <param name="String"></param>
        /// <param name="Strings"></param>
        /// <param name="Result"></param>
        public void Process_ExtractOne(string String, List<TextRecord> Strings, out ResultRecord Result)
        {
            Result = new ResultRecord();

            List<string> elements = new List<string>();
            foreach (TextRecord CurrentRec in Strings)
            {
                elements.Add(CurrentRec.Text.ToString());
            }


            ExtractedResult<string> result = FuzzySharp.Process.ExtractOne(String, elements);
            Result.String = result.Value;
            Result.Score = result.Score;
            Result.Index = result.Index;

        } // Proce_ExtractOne




    } // FuzzyWuzzy


 }
