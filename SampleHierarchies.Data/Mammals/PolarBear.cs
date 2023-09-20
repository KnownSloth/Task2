using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Data.Mammals;

namespace SampleHierarchies.Data.Mammals
{
    public class PolarBear : MammalBase,IPolarBear
    {
        #region Public Methods
        /// <inheritdoc/>
        public override void Display()
        {
            Console.WriteLine($"My name is: {Name}, my age is: {Age} and I am a bear. I have thick fur coat { ThickFurCoat}, my paws is {LargePaws}, and carnivorous diet {CarnivorousDiet}. I`m semi-aquatic {SemiAquatic}. And finally my sense of smell is {ExcellentSenseOfSmell}. ");
        }

        /// <inheritdoc/>
        public override void Copy(IAnimal animal)
        {
            if (animal is IPolarBear ad)
            {
                base.Copy(animal);
                ThickFurCoat = ad.ThickFurCoat;
                LargePaws = ad.LargePaws;
                CarnivorousDiet = ad.CarnivorousDiet;
                SemiAquatic = ad.SemiAquatic;
                ExcellentSenseOfSmell = ad.ExcellentSenseOfSmell;
            }
        }

        #endregion // Public Methods
        #region Ctors And Properties

        /// <inheritdoc/>
        public string ThickFurCoat { get; set; }
        public string LargePaws { get; set; }
        public string CarnivorousDiet { get; set; }
        public bool SemiAquatic { get; set; }
        public string ExcellentSenseOfSmell { get; set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="age">Age</param>

        public PolarBear(string name, int age, string thickFurCoat, string largePaws, string carnivorousDiet, bool semiAquatic, string excellentSenseOfSmell) : base(name, age, MammalSpecies.PolarBear)
        {
            ThickFurCoat = thickFurCoat;
            LargePaws = largePaws;
            CarnivorousDiet = carnivorousDiet;
            SemiAquatic = semiAquatic;
            ExcellentSenseOfSmell = excellentSenseOfSmell;
        }
        #endregion // Ctors And Properties
    }
}
