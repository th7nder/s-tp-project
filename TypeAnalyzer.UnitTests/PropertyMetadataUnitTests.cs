using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TypeAnalyzer.Model;

namespace TypeAnalyzer.UnitTests
{
  [TestClass]
  public class PropertyMetadataUnitTests
  {
    [TestMethod]
    public void PropertyMetadata_LoadsAttributes()
    {
      NamespaceMetadata namespaceMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.First();
      TypeMetadata propertyType = namespaceMetadata.Types.Single(type => type.Name == "BaseClass");

      PropertyMetadata propertyMetadata = propertyType.Properties.Single(prop => prop.Name == "Class");

      Assert.IsNotNull(propertyMetadata.Attributes.Single(attr => attr.Type.Name == "TestAttribute"));
    }

    [TestMethod]
    public void PropertyMetadata_LoadsAccessors()
    {
      NamespaceMetadata namespaceMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.First();
      TypeMetadata propertyType = namespaceMetadata.Types.Single(type => type.Name == "BaseClass");

      PropertyMetadata propertyMetadata = propertyType.Properties.Single(prop => prop.Name == "Class");

      Assert.AreEqual(2, propertyMetadata.Accessors.Count());
    }
  }
}
