using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ConceptorUI.Enums;


namespace ConceptorUI
{
    public partial class ConfirmDialogBox
    {
        private readonly Action _confirm;
        private readonly Action? _cancel;
        
        public ConfirmDialogBox(string title, string message, AlertType type, Action confirm, Action? cancel = null)
        {
            InitializeComponent();

            _confirm = confirm;
            _cancel = cancel;
            Refresh(title, message, type);
        }

        private void Refresh(string title, string message, AlertType type)
        {
            switch (type)
            {
                case AlertType.Confirm:
                    LTitle.Text = title;
                    LMessage.Text = message;
                    ContentIcon.Background = new BrushConverter().ConvertFrom("#e0e8ff") as SolidColorBrush;
                    Icon.Foreground = new BrushConverter().ConvertFrom("#5335e9") as SolidColorBrush;
                    Icon.Kind = PackIconKind.CheckCircleOutline;
                    BConfirm.Background = new BrushConverter().ConvertFrom("#5335e9") as SolidColorBrush;
                    IConfirm.Kind = PackIconKind.Check;
                    Confirm.Text = "Confirmer";
                    BCancel.Visibility = Visibility.Visible;
                    break;
                case AlertType.Info:
                    LTitle.Text = title;
                    LMessage.Text = message;
                    ContentIcon.Background = new BrushConverter().ConvertFrom("#d2f8eb") as SolidColorBrush;
                    Icon.Foreground = new BrushConverter().ConvertFrom("#24c295") as SolidColorBrush;
                    Icon.Kind = PackIconKind.InformationVariantCircleOutline;
                    BConfirm.Background = new BrushConverter().ConvertFrom("#24c295") as SolidColorBrush;
                    IConfirm.Kind = PackIconKind.Check;
                    Confirm.Text = "Compris";
                    BCancel.Visibility = Visibility.Hidden;
                    break;
                case AlertType.Error:
                    LTitle.Text = title;
                    LMessage.Text = message;
                    ContentIcon.Background = new BrushConverter().ConvertFrom("#fceeee") as SolidColorBrush;
                    Icon.Foreground = new BrushConverter().ConvertFrom("#d14343") as SolidColorBrush;
                    Icon.Kind = PackIconKind.AlertCircleOutline;
                    BConfirm.Background = new BrushConverter().ConvertFrom("#d14343") as SolidColorBrush;
                    IConfirm.Kind = PackIconKind.TrashCanOutline;
                    Confirm.Text = "Confirmer";
                    BCancel.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button)!.Tag.ToString()!;

            switch (tag)
            {
                case "Close":
                    Close();
                    break;
                case "Cancel":
                    _cancel?.Invoke();
                    Close();
                    break;
                case "Confirm":
                    _confirm();
                    Close();
                    break;
            }
        }
    }
}