using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace TypeAnalyzer.Model
{
  [DataContract(IsReference = true)]
  public class TypeMetadata
  {
    [DataMember]
    public string Name { get; private set;  }
    public bool IsPlaceholder { get; }
    [DataMember]
    public IEnumerable<PropertyMetadata> Properties { get; }
    [DataMember]
    public IEnumerable<TypeMetadata> BaseTypes { get; }
    [DataMember]
    public IEnumerable<MethodMetadata> Methods { get; }
    [DataMember]
    public IEnumerable<AttributeMetadata> Attributes { get; }
    [DataMember]
    public IEnumerable<TypeMetadata> TypeParameters { get; }
    [DataMember]
    public IEnumerable<TypeMetadata> TypeArguments { get; }
    [DataMember] 
    public AccessModifier AccessModifier { get; set; }
    private TypeMetadata(TypeInfo typeInfo, bool isPlaceholder)
    {
      types[GetIdentifier(typeInfo)] = this;

      if (!isPlaceholder)
      {
        Properties = from property in typeInfo.DeclaredProperties
                     select new PropertyMetadata(property);

        BaseTypes = Enumerable.Repeat(typeInfo.BaseType, 1)
                  .Concat(typeInfo.GetType().GetInterfaces())
                  .Where(type => type != null)
                  .Select(type => Analyze(type.GetTypeInfo()));

        Methods = from method in typeInfo.DeclaredMethods
                  select new MethodMetadata(method);
        
        Attributes = from attribute in typeInfo.CustomAttributes
                     select AttributeMetadata.Analyze(attribute);
      }

      TypeParameters = from typeParameter in typeInfo.GenericTypeParameters
                       select Analyze(typeParameter.GetTypeInfo());
      TypeArguments = from typeArgument in typeInfo.GenericTypeArguments
                      select Analyze(typeArgument.GetTypeInfo());
      
      AccessModifier = typeInfo.GetAccessModifier();

      Name = GetTypeSignature(typeInfo.Name);
      IsPlaceholder = isPlaceholder;
    }

    private static Dictionary<string, TypeMetadata> types = new Dictionary<string, TypeMetadata>();
    static readonly Assembly SystemAssembly = typeof(object).Assembly;
    public static TypeMetadata Analyze(TypeInfo typeInfo)
    {
      string identifier = GetIdentifier(typeInfo);
      if (types.ContainsKey(identifier))
      {
        return types[identifier];
      }

      return new TypeMetadata(typeInfo, typeInfo.Assembly == SystemAssembly);
    }

    private static string GetIdentifier(TypeInfo typeInfo)
    {
      string identifier = typeInfo.FullName;
      if (string.IsNullOrEmpty(identifier))
      {
        identifier = typeInfo.Name;
      }

      return identifier;
    }

    private string GetTypeSignature(string baseName)
    {
      IEnumerable<string> types = null;
      if (TypeArguments.Any())
      {
        types = TypeArguments.Select(type => type.Name);
      }
      else if (TypeParameters.Any())
      {
        types = TypeParameters.Select(type => type.Name);
      }

      if (types != null)
      {
        return $"{baseName}<{string.Join(", ", types)}>";
      }

      return baseName;
    }
  }
}
