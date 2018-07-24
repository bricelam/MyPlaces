using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GeoAPI.Geometries;
using Microsoft.Extensions.Configuration;
using Microsoft.Maps.MapControl.WPF;
using DrawingContext = MyPlaces.Drawing.DrawingContext;
using Location = Microsoft.Maps.MapControl.WPF.Location;

namespace MyPlaces.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        DrawingContext _drawingContext;
        ICollection<IGeometry> _geometries = new List<IGeometry>();

        public MainViewModel(IConfiguration configuraiton)
        {
            _drawingContext = new DrawingContext();
            _drawingContext.PropertyChanged += (sender, e) =>
            {
                Debug.Assert(e.PropertyName == nameof(DrawingContext.ActiveGeometry));

                RebuildChildren();
            };
            _drawingContext.GeometryDrawn += (sender, e) => AddGeometry(e.Geometry);

            BingMapsKey = configuraiton["BingMapsKey"];
            AddPushpinCommand = new RelayCommand(() => _drawingContext.AddPoint());
            AddPolylineCommand = new RelayCommand(() => _drawingContext.AddLineString());
            ViewChangeEndCommand = new RelayCommand<IPolygon>(ViewChangeEnd);
            MouseMoveCommand = new RelayCommand<(IPoint position, Handleable handleable)>(
                x => MouseMove(x.position));
            MouseDoubleClickCommand = new RelayCommand<(IPoint position, Handleable handleable)>(
                x => MouseDoubleClick(x.position, x.handleable));
            MouseClickCommand = new RelayCommand<IPoint>(MouseClick);
        }

        public string BingMapsKey { get; }
        public ObservableCollection<FrameworkElement> Children { get; } = new ObservableCollection<FrameworkElement>();

        public ICommand AddPushpinCommand { get; set; }
        public ICommand AddPolylineCommand { get; set; }
        public ICommand ViewChangeEndCommand { get; }
        public ICommand MouseMoveCommand { get; }
        public ICommand MouseDoubleClickCommand { get; }
        public ICommand MouseClickCommand { get; }

        public void AddGeometry(IGeometry geometry, bool save = true)
        {
            if (save)
            {
                _geometries.Add(geometry);
            }

            if (geometry is IPoint point)
            {
                Children.Add(new Pushpin { Location = ToLocation(point.Coordinate) });
            }
            else if (geometry is ILineString lineString)
            {
                var mapPolyline = new MapPolyline
                {
                    Stroke = Brushes.Blue,
                    StrokeThickness = 5,
                    Opacity = 0.7,
                    Locations = new LocationCollection()
                };
                foreach (var coordinate in lineString.Coordinates)
                {
                    mapPolyline.Locations.Add(ToLocation(coordinate));
                }
                Children.Add(mapPolyline);
            }
        }

        Location ToLocation(Coordinate coordinate)
            => new Location(coordinate.Y, coordinate.X);

        void ViewChangeEnd(IPolygon boundingRectangle)
        {
            // TODO: Re-query for items
        }

        void MouseMove(IPoint position)
            => _drawingContext.MouseMove(position);

        void MouseClick(IPoint position)
            => _drawingContext.MouseClick(position);

        void MouseDoubleClick(IPoint position, Handleable handleable)
            => handleable.Handled = _drawingContext.MouseDoubleClick(position);

        void RebuildChildren()
        {
            Children.Clear();
            foreach (var geometry in _geometries)
            {
                AddGeometry(geometry, false);
            }
            if (_drawingContext.ActiveGeometry != null)
            {
                AddGeometry(_drawingContext.ActiveGeometry, false);
            }
        }
    }
}
