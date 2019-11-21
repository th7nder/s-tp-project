namespace TypeAnalyzer.View
{
  public enum FileType
  {
    XML,
    DLL
  };

  public interface IUserInterface
  {

    string GetSavePath(FileType fileType);
    string GetFilePath(FileType fileType);
    void DisplayErrorMessage(string message);
    void DisplayInfoMessage(string message, string title);
  }
}