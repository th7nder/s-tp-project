using System.Collections.Generic;
using System.Linq;
using TypeAnalyzer.Model;

namespace TypeAnalyzer.ViewModel.TreeItemViewModels
{
  public class TypesViewModel : TreeItemViewModel
  {
    private readonly IEnumerable<TypeMetadata> _types;

    public TypesViewModel(IEnumerable<TypeMetadata> types, string name = "Types")
    {
      Name = name;
      _types = types;
    }

    protected override void BuildMyself()
    {
      IEnumerable<TypeViewModel> types = from typeMetadata in _types
                                         select new TypeViewModel(typeMetadata);
      foreach (TypeViewModel type in types)
      {
        Children.Add(type);
      }
    }
  }
}