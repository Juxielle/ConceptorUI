using ConceptorUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.Arm;
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
    /// Logique d'interaction pour TextPage.xaml
    /// </summary>
    public partial class TextPage : UserControl
    {
        static TextPage _obj;
        public TextPage()
        {
            InitializeComponent();
            _obj = this;
        }

        public static TextPage Instance
        {
            get { return _obj != null ? _obj : new TextPage(); }
        }

        private void TextChanged(object sender, EventArgs e)
        {
            string text = (sender as TextBox).Text;
            if (textFormated != null) { 
                textFormated.Text = text;
                PanelProperty.ReactToProps(GroupNames.Text, PropertyNames.Text, text);
            }
        }

        private void MouseDown(object sender, EventArgs e)
        {
            if (textFormated != null) textFormated.Text = "Mouse down event worked";
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            string tag = (sender as Button).Tag.ToString();
            switch (tag)
            {
                case "AddText":  break;
                case "Close": MainWindow.Instance.DisplayTextPage(false); break;
            }
        }

        public void LoadText(string value)
        {
            tb.Text = value;
        }

        public void EditText()
        {

        }

        public void RemoveText()
        {

        }

        public void setProperty(int idG, int idP, string value)
        {
            
        }
    }
}
