using System;
using System.Windows;
using System.Windows.Controls;


namespace ConceptorUI.Views.Component
{
    public partial class TextPage
    {
        public TextPage()
        {
            InitializeComponent();
        }

        private void TextChanged(object sender, EventArgs e)
        {
            var text = (sender as TextBox)!.Text;

            if (textFormated == null) return;
            textFormated.Text = text;
            //PanelProperty.ReactToProps(GroupNames.Text, PropertyNames.Text, text);
        }

        private new void MouseDown(object sender, EventArgs e)
        {
            if (textFormated != null) textFormated.Text = "Mouse down event worked";
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button)!.Tag.ToString();
            
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
    }
}
