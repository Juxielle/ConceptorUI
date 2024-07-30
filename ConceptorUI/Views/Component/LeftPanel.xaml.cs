using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ConceptorUI.Interfaces;
using ConceptorUI.ViewModels;
using MaterialDesignThemes.Wpf;


namespace ConceptorUI.Views.ComponentP
{
    /// <summary>
    /// Logique d'interaction pour LeftPanel.xaml
    /// </summary>
    public partial class LeftPanel : IComponentButton
    {
        public event EventHandler? PreMouseDownEvent;
        private readonly object _mouseDownLock = new();
        
        public LeftPanel()
        {
            InitializeComponent();
        }
        
        event EventHandler IComponentButton.OnMouseDown
        {
            add
            {
                lock (_mouseDownLock)
                {
                    PreMouseDownEvent += value;
                }
            }
            remove
            {
                lock (_mouseDownLock)
                {
                    PreMouseDownEvent -= value;
                }
            }
        }

        private void BtnClick(object sender, MouseButtonEventArgs e)
        {
            var tag = (sender as FrameworkElement)!.Tag.ToString()!;
            var componentName = string.Empty;
            
            switch (tag)
            {
                case "Menu":
                    componentName = "Menu";
                    break;
                case "Text":
                    componentName = nameof(TextSingleModel);
                    break;
                case "Grid":
                    componentName = nameof(GridModel);
                    break;
                case "Table":
                    componentName = nameof(TableModel);
                    break;
                case "Row":
                    componentName = nameof(RowModel);
                    break;
                case "Column":
                    componentName = nameof(ColumnModel);
                    break;
                case "Container":
                    componentName = nameof(ContainerModel);
                    break;
                case "Stack":
                    componentName = nameof(StackModel);
                    break;
                case "ListV":
                    componentName = nameof(ListVModel);
                    break;
                case "ListH":
                    componentName = nameof(ListHModel);
                    break;
                case "Image":
                    componentName = nameof(ImageModel);
                    break;
                case "Icon":
                    componentName = nameof(IconModel);
                    break;
                case "Shape":
                    componentName = string.Empty;
                    break;
                case "Setting":
                    componentName = string.Empty;
                    break;
            }
            
            PreMouseDownEvent!.Invoke(componentName, EventArgs.Empty);
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
