namespace TypeAnalyzer.ViewModel.TreeItemViewModels
{
  public class DetailViewModel : TreeItemViewModel
  {
    public string Value { get; }
    public TreeItemViewModel ChildrenItem { get; }

    // There are two constructors, because when you don't have any items in collection arrow is not shown.
    public DetailViewModel(string name, string value)
    {
      Name = name;
      Value = value;
      Children.Clear();
    }

    public DetailViewModel(string name, string value, TreeItemViewModel childrenItem)
    {
      Name = name;
      Value = value;
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
