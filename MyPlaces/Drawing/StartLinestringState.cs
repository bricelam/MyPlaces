using GeoAPI.Geometries;

namespace MyPlaces.Drawing
{
    class StartLineStringState : DrawingState
    {
        readonly DrawingContext _context;

        public StartLineStringState(DrawingContext context)
            => _context = context;

        public override void MouseClick(IPoint position)
            => _context.State = new ContinueLineStringState(_context, position);
    }
}
