using System.Reflection;
using System.Runtime.Serialization;

namespace TypeAnalyzer.Model
{
  [DataContract]
  public class MethodParameterMetadata
  {
    [DataMember]
    public string Name { get; private set; }
    [DataMember]
    public TypeMetadata Type { get; private set; }

    public MethodParameterMetadata(ParameterInfo parameter)
    {
      Name = parameter.Name;
      Type = TypeMetadata.Analyze(parameter.ParameterType.GetTypeInfo());
    }
  }
}
