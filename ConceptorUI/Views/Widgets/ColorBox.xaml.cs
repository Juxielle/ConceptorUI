using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ConceptorUI.Inputs;
using ConceptorUI.Models;
using ConceptorUI.Views.Modals;

namespace ConceptorUI.Views.Widgets;

public partial class ColorBox
{
    public ICommand? MouseDownCommand;

    public ColorBox()
    {
        InitializeComponent();
    }

    private void BtnClick(object sender, RoutedEventArgs e)
    {
        var colorPicker = new ColorPicker(BFillColor.Background, 1);
        colorPicker.OpacityChangedCommand = new RelayCommand((opacity) =>
        {
            MouseDownCommand?.Execute(
                new dynamic[] { GroupNames.Appearance, PropertyNames.Opacity, opacity.ToString()! }
            );
        });

        colorPicker.ColorSelectedCommand = new RelayCommand((color) =>
        {
            MouseDownCommand?.Execute(
                new dynamic[] { GroupNames.Appearance, PropertyNames.FillColor, color.ToString()! }
            );

            BFillColor.Background = new BrushConverter().ConvertFrom(color!.ToString()!) as SolidColorBrush;
        });
        colorPicker.Show();
    }
}