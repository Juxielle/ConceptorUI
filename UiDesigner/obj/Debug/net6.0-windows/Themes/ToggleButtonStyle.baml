   M S B A M L   `   `   `  ����W  SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089_ [MaterialDesignThemes.Wpf, Version=4.8.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a   RbDarkTemplate  �� 
TargetType �� Property �� Value  RadioButtonFocusVisual �� 
TargetType �� 
Background �� Bullet A�Name �� EndPoint �� 
StartPoint  BorderLightColor	 � Color
 � Offset  BorderDarkColor  CustomGrayLightColor ��Name'   !MaterialDesignThemes.Wpf.PackIcon    Kind  CustomPrimaryBrush'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard �� 
TargetName �� TargetProperty8   2System.Windows.Media.Animation.EasingColorKeyFrame �� KeyTime �� Value R� KeyTime   System.Windows.Visibility   Visible R� Value �� Triggers P� Property P� Value �� 
TargetName�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      :x,http://schemas.microsoft.com/winfx/2006/xaml      5      FmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      %%
  &	        �� 5      "  d�6#   .�5   
   ��  �@J�SnapsToDevicePixels$
 true��60   �� 5   
    �@�OverridesDefaultStyle$
 true��62   �� 5	   
    �@�FocusVisualStyle# �  6-   �� 5
   
    � �� 5      �� 5      " d�6"   .R�5      �� 
 ����  5      � 5      $��20��6#   $��20��6.   .+�5   "   A� $ Border��6*   $��1��6:   ��5   &   �� 5   *   $ 0.5,1&�6>   $ 0.5,0&�6O   .��5   .   � #	 �  6;   $
 0Q�6f   � 5   .   #	 �  6;   $
 1Q�6e   5   +   ��5   &   �� 5   *   $	 0,0&�6>   $	 0,1&�6O   ��5   2   � � 5   6   #	 �  6C   � 5   6   #	 �  6C   $	
 1.0Q�6r   5   3   5   +   5!   #   �� 5"   "   $ 	CheckMark��6)   $y�	Collapsed=�6<   $��Center=�6S   $��Center=�6p   ._�5#   &      $ Check=�6>   $��15��6K   $��15��6V   #���  6b   5$   #   5%       5'        5(      CommonStates��. 5)   "     
Normal��6.     5*   "   	MouseOver��. 5+   &   �� .��5,   *   �� $ Border��6G   $H B(Shape.Fill).(GradientBrush.GradientStops)[1].(GradientStop.Color)�5-   -   .g�5.   .     $ 0��6B   # �  6N   5/   +   50   '   51   #     52   "   Pressed��. 53   &   �� .��54   *   �� $ Border��6G   $H B(Shape.Fill).(GradientBrush.GradientStops)[1].(GradientStop.Color)�55   -   .g�56   .     $ 0��6B   # �  6N   57   +   58   '   59   #     5:   "   Disabled��. 5;   &   �� .��5<   *   �� $ Border��6G   $H B(Shape.Fill).(GradientBrush.GradientStops)[1].(GradientStop.Color)�5=   -   .g�5>   .     $ 0��6B   # �  6N   5?   +   �� 5@   *   $ Border��6G   $J D(Shape.Stroke).(GradientBrush.GradientStops)[1].(GradientStop.Color)�5A   -   .g�5B   .     $ 0��6B   $ 	#40000000��6N   5C   +   �� 5D   *   $ Border��6G   $J D(Shape.Stroke).(GradientBrush.GradientStops)[0].(GradientStop.Color)�5E   -   .g�5F   .     $ 0��6B   $ 	#40000000��6N   5G   +   5H   '   5I   #   5J        5K      CheckStates��. 5L   "     Checked��. 5M   &   �� .��5N   *   T� $ (UIElement.Visibility)�6H   $ 	CheckMark��5O   .   ..�5P   .   i� $ 0��6E   # Z 6Q   5Q   +   5R   '   5S   #     5T   "   	Unchecked��6.     5U   "   Indeterminate��6.   5V      ._�5X      �� $��4,0,0,0q�6+   $��Center=�5Y      $
��Left=�5Z      $
��True��5[      5\       5]      P� 5^       �@d�	IsChecked$
 TrueY�67   . �5_      �� $ 	CheckMark��6;    � y�$ Visible=�6R   5`      5b      5d      5e      5g      