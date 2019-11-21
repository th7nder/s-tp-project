using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace TypeAnalyzer.Model
{
  [DataContract(Namespace = "")]
  public class MethodParameterMetadata
  {
    [DataMember]
    public string Name { get; private set; }
    [DataMember]
    public TypeMetadata Type { get; private set; }
    [DataMember]
    public MethodParameterKind Kind { get; private set; }
    [DataMember]
    public object DefaultValue { get; set; }
    [DataMember]
    public IEnumerable<AttributeMetadata> Attributes { get; private set; }

    public MethodParameterMetadata(ParameterInfo parameterInfo)
    {
      Name = parameterInfo.Name;
      Kind = parameterInfo.GetKind();
      DefaultValue = !string.IsNullOrEmpty(parameterInfo.DefaultValue.ToString()) ? parameterInfo.DefaultValue : null;
      Type = TypeMetadata.Analyze(parameterInfo.ParameterType.GetTypeInfo());
      Attributes = from attribute in parameterInfo.CustomAttributes
                   select AttributeMetadata.Analyze(attribute);
    }

    public string GetSignature()
    {
      string defaultValue = DefaultValue != null ? " = " + DefaultValue : "";
      if (Kind == MethodParameterKind.None)
        return $"{Type.Name} {Name}{defaultValue}";

      return $"{Kind.ToString().ToLower()} {Type.Name} {Name}{defaultValue}";
    }
  }

  public static class MethodParameterKindUtils
  {
    public static MethodParameterKind GetKind(this ParameterInfo parameter)
    {

      if (parameter.IsIn)
        return MethodParameterKind.In;
      if (parameter.ParameterType.IsByRef)
      {
        if (parameter.IsOut)
          return MethodParameterKind.Out;

        return MethodParameterKind.Ref;
      }

      return MethodParameterKind.None;
    }
  }

  [DataContract(Namespace = "")]
  public enum MethodParameterKind
  {
    [EnumMember]
    Out,
    [EnumMember]
    Ref,
    [EnumMember]
    In,
    [EnumMember]
    None
  }
}
