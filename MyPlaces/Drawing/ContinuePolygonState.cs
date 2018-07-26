using System;
using GeoAPI.Geometries;
using NetTopologySuite.Geometries;

namespace MyPlaces.Drawing
{
    class ContinuePolygonState : DrawingState
    {
        readonly MapDrawingContext _context;
        IGeometry _baseGeometry;

        public ContinuePolygonState(MapDrawingContext context, IGeometry baseGeometry)
        {
            _context = context;
            _baseGeometry = baseGeometry;
        }

        public override void MouseMove(Coordinate position)
            => UpdateGeometry(position);

        public override void MouseClick(Coordinate position)
            => _baseGeometry = UpdateGeometry(position);

        public override bool MouseDoubleClick(Coordinate position)
        {
            // NB: Reverts the last single-click
            _context
                .End(UpdateGeometry(position, skipLast: true))
                .AddPolygon();

            return true;
        }

        IGeometry UpdateGeometry(Coordinate endPoint, bool skipLast = false)
        {
            int baseLength = _baseGeometry.Coordinates.Length;
            if (skipLast)
                baseLength--;
            var points = new Coordinate[baseLength + 1];
            Array.Copy(_baseGeometry.Coordinates, points, baseLength - 1);
            points[baseLength - 1] = endPoint;
            points[baseLength] = _baseGeometry.Coordinates[0];

            return _context.ActiveGeometry = new Polygon(new LinearRing(points));
        }
    }
}
