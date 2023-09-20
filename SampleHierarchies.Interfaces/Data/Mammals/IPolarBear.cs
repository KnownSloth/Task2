using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Interfaces.Data.Mammals
{
    public interface IPolarBear : IMammal
    {
        #region Interface characteristic
        /// <summary>
        /// Characteristic of elephant.
        /// </summary>

        public string ThickFurCoat { get; set; }
        public string LargePaws  { get; set; }
        public string CarnivorousDiet  { get; set; }
        public bool SemiAquatic { get; set; }
        public string ExcellentSenseOfSmell  { get; set; }
        #endregion // Interface characteristic
    }
}
