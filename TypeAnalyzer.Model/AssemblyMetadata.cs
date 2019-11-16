using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace TypeAnalyzer.Model
{
  [DataContract]
  public class AssemblyMetadata
  {
    [DataMember]
    public IEnumerable<NamespaceMetadata> Namespaces { get; }
    [DataMember]
    public string Name { get; private set;  }
    public AssemblyMetadata(Assembly assembly)
    {
      Name = assembly.GetName().FullName;
      Namespaces = from type in assembly.DefinedTypes
                   group type by type.Namespace into t
                   select new NamespaceMetadata(t.Key, t.ToList());
    }
  }
}
