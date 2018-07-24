using GeoAPI.Geometries;

namespace MyPlaces.Drawing
{
    class AddPointState : DrawingState
    {
        readonly DrawingContext _context;

        public AddPointState(DrawingContext context)
            => _context = context;

        public override void MouseClick(IPoint position)
            => _context.End(position);
    }
}
