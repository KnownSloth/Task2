using SampleHierarchies.Interfaces.Data;

namespace SampleHierarchies.Interfaces.Services;

public interface IScreenDefinitionService
{
    #region Interface Members

    IScreenDefinition Load(string jsonPath);

    bool Save(IScreenDefinition screenDefinition, string jsonPath);

    #endregion // Interface Members
}
