using OutSystems.ExternalLibraries.SDK;
using System;
using System.Xml.Linq;

namespace DoiTLean.FuzzyWuzzy.Structures
{
    /// <summary>
    /// The JSON Pair struct represents a record of attribute, previous and new value for each attribute compared by the DiffJSON class
    /// </summary>
    [OSStructure(Description = "Result record")]
    public struct ResultRecord
    {

        [OSStructureField(DataType = OSDataType.Text)]
        public string String;

        [OSStructureField(DataType = OSDataType.Integer)]
        public int Score;

        [OSStructureField(DataType = OSDataType.Integer)]
        public int Index;



        /// <summary>
        /// Constructs an RemoteItem.
        /// </summary>
        public ResultRecord(string sString, int iScore, int iIndex) : this()
        {
            this.String = sString ?? string.Empty;
            this.Score = iScore;
            this.Index = iIndex;
        }

        public ResultRecord()
        {
            this.String = string.Empty;
            this.Score = 0;
            this.Index = 0;
        }
    }

}


