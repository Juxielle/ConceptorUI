using System.Windows;
using System.Windows.Input;

namespace ConceptorUI.Views.Modals;

public partial class ComponentPropertyConfig
{
    public ComponentPropertyConfig()
    {
        InitializeComponent();
    }

    private void OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        var tag = ((FrameworkElement)sender).Tag.ToString();

        switch (tag)
        {
            case "Close":
                Hide();
                break;
        }
    }
}