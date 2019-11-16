using System.Collections.Generic;
using System.Linq;
using TypeAnalyzer.Model;

namespace TypeAnalyzer.ViewModel
{
  public class TypeViewModel : TreeItemViewModel
  {
    private readonly TypeMetadata _typeMetadata;

    public TypeViewModel(TypeMetadata typeMetadata)
    {
      _typeMetadata = typeMetadata;
      Name = typeMetadata.Name;
    }

    protected override void BuildMyself()
    {
      Children.Add(new BaseTypesViewModel(_typeMetadata.BaseTypes));
      Children.Add(new AttributesViewModel(_typeMetadata.Attributes));
      Children.Add(new PropertiesViewModel(_typeMetadata.Properties));
      Children.Add(new MethodsViewModel(_typeMetadata.Methods));
    }
  }
}
