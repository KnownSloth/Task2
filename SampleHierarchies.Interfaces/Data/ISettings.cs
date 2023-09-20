namespace SampleHierarchies.Interfaces.Data;

/// <summary>
/// Settings interface.
/// </summary>
public interface ISettings
{
    #region Interface Members

    /// <summary>
    /// Version of settings.
    /// </summary>
    string Version { get; set; }

    ConsoleColor MainScreen { get; set; }
    ConsoleColor AnimalsScreen { get; set; }
    ConsoleColor MammalsScreen { get; set; }
    ConsoleColor DogScreen { get; set; }

     

    #endregion // Interface Members

    void Display();
}

