using System.Linq;
using TypeAnalyzer.Model;

namespace TypeAnalyzer.ViewModel.TreeItemViewModels
{
  public class TypeViewModel : TreeItemViewModel
  {
    private readonly TypeMetadata _typeMetadata;

    public TypeViewModel(TypeMetadata typeMetadata)
    {
      _typeMetadata = typeMetadata;
      Name = typeMetadata.Name;

      if (_typeMetadata.IsPlaceholder)
      {
        Children.Clear();
      }
    }

    protected override void BuildMyself()
    {
      if (_typeMetadata.IsPlaceholder)
      {
        return;
      } 

      Children.Add(new DetailViewModel("Type Kind: ", _typeMetadata.TypeKind.ToString()));
      Children.Add(new DetailViewModel("Access Level: ", _typeMetadata.AccessModifier.ToString()));
      Children.Add(new DetailViewModel("Is Sealed: ", _typeMetadata.IsSealed.ToString()));
      Children.Add(new DetailViewModel("Is Pointer: ", _typeMetadata.IsPointer.ToString()));
      if (_typeMetadata.DeclaringType != null)
      {
        Children.Add(new DetailViewModel("Declaring Type: ", _typeMetadata.DeclaringType.Name, new TypeViewModel(_typeMetadata.DeclaringType)));
      }
      
      Children.Add(new BaseTypesViewModel(_typeMetadata.BaseTypes));

      if (_typeMetadata.Constructors.Any())
      {
        Children.Add(new ConstructorsViewModel(_typeMetadata.Constructors));
      }
      
      if (_typeMetadata.NestedTypes.Any())
      {
        Children.Add(new NestedTypesViewModel(_typeMetadata.NestedTypes));
      }

      if (_typeMetadata.GenericArguments.Any())
      {
        Children.Add(new GenericParametersViewModel(_typeMetadata.GenericArguments));
      }

      if (_typeMetadata.Attributes.Any())
      {
        Children.Add(new AttributesViewModel(_typeMetadata.Attributes));
      }

      if (_typeMetadata.Properties.Any())
      {
        Children.Add(new PropertiesViewModel(_typeMetadata.Properties));
      }

      if (_typeMetadata.Fields.Any())
      {
        Children.Add(new FieldsViewModel(_typeMetadata.Fields));
      }

      if (_typeMetadata.Methods.Any())
      {
        Children.Add(new MethodsViewModel(_typeMetadata.Methods));
      }

      if (_typeMetadata.ExtensionMethods.Any())
      {
        Children.Add(new MethodsViewModel(_typeMetadata.ExtensionMethods, "Extension Methods"));
      }
    }
  }
}
