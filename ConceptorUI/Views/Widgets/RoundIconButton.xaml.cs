using System.Windows;
using MaterialDesignThemes.Wpf;

namespace ConceptorUI.Views.Widgets
{
    public partial class RoundIconButton
    {
        public RoundIconButton()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(nameof(Icon), typeof(PackIconKind), typeof(RoundIconButton),
                new PropertyMetadata(PackIconKind.HeartOutline));
        
        public PackIconKind Icon
        {
            get => (PackIconKind)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }
    }
}