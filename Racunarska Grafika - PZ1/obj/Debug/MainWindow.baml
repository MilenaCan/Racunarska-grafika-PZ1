   M S B A M L   `   `   `  ����S  ORacunarska Grafika - PZ1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Racunarska_Grafika___PZ1Racunarska_Grafika___PZ1  5   	   )    #Racunarska_Grafika___PZ1.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:Racunarska_Grafika___PZ1     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      � .+�5   
   �� $
��Auto=�6   $
��Auto=�6:   .��5      ��  �� Orientation$ Vertical=�6   .+�5      �� -   5        $��sldZoom��5       � Minimum$	 0.5Q�6(    � Maximum$ 20Q�66    � Value$ 1Q�6C   $��20��6M   �� 5      $
��1020��6   $
��1000��6&   
���   �$��1q�6H    � LayoutTransform 5      V� 5      .~�5      ��  �� ScaleX 5   "   �� 5   &    �� ElementName$ sldZoom��6.    �� Path$ Value�6D   	 �� Mode$	 OneWay=�6Q   
 �� ScaleY
 5   "   �� 5   &   $ sldZoom��6.   $ Value�6D   $	 OneWay=�6Q   5      5      ._�5      �� -   5         ��Name$ Canvas��5      $
��1000��6�   $
��1000��6�   
������ .+�5      	� -   5         	�Name$ Ellipse��5   &   $��Ellipse��67    �� Left$	 565��6I   
 �� Top$ 10��6[   	� 5      -   5        $
 Text��5   &   $
��Text��64   $ 10��6C   $��21��6S   $��52��6_   $	 635��6j   	� 5      -   5        $ Polygon��5   &   $��Polygon��67   $ 10��6I   $��23��6Y   $��68��6e   $	 692��6p   �� 5      -   5         ��Name$
 Undo��5   !   $
��Undo��6/   $	 402��6>   $ 7��6P   $��47��6_   $��18��6j   6v   �� 5       -   5        $
 Redo��5    !   $
��Redo��6/   $	 474��6>   $ 7��6P   $��47��6_   $��18��6j   6v   �� 5!      -   5        $ Clear��5!   !   $��Clear��61   $	 692��6A   $ 38��6S   $��47��6c   $��18��6n   6z   5"      5#      5$      5%      5&      5'      