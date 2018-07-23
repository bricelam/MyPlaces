using GalaSoft.MvvmLight.Command;
using GeoAPI.Geometries;
using Microsoft.Maps.MapControl.WPF;
using NetTopologySuite.Geometries;

namespace MyPlaces.Views
{
    class ViewChangeEndEventArgsConverter : IEventArgsConverter
    {
        public object Convert(object value, object parameter)
        {
            var boundingRectangle = ((Map)parameter).BoundingRectangle;
            var startPoint = new Coordinate(boundingRectangle.West, boundingRectangle.North);

            return new Polygon(
                new LinearRing(
                    new[]
                    {
                        startPoint,
                        new Coordinate(boundingRectangle.East, boundingRectangle.North),
                        new Coordinate(boundingRectangle.West, boundingRectangle.South),
                        new Coordinate(boundingRectangle.East, boundingRectangle.South),
                        startPoint
                    }));
        }
    }
}
