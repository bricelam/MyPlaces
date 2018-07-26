using System.Collections.Generic;
using System.Collections.ObjectModel;
using GeoAPI.Geometries;

namespace MyPlaces.Models
{
    class PlaceCollection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public ICollection<Place> Places { get; set; }
        public ICollection<IGeometry> Geometries { get; } = new ObservableCollection<IGeometry>();
    }
}
