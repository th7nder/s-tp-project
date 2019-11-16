using System.Reflection;

namespace TypeAnalyzer.Model
{
  public class MethodParameterMetadata
  {
    public string Name { get; }
    public TypeMetadata Type { get; }

    public MethodParameterMetadata(ParameterInfo parameter)
    {
      Name = parameter.Name;
      Type = TypeMetadata.Analyze(parameter.ParameterType.GetTypeInfo());
    }
  }
}
