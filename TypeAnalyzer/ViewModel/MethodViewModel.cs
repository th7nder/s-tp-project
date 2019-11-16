using TypeAnalyzer.Model;

namespace TypeAnalyzer.ViewModel
{
  class MethodViewModel : TreeItemViewModel
  {
    private readonly MethodMetadata _methodMetadata;

    public MethodViewModel(MethodMetadata method)
    {
      Name = method.Name;
      _methodMetadata = method;
    }

    protected override void BuildMyself()
    {
      Children.Add(new MethodParametersViewModel(_methodMetadata.Parameters));
      Children.Add(new TypeViewModel(_methodMetadata.ReturnType));
    }
  }
}
