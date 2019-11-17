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


    private class TestReflector : Reflector
    {
      private const string ASSEMBLY_LOCATION = @"TestDLL\TypeAnalyzer.Examples.dll";
      public TestReflector() : base(ASSEMBLY_LOCATION) { }

      private static Lazy<TestReflector> _reflector = new Lazy<TestReflector>(() => new TestReflector());
      public static TestReflector Reflector => _reflector.Value;
    }
  }
}
