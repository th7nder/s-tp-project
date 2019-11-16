using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace TypeAnalyzer.Model
{
  
  [DataContract]
  public class MethodMetadata
  {
    [DataMember]
    public string Name { get; }
    [DataMember]
    public TypeMetadata ReturnType { get; }
    [DataMember]
    public IEnumerable<MethodParameterMetadata> Parameters { get; }
    [DataMember]
    public IEnumerable<AttributeMetadata> Attributes { get; }

    public MethodMetadata(MethodInfo method)
    {
      Name = method.Name;
      ReturnType = TypeMetadata.Analyze(method.ReturnType.GetTypeInfo());
      Parameters = from parameter in method.GetParameters()
                   select new MethodParameterMetadata(parameter);
      Attributes = from attribute in method.CustomAttributes
                   select new AttributeMetadata(attribute);
    }
  }
}
