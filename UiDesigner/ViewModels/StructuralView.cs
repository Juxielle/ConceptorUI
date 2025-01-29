using UiDesigner.Models;
using UiDesigner.Views.Component;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ConceptorUI.Views.Component;


namespace ConceptorUi.ViewModels
{
    internal class StructuralView
    {
        public StructuralElement StructuralElement { get; set; }
        public StackPanel Panel { get; set; }

        private const int Gap = 20;
        private static StructuralView? _obj;

        public StructuralView()
        {
            StructuralElement = new StructuralElement();
            Panel = new StackPanel();
            _obj = this;
        }

        public static StructuralView Instance => _obj ?? new StructuralView();

        public void BuildView(StructuralElement structuralElement, int gap, bool isSimpleElement = false)
        {
            /* Title */
            var title = new TextBlock
            {
                Text = structuralElement.Node,
                Foreground = Brushes.Black,
                FontWeight = FontWeights.Light,
                FontSize = 12,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(4, 0, 0, 0)
            };
            title.PreviewMouseDown += OnMouseDown;
            /* End Title */
            /* Icon */
            var contentIcon = new Border {
                Background = structuralElement.IsSimpleElement ? Brushes.Transparent : 
                    new BrushConverter().ConvertFrom("#f0f0f0") as SolidColorBrush,
                Cursor = Cursors.Hand,
                CornerRadius = new CornerRadius(5),
                Child = new PackIcon
                {
                    Kind = structuralElement.IsSimpleElement ? PackIconKind.File : PackIconKind.Plus,
                    Width = 15,
                    Height = 15,
                    Foreground = Brushes.DodgerBlue,
                    Margin = new Thickness(2)
                }
            };

            var stack = new StackPanel
            {
                Orientation = Orientation.Horizontal,
            };
            stack.Children.Add(contentIcon);
            stack.Children.Add(title);

            var itemView = new Border
            {
                Margin = new Thickness(gap, 10, 0, 0),
                Background = !structuralElement.Selected ? Brushes.White : 
                        new BrushConverter().ConvertFrom("#f0f0f0") as SolidColorBrush,
                BorderBrush = Brushes.Transparent,
                BorderThickness = new Thickness(!structuralElement.Selected ? 0 : .5),
                CornerRadius = new CornerRadius(2),
                Child = stack
            };
            itemView.PreviewMouseDown += OnMouseDown;

            Panel.Children.Add(itemView);
            /* End Icon */

            if (structuralElement.Children == null!) return;
            
            foreach (var child in structuralElement.Children)
                BuildView(child, (gap + Gap), child.IsSimpleElement);
        }
        
        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectItemView(StructuralElement, 0, e);
            PageView.Instance.SelectFromStructuralView();
        }

        private void SelectItemView(StructuralElement structuralElement, int index, MouseButtonEventArgs e)
        {
            var border = (Panel.Children[index] as Border);
            var textBlock = ((Panel.Children[index] as Border)!.Child as StackPanel)!.Children[1];

            if (structuralElement.Selected)
            {
                (Panel.Children[index] as Border)!.Background = Brushes.White;
                (Panel.Children[index] as Border)!.BorderThickness = new Thickness(0);
                structuralElement.Selected = false;
            }

            if (e.Source.Equals(border) || e.Source.Equals(textBlock))
            {
                (Panel.Children[index] as Border)!.Background = new BrushConverter().ConvertFrom("#c9e3fc") as SolidColorBrush;
                (Panel.Children[index] as Border)!.BorderThickness = new Thickness(1);
                structuralElement.Selected = true;
            }

            if (structuralElement.Children == null!) return;
            var size = index + 1;
            for (var i = 0; i < structuralElement.Children.Count; i++)
            {
                SelectItemView(structuralElement.Children[i], size, e);
                size += structuralElement.Children[i].Count();
            }
        }

    } 
}
