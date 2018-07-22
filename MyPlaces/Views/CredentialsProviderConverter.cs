using System;
using System.Globalization;
using System.Windows.Data;
using Microsoft.Maps.MapControl.WPF.Core;
using Microsoft.Maps.MapControl.WPF.Design;

namespace MyPlaces.Views
{
    [ValueConversion(typeof(string), typeof(CredentialsProvider))]
    class CredentialsProviderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => new ApplicationIdCredentialsProviderConverter().ConvertFrom(value);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
