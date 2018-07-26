using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using GeoAPI.Geometries;
using MyPlaces.ViewModels;

namespace MyPlaces.Views
{
    public partial class MainWindow : Window
    {
        Point _mouseDownAt;
        bool _waitingForMouseUp;
        Timer _waitForDoubleClick;

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
        {
            _mouseDownAt = e.GetPosition((IInputElement)sender);
            _waitingForMouseUp = true;
        }

        void HandleMouseMoveOnMap(object sender, MouseEventArgs e)
        {
            if (!_waitingForMouseUp && _waitForDoubleClick == null)
                return;

            var position = e.GetPosition((IInputElement)sender);
            if (Math.Abs(_mouseDownAt.X - position.X) > User32.GetSystemMetrics(User32.SM_CXDOUBLECLK)
                || Math.Abs(_mouseDownAt.Y - position.Y) > User32.GetSystemMetrics(User32.SM_CYDOUBLECLK))
            {
                if (_waitForDoubleClick == null)
                {
                    HandleSingleClickOnMap();
                }

                if (_waitingForMouseUp)
                {
                    _waitingForMouseUp = false;
                }
            }
        }

        void HandleMouseLeftButtonUpOnMap(object sender, MouseButtonEventArgs e)
        {
            if (!_waitingForMouseUp)
                return;

            if (_waitForDoubleClick == null)
            {
                _waitForDoubleClick = new Timer(
                    _ => Dispatcher.Invoke(HandleSingleClickOnMap),
                    null,
                    User32.GetDoubleClickTime(),
                    Timeout.Infinite);
            }

            _waitingForMouseUp = false;
        }

        private void HandleMouseDoubleClickOnMap(object sender, MouseButtonEventArgs e)
        {
            if (_waitForDoubleClick != null)
            {
                ClearWaitForDoubleClick();
            }
        }

        void ClearWaitForDoubleClick()
        {
            _waitForDoubleClick.Dispose();
            _waitForDoubleClick = null;
        }

        void HandleSingleClickOnMap()
        {
            if (_waitForDoubleClick == null)
                return;

            ClearWaitForDoubleClick();

            var location = _map.ViewportPointToLocation(_mouseDownAt);
            Vm.MouseClickCommand.Execute(new Coordinate(location.Longitude, location.Latitude));
        }
    }
}
