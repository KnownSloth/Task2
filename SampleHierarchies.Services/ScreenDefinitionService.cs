using Newtonsoft.Json;
using SampleHierarchies.Data;
using SampleHierarchies.Interfaces;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using System.Diagnostics;

namespace SampleHierarchies.Services;


public class ScreenDefinitionService : IScreenDefinitionService
{
    #region IScreenDefinitionService Implementation

    /// <inheritdoc/>
    public IScreenDefinition Load(string jsonPath)
    {
        if (jsonPath == null)
        {
            throw new ArgumentNullException(nameof(jsonPath));
        }

        try
        {
            if (File.Exists(jsonPath))
            {
                string jsonContent = File.ReadAllText(jsonPath);
                var jsonSettings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Objects
                };
                return JsonConvert.DeserializeObject<ScreenDefinition>(jsonContent, jsonSettings);
            }
            else
            {
                Console.WriteLine("File does not exist: " + jsonPath);
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error loading JSON: " + ex.ToString()); // Log the entire exception including inner exceptions
            Debug.WriteLine("Error loading JSON: " + ex.ToString());
            throw;
        }
    }

    /// <inheritdoc/>
    public bool Save(IScreenDefinition screenDefinition, string jsonPath)
    {
        if (screenDefinition == null)
        {
            throw new ArgumentNullException(nameof(screenDefinition));
        }

        if (jsonPath == null)
        {
            throw new ArgumentNullException(nameof(jsonPath));
        }

        try
        {
            var jsonSettings = new JsonSerializerSettings();
            string jsonContent = JsonConvert.SerializeObject(screenDefinition);
            string jsonContentFormatted = jsonContent.FormatJson();
            File.WriteAllText(jsonPath, jsonContentFormatted);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return false;
        }
    }

    #endregion // IScreenDefinitionService Implementation


#region Ctors

public ScreenDefinitionService()
    {
    }

    #endregion // Ctors
}
