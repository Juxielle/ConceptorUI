using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ConceptorUI.Interfaces;
using ConceptorUI.Models;
using MaterialDesignThemes.Wpf;


namespace ConceptorUI.Views.Component
{
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
                    componentName = ComponentList.TextSingle.ToString();
                    break;
                case "Grid":
                    componentName = ComponentList.Grid.ToString();
                    break;
                case "Row":
                    componentName = ComponentList.Row.ToString();
                    break;
                case "Column":
                    componentName = ComponentList.Column.ToString();
                    break;
                case "Container":
                    componentName = ComponentList.Container.ToString();
                    break;
                case "Stack":
                    componentName = ComponentList.Stack.ToString();
                    break;
                case "ListV":
                    componentName = ComponentList.ListV.ToString();
                    break;
                case "ListH":
                    componentName = ComponentList.ListH.ToString();
                    break;
                case "Image":
                    componentName = ComponentList.Image.ToString();
                    break;
                case "Icon":
                    componentName = ComponentList.Icon.ToString();
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
