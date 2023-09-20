using SampleHierarchies.Interfaces.Data;

namespace SampleHierarchies.Interfaces.Services;

public interface ISettingsService
{
    #region Interface Members

    ISettings? settings{ get; set; }


    /// <summary>
    /// Read settings.
    /// </summary>
    /// <param name="jsonPath">Json path</param>
    void Read(string jsonPath);

    /// <summary>
    /// Write settings.
    /// </summary>
    /// <param name="jsonPath">Json path</param>
    void Write(string jsonPath);

    #endregion // Interface Members
}
