   M S B A M L   `   `   `  ����E  AUiDesigner, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:UiDesigner.Views.ComponentUiDesigner.Views.Component  5      *    $UiDesigner.Views.Component.PopupView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      c _Microsoft.Expression.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35=ed4http://schemas.microsoft.com/expression/2010/drawing  5      _ [MaterialDesignThemes.Wpf, Version=4.8.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      4local(clr-namespace:UiDesigner.Views.Component  5      .��5      � -   5          �Name$  Popup��5       ���#1a8b3c � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   %  Microsoft.Expression.Shapes.Arc  -   5          Name$ 
Indication��5   m   $��0��6   $
��10 5q�6!   $��40��6/   $��40��6:   
�������$
��Left=�6R    �� Stretch$
 None=�5         ArcThickness$ 6Q�6       ArcThicknessUnit$ Pixel=�61      EndAngle$	 360Q�6J    J� Opacity$	 0.6Q�6Y     5   
   -   5        $ RIndication��5   p   $��0��6   $
��10 5q�6!   $��40��6/   $��40��6:   ���#ffffff$
��Left=�6U   $
 None=�5      $ 6Q�6    $ Pixel=�61      
StartAngle$ 0Q�6J   $ 60Q�6Y   �� 5   
   -   5        	 ��Name$	 Message��5   T   $,��&Enregistrement effectué avec succès.��5      $��14(�6@   $
��Left=�6N   $��Center=�5      ���#ffffff$��1��6D   6T   �� 5   
   -   5        
������ 
������ $	��0.5q�5   D   

 � Tag$	
 Add��6Z    �� CornerRadius$ 4��6d   $��1��6u    � Cursor$
 Hand��5      $��30��67   $��30��6B   $��Right=�6N   $��10q�6j   ._�5      '  !MaterialDesignThemes.Wpf.PackIcon  -   5          Name$ Close��5   A      Kind$ Close=�5   &   $��20��63   $��20��6>   ���#ffffff$��Center=�6_   $��Center=�5   &   6A   5      5      5      