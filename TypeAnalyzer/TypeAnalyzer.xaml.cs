using System.Windows;
using TypeAnalyzer.View;
using TypeAnalyzer.ViewModel;

namespace TypeAnalyzer
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      DataContext = new TypeAnalyzerViewModel(new WPFUserInterface());
    }
  }
}
