using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GeoAPI.Geometries;
using Microsoft.Maps.MapControl.WPF;
using MyPlaces.ViewModels;
using NetTopologySuite.Geometries;

namespace MyPlaces.Views
{
    class MouseEventArgsConverter : IEventArgsConverter
    {
        public object Convert(object value, object parameter)
        {
            var e = (MouseEventArgs)value;
            var map = ((Map)parameter);
            var location = map.ViewportPointToLocation(e.GetPosition(map));

            return ((IPoint, Handleable))(
                new Point(location.Longitude, location.Latitude),
                new Handleable(() => e.Handled = true));
        }
    }
}
