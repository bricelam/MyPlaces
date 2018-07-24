using Microsoft.EntityFrameworkCore;

namespace MyPlaces.Models
{
    class PlaceContext : DbContext
    {
        public PlaceContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<PlaceCollection> Collections { get; set; }
    }
}
