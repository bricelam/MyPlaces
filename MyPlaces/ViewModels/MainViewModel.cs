using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GeoAPI.Geometries;
using Microsoft.Extensions.Configuration;

namespace MyPlaces.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        public MainViewModel(IConfiguration configuraiton)
        {
            BingMapsKey = configuraiton["BingMapsKey"];
            ViewChangeEndCommand = new RelayCommand<IPolygon>(ViewChangeEnd);
            MouseMoveCommand = new RelayCommand<(IPoint position, Handleable handleable)>(
                x => MouseMove(x.position, x.handleable));
            MouseDoubleClickCommand = new RelayCommand<(IPoint position, Handleable handleable)>(
                x => MouseDoubleClick(x.position, x.handleable));
            MouseLeftButtonUpCommand = new RelayCommand<(IPoint position, Handleable handleable)>(
                x => MouseLeftButtonUp(x.position, x.handleable));
            MouseLeftButtonDownCommand = new RelayCommand<(IPoint position, Handleable handleable)>(
                x => MouseLeftButtonDown(x.position, x.handleable));
        }

        public string BingMapsKey { get; }

        public ICommand ViewChangeEndCommand { get; }
        public ICommand MouseMoveCommand { get; }
        public ICommand MouseDoubleClickCommand { get; }
        public ICommand MouseLeftButtonUpCommand { get; }
        public ICommand MouseLeftButtonDownCommand { get; }

        void ViewChangeEnd(IPolygon boundingRectangle)
        {
            // TODO: Re-query for items
        }

        void MouseMove(IPoint position, Handleable handleable)
        {
            // TODO: State pattern
        }

        void MouseDoubleClick(IPoint position, Handleable handleable)
        {
            // TODO: State pattern
        }

        void MouseLeftButtonUp(IPoint position, Handleable handleable)
        {
            // TODO: State pattern
        }

        void MouseLeftButtonDown(IPoint position, Handleable handleable)
        {
            // TODO: State pattern
        }
    }
}
