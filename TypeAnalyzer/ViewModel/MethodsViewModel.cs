using System.Collections.Generic;
using System.Linq;
using TypeAnalyzer.Model;

namespace TypeAnalyzer.ViewModel
{
  class MethodsViewModel : TreeItemViewModel
  {
    private readonly IEnumerable<MethodMetadata> _methods;

    public MethodsViewModel(IEnumerable<MethodMetadata> methods)
    {
      Name = "Methods";
      _methods = methods;
    }

    protected override void BuildMyself()
    {
      IEnumerable<MethodViewModel> methods = from methodMetadata in _methods
                                             select new MethodViewModel(methodMetadata);
      foreach (MethodViewModel method in methods)
      {
        Children.Add(method);
      }
    }
  }
}
