using TypeAnalyzer.Model;

namespace TypeAnalyzer.ViewModel.TreeItemViewModels
{
  public class NamespaceViewModel : TreeItemViewModel
  {
    private NamespaceMetadata _namespaceMetadata;

    public NamespaceViewModel(NamespaceMetadata namespaceMetadata)
    {
      Name = namespaceMetadata.Name;
      _namespaceMetadata = namespaceMetadata;
    }

    protected override void BuildMyself()
    {
      foreach (TypeMetadata typeMetadata in _namespaceMetadata.Types)
      {
        Children.Add(new TypeViewModel(typeMetadata));
      }
    }
  }
}