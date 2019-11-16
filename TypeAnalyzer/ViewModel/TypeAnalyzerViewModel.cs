using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Forms;
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

    private string _xmlSavePath;
    public string XMLSavePath
    {
      get => _xmlSavePath;
      set
      {
        if (value != null && _xmlSavePath != value)
        {
          _xmlSavePath = value;
          RaisePropertyChanged();
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

      XMLSavePath = GetDirectory();
      if (XMLSavePath == null)
      {
        return;
      }

      XmlSerialization xmlSerialization = new XmlSerialization();
      xmlSerialization.WriteFile(assemblyMetadata, $@"{XMLSavePath}/AssemblyMetadata.xml" );

      MessageBox.Show("Succesfully serialized data", "Data serializaton", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private DialogResult ShowErrorBox(string message)
    {
      return MessageBox.Show(message, "Error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    private string GetDirectory()
    {
      using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
      {
        DialogResult dialogResult = folderBrowserDialog.ShowDialog();
        if (dialogResult == DialogResult.OK && !string.IsNullOrEmpty(folderBrowserDialog.SelectedPath))
        {
          return folderBrowserDialog.SelectedPath;
        } 
      }

      return null;
    }

    private string GetPath()
    {
      Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
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
