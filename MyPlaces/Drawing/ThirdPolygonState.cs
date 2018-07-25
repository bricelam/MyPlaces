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
            // NOTE: The view cancels single-click events when double-clicking. Alternatively, the single-click effects
            //       could be reverted here by removing the last point
            _context
                .End(UpdateGeometry(position))
                .AddPolygon();

            return true;
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
