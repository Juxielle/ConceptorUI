using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace ConceptorUI.Views.Component
{
    /// <summary>
    /// Logique d'interaction pour PopupView.xaml
    /// </summary>
    public partial class PopupView : UserControl
    {
        public bool loading;
        public PopupView()
        {
            InitializeComponent();
            loading = true;
            DoWork();
        }

        public void Refresh(PopupModel pm)
        {
            if (pm.Display == true)
            {
                Indication.Visibility = RIndication.Visibility = pm.Loading == true ? Visibility.Visible : Visibility.Collapsed;
                RIndication.Fill = pm.Color;
                Close.Foreground = pm.Color;
                Popup.Background = pm.BackColor;
                Message.Foreground = pm.Color;
                Message.Text = pm.Message;
                DoWork();
            }
            else
            {
                loading = false;
            }
        }

        private void DoWork()
        {
            var sc = SynchronizationContext.Current;
            ThreadPool.QueueUserWorkItem(delegate
            {
                while (loading)
                {
                    sc!.Post(delegate
                    {
                        RIndication.StartAngle += 4;
                        RIndication.EndAngle += 4;
                    }, null);
                    Thread.Sleep(10);
                }
            });
        }

        public void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            loading = false;
            PreviewPage.Instance.HidePopup(true);
        }
    }

    public class PopupModel
    {
        public Brush? BackColor { get; set; } = new BrushConverter().ConvertFrom("#1a8b3c") as SolidColorBrush;
        public Brush? Color { get; set; } = new BrushConverter().ConvertFrom("#ffffff") as SolidColorBrush;
        public string? Message { get; set; }
        public bool? Display { get; set; } = false;
        public bool? Loading { get; set; } = true;
    }
}
