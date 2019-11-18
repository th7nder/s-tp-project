using System;
using TypeAnalyzer.Model;

namespace TypeAnalyzer.UnitTests
{ 
  internal class TestReflector : Reflector
  {
    private const string ASSEMBLY_LOCATION = @"TestDLL\TypeAnalyzer.Examples.dll";
    public TestReflector() : base(ASSEMBLY_LOCATION) { }

    private static Lazy<TestReflector> _reflector = new Lazy<TestReflector>(() => new TestReflector());
    public static TestReflector Reflector => _reflector.Value;
  }
}
