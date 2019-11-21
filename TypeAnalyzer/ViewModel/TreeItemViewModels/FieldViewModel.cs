using System.Linq;
using TypeAnalyzer.Model;

namespace TypeAnalyzer.ViewModel.TreeItemViewModels
{
  public class FieldViewModel : TreeItemViewModel
  {
    private readonly FieldMetadata _fieldMetadata;

    public FieldViewModel(FieldMetadata fieldMetadata)
    {
      _fieldMetadata = fieldMetadata;
      Name = GetPropertySignature();
    }

    protected override void BuildMyself()
    {
      Children.Add(new DetailViewModel("Name: ", _fieldMetadata.Name));
      Children.Add(new DetailViewModel("Access Level: ", _fieldMetadata.AccessModifier.ToString()));
      Children.Add(new TypeViewModel(_fieldMetadata.Type));

      if (_fieldMetadata.Attributes.Any())
      {
        Children.Add(new AttributesViewModel(_fieldMetadata.Attributes));
      }
    }

    private string GetPropertySignature()
    {
      return $"{_fieldMetadata.Name}: {_fieldMetadata.Type.Name}";
    }
  }
}