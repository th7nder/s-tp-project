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
    public string Name { get; private set; }
    [DataMember]
    public TypeMetadata ReturnType { get; private set; }
    [DataMember]
    public IEnumerable<MethodParameterMetadata> Parameters { get; }
    [DataMember]
    public IEnumerable<AttributeMetadata> Attributes { get; }
    [DataMember] 
    public AccessModifier AccessModifier { get; set; }

    public MethodMetadata(MethodInfo method)
    {
      Name = method.Name;
      ReturnType = TypeMetadata.Analyze(method.ReturnType.GetTypeInfo());
      Parameters = from parameter in method.GetParameters()
                   select new MethodParameterMetadata(parameter);
      Attributes = from attribute in method.CustomAttributes
                   select AttributeMetadata.Analyze(attribute);
      AccessModifier = method.GetAccessModifier();
    }
  }
}
