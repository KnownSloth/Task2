using Newtonsoft.Json;
using SampleHierarchies.Data;
using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using System.Diagnostics;

namespace SampleHierarchies.Services;

/// <summary>
/// Settings service.
/// </summary>
public class SettingsService : ISettingsService
{

    public ISettings? settings { get; set; }


    /// <inheritdoc/>
    public void Read(string jsonPath)
       
    {
        using (StreamReader r = new StreamReader("color.json"))
        {
            string json = r.ReadToEnd();
            settings = JsonConvert.DeserializeObject<Settings>(json);
        }
    }

    /// <inheritdoc/>
    public void Write(string jsonPath)
    {
        try
        {
            var jsonSettings = new JsonSerializerSettings();
            string jsonContent = JsonConvert.SerializeObject(settings);
            string jsonContentFormatted = jsonContent.FormatJson();
            File.WriteAllText(jsonPath + "color.json", jsonContentFormatted);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }


    public SettingsService()
    {
        settings = new Settings();
    }
}