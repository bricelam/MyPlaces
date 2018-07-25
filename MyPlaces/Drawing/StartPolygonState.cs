using GeoAPI.Geometries;

namespace MyPlaces.Drawing
{
    class StartPolygonState : DrawingState
    {
        readonly MapDrawingContext _context;

        public StartPolygonState(MapDrawingContext context)
            => _context = context;

        public override void MouseClick(Coordinate position)
            => _context.State = new SecondPolygonState(_context, position);
    }
}
