using ConceptorUI.Models;
using ConceptorUI.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;






namespace ConceptorUI.Views.Components
{
    /// <summary>
    /// Logique d'interaction pour PhoneScreen.xaml
    /// </summary>
    public partial class PhoneScreen : UserControl
    {
        private WindowModel window;
        private RowModel row;
        private bool _Selected;
        static PhoneScreen _obj;

        public PhoneScreen()
        {
            InitializeComponent();
            _obj = this;
            _Selected = true;
            //new ManageEnums();
            //new ConfiguredComps();
            window = new WindowModel();
            row = new RowModel();
            row.OnSelected();
            //screenContent.Children.Add(row.ComponentView);
        }
        
        public static PhoneScreen Instance
        {
            get { return _obj == null ? new PhoneScreen() : _obj; }
        }

        public void OnUnselected() {
            _Selected = false;
            screen.BorderBrush = Brushes.Transparent;
            screen.BorderThickness = new Thickness(0);
        }

        public void AddComponent(int id, ComponentList name)
        {
            //Properties.SetComponentList(id);
            //Properties.InitComps(name);
            //row.OnConfiguOut(id);
        }

        public void setProperty(int i, int j, string value)
        {
            //row.OnUpdated(i, j, value);
        }

        private void ScreenMouseDown(object sender, MouseButtonEventArgs e)
        {
            string tag = (sender as FrameworkElement).Tag.ToString();
            setFocus(tag);
        }

        private void setFocus(string tag)
        {
            screen.BorderBrush = status.BorderBrush = bottomNavSys.BorderBrush = Brushes.Transparent;
            screen.BorderThickness = status.BorderThickness = bottomNavSys.BorderThickness = new Thickness(0);
            switch (tag)
            {
                case "window": break;
                case "screen":
                    if (_Selected)
                    {
                        screen.BorderBrush = Brushes.DodgerBlue;
                        screen.BorderThickness = new Thickness(1);
                        //row.OnUnselected();
                    }
                    break;
                case "status":
                    status.BorderBrush = Brushes.DodgerBlue;
                    status.BorderThickness = new Thickness(1); break;
                case "bottomNavSys":
                    bottomNavSys.BorderBrush = Brushes.DodgerBlue;
                    bottomNavSys.BorderThickness = new Thickness(1); break;
            }
            _Selected = true;
        }
    }
}
