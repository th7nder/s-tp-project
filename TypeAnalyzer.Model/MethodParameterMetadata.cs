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
    [DataMember]
    public MethodParameterKind Kind { get; private set; }
    [DataMember]
    public object DefaultValue { get; set; }

    public MethodParameterMetadata(ParameterInfo parameterInfo)
    {
      Name = parameterInfo.Name;
      Kind = parameterInfo.GetKind();
      DefaultValue = !string.IsNullOrEmpty(parameterInfo.DefaultValue.ToString()) ? parameterInfo.DefaultValue : null;
      Type = TypeMetadata.Analyze(parameterInfo.ParameterType.GetTypeInfo());
    }

    public string GetSignature()
    {
      string defaultValue = DefaultValue != null ? " = " + DefaultValue.ToString() : "";
      if (Kind == MethodParameterKind.None)
        return $"{Name}{defaultValue}";

      
      return $"{Kind.ToString().ToLower()} {Name}{defaultValue}";
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

  [DataContract]
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
