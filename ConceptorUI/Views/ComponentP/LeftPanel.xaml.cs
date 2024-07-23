using ConceptorUI.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;


namespace ConceptorUI.Views.ComponentP
{
    /// <summary>
    /// Logique d'interaction pour LeftPanel.xaml
    /// </summary>
    public partial class LeftPanel
    {
        public LeftPanel()
        {
            InitializeComponent();
        }

        private void BtnClick(object sender, MouseButtonEventArgs e)
        {
            var tag = (sender as FrameworkElement)!.Tag.ToString()!;
            switch (tag)
            {
                case "Menu":
                    //PageView.Instance.OnSaved();
                    //PageView.Instance.OnDuplicated();
                    MainWindow.Instance.DisplayCompPage(); //Process P = Process.Start(@"C:\Program Files\Sublime Text 3\sublime_text.exe");
                    break;
                case "Text":
                    PageView.Instance.AddComponent(0, ComponentList.TextSingle); break;
                case "Grid":
                    PageView.Instance.AddComponent(0, ComponentList.Grid); break;
                case "Table":
                    PageView.Instance.AddComponent(0, ComponentList.Table); break;
                case "Row":
                    PageView.Instance.AddComponent(0, ComponentList.Row); break;
                case "Column":
                    PageView.Instance.AddComponent(0, ComponentList.Column); break;
                case "Container":
                    PageView.Instance.AddComponent(0, ComponentList.Container); break;
                case "Stack":
                    PageView.Instance.AddComponent(0, ComponentList.Stack); break;
                case "ListV":
                    PageView.Instance.AddComponent(0, ComponentList.ListV); break;
                case "ListH":
                    PageView.Instance.AddComponent(0, ComponentList.ListH); break;
                case "Image":
                    PageView.Instance.AddComponent(0, ComponentList.Image); break;
                case "Icon":
                    PageView.Instance.AddComponent(0, ComponentList.Icon); break;
                case "Shape":
                    /*PageView.Instance.OnDuplicated();*/ break;
                case "Setting":
                    PageView.Instance.AddComponent(0, ComponentList.Table); break;
            }
        }

        private void BtnMouseEnter(object sender, MouseEventArgs e)
        {
            var border = sender as Border;
            var packIcon = border!.Child as PackIcon;
            border.BorderBrush = packIcon!.Foreground = new BrushConverter().ConvertFrom("#6739b7") as SolidColorBrush;
        }

        private void BtnMouseLeave(object sender, MouseEventArgs e)
        {
            var border = sender as Border;
            var packIcon = border!.Child as PackIcon;
            border.BorderBrush = packIcon!.Foreground = new BrushConverter().ConvertFrom("#666666") as SolidColorBrush;
        }

    }

}
