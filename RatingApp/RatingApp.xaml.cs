using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using RatingApp.Model;
using System.Collections.ObjectModel;

namespace RatingApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<Player> _playersCollection;
        private readonly PlayersManager _manager;
        public MainWindow()
        {
            InitializeComponent();
            //инициализируем коллекцию игроков и потом привязываем DataGrid к данной коллекции
            _playersCollection = new ObservableCollection<Player>();
            resultDataGrid.ItemsSource = _playersCollection;
            _manager = new PlayersManager();
        }

        private void addPlayerToCalculateRating_Click(object sender, RoutedEventArgs e)
        {
            //проверка, выбран ли игрок
            if((Player)playersList.SelectedItem!=null)
            {
                //добавляем новую строку и новый столбец
                var currentRowsCount = resultDataGrid.Items.Count;
                DataGridTextColumn newColumn = new DataGridTextColumn();
                newColumn.Header = (currentRowsCount + 1).ToString();
                newColumn.Binding = new Binding(string.Format("[{0}]", 1));

                resultDataGrid.Columns.Insert(resultDataGrid.Columns.Count - 4, newColumn);
                //выделенного игрока сначала добавляем в коллекцию игроков, а потом в DataGrid
            
                _playersCollection.Add((Player)playersList.SelectedItem);
                //Получение и закраска ячеек по диагонали
                //Получаем строку в виде DataGridRow
                var row = AccessToCellMethods.GetRow(resultDataGrid, currentRowsCount);
                // Получаем ячейку в виде DataGridCell
                var cell = AccessToCellMethods.GetCell(resultDataGrid, row, currentRowsCount + 2);
                //закрашиваем ячейки
                cell.Background = Brushes.Black;
                //ставим ячейки недоступными
                cell.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Не выбран игрок для добавления в расчеты!");
            }
        }

        private void removePlayerFromCalculateRating_Click(object sender, RoutedEventArgs e)
        {
            var index = resultDataGrid.SelectedIndex;
            //проверка, выбран ли игрок
            if(index>=0)
            {
                resultDataGrid.Columns.RemoveAt(index + 2);
                //удаляем игрока из коллекции и соответственно из DataGrid
                _playersCollection.RemoveAt(index);
            }
            else
            {
                MessageBox.Show("Не выбран игрок для удаления из расчетов!");
            }
        }

        private void ShowRating_Click(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void AddPlayer_Click(object sender, RoutedEventArgs e)
        {
            Add();
        }

        private void EditPlayer_Click(object sender, RoutedEventArgs e)
        {
            Edit();
        }

        private void RemovePlayer_Click(object sender, RoutedEventArgs e)
        {
            Remove();
        }

        private void calculateRating_Click(object sender, RoutedEventArgs e)
        {
            OperationsWithRating.CalculateRating(resultDataGrid);
        }

        private void savePlayersRating_Click(object sender, RoutedEventArgs e)
        {
            OperationsWithRating.SaveRating(resultDataGrid);
            Load();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ExportRating_Click(object sender, RoutedEventArgs e)
        {
            OperationsWithRating.ExportRating();
        }

        private void Load()
        {
            playersList.Items.Clear();

            foreach (var item in _manager.Load())
            {
                playersList.Items.Add(item);
            }
        }

        private void Add()
        {
            AddPlayer newPlayer = new AddPlayer(new Player());
            newPlayer.ShowDialog();
            Load();
        }

        private void Edit()
        {
            Player player = playersList.SelectedItem as Player;
            
            if (player != null)
            {
                AddPlayer newPlayer = new AddPlayer(player);
                newPlayer.ShowDialog();
                MessageBox.Show("Данные игрока отредактированы!");
                Load();
            }
            else
            {
                MessageBox.Show("Игрок не выбран!");
            }


        }

        private void Remove()
        {
            if (playersList.SelectedItem == null)
            {
                MessageBox.Show("Игрок не выбран!");
                return;
            }
            Player player = playersList.SelectedItem as Player;
            _manager.Remove(player);
            MessageBox.Show("Игрок удален!");
            Load();
        }
    }
}
