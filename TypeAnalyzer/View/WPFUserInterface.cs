using Microsoft.Win32;
using System.Windows;

namespace TypeAnalyzer.View
{
  public class WPFUserInterface : IUserInterface
  {
    public void DisplayErrorMessage(string message)
    {
      MessageBox.Show(message, "Error occured", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    public string GetSavePath(FileType fileType)
    {
      SaveFileDialog saveFileDialog = new SaveFileDialog()
      {
        Filter = GetFilterFromFileType(fileType),
      };

      if (saveFileDialog.ShowDialog() == true)
      {
        return saveFileDialog.FileName;
      }

      return null;
    }

    public string GetFilePath(FileType fileType)
    {
      OpenFileDialog openFileDialog = new OpenFileDialog
      {
        Filter = GetFilterFromFileType(fileType),
        Multiselect = false
      };
      if (openFileDialog.ShowDialog() == true)
      {
        return openFileDialog.FileName;
      }

      return null;
    }

    private string GetFilterFromFileType(FileType fileType)
    {
      switch (fileType)
      {
        case FileType.DLL:
          return FILTER_DLL;
        case FileType.XML:
          return FILTER_XML;
      }

      return "";
    }

    public void DisplayInfoMessage(string message, string title)
    {
      MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private const string FILTER_XML = "XML File (*.xml)|*.xml";
    private const string FILTER_DLL = "DLL File (*.dll)|*.dll";
  }
}
