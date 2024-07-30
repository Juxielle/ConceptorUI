using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConceptorUI.Views.ComponentP
{
    /// <summary>
    /// Logique d'interaction pour NativeComponent.xaml
    /// </summary>
    public partial class NativeComponent : UserControl
    {
        public NativeComponent()
        {
            InitializeComponent();
            PIcon.Visibility = Visibility.Collapsed;
        }

        public void Refresh(string text, bool isIcon = true, PackIconKind iconKind = PackIconKind.ReceiptTextPlusOutline, string image = "text.png")
        {
            LBText.Text = text;
            PIcon.Kind = iconKind;
            PIcon.Visibility = isIcon ? Visibility.Visible : Visibility.Collapsed;
            IIcon.Visibility = !isIcon ? Visibility.Visible : Visibility.Collapsed;
            //Load image
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri($"/Assets/Composanticons/{image}", UriKind.Relative);
            bitmap.EndInit();
            IIcon.Source = bitmap;
            //End loading image
        }

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            BContent.BorderBrush = LBText.Foreground = PIcon.Foreground = new BrushConverter().ConvertFrom("#6739b7") as SolidColorBrush;
            BContent.Background = new BrushConverter().ConvertFrom("#eee6ff") as SolidColorBrush;
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            BContent.BorderBrush = LBText.Foreground = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
            PIcon.Foreground = new BrushConverter().ConvertFrom("#dfe2e7") as SolidColorBrush;
            BContent.Background = new BrushConverter().ConvertFrom("#ffffff") as SolidColorBrush;
        }
    }
}
