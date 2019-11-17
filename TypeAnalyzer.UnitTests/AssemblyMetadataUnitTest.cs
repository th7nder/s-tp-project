using System;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeAnalyzer.Model;

namespace TypeAnalyzer.UnitTests
{
  [TestClass]
  public class AssemblyMetadataUnitTest
  {

    [TestMethod]
    public void AssemblyMetadata_ParsesAssembly()
    {
      AssemblyMetadata assemblyMetadata = TestReflector.Reflector.AssemblyMetadata;

      Assert.IsTrue(assemblyMetadata.Name.Contains("TypeAnalyzer.Examples"));
      Assert.AreEqual(assemblyMetadata.Namespaces.Count(), 1);
    }

    [TestMethod]
    public void AssemblyMetadata_ParsesNamespace()
    {
      NamespaceMetadata namespaceMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.Single();

      Assert.IsTrue(namespaceMetadata.Types.Count() > 5);
    }

    [TestMethod]
    public void AssemblyMetadata_ParsesType()
    {
      TypeMetadata typeMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.Single().Types.Single(type => type.Name == "Class1");

      Assert.IsNotNull(typeMetadata);
    }

    [TestMethod]
    public void AssemblyMetadata_ParsesTypePropertyRecursively()
    {
      TypeMetadata typeMetadata = TestReflector.Reflector.AssemblyMetadata.Namespaces.Single().Types.Single(type => type.Name == "Class1");

      PropertyMetadata propertyMetadata = typeMetadata.Properties.Single(prop => prop.Name == "Class");

      Assert.IsNotNull(propertyMetadata);
      Assert.AreSame(typeMetadata, propertyMetadata.TypeMetadata);
    }

    [TestMethod]
    public void AssemblyMetadata_DoesNotDuplicateAttributeMetadata()
    {
      Assert.Fail("not implemented");
    }


    private class TestReflector : Reflector
    {
      private const string ASSEMBLY_LOCATION = @"TestDLL\TypeAnalyzer.Examples.dll";
      public TestReflector() : base(ASSEMBLY_LOCATION) { }

      private static Lazy<TestReflector> _reflector = new Lazy<TestReflector>(() => new TestReflector());
      public static TestReflector Reflector => _reflector.Value;
    }
  }
}
