using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Interfaces.Data.Mammals
{
    public interface IAfricanElephant : IMammal
    {
        #region Interface characteristic
        /// <summary>
        /// Characteristic of elephant.
        /// </summary>

        public float Height { get; set; }
        public float Weight { get; set; }
        public float Tusklength  { get; set; }
        public int Longlifespan { get; set; }
        public string Socialbehavior { get; set; }
        #endregion // Interface characteristic
    }
}
