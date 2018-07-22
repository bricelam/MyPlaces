using System.Windows;

namespace MyPlaces.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Closing += (sender, e) => ViewModelLocator.Cleanup();
        }
    }
}
