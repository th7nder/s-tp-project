using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TypeAnalyzer.ViewModel.MVVM;

namespace TypeAnalyzer.ViewModel
{
  class TypeAnalyzerViewModel : ViewModelBase
  {
    public TypeAnalyzerViewModel()
    {
      BrowseCommand = new RelayCommand(Browse);
    }

    public ObservableCollection<TreeItemViewModel> AssemblyModel { get; set; } = new ObservableCollection<TreeItemViewModel> { new TreeItemViewModel() { Name = "mscore.dll" } };

    public ICommand BrowseCommand { get; }
    public string PathVariable
    {
      get => _pathVariable;
      set
      {
        if (value != null && _pathVariable != value)
        {
          _pathVariable = value;
          RaisePropertyChanged();
        }
      }
    }

    private String _pathVariable;
    private void Browse()
    {
      PathVariable = GetPath();
    }

    private string GetPath()
    {
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Multiselect = false;
      if (openFileDialog.ShowDialog() == true)
      {
        return openFileDialog.FileName;
      }

      return null;
    }
  }
}
