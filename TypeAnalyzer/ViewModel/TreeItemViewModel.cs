using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeAnalyzer.ViewModel.MVVM;

namespace TypeAnalyzer.ViewModel
{
  public class TreeItemViewModel : ViewModelBase
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
    protected virtual void BuildMyself() { }
  }
}
