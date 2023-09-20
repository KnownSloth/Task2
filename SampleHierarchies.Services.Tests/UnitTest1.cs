using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SampleHierarchies.Data;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using System.IO;

namespace SampleHierarchies.Services.Tests
{
    [TestClass]
    public class ScreenDefinitionServiceTests
    {
        private Mock<IScreenDefinition> mockScreenDefinition;
        private IScreenDefinitionService screenDefinitionService;

        [TestInitialize]
        public void Initialize()
        {
            // Initialize the mock and service before each test
            mockScreenDefinition = new Mock<IScreenDefinition>();
            screenDefinitionService = new ScreenDefinitionService();
        }

        [TestMethod]
        public void Load_ValidJsonPath_ReturnsScreenDefinition()
        {
            // Arrange
            string jsonPath = "valid.json";

            // Act
            IScreenDefinition result = screenDefinitionService.Load(jsonPath);

            // Assert
            Assert.IsNotNull(result);
            // Add more assertions as needed
        }

        [TestMethod]
        public void Load_InvalidJsonPath_ReturnsNull()
        {
            // Arrange
            string jsonPath = "invalid.json";

            // Act
            IScreenDefinition result = screenDefinitionService.Load(jsonPath);

            // Assert
            Assert.IsNull(result);
            // Add more assertions as needed
        }

        [TestMethod]
        public void Save_ValidScreenDefinition_ReturnsTrue()
        {
            // Arrange
            var screenDefinitionService = new ScreenDefinitionService();
            var screenDefinition = new ScreenDefinition();
            string jsonPath = "valid.json";
            

            // Act
            bool result = screenDefinitionService.Save(screenDefinition, jsonPath);

            // Assert
            Assert.IsTrue(result);
            // Add more assertions as needed
        }

        [TestMethod]
        public void Save_InvalidScreenDefinition_ReturnsFalse()
        {
            // Arrange
            string jsonPath = "valid.json";

            // Setup the mock to throw an exception when serialized
            mockScreenDefinition.Setup(sd => sd.Serialize()).Throws(new Exception("Serialization error"));

            // Act
            bool result = screenDefinitionService.Save(mockScreenDefinition.Object, jsonPath);

            // Assert
            Assert.IsFalse(result);
            // Add more assertions as needed
        }
    }
}
