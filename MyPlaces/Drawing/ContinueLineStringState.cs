using System;
using GeoAPI.Geometries;
using NetTopologySuite.Geometries;

namespace MyPlaces.Drawing
{
    class ContinueLineStringState : DrawingState
    {
        readonly DrawingContext _context;
        IGeometry _baseGeometry;

        public ContinueLineStringState(DrawingContext context, IGeometry baseGeometry)
        {
            _context = context;
            _baseGeometry = baseGeometry;
        }

        public override void MouseMove(IPoint position)
            => UpdateGeometry(position);

        public override void MouseClick(IPoint position)
            => _baseGeometry = UpdateGeometry(position);

        public override bool MouseDoubleClick(IPoint position)
        {
            _context.End(UpdateGeometry(position));

            return true;
        }

        IGeometry UpdateGeometry(IPoint endPoint)
        {
            var points = new Coordinate[_baseGeometry.Coordinates.Length + 1];
            Array.Copy(_baseGeometry.Coordinates, points, _baseGeometry.Coordinates.Length);
            points[_baseGeometry.Coordinates.Length] = endPoint.Coordinate;

            return _context.ActiveGeometry = new LineString(points);
        }
    }
}
