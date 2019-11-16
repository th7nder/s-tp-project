using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace TypeAnalyzer.Model
{
  [DataContract]
  public class NamespaceMetadata
  {
    [DataMember]
    public string Name { get; }
    [DataMember]
    public IEnumerable<TypeMetadata> Types { get; }
    public NamespaceMetadata(string name, IEnumerable<TypeInfo> types)
    {
      Name = name;
      Types = from type in types select TypeMetadata.Analyze(type);
    }
  }
}