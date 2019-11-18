using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TypeAnalyzer.Model;

namespace TypeAnalyzer.UnitTests
{
  [TestClass]
  public class FieldMetadataUnitTests
  {

    [TestMethod]
    public void FieldMetadata_ReadAttributes()
    {
      NamespaceMetadata namespaceMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.First();
      TypeMetadata fieldType = namespaceMetadata.Types.Single(type => type.Name == "Class1");
      FieldMetadata fieldMetadata = fieldType.Fields.Single(field => field.Name == "AnimalField");

      Assert.AreEqual("TestAttribute", fieldMetadata.Attributes.First().Type.Name);
    }

    [TestMethod]
    public void FieldMetadata_ParsesValidAccessModifier()
    {
      NamespaceMetadata namespaceMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.First();
      TypeMetadata containingType = namespaceMetadata.Types.Single(type => type.Name == "FieldTest");

      AccessModifier internalTypeModifier = containingType.Fields.Single(field => field.Name == "internalField").AccessModifier;
      AccessModifier privateTypeModifier = containingType.Fields.Single(field => field.Name == "privateField").AccessModifier;
      AccessModifier publicTypeModifier = containingType.Fields.Single(field => field.Name == "publicField").AccessModifier;
      AccessModifier protectedTypeModifier = containingType.Fields.Single(field => field.Name == "protectedField").AccessModifier;
      AccessModifier protectedInternalTypeModifier = containingType.Fields.Single(field => field.Name == "protectedInternalField").AccessModifier;
      AccessModifier privateProtectedTypeModifier = containingType.Fields.Single(field => field.Name == "privateProtectedField").AccessModifier;

      Assert.AreEqual(AccessModifier.Internal, internalTypeModifier);
      Assert.AreEqual(AccessModifier.Private, privateTypeModifier);
      Assert.AreEqual(AccessModifier.Public, publicTypeModifier);
      Assert.AreEqual(AccessModifier.Protected, protectedTypeModifier);
      Assert.AreEqual(AccessModifier.ProtectedInternal, protectedInternalTypeModifier);
      Assert.AreEqual(AccessModifier.PrivateProtected, privateProtectedTypeModifier);
    }
  }
}
