   M S B A M L   `   `   `  ����E  AUiDesigner, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:UiDesigner.BindingUiDesigner.Binding  5   	   )    #UiDesigner.Views.Modals.ScreenModal   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   4binding clr-namespace:UiDesigner.Binding     5   	     � Title$  Liste des écrans��5   	    � WindowStartupLocation$ CenterScreen=�5	   	    � Icon$ /Assets/icon2.png��5
   	   $	��600��5   	   $	��900��5   	    � 	Resources 5      -   'UiDesigner.Binding.ScreenImageConverter   ScreenImageConverter �� 
TargetType  	CardStyle �� Property �� Value �� Triggers P� Property	 P� Value��%7  &	        &	        5   
   6'   �� 5   
   " ��6   .�5      ��  � �� �#ffffff6+    5      P� 5       �@J�IsMouseOver$
	 True��61   . �5      ��  � �� �#e4e4e463   �� 5       � �� �#e4e4e464   �� 5       � ��$ 1q�68   5      5      5      .��5      � 
�������
 � RowDefinitions
 5   
   �� 5      $
��Auto �6   �� 5       $��* �6   .+�5#   
   �� $��0��6   $��40��5$      ���#e4e4e4 �� Padding$
 20 0q�5&      ._�5'      �� $��Liste des écrans��6   $��20(�5(      $
��Bold"�5)      $��Center=�5*      5+      �� 5-   
   $��1��6   $
��Auto=�5.      $��Disabled=�5/      $��20q�50      .��51      �� -   5         ��Name$ Items��51      ��52      �� 53      .R�54      � .+�55      �� -   5        $	��250��55   %   $	��400��56   %   $��10q�57   %   
������ $��0q�59   %   
������ $��	0 0 20 20q�5;   %   
 � Tag 5<   %   �� *Label+.��5B   "   �� $��Uniform=�5C   (   $��Center=�5D   (   ��5B   (   �� *Label+ �� 	Converter# [  5D   (   5F      5G      5H      ��5J      �� 5K      .R�5L      � 5M      5O      5P      5Q      5R      