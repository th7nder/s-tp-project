using TypeAnalyzer.Model;

namespace TypeAnalyzer.ViewModel
{
  class MethodParameterViewModel : TreeItemViewModel
  {
    private readonly MethodParameterMetadata _parameterMetadata;

    public MethodParameterViewModel(MethodParameterMetadata parameterMetadata)
    {
      Name = parameterMetadata.Name;
      this._parameterMetadata = parameterMetadata;
    }

    protected override void BuildMyself()
    {
      Children.Add(new TypeViewModel(_parameterMetadata.Type));
    }
  }
}
