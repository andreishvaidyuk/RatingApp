using RatingApp.Model;
using RatingApp.ViewModel;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RatingApp
{
    public class PlayersManager
    {
        public List<Player> Load()
        {
            using (TableTennisContext db = new TableTennisContext())
            {
                return db.Players.ToList();
            }
        }

        public Player Add(Player p)
        {
            using (TableTennisContext db = new TableTennisContext())
            {
                db.Players.Add(p);
                db.SaveChanges();
                return p;
            }
        }

        public Player Edit(Player p)
        {
            using (TableTennisContext db = new TableTennisContext())
            {
                AddPlayer newPlayer = new AddPlayer(p);
                p = db.Players.Find(newPlayer.Player.Id);
                if (p != null)
                {
                    p.FirstName = newPlayer.Player.FirstName;
                    p.LastName = newPlayer.Player.LastName;
                    p.Rating = newPlayer.Player.Rating;
                    db.Entry(p).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return p;
            }
        }

        public void Remove(Player p)
        {
            using (TableTennisContext db = new TableTennisContext())
            {
                db.Players.Attach(p);
                db.Players.Remove(p);
                db.SaveChanges();
            }
        }
    }
}
