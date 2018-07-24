using GeoAPI.Geometries;

namespace MyPlaces.Models
{
    class Place
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IGeometry Geometry { get; set; }

        public int CollectionId { get; set; }
        public PlaceCollection Collection { get; set; }
    }
}
