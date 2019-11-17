using System.Reflection;
using System.Runtime.Serialization;

namespace TypeAnalyzer.Model
{
  static class AccessModifierUtils
  {
    public static AccessModifier GetAccessModifier(this TypeInfo typeInfo)
    {
      if (typeInfo.IsPublic)
        return AccessModifier.Public;
      if (typeInfo.IsNotPublic)
        return AccessModifier.Internal;
      if (typeInfo.IsNestedPrivate)
        return AccessModifier.Private;
      if (typeInfo.IsNestedFamily)
        return AccessModifier.Protected;
      if (typeInfo.IsNestedFamORAssem)
        return AccessModifier.ProtectedInternal;
      if (typeInfo.IsNestedFamANDAssem)
        return AccessModifier.PrivateProtected;

      return AccessModifier.None;
    }

    public static AccessModifier GetAccessModifier(this MethodBase methodInfo)
    {
      if (methodInfo.IsPrivate)
        return AccessModifier.Private;
      if (methodInfo.IsFamily)
        return AccessModifier.Protected;
      if (methodInfo.IsFamilyOrAssembly)
        return AccessModifier.ProtectedInternal;
      if (methodInfo.IsAssembly)
        return AccessModifier.Internal;
      if (methodInfo.IsPublic)
        return AccessModifier.Public;
      if (methodInfo.IsFamilyAndAssembly)
        return AccessModifier.PrivateProtected;

      return AccessModifier.None;
    }

    public static AccessModifier GetAccessModifier(this FieldInfo fieldInfo)
    {
      if (fieldInfo.IsPrivate)
        return AccessModifier.Private;
      if (fieldInfo.IsFamily)
        return AccessModifier.Protected;
      if (fieldInfo.IsFamilyOrAssembly)
        return AccessModifier.ProtectedInternal;
      if (fieldInfo.IsAssembly)
        return AccessModifier.Internal;
      if (fieldInfo.IsPublic)
        return AccessModifier.Public;
      if (fieldInfo.IsFamilyAndAssembly)
        return AccessModifier.PrivateProtected;

      return AccessModifier.None;
    }

    public static AccessModifier GetAccessModifier(this PropertyInfo propertyInfo)
    {
      return GetAccessModifier(propertyInfo.GetMethod);
    }
  }

  [DataContract]
  public enum AccessModifier
  {
    [EnumMember]
    Private,
    [EnumMember]
    Protected,
    [EnumMember]
    ProtectedInternal,
    [EnumMember]
    Internal,
    [EnumMember]
    Public,
    [EnumMember]
    PrivateProtected,
    [EnumMember]
    None
  }
}