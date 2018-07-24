using GeoAPI.Geometries;

namespace MyPlaces.Drawing
{
    abstract class DrawingState
    {
        public virtual void MouseMove(IPoint position)
        {
        }

        public virtual void MouseClick(IPoint position)
        {
        }

        public virtual bool MouseDoubleClick(IPoint position)
            => false;
    }
}
