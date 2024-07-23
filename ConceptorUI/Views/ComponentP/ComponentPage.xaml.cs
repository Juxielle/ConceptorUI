using ConceptorUI.Models;
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
    /// Logique d'interaction pour ComponentPage.xaml
    /// </summary>
    public partial class ComponentPage : UserControl
    {
        private static ComponentPage _obj;

        public ComponentPage()
        {
            InitializeComponent();
            _obj = this;
        }

        public static ComponentPage Instance
        {
            get { return _obj == null ? new ComponentPage() : _obj; }
        }

        public void Refresh(bool added = true)
        {
            page.Children.Clear();
            page.Children.Add(Properties.Instance.ComponentMNS[Properties.Instance.SelectedComponent].ComponentView);
        }
    }
}
