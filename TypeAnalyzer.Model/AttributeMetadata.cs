using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace TypeAnalyzer.Model
{
  [DataContract(IsReference = true, Namespace = "")]
  public class AttributeMetadata
  {

    [DataMember]
    public TypeMetadata Type { get; private set; }

    private AttributeMetadata(CustomAttributeData customAttributeData)
    {
      string identifier = GetIdentifier(customAttributeData.AttributeType.GetTypeInfo());
      _attributes[identifier] = this;

      Type = TypeMetadata.Analyze(customAttributeData.AttributeType.GetTypeInfo());
    }

    private static Dictionary<string, AttributeMetadata> _attributes = new Dictionary<string, AttributeMetadata>();
    public static AttributeMetadata Analyze(CustomAttributeData customAttributeData)
    {
      string identifier = GetIdentifier(customAttributeData.AttributeType.GetTypeInfo());
      if (_attributes.ContainsKey(identifier))
      {
        return _attributes[identifier];
      }

      return new AttributeMetadata(customAttributeData);
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
  }
}