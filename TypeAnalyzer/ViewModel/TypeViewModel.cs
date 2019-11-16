using TypeAnalyzer.Model;

namespace TypeAnalyzer.ViewModel
{
  public class TypeViewModel : TreeItemViewModel
  {
    private readonly TypeMetadata _typeMetadata;

    public TypeViewModel(TypeMetadata typeMetadata)
    {
      Name = typeMetadata.Name;
      _typeMetadata = typeMetadata;
    }

    protected override void BuildMyself()
    {
      Children.Add(new BaseTypesViewModel(_typeMetadata.BaseTypes));
      Children.Add(new PropertiesViewModel(_typeMetadata.Properties));
      Children.Add(new MethodsViewModel(_typeMetadata.Methods));
    }
  }
}
