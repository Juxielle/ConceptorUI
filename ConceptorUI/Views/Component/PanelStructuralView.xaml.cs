using ConceptorUi.ViewModels;
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

namespace ConceptorUI.Views.Component
{
    /// <summary>
    /// Logique d'interaction pour PanelStructuralView.xaml
    /// </summary>
    public partial class PanelStructuralView : UserControl
    {
        private static PanelStructuralView _obj;

        public PanelStructuralView()
        {
            InitializeComponent();
            _obj = this;
        }

        public static PanelStructuralView Instance
        {
            get { return _obj ?? new PanelStructuralView(); }
        }

        public void Refresh()
        {
            Property.Children.Clear();
            Property.Children.Add(StructuralView.Instance.Panel);
        }
    }
}
