using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Interfaces.Data.Mammals
{
    public interface IChimpanzee : IMammal
    {
        #region Interface characteristic
        /// <summary>
        /// Characteristic of chimpanzee.
        /// </summary>

        public bool OpposableThumbs { get; set; }
        public string ComplexSocialBehavior { get; set; }
        public bool ToolUse { get; set; }
        public int HighIntelligence { get; set; }
        public string FlexibleDiet { get; set; }
        #endregion // Interface characteristic
    }
}
