using System.Collections.Generic;
using System.Linq;
using TypeAnalyzer.Model;

namespace TypeAnalyzer.ViewModel
{
  class MethodParametersViewModel : TreeItemViewModel
  {
    private readonly IEnumerable<MethodParameterMetadata> _parameters;

    public MethodParametersViewModel(IEnumerable<MethodParameterMetadata> parameters)
    {
      Name = "Parameters";
      this._parameters = parameters;
    }

    protected override void BuildMyself()
    {
      IEnumerable<MethodParameterViewModel> parameters = from parameterMetadata in _parameters
                                                         select new MethodParameterViewModel(parameterMetadata);
      foreach (MethodParameterViewModel parameter in parameters)
      {
        Children.Add(parameter);
      }
    }
  }
}
