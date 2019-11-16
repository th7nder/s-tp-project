using TypeAnalyzer.Model;

namespace TypeAnalyzer.ViewModel
{
  public class AttributeViewModel : TreeItemViewModel
  {
    private AttributeMetadata _attributeMetadata;

    public AttributeViewModel(AttributeMetadata attributeMetadata)
    {
      Name = attributeMetadata.Type.Name;
      _attributeMetadata = attributeMetadata;
    }

    protected override void BuildMyself()
    {
      Children.Add(new TypeViewModel(_attributeMetadata.Type));
    }
  }
}