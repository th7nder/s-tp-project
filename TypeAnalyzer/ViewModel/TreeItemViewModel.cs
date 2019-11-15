using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeAnalyzer.ViewModel
{
  public class TreeItemViewModel : ViewModelBase
  {

    public string Name { get; set; }
    public ObservableCollection<TreeItemViewModel> Children { get; } = new ObservableCollection<TreeItemViewModel>() { null };
    public bool TreeViewItemIsExpanded
    {
      get => m_IsExpanded;
      set
      {
        m_IsExpanded = value;
        if (m_WasBuilt)
          return;
        Children.Clear();
        BuildMyself();
        m_WasBuilt = true;
        RaisePropertyChanged();
      }
    }

    private bool m_WasBuilt = false;
    private bool m_IsExpanded = false;
    private static Random m_Random = new Random();
    private void BuildMyself()
    {
      int _numberOfChildren = Math.Max(1, m_Random.Next(7));
      for (int i = 0; i < _numberOfChildren; i++)
        this.Children.Add(new TreeItemViewModel() { Name = $"sample{i}" });
    }
  }
}
