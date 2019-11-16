using TypeAnalyzer.Model;

namespace TypeAnalyzer.ViewModel
{
  public class AssemblyViewModel : TreeItemViewModel
  {
    private readonly AssemblyMetadata _assemblyMetadata;

    public AssemblyViewModel(AssemblyMetadata assemblyMetadata)
    {
      Name = assemblyMetadata.Name;
      _assemblyMetadata = assemblyMetadata;
    }

    protected override void BuildMyself()
    {
      foreach (NamespaceMetadata namespaceMetadata in _assemblyMetadata.Namespaces)
      {
        Children.Add(new NamespaceViewModel(namespaceMetadata));
      }
    }
  }
}