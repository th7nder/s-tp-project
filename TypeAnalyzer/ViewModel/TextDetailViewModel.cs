namespace TypeAnalyzer.ViewModel
{
  public class TextDetailViewModel : TreeItemViewModel
  {
    public string Value { get; set; }

    public TextDetailViewModel(string name, string value)
    {
      Name = name;
      Value = value;
    }
  }
}
