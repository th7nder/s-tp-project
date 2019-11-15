using System.Collections.Generic;
using System.Linq;
using TypeAnalyzer.Model;

namespace TypeAnalyzer.ViewModel
{
  public class PropertiesViewModel : TreeItemViewModel
  {
    private readonly IEnumerable<PropertyMetadata> _properties;

    public PropertiesViewModel(IEnumerable<PropertyMetadata> properties)
    {
      Name = "Properties";
      _properties = properties;
    }

    protected override void BuildMyself()
    {
      IEnumerable<PropertyViewModel> properties = from propertyMetadata in _properties
                                                  select new PropertyViewModel(propertyMetadata);
      foreach (PropertyViewModel property in properties)
      {
        Children.Add(property);
      }
    }
  }
}
