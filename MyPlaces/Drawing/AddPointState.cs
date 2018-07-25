using GeoAPI.Geometries;
using NetTopologySuite.Geometries;

namespace MyPlaces.Drawing
{
    class AddPointState : DrawingState
    {
        readonly MapDrawingContext _context;

        public AddPointState(MapDrawingContext context)
            => _context = context;

        public override void MouseClick(Coordinate position)
            => _context
                .End(new Point(position))
                .AddPoint();
    }
}
