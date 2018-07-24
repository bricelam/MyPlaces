using GeoAPI.Geometries;

namespace MyPlaces.Drawing
{
    class StartPolygonState : DrawingState
    {
        readonly DrawingContext _context;

        public StartPolygonState(DrawingContext context)
            => _context = context;

        public override void MouseClick(IPoint position)
            => _context.State = new SecondPolygonState(_context, position);
    }
}
