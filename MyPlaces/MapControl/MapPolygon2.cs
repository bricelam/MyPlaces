using System.Reflection;
using System.Windows;
using Microsoft.Maps.MapControl.WPF;

namespace MyPlaces.MapControl
{
    class MapPolygon2 : MapPolygon
    {
        readonly static DependencyProperty Locations2Property = DependencyProperty.Register(
            "Locations2",
            typeof(LocationCollection),
            typeof(MapPolygon2),
            new PropertyMetadata(OnLocations2Changed));

        public LocationCollection Locations2
        {
            get => (LocationCollection)GetValue(Locations2Property);
            set => SetValue(Locations2Property, value);
        }

        static void OnLocations2Changed(DependencyObject o, DependencyPropertyChangedEventArgs ea)
        {
            var baseProperty = typeof(MapShapeBase).GetField("LocationsProperty", BindingFlags.NonPublic | BindingFlags.Static);
            var basePropertyValue = (DependencyProperty)baseProperty.GetValue(null);
            o.SetValue(basePropertyValue, ea.NewValue);

            // NB: This line is missing in base
            ((UIElement)o).InvalidateMeasure();
        }
    }
}
