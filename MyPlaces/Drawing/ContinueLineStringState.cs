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
            // NOTE: The view cancels single-click events when double-clicking. Alternatively, the single-click effects
            //       could be reverted here by removing the last point
            _context
                .End(UpdateGeometry(position))
                .AddLineString();

            return true;
        }

        IGeometry UpdateGeometry(Coordinate endPoint)
        {
            var baseLength = _baseGeometry.Coordinates.Length;
            var points = new Coordinate[baseLength + 1];
            Array.Copy(_baseGeometry.Coordinates, points, baseLength);
            points[baseLength] = endPoint;

            return _context.ActiveGeometry = new LineString(points);
        }
    }
}
