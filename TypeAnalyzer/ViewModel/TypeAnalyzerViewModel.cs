using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
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
    public ObservableCollection<TreeItemViewModel> AssemblyModel { get; set; } = new ObservableCollection<TreeItemViewModel>();

    public ICommand LoadDLLCommand { get; }
    public ICommand SaveXMLCommand { get; }
    public ICommand LoadXMLCommand { get; }

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
        }
      }
    }
    public TypeAnalyzerViewModel()
    {
      LoadDLLCommand = new RelayCommand(LoadDLL);
      SaveXMLCommand = new RelayCommand(SaveXML);
      LoadXMLCommand = new RelayCommand(LoadXML);
    }

    private AssemblyMetadata _assemblyMetadata = null;
    private void LoadDLL()
    {
      PathVariable = GetFilePath(FILTER_DLL);
      if (_pathVariable != null)
      {
        AssemblyMetadata assemblyMetadata = new Reflector(_pathVariable).AssemblyMetadata;
        LoadAssemblyMetadata(assemblyMetadata);
      }
    }

    private void SaveXML()
    {
      if (_assemblyMetadata == null)
      {
        ShowErrorBox("There is no assembly to save");
        return;
      }

      string savePath = GetSavePath(FILTER_XML);
      if (savePath == null)
      {
        return;
      }

      XmlSerialization xmlSerialization = new XmlSerialization();
      xmlSerialization.WriteFile(_assemblyMetadata, savePath);

      MessageBox.Show($"Succesfully serialized data to {savePath}", "Data serializaton", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void LoadXML()
    {
      PathVariable = GetFilePath(FILTER_XML);
      if (PathVariable == null)
      {
        return;
      }

      XmlSerialization xmlSerialization = new XmlSerialization();
      AssemblyMetadata assemblyMetadata = xmlSerialization.ReadFile<AssemblyMetadata>(PathVariable, FileMode.Open);
      LoadAssemblyMetadata(assemblyMetadata);
    }

    private void LoadAssemblyMetadata(AssemblyMetadata assemblyMetadata)
    {
      _assemblyMetadata = assemblyMetadata;
      AssemblyViewModel model = new AssemblyViewModel(assemblyMetadata);
      AssemblyModel.Clear();
      AssemblyModel.Add(model);
    }

    private MessageBoxResult ShowErrorBox(string message)
    {
      return MessageBox.Show(message, "Error occured", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    private string GetSavePath(string filter)
    {
      SaveFileDialog saveFileDialog = new SaveFileDialog()
      {
        Filter = filter,
      };

      if (saveFileDialog.ShowDialog() == true)
      {
        return saveFileDialog.FileName;
      }

      return null;
    }

    private string GetFilePath(string filter)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog
      {
        Filter = filter,
        Multiselect = false
      };
      if (openFileDialog.ShowDialog() == true)
      {
        return openFileDialog.FileName;
      }

      return null;
    }

    private const string FILTER_XML = "XML File (*.xml)|*.xml";
    private const string FILTER_DLL = "DLL File (*.dll)|*.dll";
  }
}
