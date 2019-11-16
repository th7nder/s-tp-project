using TypeAnalyzer.Model;

namespace TypeAnalyzer.ViewModel
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
      Children.Add(new TypeViewModel(_propertyMetadata.TypeMetadata));
    }

    private string GetPropertySignature()
    {
      return $"{_propertyMetadata.Name}: {_propertyMetadata.TypeMetadata.Name}";
    }
  }
}
