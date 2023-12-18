using OutSystems.ExternalLibraries.SDK;
using System;
using System.Xml.Linq;

namespace DoiTLean.FuzzyWuzzy.Structures
{
    /// <summary>
    /// TextRecord
    /// </summary>
    [OSStructure(Description = "Text Record")]
    public struct TextRecord
    {

        [OSStructureField(DataType = OSDataType.Text)]
        public string Text;


        /// <summary>
        /// Constructs an TextRecord.
        /// </summary>
        public TextRecord(string sText) : this()
        {
            this.Text = sText ?? string.Empty;
           

        }
    }

}


