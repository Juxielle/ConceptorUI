   M S B A M L   `   `   `  ����  
UiDesigner!    UiDesigner.ConfirmDialogBox   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=4.8.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � WindowStyle$
  None=�5   	    � WindowStartupLocation$ CenterScreen=�5	   	    � AllowsTransparency$
 True��5
   	   
������  � Title$ ConfirmDialogBox��5   	   $	��220��5   	   $	��280��5   	   .��5      ��  �� CornerRadius$ 4��6   ���#ffffff���#5335e9$	��0.5q�5      ._�5   
   �  � RowDefinitions 5      �� 5      $��* �6    �� 5      $��50 �6    .+�5      �� $��0��6   $��15q�5      .+�5      � $��50��6   .+�5      �� -   5         ��Name$ ContentIcon��5#      $��40��5      $��40��5      $ 20��5      $��Center=�5       $
��Left=�5!      ���#d2f8eb._�5$      '  !MaterialDesignThemes.Wpf.PackIcon  -   5          Name$
 Icon��5*   2      Kind$ CheckCircleOutline=�5$   2   $��18��5%   2   $��18��5&   2   ���#24c295$��Center=�5(   2   $��Center=�5)   2   5*   2   5+      �� 5,      -   5        $��20��5,      $��20��5-      $	��Top=�5.      $��Right=�5/      
	 � Tag$	 Close��50      ���#ffffff���#000000$	��0.4q�53      
 � Cursor$

 Hand��55      .��56        $ Close=�62   $��14��57   2   $��14��58   2   ���#000000$��Center=�5:   2   $��Center=�5;   2   5<      5=      �� 5?      -   5         ��Name$ LTitle��5D      $#��You are about to delete task.��5?      $��12(�5@      $
��Bold"�5A      ���#000000$��0 20 0 8q�5C      5D      �� 5F      -   5        $ LMessage��5J      $O��IAre you sure you want to delete this post ? This action cannot be undone.��5F      $��10(�5G      ���#000000$
��Wrap=�5I      5J      5K      �� 5M      ���#f7f7f7$��1��5N      $ 	0 0 10 10��5O      ._�5P      �  � ColumnDefinitions 5Q      �� 5R      $��* �6+   �� 5S      $��* �6+   .+�5U      �� -   5         ��Name$ BCancel��5b      $��0��5U      $��0��5V      $��10(�5W      ���#000000���#ffffff���#c2c2c2$��1q�5[      $

 Hand��5\      $��30��5]      $��Right=�5^      $
��20 0q�5_      $	��5 0q�5`      $	 Cancel��5a      .��5d      ��  �� Orientation$ 
Horizontal=�6%   .+�5e        $ Close=�66   $��14��5f   6   $��14��5g   6   ���#8c8c8a$��0 0 10 0q�5i   6   �� 5k      $��Annuler��6(   $��10(�5l   (   ���#0000005m   (   5n      5o      �� 5p      -   5        $ BConfirm��5|      $��10(�5p      ���#ffffff���#5335e9���#c2c2c2$��1q�5t      $

 Hand��5u      $��30��5v      $
��Left=�5w      $
��20 0q�5x      $	��5 0q�5y      $��1��5z      $	 Confirm��5{      .��5~      �� $ 
Horizontal=�6%   .+�5        -   5        $ IConfirm��5�   6   $ Check=�5   6   $��14��5�   6   $��14��5�   6   ���#ffffff$��0 0 10 0q�5�   6   5�   6   �� 5�      -	   5        $ Confirm��5�   (   $��	Confirmer��5�   (   $��10(�5�   (   ���#ffffff5�   (   5�      5�      5�      5�      5�      5�      5�      