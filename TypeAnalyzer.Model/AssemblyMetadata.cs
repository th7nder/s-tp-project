using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TypeAnalyzer.Model
{
  public class AssemblyMetadata
  {
    public IEnumerable<NamespaceMetadata> Namespaces { get; }
    public string Name { get; }
    public AssemblyMetadata(Assembly assembly)
    {
      Name = assembly.GetName().FullName;
      Namespaces = from type in assembly.DefinedTypes
                   group type by type.Namespace into t
                   select new NamespaceMetadata(t.Key, t.ToList());
    }
  }
}
