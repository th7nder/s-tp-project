using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TypeAnalyzer.Model
{
  public static class TypeKindUtils
  {
    public static TypeKind GetTypeKind(this TypeInfo typeInfo)
    {
      TypeKind typeKind = TypeKind.None;
      if (typeInfo.IsInterface)
        return TypeKind.Interface;
      if (typeInfo.IsAbstract)
        return TypeKind.AbstractClass;
      if (typeInfo.IsValueType)
        return TypeKind.ValueType;
      if (typeInfo.IsClass)
        return TypeKind.Class;
      if (typeInfo.IsEnum)
        return TypeKind.Enum;

      return typeKind;
    }
  }


  [DataContract]
  public enum TypeKind
  {
    [EnumMember]
    Interface,
    [EnumMember]
    AbstractClass,
    [EnumMember]
    ValueType,
    [EnumMember]
    Class,
    [EnumMember]
    Enum,
    [EnumMember]
    None
  }

}
