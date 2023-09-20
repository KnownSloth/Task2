using SampleHierarchies.Interfaces.Data;
using Newtonsoft.Json;
using System.Text.Json;

namespace SampleHierarchies.Data;

public class ScreenDefinition : IScreenDefinition
{

    public List<ScreenLineEntry> LineEntries { get; set; }
 
    public ScreenDefinition()
    {
        LineEntries = new List<ScreenLineEntry>();
    }
    public string Serialize()
    {
        string jsonContent = JsonConvert.SerializeObject(this);
        return jsonContent;
    }


}
