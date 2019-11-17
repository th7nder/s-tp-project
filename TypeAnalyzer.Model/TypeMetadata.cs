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
    [DataMember]
    public bool IsPlaceholder { get; private set; }
    [DataMember]
    public IEnumerable<PropertyMetadata> Properties { get; private set; } = new List<PropertyMetadata>();
    [DataMember]
    public IEnumerable<FieldMetadata> Fields { get; private set; } = new List<FieldMetadata>();
    [DataMember]
    public IEnumerable<TypeMetadata> BaseTypes { get; private set; } = new List<TypeMetadata>();
    [DataMember]
    public IEnumerable<MethodMetadata> Methods { get; private set; } = new List<MethodMetadata>();
    [DataMember]
    public IEnumerable<AttributeMetadata> Attributes { get; private set; } = new List<AttributeMetadata>();
    [DataMember]
    public IEnumerable<TypeMetadata> TypeParameters { get; private set; } = new List<TypeMetadata>();
    [DataMember]
    public IEnumerable<TypeMetadata> TypeArguments { get; private set; } = new List<TypeMetadata>();
    [DataMember]
    public IEnumerable<TypeMetadata> GenericArguments { get; private set; } = new List<TypeMetadata>();
    [DataMember] 
    public AccessModifier AccessModifier { get; set; }
    [DataMember]
    public bool IsSealed { get; set; }
    
    private TypeMetadata(TypeInfo typeInfo, bool isPlaceholder)
    {
      types[GetIdentifier(typeInfo)] = this;

      if (!isPlaceholder)
      {
        Properties = from property in typeInfo.DeclaredProperties
                     select new PropertyMetadata(property);

        Fields = from field in typeInfo.DeclaredFields
                 select new FieldMetadata(field);

        BaseTypes = Enumerable.Repeat(typeInfo.BaseType, 1)
                  .Concat(typeInfo.GetType().GetInterfaces())
                  .Where(type => type != null)
                  .Select(type => Analyze(type.GetTypeInfo()));

        Methods = from method in typeInfo.DeclaredMethods
                  select new MethodMetadata(method);
        
        Attributes = from attribute in typeInfo.CustomAttributes
                     select AttributeMetadata.Analyze(attribute);
        IsSealed = typeInfo.IsSealed;
      }

      TypeParameters = from typeParameter in typeInfo.GenericTypeParameters
                       select Analyze(typeParameter.GetTypeInfo());
      TypeArguments = from typeArgument in typeInfo.GenericTypeArguments
                      select Analyze(typeArgument.GetTypeInfo());
      
      AccessModifier = typeInfo.GetAccessModifier();
      GenericArguments = from generic in typeInfo.GetGenericArguments()
                         select Analyze(generic.GetTypeInfo());

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
