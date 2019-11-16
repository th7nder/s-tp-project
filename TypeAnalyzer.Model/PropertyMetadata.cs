using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TypeAnalyzer.Model
{
  public class PropertyMetadata
  {

    public string Name { get; }
    public TypeMetadata TypeMetadata { get;  }
    public IEnumerable<MethodMetadata> Accessors { get; }
    public IEnumerable<AttributeMetadata> Attributes { get; }
    public PropertyMetadata(PropertyInfo property)
    {
      Name = property.Name;
      TypeMetadata = TypeMetadata.Analyze(property.PropertyType.GetTypeInfo());
      Accessors = from methodInfo in property.GetAccessors()
                  select new MethodMetadata(methodInfo);
      Attributes = from customAttributeData in property.CustomAttributes
                   select new AttributeMetadata(customAttributeData);
    }
  }
}
