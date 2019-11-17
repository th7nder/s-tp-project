using System.Linq;
using TypeAnalyzer.Model;

namespace TypeAnalyzer.ViewModel.TreeItemViewModels
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
      Children.Add(new DetailViewModel("Name: ", _parameterMetadata.Name));
      Children.Add(new DetailViewModel("Kind: ", _parameterMetadata.Kind.ToString()));
      Children.Add(new DetailViewModel("Type: ", _parameterMetadata.Type.Name, new TypeViewModel(_parameterMetadata.Type)));
      if (_parameterMetadata.Attributes.Any())
      {
        Children.Add(new AttributesViewModel(_parameterMetadata.Attributes));
      }
    }
  }
}
