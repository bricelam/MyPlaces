using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GeoAPI.Geometries;
using Microsoft.Extensions.Configuration;
using MyPlaces.Drawing;
using MyPlaces.Models;

namespace MyPlaces.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        readonly MapDrawingContext _drawingContext;

        PlaceCollection _activeCollection;

        public MainViewModel(IConfiguration configuraiton)
        {
            _drawingContext = new MapDrawingContext();
            _drawingContext.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(MapDrawingContext.ActiveGeometry))
                    RaisePropertyChanged(nameof(ActiveGeometry));
            };
            _drawingContext.GeometryDrawn += (sender, e) =>
            {
                if (ActiveCollection == null)
                {
                    Collections.Add(ActiveCollection = new PlaceCollection { Name = "New Collection" });
                }

                ActiveCollection.Geometries.Add(e.Geometry);
            };

            BingMapsKey = configuraiton["BingMapsKey"];

            BrowseCommand = new RelayCommand(() => _drawingContext.Browse());
            AddPushpinCommand = new RelayCommand(() => _drawingContext.AddPoint());
            AddPolylineCommand = new RelayCommand(() => _drawingContext.AddLineString());
            AddPolygonCommand = new RelayCommand(() => _drawingContext.AddPolygon());
            AddCollectionCommand = new RelayCommand(() => Collections.Add(new PlaceCollection { Name = "New Collection" }));
            ViewChangeEndCommand = new RelayCommand<IPolygon>(ViewChangeEnd);
            MouseMoveCommand = new RelayCommand<(Coordinate position, Handleable handleable)>(
                x => MouseMove(x.position));
            MouseDoubleClickCommand = new RelayCommand<(Coordinate position, Handleable handleable)>(
                x => MouseDoubleClick(x.position, x.handleable));
            MouseClickCommand = new RelayCommand<Coordinate>(MouseClick);
        }

        public string BingMapsKey { get; }

        public ICollection<PlaceCollection> Collections { get; } = new ObservableCollection<PlaceCollection>();

        public PlaceCollection ActiveCollection
        {
            get => _activeCollection;
            set => Set(nameof(ActiveCollection), ref _activeCollection, value);
        }

        public IGeometry ActiveGeometry
        {
            get => _drawingContext.ActiveGeometry;
            set => _drawingContext.ActiveGeometry = value;
        }

        public ICommand BrowseCommand { get; set; }
        public ICommand AddPushpinCommand { get; set; }
        public ICommand AddPolylineCommand { get; set; }
        public ICommand AddPolygonCommand { get; set; }
        public ICommand AddCollectionCommand { get; set; }
        public ICommand ViewChangeEndCommand { get; }
        public ICommand MouseMoveCommand { get; }
        public ICommand MouseDoubleClickCommand { get; }
        public ICommand MouseClickCommand { get; }

        void ViewChangeEnd(IPolygon boundingRectangle)
        {
            // TODO: Re-query for items
        }

        void MouseMove(Coordinate position)
            => _drawingContext.MouseMove(position);

        void MouseClick(Coordinate position)
            => _drawingContext.MouseClick(position);

        void MouseDoubleClick(Coordinate position, Handleable handleable)
            => handleable.Handled = _drawingContext.MouseDoubleClick(position);
    }
}
