using RatingApp.Model;
using RatingApp.ViewModel;
using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.SQLite;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace RatingApp
{
    public static class OperationsWithRating
    {
        public static void CalculateRating(DataGrid resultDataGrid)
        {
            // количество игроков, выбранных для расчета рейтинга
            int currentRowsCount = resultDataGrid.Items.Count;

            // цикл по списку игроков в расчете рейтинга
            for (int i = 0; i < currentRowsCount; i++)
            {
                double deltaCompetition = 0;
                double totalDelta = 0;
                bool isWin = false;
                //Получаем строку в которой записан текущий игрок
                var currentPlayerRow = AccessToCellMethods.GetRow(resultDataGrid, i);

                // цикл по оппонентам текущего игрока
                for (int j = 0; j < currentRowsCount; j++)
                {
                    // Получаем значение рейтинга текущего игрока
                    DataGridCell currentPlayerCell = AccessToCellMethods.GetCell(resultDataGrid, currentPlayerRow, j + 2);
                    var currentPlayerRating = AccessToCellMethods.GetPlayerRating(currentPlayerRow, currentPlayerCell);
                    TextBlock text = currentPlayerCell.Content as TextBlock;

                    // Получаем значение рейтинга оппонента
                    var opponentPlayerRow = AccessToCellMethods.GetRow(resultDataGrid, j);
                    var opponentPlayerCell = AccessToCellMethods.GetCell(resultDataGrid, currentPlayerRow, j + 2);
                    var opponentPlayerRating = AccessToCellMethods.GetPlayerRating(opponentPlayerRow, opponentPlayerCell);

                    if (currentPlayerRating != null && opponentPlayerRating != null)
                    {
                        if (text.Text == "")
                        {
                            MessageBox.Show("Заполните результаты игр!");
                            break;
                        }
                        else if (text.Text == "1")
                        {
                            isWin = true;
                        }
                        else if (text.Text == "0")
                        {
                            isWin = false;
                        }
                        else
                        {
                            MessageBox.Show("Значения могут быть только 1 или 0");
                        }
                        var delta = RatingCalculation.ToCalculateDelta(currentPlayerRating, opponentPlayerRating, isWin);

                        deltaCompetition += delta;
                    }
                }
                // расчета изменения рейтинга по итогам всех встреч
                double averageDeltaCompetition = deltaCompetition / (currentRowsCount - 1);

                try
                {
                    // расчет итогового изменения рейтинга с учетом бонусов и коээфициентов
                    // получение бонуса за место
                    DataGridCell currentPlayerBonusCell = AccessToCellMethods.GetCell(resultDataGrid, currentPlayerRow, currentRowsCount + 2);
                    TextBlock bonus = currentPlayerBonusCell.Content as TextBlock;
                    int bonusForPlace = int.Parse(bonus.Text);

                    // получение коэффициента значимости соревнований
                    DataGridCell currentPlayerKZS = AccessToCellMethods.GetCell(resultDataGrid, currentPlayerRow, currentRowsCount + 3);
                    TextBlock kzsTB = currentPlayerKZS.Content as TextBlock;
                    double kzs = double.Parse(kzsTB.Text, CultureInfo.InvariantCulture); //CultureInfo.InvariantCulture используется для исключения учета формата Double

                    // получение коэффициента за лигу
                    DataGridCell currentPlayerLeagueCoeff = AccessToCellMethods.GetCell(resultDataGrid, currentPlayerRow, currentRowsCount + 4);
                    TextBlock leagueCoeffTB = currentPlayerLeagueCoeff.Content as TextBlock;
                    double leagueCoeff = double.Parse(leagueCoeffTB.Text, CultureInfo.InvariantCulture);

                    //получение и заполнение итогового изменения рейтинга
                    totalDelta = RatingCalculation.ToCalculateTotalDelta(averageDeltaCompetition, bonusForPlace, kzs, leagueCoeff);

                    resultDataGrid.GetCell(currentPlayerRow, currentRowsCount + 5).Content = totalDelta.ToString();
                }
                catch (Exception)
                {
                    MessageBox.Show("Заполните все поля числовыми данными!!!");
                }
            }
        }

        public static void SaveRating(DataGrid resultDataGrid)
        {
            try
            {
                using (TableTennisContext db = new TableTennisContext())
                {
                    // получение количества игроков в расчете рейтинга
                    int currentRowsCount = resultDataGrid.Items.Count;

                    // цикл по списку игроков 
                    for (int i = 0; i < currentRowsCount; i++)
                    {
                        DataGridRow currentPlayerRow = AccessToCellMethods.GetRow(resultDataGrid, i);
                        // получение итогового увеличения рейтинга
                        DataGridCell currentPlayerTotalDelta = AccessToCellMethods.GetCell(resultDataGrid, currentPlayerRow, currentRowsCount + 5);
                        double totalDelta = 0;
                        //приведение к типу double
                        totalDelta = double.Parse(currentPlayerTotalDelta.Content.ToString());
                        object data = currentPlayerRow.Item;

                        // извлекаем его свойства
                        PropertyDescriptorCollection properties =
                            TypeDescriptor.GetProperties(data);

                        // находим свойство "Рейтинг"
                        PropertyDescriptor property = properties["Rating"];
                        object rating = property.GetValue(data);

                        //добавляем изменение рейтинга в базу
                        Player player = data as Player;
                        player.Rating += totalDelta;
                        db.Entry(player).State = EntityState.Modified;
                    }
                    //сохраняем изменения в базе
                    db.SaveChanges();
                    MessageBox.Show("Новый рейтинг сохранен в базе!");
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Данные для сохранения не расчитаны! Заполните результаты игр");
            }
        }

        public static void ExportRating()
        {
            Microsoft.Office.Interop.Excel.Application xlApp;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;

            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add();
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            string cs = @"URI=file:..\..\ttplayers.db";
            object data = string.Empty;
            int i = 0;
            int j = 0;
            using (SQLiteConnection con = new SQLiteConnection(cs))
            {
                con.Open();
                string stm = "SELECT * FROM Players";
                using (SQLiteCommand cmd = new SQLiteCommand(stm, con))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        //Чтение строк
                        while (rdr.Read())
                        {
                            //Цикл по колонкам
                            for (j = 0; j <= rdr.FieldCount - 1; j++)
                            {
                                data = rdr.GetValue(j);
                                xlWorkSheet.Cells[1, 1] = "ID";
                                xlWorkSheet.Cells[1, 2] = "Имя";
                                xlWorkSheet.Cells[1, 3] = "Фамилия";
                                xlWorkSheet.Cells[1, 4] = "Рейтинг";
                                xlWorkSheet.Cells[i + 2, j + 1] = data;
                            }
                            i++;
                        }
                    }
                }
                con.Close();
            }

            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.FileName = "Rating";
            sfd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            bool? result = sfd.ShowDialog();
            if (result == true)
            {
                xlWorkBook.SaveAs(sfd.FileName);
                xlWorkBook.Close(true);
                xlApp.Quit();
                MessageBox.Show("База данных экспортирована!");
            }
        }
    }


}
