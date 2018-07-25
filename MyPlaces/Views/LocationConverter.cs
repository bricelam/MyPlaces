using System;
using System.Globalization;
using System.Windows.Data;
using GeoAPI.Geometries;
using Location = Microsoft.Maps.MapControl.WPF.Location;

namespace MyPlaces.Views
{
    [ValueConversion(typeof(Coordinate), typeof(Location))]
    class LocationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var coordinate = (Coordinate)value;

            return new Location(coordinate.Y, coordinate.X);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
