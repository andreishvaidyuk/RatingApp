using RatingApp.Model.Abstract;
using RatingApp.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingApp.Model
{
    public class Tournament : Entity
    {
        public TournamentLevel TournamentName { get; set; }
        public DateTime TournamentDate { get; set; }
        public ICollection<Play> PlaysOfTournament { get; set; }
        public ICollection<Player> PlayersOfTournament { get; set; }
    }
}
