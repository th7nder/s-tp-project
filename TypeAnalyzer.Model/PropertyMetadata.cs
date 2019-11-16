using System.Reflection;

namespace TypeAnalyzer.Model
{
  public class PropertyMetadata
  {

    public string Name { get; }
    public TypeMetadata TypeMetadata { get;  }
    public PropertyMetadata(PropertyInfo property)
    {
      Name = property.Name;
      TypeMetadata = TypeMetadata.Analyze(property.PropertyType.GetTypeInfo());
    }
  }
}
