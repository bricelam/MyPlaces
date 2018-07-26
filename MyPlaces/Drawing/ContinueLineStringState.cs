using System;
using GeoAPI.Geometries;
using NetTopologySuite.Geometries;

namespace MyPlaces.Drawing
{
    class ContinueLineStringState : DrawingState
    {
        readonly MapDrawingContext _context;
        IGeometry _baseGeometry;

        public ContinueLineStringState(MapDrawingContext context, IGeometry baseGeometry)
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
            if (_baseGeometry.Coordinates.Length == 1)
            {
                _context.State = new StartLineStringState(_context);

                return false;
            }
            else
            {
                _context
                    .End(UpdateGeometry(position, skipLast: true))
                    .AddLineString();

                return true;
            }
        }

        IGeometry UpdateGeometry(Coordinate endPoint, bool skipLast = false)
        {
            var baseLength = _baseGeometry.Coordinates.Length;
            if (skipLast)
                baseLength--;
            var points = new Coordinate[baseLength + 1];
            Array.Copy(_baseGeometry.Coordinates, points, baseLength);
            points[baseLength] = endPoint;

            return _context.ActiveGeometry = new LineString(points);
        }
    }
}
