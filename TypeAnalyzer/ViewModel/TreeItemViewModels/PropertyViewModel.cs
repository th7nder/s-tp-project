using System.Linq;
using TypeAnalyzer.Model;

namespace TypeAnalyzer.ViewModel.TreeItemViewModels
{
  public class PropertyViewModel : TreeItemViewModel
  {
    private readonly PropertyMetadata _propertyMetadata;

    public PropertyViewModel(PropertyMetadata propertyMetadata)
    {
      _propertyMetadata = propertyMetadata;
      Name = GetPropertySignature();
    }

    protected override void BuildMyself()
    {
      Children.Add(new DetailViewModel("Name: ", _propertyMetadata.Name));
      Children.Add(new DetailViewModel("Access Level: ", _propertyMetadata.AccessModifier.ToString()));
      Children.Add(new DetailViewModel("Type: ", _propertyMetadata.TypeMetadata.Name, new TypeViewModel(_propertyMetadata.TypeMetadata)));

      if (_propertyMetadata.Attributes.Any())
      {
        Children.Add(new AttributesViewModel(_propertyMetadata.Attributes));
      }

      if (_propertyMetadata.Accessors.Any())
      {
        Children.Add(new MethodsViewModel(_propertyMetadata.Accessors, "Accessors"));
      }
    }

    private string GetPropertySignature()
    {
      return $"{_propertyMetadata.Name}: {_propertyMetadata.TypeMetadata.Name}";
    }
  }
}
