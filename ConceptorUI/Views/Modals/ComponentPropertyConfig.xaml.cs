using System.Windows;
using System.Windows.Input;

namespace ConceptorUI.Views.Modals;

public partial class ComponentPropertyConfig
{
    private static ComponentPropertyConfig? _obj;

    public ComponentPropertyConfig()
    {
        InitializeComponent();
        _obj = this;
    }

    public static ComponentPropertyConfig Instance => _obj ?? new ComponentPropertyConfig();

    public void Refresh()
    {
        Show();
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

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        DragMove();
    }
}