using System.Collections.Generic;

namespace MyPlaces.Models
{
    class PlaceCollection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Place> Places { get; set; }
    }
}
