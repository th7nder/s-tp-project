using System.Reflection;
using System.Runtime.Serialization;

namespace TypeAnalyzer.Model
{
  public static class MethodModifierUtils
  {
    public static MethodModifier GetMethodModifier(this MethodBase methodInfo)
    {
      if (methodInfo.IsStatic)
        return MethodModifier.Static;
      if (methodInfo.IsFinal)
        return MethodModifier.Final;
      if (methodInfo.IsAbstract)
        return MethodModifier.Abstract;
      if (methodInfo.IsVirtual)
        return MethodModifier.Virtual;

      return MethodModifier.None;
    }
  }


  [DataContract(Namespace = "")]
  public enum MethodModifier
  {
    [EnumMember]
    Static,
    [EnumMember]
    Virtual,
    [EnumMember]
    Abstract,
    [EnumMember]
    Final,
    [EnumMember]
    None
  }
}
