namespace TypeAnalyzer.ViewModel
{
  public class DetailViewModel : TreeItemViewModel
  {
    public string Value { get; }
    public TreeItemViewModel ChildrenItem { get; }

    public DetailViewModel(string name, string value)
    {
      Name = name;
      Value = value;
    }

    public DetailViewModel(string name, string value, TreeItemViewModel childrenItem) : this(name, value)
    {
      ChildrenItem = childrenItem;
    }

    protected override void BuildMyself()
    {
      if (ChildrenItem != null)
      {
        Children.Add(ChildrenItem);
      }
    }
  }
}
