using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RatingApp.Model
{
    public class Rating:INotifyPropertyChanged
    {
        private double ratingValue;
        private DateTime date;

        public double RatingValue
        {
            get { return ratingValue; }
            set
            {
                ratingValue = value;
                OnPropertyChanged("RatingValue");
            }
        }
        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
