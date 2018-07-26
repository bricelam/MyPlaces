using System;
using System.Windows;
using System.Windows.Input;
using GeoAPI.Geometries;
using Microsoft.Maps.MapControl.WPF;
using MyPlaces.ViewModels;

namespace MyPlaces.Views
{
    public partial class MainWindow : Window
    {
        Point? _mouseDownAt;
        bool _doubleClicking;

        public MainWindow()
        {
            InitializeComponent();
            Closing += (sender, e) => ViewModelLocator.Cleanup();

            // HACK: Remove this. Fix it right
            Vm.PropertyChanged += (sender, e) => ((UIElement)((FrameworkElement)_map.Children[0]).Parent).InvalidateMeasure();
        }

        MainViewModel Vm
            => (MainViewModel)DataContext;

        void HandleMouseLeftButtonDownOnMap(object sender, MouseButtonEventArgs e)
            => _mouseDownAt = e.GetPosition((IInputElement)sender);

        void HandleMouseMoveOnMap(object sender, MouseEventArgs e)
        {
            if (!_mouseDownAt.HasValue)
                return;

            var position = e.GetPosition((IInputElement)sender);
            if (Math.Abs(_mouseDownAt.Value.X - position.X) > 4
                || Math.Abs(_mouseDownAt.Value.Y - position.Y) > 4)
            {
                _mouseDownAt = null;
                _doubleClicking = false;
            }
        }

        void HandleMouseLeftButtonUpOnMap(object sender, MouseButtonEventArgs e)
        {
            if (_doubleClicking)
            {
                _doubleClicking = false;

                return;
            }
            if (!_mouseDownAt.HasValue)
            {
                return;
            }

            var map = (MapCore)sender;
            var location = map.ViewportPointToLocation(e.GetPosition(map));

            Vm.MouseClickCommand.Execute(new Coordinate(location.Longitude, location.Latitude));

            _mouseDownAt = null;
        }

        private void HandleMouseDoubleClickOnMap(object sender, MouseButtonEventArgs e)
            => _doubleClicking = true;
    }
}
