using System;
using System.Collections.Generic;
using System.Linq;
using TypeAnalyzer.Model;

namespace TypeAnalyzer.ViewModel.TreeItemViewModels
{
  public class ConstructorViewModel : TreeItemViewModel
  {
    private readonly ConstructorMetadata _constructorMetadata;

    public ConstructorViewModel(ConstructorMetadata constructor)
    {
      _constructorMetadata = constructor;
      Name = GetMethodSignature();
    }

    private string GetMethodSignature()
    {
      List<string> parameterSignatures = new List<string>();
      foreach (MethodParameterMetadata parameter in _constructorMetadata.Parameters)
      {
        parameterSignatures.Add(parameter.GetSignature());
      }

      string methodModifier = _constructorMetadata.MethodModifier != MethodModifier.None ? _constructorMetadata.MethodModifier.ToString().ToLower() + " " : "";
      return $"{methodModifier}{_constructorMetadata.Name}({String.Join(", ", parameterSignatures.ToArray())})";
    }

    protected override void BuildMyself()
    {
      Children.Add(new DetailViewModel("Name: ", _constructorMetadata.Name));
      Children.Add(new DetailViewModel("Access Level: ", _constructorMetadata.AccessModifier.ToString()));

      if (_constructorMetadata.Parameters.Any())
      {
        Children.Add(new MethodParametersViewModel(_constructorMetadata.Parameters));
      }

      if (_constructorMetadata.Attributes.Any())
      {
        Children.Add(new AttributesViewModel(_constructorMetadata.Attributes));
      }
    }
  }
}