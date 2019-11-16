using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace TypeAnalyzer.Model
{
  public class AttributeMetadata
  {

    public TypeMetadata Type { get; }

    public AttributeMetadata(CustomAttributeData customAttributeData)
    {
      Type = TypeMetadata.Analyze(customAttributeData.AttributeType.GetTypeInfo());
    }
  }
}