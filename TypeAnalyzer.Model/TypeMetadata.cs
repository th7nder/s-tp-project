using System.Collections.Generic;

namespace TypeAnalyzer.Model
{
  public class TypeMetadata
  {
    public string Name { get; set; }
    public IEnumerable<PropertyMetadata> Properties { get; set; }
  }
}
