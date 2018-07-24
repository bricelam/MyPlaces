using System;
using GalaSoft.MvvmLight;
using GeoAPI.Geometries;

namespace MyPlaces.Drawing
{
    class DrawingContext : ObservableObject
    {
        static readonly DrawingState _browseState = new BrowseState();

        IGeometry _activeGeometry;

        public DrawingState State { get; set; } = _browseState;

        public IGeometry ActiveGeometry
        {
            get => _activeGeometry;
            set => Set(nameof(ActiveGeometry), ref _activeGeometry, value);
        }

        public event EventHandler<GeometryDrawnEventArgs> GeometryDrawn;

        public void End(IGeometry geometry)
        {
            OnGeometryDrawn(new GeometryDrawnEventArgs(geometry));
            State = _browseState;
            ActiveGeometry = null;
        }

        public void AddPoint()
        {
            State = new AddPointState(this);
            ActiveGeometry = null;
        }

        public void AddLineString()
        {
            State = new StartLineStringState(this);
            ActiveGeometry = null;
        }

        public void MouseMove(IPoint position)
            => State.MouseMove(position);

        public void MouseClick(IPoint position)
            => State.MouseClick(position);

        public bool MouseDoubleClick(IPoint position)
            => State.MouseDoubleClick(position);

        private void OnGeometryDrawn(GeometryDrawnEventArgs e)
            => GeometryDrawn?.Invoke(this, e);
    }
}
