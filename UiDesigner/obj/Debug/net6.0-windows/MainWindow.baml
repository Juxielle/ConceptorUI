   M S B A M L   `   `   `  ����E  AUiDesigner, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:UiDesigner.Views.ComponentUiDesigner.Views.Component  5   	   O,clr-namespace:UiDesigner.Views.PropertyPanelUiDesigner.Views.PropertyPanel  5   	   C&clr-namespace:UiDesigner.Views.WidgetsUiDesigner.Views.Widgets  5	   	       UiDesigner.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=4.8.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   8	component(clr-namespace:UiDesigner.Views.Component  5   	   @propertyPanel,clr-namespace:UiDesigner.Views.PropertyPanel  5   	   4widgets&clr-namespace:UiDesigner.Views.Widgets  5	   	   -   5          � WindowStartupLocation$  CenterScreen=�5
       � Icon$ /Assets/icon2.png��5   	    � Title$ Ui Designer��5   	   $	��700��5   	   $
��1035��5   	   $	��700��5   	   $
��1035��5   	   ���#d2d2d2 � 	Resources 5       �� 
TargetType �� Property �� Value �� 
TargetType � ColumnDefinitions	 � RowDefinitions   BorderMediumColor
 �� Color �� CanContentScroll ��Name �� VerticalOffset � Value �� ScrollableHeight � Maximum �� ViewportHeight �� ViewportSize* �� #ComputedVerticalScrollBarVisibility �� Orientation �� HorizontalOffset �� ScrollableWidth �� ViewportWidth, �� %ComputedHorizontalScrollBarVisibility��%�  '��       �� 5   
   " ��6   .�5      ��  �@�OverridesDefaultStyle$
 True��5      �� 5       � �� 5      �� 5      " ��6&   .R�5      �  5      �� 5   "   $
��Auto �63   �� 5   "   	 5       �� 5!   "   �� 5"   "   $
��Auto �60   .+�5$      �� $��1��6%   $��0,1,1,1q�65   ��5%   "   �� 5&   &   #
 �   66   ._�5(   "   �� # z��69   5)      �� 5*      $ PART_VerticalScrollBar��6E   $��0��6(   $��0��65   # z 5+   (   # z 5,   (   # z 5-   (   #y�z 5.   (   �� 50      $ PART_HorizontalScrollBar��6(   $ 
Horizontal=�51   (   $��1��52   (   $��1��53   (   # z 54   (   # z 55   (   # z 56   (   #y�z 57   (   59      5:      5<      5=      5>      .��5@      � -   5         �Name$ Content��5@      ���#e4e4e4.+�5C   
   ]� .+�5D      *   $UiDesigner.Views.Component.LeftPanel  -   5          Name$ ComponentButtons��5G   "   $	��0 2q�5D   "   
�������$
��Left=�5F   "   5G   "   +   %UiDesigner.Views.Component.RightPanel  5I      $	��0 2q�6#   
�������$��Right=�5K   #   .   (UiDesigner.Views.Component.PanelProperty  5M      -   5          Name$ 
RightPanel��5R   &   $	��320��5M   &   $	��0 2q�5N   &   ���#d2d2d2$��Right=�5P   &   $y�	Collapsed=�5Q   &   5R   &   2   ,UiDesigner.Views.PropertyPanel.ComponentList  5T      -   5          Name$ ComponentList��5Y   *   $	��320��5T   *   $	��0 2q�5U   *   ���#d2d2d2$��Right=�5W   *   $y�	Collapsed=�5X   *   5Y   *   � 5[      -   5        $ ContentPages��5\      $��0 0 0 2q�5[      .+�5^      �� -   5         ��Name$ Pages��5`      $��Hidden=�5^      $��Hidden=�5_      .��5c      � .+�5d      )   #UiDesigner.Views.Component.PageView  -   5          Name$ PageView��5g   -   $
��2000��5d   -   $
��2000��5e   -   $y�Visible=�5f   -   5g   -   .   (UiDesigner.Views.Component.ComponentPage  5i      -	   5          Name$ PageComponent��5k   2   $��60q�5i   2   $y�	Collapsed=�5j   2   5k   2   5l      5m      �� 5o      .+�5p      �� $��10q�6!   $��50��5q   !   $
��Left=�5r   !   .+�5s      �� -
   5        
  � Tag$
  Save��5s   !   $��40��5t   !   $��40��5u   !   ! � Cursor$
! Hand��5v   !   ���#e1e1e1���#666666$	��0.4q�5z   !   $	��Top=�5{   !   $��Center=�5|   !   .��5}      '  !MaterialDesignThemes.Wpf.PackIcon  "   Kind$" ContentSave=�66   $��30��5~   6   $��30��5   6   ���#6666665�   6   5�      �� 5�      $��ENREGISTRER��6$   $��7(�5�   $   $��Center=�5�   $   ���#0000005�   $   5�      �� 5�      $��10q�6!   $��50��5�   !   $
��Left=�5�   !   .+�5�      �� -   5        ���#e1e1e1���#8c8c8a$	��0.4q�5�   !   $
! Hand��5�   !   $��40��5�   !   $��40��5�   !   $  	AddReport��5�   !   $��Center=�5�   !   $	��Top=�5�   !   .��5�        $" BookPlusOutline=�66   $��30��5�   6   $��30��5�   6   ���#6666665�   6   5�      �� 5�      $��AJOUTER PAGE��6$   $��7(�5�   $   $��Center=�5�   $   ���#0000005�   $   5�      �� 5�      $��10q�6!   $��50��5�   !   $
��Left=�5�   !   .+�5�      �� -   5        ���#e1e1e1���#8c8c8a$	��0.4q�5�   !   $
! Hand��5�   !   $��40��5�   !   $��40��5�   !   $  AddComponent��5�   !   $��Center=�5�   !   $	��Top=�5�   !   .��5�        $" ShapeRectanglePlus=�66   $��30��5�   6   $��30��5�   6   ���#6666665�   6   5�      �� 5�      $��AJOUTER COMPOSANT��6$   $��7(�5�   $   # �� TextAlignment$# Center=�5�   $   $
��Wrap=�5�   $   $��Center=�5�   $   ���#0000005�   $   5�      �� 5�      $��10q�6!   $��50��5�   !   $
��Left=�5�   !   .+�5�      �� -   5        ���#e1e1e1���#8c8c8a$	��0.4q�5�   !   $
! Hand��5�   !   $��40��5�   !   $��40��5�   !   $  RefreshComponent��5�   !   $��Center=�5�   !   $	��Top=�5�   !   .��5�        $" Refresh=�66   $��30��5�   6   $��30��5�   6   ���#6666665�   6   5�      �� 5�      $��
RAFRAICHIR��6$   $��7(�5�   $   $# Center=�5�   $   $
��Wrap=�5�   $   $��Center=�5�   $   ���#0000005�   $   5�      �� 5�      $��10q�6!   $��50��5�   !   $
��Left=�5�   !   .+�5�      �� -   5        ���#e1e1e1���#8c8c8a$	��0.4q�5�   !   $
! Hand��5�   !   $��40��5�   !   $��40��5�   !   $  OpenScreenModal��5�   !   $��Center=�5�   !   $	��Top=�5�   !   .��5�        $" FitToScreen=�66   $��30��5�   6   $��30��5�   6   ���#6666665�   6   5�      �� 5�      $��ECRANS��6$   $��7(�5�   $   $# Center=�5�   $   $
��Wrap=�5�   $   $��Center=�5�   $   ���#0000005�   $   5�      �� 5�      $��10q�6!   $��50��5�   !   $
��Left=�5�   !   .+�5�      �� -   5        ���#e1e1e1���#8c8c8a$	��0.4q�5�   !   $
! Hand��5�   !   $��40��5�   !   $��40��5�   !   $  Trash��5�   !   $��Center=�5�   !   $	��Top=�5�   !   .��5�        $" Delete=�66   $��30��5�   6   $��30��5�   6   ���#6666665�   6   5      �� 5     $��	SUPPRIMER��6$   $��7(�5  $   $��Center=�5  $   ���#0000005  $   5     *   $UiDesigner.Views.Widgets.ZoomButtons  5     $��10q�6*   $	��150��5  *   $
��Left=�5	  *   5
     .	   (UiDesigner.Views.Component.LeftCompPanel	  5     -   5        $ 	 Name$$ CompPage��5  *   $
��Left=�5  *   $��0q�5  *   $	��400��5  *   $y�	Collapsed=�5  *   5  *   7
   1UiDesigner.Views.PropertyPanel.PanelActionButtons
  5     $��50��63   $��Bottom=�5  3   5     5     � 5  
   -   5        % �Name$% Loading��5#     $	��100��5     $��4��5     & � IsIndeterminate$
& True��5     ���#5dacff���#1960ea$��	50 0 0 40q�5     $��0q�5     ' � Minimum$' 0Q�5     $	 100Q�5     $
��Left=�5      $��Bottom=�5!     $y�	Collapsed=�5"     5#     5$     5%     