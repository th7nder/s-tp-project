using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace TypeAnalyzer.Model
{
  [DataContract(Namespace = "")]
  public class MethodBaseMetadata
  {
    [DataMember]
    public string Name { get; private set; }
    [DataMember]
    public IEnumerable<MethodParameterMetadata> Parameters { get; private set; }
    [DataMember]
    public IEnumerable<AttributeMetadata> Attributes { get; private set; }
    [DataMember]
    public AccessModifier AccessModifier { get; set; }
    [DataMember]
    public MethodModifier MethodModifier { get; private set; }

    public MethodBaseMetadata(MethodBase method)
    {
      Name = method.Name;
      Parameters = from parameter in method.GetParameters()
                   select new MethodParameterMetadata(parameter);
      Attributes = from attribute in method.CustomAttributes
                   select AttributeMetadata.Analyze(attribute);
      AccessModifier = method.GetAccessModifier();
      MethodModifier = method.GetMethodModifier();
    }
  }
}