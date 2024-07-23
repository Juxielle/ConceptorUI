using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;



namespace ConceptorUI
{
    /// <summary>
    /// Logique d'interaction pour ConfirmDialogBox.xaml
    /// </summary>
    public partial class ConfirmDialogBox : Window
    {
        private Action confirm;
        private Action cancel;
        public ConfirmDialogBox(Action confirm, Action cancel, string title, string message, int type = 0)
        {
            InitializeComponent();
            this.confirm = confirm;
            this.cancel = cancel;
            Refresh(title, message, type);
        }

        public void Refresh(string title, string message, int type = 0)
        {
            if(type == 0)
            {
                //Confirmation
                LTitle.Text = title;
                LMessage.Text = message;
                ContentIcon.Background = new BrushConverter().ConvertFrom("#e0e8ff") as SolidColorBrush;
                Icon.Foreground = new BrushConverter().ConvertFrom("#5335e9") as SolidColorBrush;
                Icon.Kind = PackIconKind.CheckCircleOutline;
                BConfirm.Background = new BrushConverter().ConvertFrom("#5335e9") as SolidColorBrush;
                IConfirm.Kind = PackIconKind.Check;
                Confirm.Text = "Confirmer";
                BCancel.Visibility = Visibility.Visible;
            }
            else if (type == 1)
            {
                //Information
                LTitle.Text = title;
                LMessage.Text = message;
                ContentIcon.Background = new BrushConverter().ConvertFrom("#d2f8eb") as SolidColorBrush;
                Icon.Foreground = new BrushConverter().ConvertFrom("#24c295") as SolidColorBrush;
                Icon.Kind = PackIconKind.InformationVariantCircleOutline;
                BConfirm.Background = new BrushConverter().ConvertFrom("#24c295") as SolidColorBrush;
                IConfirm.Kind = PackIconKind.Check;
                Confirm.Text = "Compris";
                BCancel.Visibility = Visibility.Hidden;
            }
            else if (type == 2)
            {
                //Danger
                LTitle.Text = title;
                LMessage.Text = message;
                ContentIcon.Background = new BrushConverter().ConvertFrom("#fceeee") as SolidColorBrush;
                Icon.Foreground = new BrushConverter().ConvertFrom("#d14343") as SolidColorBrush;
                Icon.Kind = PackIconKind.AlertCircleOutline;
                BConfirm.Background = new BrushConverter().ConvertFrom("#d14343") as SolidColorBrush;
                IConfirm.Kind = PackIconKind.TrashCanOutline;
                Confirm.Text = "Confirmer";
                BCancel.Visibility = Visibility.Visible;
            }
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            string tag = (sender as Button)!.Tag.ToString()!;
            var button = (sender as Button)!;
            switch (tag)
            {
                case "Close": this.Close(); break;
                case "Cancel": cancel(); this.Close(); break;
                case "Confirm": confirm(); this.Close(); break;
            }
        }
    }
}
