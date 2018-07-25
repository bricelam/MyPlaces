using GeoAPI.Geometries;

namespace MyPlaces.Drawing
{
    abstract class DrawingState
    {
        public virtual void MouseMove(Coordinate position)
        {
        }

        public virtual void MouseClick(Coordinate position)
        {
        }

        public virtual bool MouseDoubleClick(Coordinate position)
            => false;
    }
}
