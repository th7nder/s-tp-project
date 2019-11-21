using System.Reflection;

namespace TypeAnalyzer.Model
{
  public class Reflector
  {
    public AssemblyMetadata AssemblyMetadata { get; private set; }
    public Reflector(string assemblyFile)
    {
      AssemblyMetadata = new AssemblyMetadata(Assembly.LoadFrom(assemblyFile));
    }
  }
}
