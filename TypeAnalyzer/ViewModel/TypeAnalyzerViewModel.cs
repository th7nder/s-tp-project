using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
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

    public ObservableCollection<TreeItemViewModel> AssemblyModel { get; set; } = new ObservableCollection<TreeItemViewModel>();

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
          AssemblyViewModel model = new AssemblyViewModel(new Model.AssemblyMetadata(Assembly.LoadFrom(_pathVariable)));
          AssemblyModel.Add(model);
        }
      }
    }

    private string _pathVariable;
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
