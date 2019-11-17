using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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
    public IEnumerable<MethodParameterMetadata> Parameters { get; private set; }
    [DataMember]
    public IEnumerable<AttributeMetadata> Attributes { get; private set; }
    [DataMember]
    public IEnumerable<TypeMetadata> GenericArguments { get; private set; }
    [DataMember] 
    public AccessModifier AccessModifier { get; set; }
    [DataMember]
    public MethodModifier MethodModifier { get; private set; }
    [DataMember]
    public bool IsExtensionMethod { get; private set; }

    public MethodMetadata(MethodInfo method)
    {
      Name = method.Name;
      IsExtensionMethod = method.IsDefined(typeof(ExtensionAttribute), false);
      ReturnType = TypeMetadata.Analyze(method.ReturnType.GetTypeInfo());
      Parameters = from parameter in method.GetParameters()
                   select new MethodParameterMetadata(parameter);
      Attributes = from attribute in method.CustomAttributes
                   select AttributeMetadata.Analyze(attribute);
      AccessModifier = method.GetAccessModifier();
      MethodModifier = method.GetMethodModifier();
      GenericArguments = from generic in method.GetGenericArguments()
                         select TypeMetadata.Analyze(generic.GetTypeInfo());
    }
  }
}
