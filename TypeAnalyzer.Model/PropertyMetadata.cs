using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace TypeAnalyzer.Model
{
  [DataContract]
  public class PropertyMetadata
  {

    [DataMember]
    public string Name { get; }
    [DataMember]
    public TypeMetadata TypeMetadata { get;  }
    [DataMember]
    public IEnumerable<MethodMetadata> Accessors { get; }
    [DataMember]
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
