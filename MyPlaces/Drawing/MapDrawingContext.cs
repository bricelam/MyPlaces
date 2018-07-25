using System;
using GalaSoft.MvvmLight;
using GeoAPI.Geometries;

namespace MyPlaces.Drawing
{
    class MapDrawingContext : ObservableObject
    {
        IGeometry _activeGeometry;

        public DrawingState State { get; set; } = new BrowseState();

        public IGeometry ActiveGeometry
        {
            get => _activeGeometry;
            set => Set(nameof(ActiveGeometry), ref _activeGeometry, value);
        }

        public event EventHandler<GeometryDrawnEventArgs> GeometryDrawn;

        public void Browse()
        {
            ActiveGeometry = null;
            State = new BrowseState();
        }

        public void AddPoint()
        {
            ActiveGeometry = null;
            State = new AddPointState(this);
        }

        public void AddLineString()
        {
            ActiveGeometry = null;
            State = new StartLineStringState(this);
        }

        public void AddPolygon()
        {
            ActiveGeometry = null;
            State = new StartPolygonState(this);
        }

        public MapDrawingContext End(IGeometry geometry)
        {
            ActiveGeometry = null;
            OnGeometryDrawn(new GeometryDrawnEventArgs(geometry));

            return this;
        }

        public void MouseMove(Coordinate position)
            => State.MouseMove(position);

        public void MouseClick(Coordinate position)
            => State.MouseClick(position);

        public bool MouseDoubleClick(Coordinate position)
            => State.MouseDoubleClick(position);

        private void OnGeometryDrawn(GeometryDrawnEventArgs e)
            => GeometryDrawn?.Invoke(this, e);
    }
}
