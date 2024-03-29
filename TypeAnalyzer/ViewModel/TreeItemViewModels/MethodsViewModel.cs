﻿using System.Collections.Generic;
using System.Linq;
using TypeAnalyzer.Model;

namespace TypeAnalyzer.ViewModel.TreeItemViewModels
{
  class MethodsViewModel : TreeItemViewModel
  {
    private readonly IEnumerable<MethodMetadata> _methods;

    public MethodsViewModel(IEnumerable<MethodMetadata> methods, string name = "Methods")
    {
      Name = name;
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
