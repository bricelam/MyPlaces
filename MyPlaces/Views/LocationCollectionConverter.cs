using System;
using System.Globalization;
using System.Windows.Data;
using GeoAPI.Geometries;
using Microsoft.Maps.MapControl.WPF;
using Location = Microsoft.Maps.MapControl.WPF.Location;

namespace MyPlaces.Views
{
    [ValueConversion(typeof(Coordinate[]), typeof(LocationCollection))]
    class LocationCollectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var locations = new LocationCollection();
            var coordinates = (Coordinate[])value;
            var length = coordinates.Length;
            if ((string)parameter == "SkipLast")
                length -= 1;

            for (var i = 0; i < length; i++)
            {
                var coordinate = coordinates[i];
                locations.Add(new Location(coordinate.Y, coordinate.X));
            }

            return locations;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
