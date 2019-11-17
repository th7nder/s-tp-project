using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using TypeAnalyzer.Model;
using TypeAnalyzer.Model.Serialization;
using TypeAnalyzer.ViewModel.MVVM;
using TypeAnalyzer.ViewModel.TreeItemViewModels;

namespace TypeAnalyzer.ViewModel
{
  class TypeAnalyzerViewModel : ViewModelBase
  {
    public TypeAnalyzerViewModel()
    {
      BrowseCommand = new RelayCommand(Browse);
      SaveXMLCommand = new RelayCommand(SaveXML);
    }

    public ObservableCollection<TreeItemViewModel> AssemblyModel { get; set; } = new ObservableCollection<TreeItemViewModel>();

    public ICommand BrowseCommand { get; }
    public ICommand SaveXMLCommand { get; }

    private string _pathVariable;
    public string PathVariable
    {
      get => _pathVariable;
      set
      {
        if (value != null && _pathVariable != value)
        {
          _pathVariable = value;
          RaisePropertyChanged();
          assemblyMetadata = new AssemblyMetadata(Assembly.LoadFrom(_pathVariable));
          AssemblyViewModel model = new AssemblyViewModel(assemblyMetadata);
          AssemblyModel.Add(model);
        }
      }
    }

    private AssemblyMetadata assemblyMetadata = null;
    private void Browse()
    {
      PathVariable = GetPath();
    }

    private void SaveXML()
    {
      if (assemblyMetadata == null)
      {
        ShowErrorBox("There is no assembly to save");
        return;
      }

      string savePath = GetSavePath();
      if (savePath == null)
      {
        return;
      }

      XmlSerialization xmlSerialization = new XmlSerialization();
      xmlSerialization.WriteFile(assemblyMetadata, savePath);

      MessageBox.Show($"Succesfully serialized data to {savePath}", "Data serializaton", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private MessageBoxResult ShowErrorBox(string message)
    {
      return MessageBox.Show(message, "Error occured", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    private string GetSavePath()
    {
      SaveFileDialog saveFileDialog = new SaveFileDialog()
      {
        Filter = "XML File (*.xml)|*.xml",
      };

      if (saveFileDialog.ShowDialog() == true)
      {
        return saveFileDialog.FileName;
      }

      return null;
    }

    private string GetPath()
    {
      OpenFileDialog openFileDialog = new OpenFileDialog
      {
        Multiselect = false
      };
      if (openFileDialog.ShowDialog() == true)
      {
        return openFileDialog.FileName;
      }

      return null;
    }
  }
}
