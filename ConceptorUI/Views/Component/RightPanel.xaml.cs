using System.Windows.Controls;
using System.Windows.Input;
using ConceptorUI.Enums;


namespace ConceptorUI.Views.Component
{
    public partial class RightPanel
    {
        public RightPanel()
        {
            InitializeComponent();
        }

        private void BtnClick(object sender, MouseButtonEventArgs e)
        {
            var tag = (sender as Border)!.Tag.ToString()!;
            switch (tag)
            {
                case "Props":
                    MainWindow.Instance.OpenRightPanel(RightPanelAction.DisplayPropertyPanel);
                    break;
                case "StructView":
                    //MainWindow.Instance.OpenRightPanel(RightPanelAction.DisplayStructuralView);
                    break;
                case "Component":
                    MainWindow.Instance.OpenRightPanel(RightPanelAction.DisplayComponentPanel);
                    break;
            }
        }

        private void BtnMouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void BtnMouseLeave(object sender, MouseEventArgs e)
        {

        }
    }
}
