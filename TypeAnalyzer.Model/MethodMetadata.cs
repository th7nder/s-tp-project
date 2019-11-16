using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TypeAnalyzer.Model
{
  public class MethodMetadata
  {
    public string Name { get; }
    public TypeMetadata ReturnType { get; }
    public IEnumerable<MethodParameterMetadata> Parameters { get; }
    public IEnumerable<AttributeMetadata> Attributes { get; }

    public MethodMetadata(MethodInfo method)
    {
      Name = method.Name;
      ReturnType = TypeMetadata.Analyze(method.ReturnType.GetTypeInfo());
      Parameters = from parameter in method.GetParameters()
                   select new MethodParameterMetadata(parameter);
      Attributes = from attribute in method.CustomAttributes
                   select new AttributeMetadata(attribute);
    }
  }
}
