using RatingApp.Model.Abstract;
using RatingApp.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingApp.Model
{
    public class Play : Entity
    {
        public DateTime PlayDate { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public WinnerMark Winner { get; set; }
        public Play()
        {

        }
    }
}
