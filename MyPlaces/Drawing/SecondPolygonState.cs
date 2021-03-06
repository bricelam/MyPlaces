﻿using GeoAPI.Geometries;
using NetTopologySuite.Geometries;

namespace MyPlaces.Drawing
{
    class SecondPolygonState : DrawingState
    {
        readonly MapDrawingContext _context;
        readonly Coordinate _startpoint;

        public SecondPolygonState(MapDrawingContext context, Coordinate startPoint)
        {
            _context = context;
            _startpoint = startPoint;
        }

        public override void MouseMove(Coordinate position)
            => UpdateGeometry(position);

        public override void MouseClick(Coordinate position)
            => _context.State = new ThirdPolygonState(_context, UpdateGeometry(position));

        public override bool MouseDoubleClick(Coordinate position)
        {
            // NB: Reverts the last single-click
            _context.State = new StartPolygonState(_context);

            return false;
        }

        IGeometry UpdateGeometry(Coordinate secondPoint)
            => _context.ActiveGeometry = new LineString(new[] { _startpoint, secondPoint });
    }
}
