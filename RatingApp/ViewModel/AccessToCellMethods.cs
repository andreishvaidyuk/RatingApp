using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace RatingApp
{
    //класс для получения доступа к ячейкам DataGrid
    public static class AccessToCellMethods
    {
        private static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        public static DataGridRow GetRow(this DataGrid grid, int index)
        {
            DataGridRow row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                grid.UpdateLayout();
                grid.ScrollIntoView(grid.Items[index]);
                row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }

        public static DataGridCell GetCell(this DataGrid grid, DataGridRow row, int column)
        {
            if (row != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);

                if (presenter == null)
                {
                    grid.ScrollIntoView(row, grid.Columns[column]);
                    presenter = GetVisualChild<DataGridCellsPresenter>(row);
                }

                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                return cell;
            }
            return null;
        }

        public static object GetPlayerRating(DataGridRow row, DataGridCell cell)
        {
            if (cell.IsEnabled == true)
            {
                // находим объект, связанный со строкой
                object data = row.Item;

                // извлекаем его свойства
                PropertyDescriptorCollection properties =
                    TypeDescriptor.GetProperties(data);

                // находим свойство "Рейтинг"
                PropertyDescriptor property = properties["Rating"];
                object value = property.GetValue(data);

                return value;
            }
            return null;
        }
    }
}
