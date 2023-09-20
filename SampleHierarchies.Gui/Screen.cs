namespace SampleHierarchies.Gui;

/// <summary>
/// Abstract base class for a screen.
/// </summary>
public abstract class Screen
{
    #region Public Methods

    /// <summary>
    /// Show the screen.
    /// </summary>
    public virtual void Show()
    {
        Console.Clear();
        Console.WriteLine("Showing screen");
        Console.Clear();
    }

    protected string ScreenDefinitionJson;

    #endregion // Public Methods
}
