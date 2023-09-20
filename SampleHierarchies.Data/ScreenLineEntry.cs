using SampleHierarchies.Interfaces.Data;

namespace SampleHierarchies.Data;

public class ScreenLineEntry : IScreenEntry
{

    public string Text { get; set; }

    public ConsoleColor BackgroundColor { get; set; }
    public ConsoleColor ForegroundColor { get; set; }

 
    public ScreenLineEntry()
    {
        Text = "Text";
        BackgroundColor = ConsoleColor.Black;
        ForegroundColor = ConsoleColor.White;
    }

}
