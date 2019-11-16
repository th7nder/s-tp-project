using System;
using System.Collections.Generic;
using System.Text;
using TypeAnalyzer.Model;

namespace TypeAnalyzer.ViewModel.TreeItemViewModels
{
  class MethodViewModel : TreeItemViewModel
  {
    private readonly MethodMetadata _methodMetadata;

    public MethodViewModel(MethodMetadata method)
    {
      _methodMetadata = method;
      Name = GetMethodSignature();
    }

    private string GetMethodSignature()
    {
      List<string> parameterSignatures = new List<string>();
      foreach (MethodParameterMetadata parameter in _methodMetadata.Parameters)
      {
        parameterSignatures.Add($"{parameter.Type.Name} {parameter.Name}");
      }

      return $"{_methodMetadata.Name}({String.Join(", ", parameterSignatures.ToArray())}):{_methodMetadata.ReturnType.Name}";
    }

    protected override void BuildMyself()
    {
      Children.Add(new DetailViewModel("Name: ", _methodMetadata.Name));
      Children.Add(new DetailViewModel("Return type: ", _methodMetadata.ReturnType.Name, new TypeViewModel(_methodMetadata.ReturnType)));
      Children.Add(new MethodParametersViewModel(_methodMetadata.Parameters));
      Children.Add(new AttributesViewModel(_methodMetadata.Attributes));  
    }
  }
}
