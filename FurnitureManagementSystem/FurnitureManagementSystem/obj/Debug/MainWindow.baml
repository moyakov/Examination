   M S B A M L   `   `   `  ����T  PFurnitureManagementSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:FurnitureManagementSystemFurnitureManagementSystem  5   	   *    $FurnitureManagementSystem.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:FurnitureManagementSystem     5   	     � Icon$  /img/logo.ico��5   	    � Title$ Комфорт��5	   	   $	��600��6   $	��900��6&   .��5
      � ���	#FFF5F5F5 � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��Center=�6.   $
��0,10q�6K   .+�5      �� $��/img/logo.ico��6   $��30��6+   $��30��66   $��0,0,10,0q�6B   �� 5      $��Продукция��6   $��20(�6)   $
��Bold"�67   $��Center=�6I   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ productsGrid��5      $��1��6)   $��10q�66      AutoGenerateColumns .     
IsReadOnly .    HeadersVisibility$
 None=�5   %      	RowHeight$ 90Q�6>   
������ $��0q�5      	   HorizontalGridLinesBrush
	 ���� 
   VerticalGridLinesBrush

 ����    SelectionMode$ Single=�5         SelectionUnit$ FullRow=�6*      Columns 5      4  .System.Windows.Controls.DataGridTemplateColumn  5      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6)      CellTemplate 5      �� 5      .R�5      �  � ColumnDefinitions 5   "   �� 5    &   $��* �67   �� 5!   &   $
��Auto �67   .+�5$   "   �� $��0��6-   $��5q�6=   .+�5%   &   �� $
��Bold"�6V   $��14(�6h   ��60   �� *ProductType.TypeName+6h   �� 5&   &   $��16(�6M   $
��Bold"�6[   $��0,5,0,0q�6m   ��60   �� *ProductName+6m   �� 5'   &   $ 
Horizontal=�61   $��0,5,0,0q�6J   .+�5(   *   �� $��Артикул: ��64   $��SemiBold"�6E   �� 5)   *   ��64   �� *ArticleNumber+5*   '   �� 5+   &   $ 
Horizontal=�61   $��0,5,0,0q�6J   .+�5,   *   �� $'��!Минимальная цена: ��64   $��SemiBold"�6N   �� 5-   *   ��64   �� *MinPartnerPrice+ �� StringFormat$ {0:C}��5.   '   5/   #   �� 51   "   $��1��6)   $��70��69   $��70��6D   ���	#FFE3F2FD �� CornerRadius$ 35��52   )   $��Center=�6;   $��Center=�6X   $��0,0,10,0q�53   )   ._�54   &   �� $��14(�55   0   $
��Bold"�6>   ���#355CBD$��Center=�56   0   $��Center=�6M    �� TextAlignment$ Center=�57   0   ��54   0   �� *TotalProductionTime+$ {0} ч��57   0   58   #   59      5:      5<         	CellStyle 5?      �� 5@       �� 
TargetType*  $System.Windows.Controls.DataGridCell"  6   .�5A      ��  �� Property � �� �� Value$ 0q�68   �� 5B       �@�FocusVisualStyle 69   Z� �� 5C       � ��$ 5q�60   �� 5D       � ��
 �����63   �� 5E       � ��$ 0,0,0,5q�6/    �� Triggers 5F      P� 5G       P� Property �@ 
IsSelected P� Value . . �5H      ��  � �� �	#FFE3F2FD6;   �� 5I       � ��
 �   �6;   5J      5L      5N      �� 5P   
   $��2��6   $ 
Horizontal=�6"   $��Right=�6;   $��	0,0,10,10q�6W   .+�5Q      �� -   5        $��Добавить��5Q      $	��100��6(   $��5q�64   $��5q�6?   ���#355CBD
�������
������ 5R   =   �� 5S      -   5        $ ��Редактировать��5S      $	��100��6-   $��5q�69   $��5q�6D   ���#355CBD
�������
������ 5T   =   �� 5U      -   5        $��Удалить��5U      $	��100��6'   $��5q�63   $��5q�6>   ���#355CBD
�������
������ 5V   =   5W      5X      5Y      