using GeoAPI.Geometries;
using NetTopologySuite.Geometries;

namespace MyPlaces.Drawing
{
    class StartLineStringState : DrawingState
    {
        readonly MapDrawingContext _context;

        public StartLineStringState(MapDrawingContext context)
            => _context = context;

        public override void MouseClick(Coordinate position)
            => _context.State = new ContinueLineStringState(_context, new Point(position));
    }
}
