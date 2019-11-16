using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TypeAnalyzer.Model
{
  public class TypeMetadata
  {
    public string Name { get; }
    public IEnumerable<PropertyMetadata> Properties { get; }
    public IEnumerable<TypeMetadata> BaseTypes { get; }
    public IEnumerable<MethodMetadata> Methods { get; }
    public IEnumerable<AttributeMetadata> Attributes { get; }
    private TypeMetadata(TypeInfo typeInfo)
    {
      types[typeInfo.FullName] = this;
      Name = typeInfo.Name;
      Properties = from property in typeInfo.DeclaredProperties
                   select new PropertyMetadata(property);

      BaseTypes = Enumerable.Repeat(typeInfo.BaseType, 1)
                .Concat(typeInfo.GetType().GetInterfaces())
                .Where(type => type != null)
                .Select(type => new TypeMetadata(type.GetTypeInfo()));

      Methods = from method in typeInfo.DeclaredMethods
                select new MethodMetadata(method);
      Attributes = from attribute in typeInfo.CustomAttributes
                   select new AttributeMetadata(attribute);
    }

    private static Dictionary<string, TypeMetadata> types = new Dictionary<string, TypeMetadata>();
    public static TypeMetadata Analyze(TypeInfo typeInfo)
    {
      if (types.ContainsKey(typeInfo.FullName))
      {
        return types[typeInfo.FullName];
      }

      return new TypeMetadata(typeInfo);
    }
  }
}
