using System.Windows;
using System.Windows.Input;
using Microsoft.Maps.MapControl.WPF;
using MyPlaces.ViewModels;
using NTSPoint = NetTopologySuite.Geometries.Point;

namespace MyPlaces.Views
{
    public partial class MainWindow : Window
    {
        Point _downAt;

        public MainWindow()
        {
            InitializeComponent();
            Closing += (sender, e) => ViewModelLocator.Cleanup();
        }

        MainViewModel ViewModel
            => (MainViewModel)DataContext;

        void HandleMouseLeftButtonDownOnMap(object sender, MouseButtonEventArgs e)
        {
            _downAt = e.GetPosition((IInputElement)sender);
        }

        void HandleMouseLeftButtonUpOnMap(object sender, MouseButtonEventArgs e)
        {
            var map = (Map)sender;
            var upAt = e.GetPosition(map);

            var dX = _downAt.X - upAt.X;
            var dY = _downAt.Y - upAt.Y;
            if (dX < -3 || dX > 3 || dY < -3 || dY > 3)
                return;

            var location = map.ViewportPointToLocation(upAt);
            ViewModel.MouseClickCommand.Execute(new NTSPoint(location.Longitude, location.Latitude));
        }
    }
}
