using System;
using System.Collections.Generic;
using System.Linq;
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

      List<string> genericsNames = (from genericArgument in _methodMetadata.GenericArguments
                                    select genericArgument.Name).ToList();

      string methodModifier = _methodMetadata.MethodModifier != MethodModifier.None ? _methodMetadata.MethodModifier.ToString().ToLower() + " " : "";
      return $"{methodModifier}{_methodMetadata.Name}{(genericsNames.Any() ? "<" + String.Join(", ", genericsNames) + ">" : "")}({String.Join(", ", parameterSignatures.ToArray())}):{_methodMetadata.ReturnType.Name}";
    }

    protected override void BuildMyself()
    {
      Children.Add(new DetailViewModel("Name: ", _methodMetadata.Name));
      Children.Add(new DetailViewModel("Access Level: ", _methodMetadata.AccessModifier.ToString()));
      Children.Add(new DetailViewModel("Return type: ", _methodMetadata.ReturnType.Name, new TypeViewModel(_methodMetadata.ReturnType)));

      if (_methodMetadata.GenericArguments.Any())
      {
        Children.Add(new GenericParametersViewModel(_methodMetadata.GenericArguments));
      }

      if (_methodMetadata.Parameters.Any())
      {
        Children.Add(new MethodParametersViewModel(_methodMetadata.Parameters));
      }

      if (_methodMetadata.Attributes.Any())
      {
        Children.Add(new AttributesViewModel(_methodMetadata.Attributes));  
      }
    }
  }
}
