using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace TypeAnalyzer.Model
{
  [DataContract(Namespace = "")]
  public class NamespaceMetadata
  {
    [DataMember]
    public string Name { get; private set; }
    [DataMember]
    public IEnumerable<TypeMetadata> Types { get; private set; }
    public NamespaceMetadata(string name, IEnumerable<TypeInfo> types)
    {
      Name = name;
      Types = from type in types select TypeMetadata.Analyze(type);
    }
  }
}