using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TypeAnalyzer.Model
{
  public class NamespaceMetadata
  {
    public string Name { get; }
    public IEnumerable<TypeMetadata> Types { get; }
    public NamespaceMetadata(string name, IEnumerable<TypeInfo> types)
    {
      Name = name;
      Types = from type in types select TypeMetadata.Analyze(type);
    }
  }
}