using GeoAPI.Geometries;
using NetTopologySuite.Geometries;

namespace MyPlaces.Drawing
{
    class ThirdPolygonState : DrawingState
    {
        readonly DrawingContext _context;
        readonly IGeometry _firstSegment;

        public ThirdPolygonState(DrawingContext context, IGeometry firstSegment)
        {
            _context = context;
            _firstSegment = firstSegment;
        }

        public override void MouseMove(IPoint position)
            => UpdateGeometry(position);

        public override void MouseClick(IPoint position)
            => _context.State = new ContinuePolygonState(_context, UpdateGeometry(position));

        public override bool MouseDoubleClick(IPoint position)
        {
            _context.End(UpdateGeometry(position));

            return true;
        }

        IGeometry UpdateGeometry(IPoint thirdPoint)
            => _context.ActiveGeometry = new Polygon(
                new LinearRing(
                    new[]
                    {
                        _firstSegment.Coordinates[0],
                        _firstSegment.Coordinates[1],
                        thirdPoint.Coordinate,
                        _firstSegment.Coordinates[0]
                    }));
    }
}
