   M S B A M L   `   `   `  ����T  PFurnitureManagementSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:FurnitureManagementSystemFurnitureManagementSystem  5   	   1    +FurnitureManagementSystem.ProductEditDialog   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:FurnitureManagementSystem     5   	     � Icon$  /img/logo.ico��5   	    � Title$3 -Редактирование продукта��5	   	   $	��400��6)   $	��500��66   $��Candara)�5
   	    � WindowStartupLocation$ CenterOwner=�6   .��5      � ���	#FFFFFFFF$��10q�6"    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $0��*Наименование продукта:��6   $��0��62   $	��0,5q�6?   �� 5   
   -   5         ��Name$ productNameTextBox��5      $��1��6.   $��0,0,0,10q�6;   �� 5   
   $��Артикул:��6   $��2��6$   $	��0,5q�61   �� 5   
   -   5        $ articleNumberTextBox��5      $��3��60   $��0,0,0,10q�6=   �� 5    
   $>��8Минимальная цена для партнера:��6   $��4��6:   $	��0,5q�6G   �� 5!   
   -   5        $ priceTextBox��5!      $��5��6(   $��0,0,0,10q�65   �� 5#   
   $��Тип продукта:��6   $��6��6)   $	��0,5q�66   �� 5$   
   -   5         ��Name$ productTypeComboBox��5$      $��7��60   $��0,0,0,10q�6=   �� 5&   
   $(��"Основной материал:��6   $��8��6.   $	��0,5q�6;   �� 5'   
   -   5        $ materialComboBox��5'      $��9��6-   $��0,0,0,20q�6:   �� 5)   
   $��10��6    �� Orientation$ 
Horizontal=�6#   $��Right=�6<   .+�5*      �� -   5        $��Сохранить��5*      $	��100��6)   $��5q�65   $��5q�6@   ���	#FF355CBD
�������
������ 5+   ?   �� 5,      -   5        $��Отмена��5,      $	��100��6&   $��5q�62   $��5q�6=   ���	#FF355CBD
�������
������ 5-   ?   5.      5/      50      