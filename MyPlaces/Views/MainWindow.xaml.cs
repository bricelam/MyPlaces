using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using MyPlaces.ViewModels;
using NTSPoint = NetTopologySuite.Geometries.Point;

namespace MyPlaces.Views
{
    public partial class MainWindow : Window
    {
        Point _mouseDownAt;
        bool _waitingForMouseUp;
        Timer _waitForDoubleClick;
        bool _waitingForDoubleClick;

        public MainWindow()
        {
            InitializeComponent();
            Closing += (sender, e) => ViewModelLocator.Cleanup();
            _map.MouseMove += HandleMouseMoveOnMap;
        }

        MainViewModel ViewModel
            => (MainViewModel)DataContext;

        void HandleMouseLeftButtonDownOnMap(object sender, MouseButtonEventArgs e)
        {
            _mouseDownAt = e.GetPosition((IInputElement)sender);
            _waitingForMouseUp = true;
        }

        void HandleMouseMoveOnMap(object sender, MouseEventArgs e)
        {
            if (!(_waitingForMouseUp || _waitingForDoubleClick))
                return;

            var position = e.GetPosition((IInputElement)sender);
            if (Math.Abs(_mouseDownAt.X - position.X) > User32.GetSystemMetrics(User32.SM_CXDOUBLECLK)
                || Math.Abs(_mouseDownAt.Y - position.Y) > User32.GetSystemMetrics(User32.SM_CYDOUBLECLK))
            {
                _waitingForMouseUp = false;

                if (_waitingForDoubleClick)
                {
                    HandleSingleClickOnMap();
                }
            }
        }

        void HandleMouseLeftButtonUpOnMap(object sender, MouseButtonEventArgs e)
        {
            if (!_waitingForMouseUp)
                return;

            _waitingForMouseUp = false;

            if (_waitingForDoubleClick)
            {
                ClearWaitForDoubleClick();

                return;
            }

            _waitForDoubleClick = new Timer(
                _ => Dispatcher.BeginInvoke((Action)HandleSingleClickOnMap),
                null,
                User32.GetDoubleClickTime(),
                Timeout.Infinite);
            _waitingForDoubleClick = true;
        }

        void ClearWaitForDoubleClick()
        {
            _waitingForDoubleClick = false;
            _waitForDoubleClick.Dispose();
            _waitForDoubleClick = null;
        }

        void HandleSingleClickOnMap()
        {
            ClearWaitForDoubleClick();

            var location = _map.ViewportPointToLocation(_mouseDownAt);

            ViewModel.MouseClickCommand.Execute(new NTSPoint(location.Longitude, location.Latitude));
        }
    }
}
