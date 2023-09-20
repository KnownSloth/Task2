namespace SampleHierarchies.Interfaces.Data;

/// <summary>
/// Settings interface.
/// </summary>
public interface IScreenEntry
{
    #region Interface Members

    string Text { get; set; }

    ConsoleColor BackgroundColor { get; set; }
    ConsoleColor ForegroundColor { get; set; }


    #endregion // Interface Members
}

