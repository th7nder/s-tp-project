using TypeAnalyzer.Model;

namespace TypeAnalyzer.ViewModel
{
  public class PropertyViewModel : TreeItemViewModel
  {
    private readonly PropertyMetadata _propertyMetadata;

    public PropertyViewModel(PropertyMetadata propertyMetadata)
    {
      _propertyMetadata = propertyMetadata;
      Name = propertyMetadata.Name;
    }

    protected override void BuildMyself()
    {
      Children.Add(new TypeViewModel(_propertyMetadata.TypeMetadata));
    }
  }
}
