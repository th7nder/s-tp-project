using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TypeAnalyzer.Model
{
  [DataContract]
  public class AttributeMetadata
  {

    [DataMember]
    public TypeMetadata Type { get; }

    public AttributeMetadata(CustomAttributeData customAttributeData)
    {
      Type = TypeMetadata.Analyze(customAttributeData.AttributeType.GetTypeInfo());
    }
  }
}