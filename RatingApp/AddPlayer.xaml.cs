using System.Windows;
using RatingApp.Model;

namespace RatingApp
{
    /// <summary>
    /// Логика взаимодействия для AddPlayer.xaml
    /// </summary>
    public partial class AddPlayer : Window
    {
        public Player Player { get; private set; }
        private readonly PlayersManager _manager;
        public AddPlayer(Player p)
        {
            InitializeComponent();
            Player = p;
            DataContext = Player;
            _manager = new PlayersManager();
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if(Player.Id > 0)
            {
                _manager.Edit(Player);
            }
            else
            {
                _manager.Add(Player);
            }
            this.Close();
        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
