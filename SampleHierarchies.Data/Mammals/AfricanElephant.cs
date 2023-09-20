using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Data.Mammals;

namespace SampleHierarchies.Data.Mammals;
/// <summary>
/// Very basic dog class.
/// </summary>
public class AfricanElephant : MammalBase, IAfricanElephant
{
    #region Public Methods

    /// <inheritdoc/>
    public override void MakeSound()
    {
        Console.WriteLine("My name is: {0} and I am barking", Name);
    }

    /// <inheritdoc/>
    public override void Move()
    {
        Console.WriteLine("My name is: {0} and I am running", Name);
    }

    /// <inheritdoc/>
    public override void Display()
    {
        Console.WriteLine($"My name is: {Name}, my age is: {Age} and I am an elephant. My height is {Height}, my weight is{Weight}, my tusk have {Tusklength} length. I can live about {Longlifespan}. My social behavior is {Socialbehavior} ");
    }

    /// <inheritdoc/>
    public override void Copy(IAnimal animal)
    {
        if (animal is IAfricanElephant ad)
        {
            base.Copy(animal);
            Height = ad.Height;
            Weight = ad.Weight;
            Tusklength = ad.Tusklength;
            Longlifespan = ad.Longlifespan;
            Socialbehavior = ad.Socialbehavior;
        }
    }

    #endregion // Public Methods

    #region Ctors And Properties

    /// <inheritdoc/>
    public float Height { get; set; }
    public float Weight { get; set; }
    public float Tusklength { get; set; }
    public int Longlifespan { get; set; }
    public string Socialbehavior { get; set; }

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="name">Name</param>
    /// <param name="age">Age</param>
    public AfricanElephant(string name, int age, float height, float weight, float tuskLength, int longLifespan,string socialBehavior) : base(name, age, MammalSpecies.AfricanElephant)
    {
        Height = height;
        Weight = weight;
        Tusklength = tuskLength;
        Longlifespan = longLifespan;
        Socialbehavior = socialBehavior;
            
    }

    #endregion // Ctors And Properties
}
