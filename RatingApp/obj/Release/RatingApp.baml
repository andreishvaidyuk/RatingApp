   M S B A M L   `   `   `  ���� D  @RatingApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:RatingApp	RatingApp  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections     RatingApp.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:RatingApp     Bcol2clr-namespace:System.Collections;assembly=mscorlib       � Title$  	RatingApp��$	��600��$	��800��.���  � RowDefinitions �� $��20 ���  � ColumnDefinitions �� $	��250 ��� $��50 ��� $��2* �.+�o� .@�m� $��Файл��.@�m� -    m�Name$ 
ShowRating��$#��Вывести рейтинг��m� -   $ ExportRating��$1��+Экспортировать рейтинг���� m� -   $
 Exit��$��
Выход��m� $��Правка��.@�m� -   $ 	AddPlayer��$#��Добавить игрока��m� -   $ RemovePlayer��$!��Удалить игрока��m� -   $ 
EditPlayer��$-��'Редактировать игрока���� -   $��playersList��$��0��$��1�� �� View �� .P��� $	��№�� �� DisplayMemberBinding �� *Id+�� $��Фамилия�� �� *
LastName+�� $��Имя�� �� *	FirstName+�� $��Рейтинг�� �� *Rating+�� $��1��$��1��.+��� -   $ ��addPlayerToCalculateRating��$��+��$��3 3 3 3q��� -	   $%��removePlayerFromCalculateRating��$��-��$��3 3 3 3q�� $��2��$��1�� �� $��30 ��� �� $��50 � �� $��* �.+��� $%��Окно результатов��$��0��&   System.Windows.Controls.DataGrid  -
   $��resultDataGrid��$��1��   AutoGenerateColumns .     CanUserAddRows .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn	   Header$		 №��
   
IsReadOnly
 . 1  +System.Windows.Controls.DataGridBoundColumn   Binding ��  �� Path$ Id�  $	 Фамилия��
 .  �� $ LastName�  $ 	 Бонус за место�� ��  �� Mode$ TwoWay=�$ 
IsSelected�  $	 КЗС�� �� $ TwoWay=�$ 
IsSelected�  $*	 $Коэффициент за лигу�� �� $ TwoWay=�$ 
IsSelected�  $+	 %Увеличение рейтинга��-   ratingDelta��
 .  �� $ TwoWay=�$ 
IsSelected��� $��0��$��2�� �� Orientation$ 
Horizontal=�.+��� -   $��calculateRating��$��Расчитать��$��2��$	��158��$��3 3 3 3q��� -    ��Name$ savePlayersRating��$��Сохранить��$	��158��$��3 3 3 3 q��� -   $
��exit��$��Закрыть��$	��158��$��3 3 3 3 q�