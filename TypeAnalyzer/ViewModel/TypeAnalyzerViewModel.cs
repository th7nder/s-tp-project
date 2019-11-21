using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using TypeAnalyzer.Model;
using TypeAnalyzer.Model.Serialization;
using TypeAnalyzer.View;
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
    public TypeAnalyzerViewModel(IUserInterface userInterface)
    {
      _userInterface = userInterface;
      LoadDLLCommand = new RelayCommand(LoadDLL);
      SaveXMLCommand = new RelayCommand(SaveXML);
      LoadXMLCommand = new RelayCommand(LoadXML);
    }

    private AssemblyMetadata _assemblyMetadata = null;
    private readonly IUserInterface _userInterface;

    private void LoadDLL()
    {
      PathVariable = _userInterface.GetFilePath(FileType.DLL);
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
        _userInterface.DisplayErrorMessage("There is no assembly to save");
        return;
      }

      string savePath = _userInterface.GetSavePath(FileType.XML);
      if (savePath == null)
      {
        return;
      }

      XmlSerialization xmlSerialization = new XmlSerialization();
      xmlSerialization.WriteFile(_assemblyMetadata, savePath, "transform.xslt");

      _userInterface.DisplayInfoMessage($"Succesfully serialized data to {savePath}", "Data serializaton");
    }

    private void LoadXML()
    {
      PathVariable = _userInterface.GetFilePath(FileType.XML);
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
  }
}
