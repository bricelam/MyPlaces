using GeoAPI.Geometries;

namespace MyPlaces.Drawing
{
    class GeometryDrawnEventArgs
    {
        public GeometryDrawnEventArgs(IGeometry geometry)
            => Geometry = geometry;

        public IGeometry Geometry { get; }
    }
}
