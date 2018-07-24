using GeoAPI.Geometries;
using NetTopologySuite.Geometries;

namespace MyPlaces.Drawing
{
    class SecondPolygonState : DrawingState
    {
        readonly DrawingContext _context;
        readonly IGeometry _startpoint;

        public SecondPolygonState(DrawingContext context, IGeometry startPoint)
        {
            _context = context;
            _startpoint = startPoint;
        }

        public override void MouseMove(IPoint position)
            => UpdateGeometry(position);

        public override void MouseClick(IPoint position)
            => _context.State = new ThirdPolygonState(_context, UpdateGeometry(position));

        IGeometry UpdateGeometry(IPoint secondPoint)
            => _context.ActiveGeometry = new LineString(new[] { _startpoint.Coordinate, secondPoint.Coordinate });
    }
}
