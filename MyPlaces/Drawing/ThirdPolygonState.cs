using GeoAPI.Geometries;
using NetTopologySuite.Geometries;

namespace MyPlaces.Drawing
{
    class ThirdPolygonState : DrawingState
    {
        readonly MapDrawingContext _context;
        readonly IGeometry _firstSegment;

        public ThirdPolygonState(MapDrawingContext context, IGeometry firstSegment)
        {
            _context = context;
            _firstSegment = firstSegment;
        }

        public override void MouseMove(Coordinate position)
            => UpdateGeometry(position);

        public override void MouseClick(Coordinate position)
            => _context.State = new ContinuePolygonState(_context, UpdateGeometry(position));

        public override bool MouseDoubleClick(Coordinate position)
        {
            // NB: Reverts the last single-click
            _context.State = new SecondPolygonState(_context, _firstSegment.Coordinates[0]);

            return false;
        }

        IGeometry UpdateGeometry(Coordinate thirdPoint)
            => _context.ActiveGeometry = new Polygon(
                new LinearRing(
                    new[]
                    {
                        _firstSegment.Coordinates[0],
                        _firstSegment.Coordinates[1],
                        thirdPoint,
                        _firstSegment.Coordinates[0]
                    }));
    }
}
