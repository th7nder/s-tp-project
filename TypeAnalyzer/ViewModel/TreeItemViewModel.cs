using System.Collections.ObjectModel;
using TypeAnalyzer.ViewModel.MVVM;

namespace TypeAnalyzer.ViewModel
{
  public abstract class TreeItemViewModel : ViewModelBase
  {

    public string Name { get; set; }
    public ObservableCollection<TreeItemViewModel> Children { get; } = new ObservableCollection<TreeItemViewModel>() { null };
    public bool TreeViewItemIsExpanded
    {
      get => _isExpanded;
      set
      {
        _isExpanded = value;
        if (_wasBuilt)
          return;
        Children.Clear();
        BuildMyself();
        _wasBuilt = true;
        RaisePropertyChanged();
      }
    }

    private bool _wasBuilt = false;
    private bool _isExpanded = false;
    protected abstract void BuildMyself();
  }
}
