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
    /// Logique d'interaction pour ParentProperties.xaml
    /// </summary>
    public partial class ParentProperties : UserControl
    {
        static ParentProperties _obj;

        public ParentProperties()
        {
            InitializeComponent();
            _obj = this;
        }

        public static ParentProperties Instance
        {
            get { return _obj == null ? new ParentProperties() : _obj; }
        }

        public void FeedProps()
        {
            foreach (var prop in Properties.groupProps[0].Properties)
            {
                if (prop.Visibility == VisibilityValue.Visible.ToString())
                {
                    if (prop.Name == GroupNames.Alignment.ToString())
                    {

                    }
                    else if (prop.Name == GroupNames.Transform.ToString())
                    {

                    }
                }
            }
        }
    }
}
