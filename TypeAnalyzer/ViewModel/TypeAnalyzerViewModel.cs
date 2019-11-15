using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TypeAnalyzer.ViewModel.MVVM;

namespace TypeAnalyzer.ViewModel
{
  class TypeAnalyzerViewModel : ViewModelBase
  {
    public TypeAnalyzerViewModel()
    {
      BrowseCommand = new RelayCommand(Browse);

      var typeMetadata = new Model.TypeMetadata
      {
        Name = "Macbook"
      };
     
      var properties = new List<Model.PropertyMetadata>
        {
          new Model.PropertyMetadata
          {
            Name = "Price",
            TypeMetadata = typeMetadata
          }
        };

      typeMetadata.Properties = properties;

      var typeViewModel = new TypeViewModel(typeMetadata);
      AssemblyModel.Add(typeViewModel);
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
