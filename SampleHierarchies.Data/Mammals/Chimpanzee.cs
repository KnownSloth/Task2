using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Data.Mammals;

namespace SampleHierarchies.Data.Mammals
{
    public class Chimpanzee : MammalBase, IChimpanzee
    {
        #region Public Methods

        /// <inheritdoc/>
        public override void Display()
        {
            Console.WriteLine($"My name is: {Name}, my age is: {Age} and I am a chimpanzee. Do opposable thumbs allow me to manipulate objects with dexterity? {OpposableThumbs}. My social behavior: {ComplexSocialBehavior}. Can I use the tools? {ToolUse}. My intelligence level is : {HighIntelligence}. My diet : {FlexibleDiet}. ");
        }

        /// <inheritdoc/>
        public override void Copy(IAnimal animal)
        {
            if (animal is IChimpanzee ad)
            {
                base.Copy(animal);
                OpposableThumbs = ad.OpposableThumbs;
                ComplexSocialBehavior = ad.ComplexSocialBehavior;
                ToolUse = ad.ToolUse;
                HighIntelligence = ad.HighIntelligence;
                FlexibleDiet = ad.FlexibleDiet;

            }
        }

        #endregion // Public Methods

        #region Ctors And Properties

        /// <inheritdoc/>
        public bool OpposableThumbs { get; set; }
        public string ComplexSocialBehavior { get; set; }
        public bool ToolUse { get; set; }
        public int HighIntelligence { get; set; }
        public string FlexibleDiet { get; set; }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="age">Age</param>
        public Chimpanzee(string name, int age, bool opposableThumbs, string complexSocialBehavior, bool toolUse, int highIntelligence, string flexibleDiet) : base(name, age, MammalSpecies.AfricanElephant)
        {
            OpposableThumbs = opposableThumbs;
            ComplexSocialBehavior = complexSocialBehavior;
            ToolUse = toolUse;
            HighIntelligence = highIntelligence;
            FlexibleDiet = flexibleDiet;

        }

        #endregion // Ctors And Properties
    }
}