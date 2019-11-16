using System.Collections.Generic;
using TypeAnalyzer.Model;

namespace TypeAnalyzer.ViewModel
{
  class AttributesViewModel : TreeItemViewModel
  {
    private readonly IEnumerable<AttributeMetadata> _attributes;
    public AttributesViewModel(IEnumerable<AttributeMetadata> attributes)
    {
      Name = "Attributes";
      _attributes = attributes;
    }

    protected override void BuildMyself()
    {
      foreach (AttributeMetadata attributeMetadata in _attributes)
      {
        Children.Add(new AttributeViewModel(attributeMetadata));
      }
    }
  }
}
