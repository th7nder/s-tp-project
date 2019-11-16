using System.Collections.Generic;

namespace TypeAnalyzer.ViewModel.TreeItemViewModels
{
  public class DetailViewModel : TreeItemViewModel
  {
    public string Value { get; }
    public List<TreeItemViewModel> ChildrenItems { get; } = new List<TreeItemViewModel>();

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
      ChildrenItems.Add(childrenItem);
    }
    
    public DetailViewModel(string name, string value, List<TreeItemViewModel> childrenItems)
    {
      Name = name;
      Value = value;
      ChildrenItems = childrenItems;
    }

    protected override void BuildMyself()
    {
      foreach (TreeItemViewModel item in ChildrenItems)
      {
        Children.Add(item);
      }
    }
  }
}
