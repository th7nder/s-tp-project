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
    private static Random _random = new Random();
    protected virtual void BuildMyself()
    {
      int _numberOfChildren = Math.Max(1, _random.Next(7));
      for (int i = 0; i < _numberOfChildren; i++)
        this.Children.Add(new TreeItemViewModel() { Name = $"sample{i}" });
    }
  }
}
