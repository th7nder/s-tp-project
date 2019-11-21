using System.Collections.Generic;
using System.Linq;
using TypeAnalyzer.Model;

namespace TypeAnalyzer.ViewModel.TreeItemViewModels
{
  public class FieldsViewModel : TreeItemViewModel
  {
    private readonly IEnumerable<FieldMetadata> _fields;

    public FieldsViewModel(IEnumerable<FieldMetadata> fields)
    {
      Name = "Fields";
      _fields = fields;
    }

    protected override void BuildMyself()
    {
      IEnumerable<FieldViewModel> fields = from fieldMetadata in _fields
                                           select new FieldViewModel(fieldMetadata);
      foreach (FieldViewModel field in fields)
      {
        Children.Add(field);
      }
    }
  }
}