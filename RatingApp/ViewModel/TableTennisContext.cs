using RatingApp.Model;
using System.Data.Entity;

namespace RatingApp.ViewModel
{
    class TableTennisContext : DbContext
    {
        public TableTennisContext() : base("SQLiteConnection")
        {

        }

        public DbSet<Player> Players { get; set; }
    }
}
