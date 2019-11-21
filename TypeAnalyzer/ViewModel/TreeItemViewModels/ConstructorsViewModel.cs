using System.Collections.Generic;
using System.Linq;
using TypeAnalyzer.Model;

namespace TypeAnalyzer.ViewModel.TreeItemViewModels
{
  public class ConstructorsViewModel : TreeItemViewModel
  {
    private readonly IEnumerable<ConstructorMetadata> _constructors;

    public ConstructorsViewModel(IEnumerable<ConstructorMetadata> constructors, string name = "Constructors")
    {
      Name = name;
      _constructors = constructors;
    }

    protected override void BuildMyself()
    {
      IEnumerable<ConstructorViewModel> constructors = from constructorMetadata in _constructors
                                                       select new ConstructorViewModel(constructorMetadata);
      foreach (ConstructorViewModel constructor in constructors)
      {
        Children.Add(constructor);
      }
    }
  }
}