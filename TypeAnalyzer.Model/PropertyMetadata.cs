using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace TypeAnalyzer.Model
{
  [DataContract(Namespace = "")]
  public class PropertyMetadata
  {

    [DataMember]
    public string Name { get; private set; }
    [DataMember]
    public TypeMetadata TypeMetadata { get; private set; }
    [DataMember]
    public IEnumerable<MethodMetadata> Accessors { get; private set; }
    [DataMember]
    public IEnumerable<AttributeMetadata> Attributes { get; private set; }
    [DataMember] 
    public AccessModifier AccessModifier { get; set; }
    public PropertyMetadata(PropertyInfo property)
    {
      Name = property.Name;
      TypeMetadata = TypeMetadata.Analyze(property.PropertyType.GetTypeInfo());
      Accessors = from methodInfo in property.GetAccessors()
                  select new MethodMetadata(methodInfo);
      Attributes = from customAttributeData in property.CustomAttributes
                   select AttributeMetadata.Analyze(customAttributeData);
      AccessModifier = property.GetAccessModifier();
    }
  }
}
