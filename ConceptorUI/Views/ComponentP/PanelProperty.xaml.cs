using System;
using System.Windows;
using ConceptorUI.Models;
using System.Windows.Controls;
using System.Windows.Media;



namespace ConceptorUI.Views.ComponentP
{
    /// <summary>
    /// Logique d'interaction pour PanelProperty.xaml
    /// </summary>
    partial class PanelProperty
    {
        private static PanelProperty? _obj;
        
        public PanelProperty()
        {
            InitializeComponent();
            _obj = this;
            AlignSelf.Refresh(false);
            Properties.InitProps();

            Global.Visibility = Align.Visibility = AlignSelf.Visibility = Transform.Visibility =
            Grid.Visibility = Text.Visibility = Appearance.Visibility = Shadow.Visibility = Visibility.Collapsed;
        }

        public static PanelProperty Instance => _obj == null! ? new PanelProperty() : _obj;

        public void FeedProps()
        {
            Global.Visibility = Align.Visibility = AlignSelf.Visibility = Transform.Visibility = Grid.Visibility =
            Text.Visibility = Appearance.Visibility = Shadow.Visibility = Visibility.Collapsed;
            foreach(var groupe in Properties.groupProps!)
            {
                if (groupe.Visibility != VisibilityValue.Visible.ToString()) continue;
                if(groupe.Name == GroupNames.Alignment.ToString())
                {
                    Align.FeedProps(); Align.Visibility = Visibility.Visible;
                }
                else if (groupe.Name == GroupNames.SelfAlignment.ToString())
                {
                    AlignSelf.FeedProps(); AlignSelf.Visibility = Visibility.Visible;
                }
                else if (groupe.Name == GroupNames.Transform.ToString())
                {
                    TransformProperty.Instance.FeedProps(); Transform.Visibility = Visibility.Visible;
                }
                else if (groupe.Name == GroupNames.Text.ToString())
                {
                    TextProperty.Instance.FeedProps(); Text.Visibility = Visibility.Visible;
                }
                else if (groupe.Name == GroupNames.Appearance.ToString())
                {
                    AppearanceProperty.Instance.FeedProps(); Appearance.Visibility = Visibility.Visible;
                }
                else if (groupe.Name == GroupNames.GridProperty.ToString())
                {
                    GridProperty.Instance.FeedProps(); Grid.Visibility = Visibility.Visible;
                }
                else if (groupe.Name == GroupNames.Global.ToString())
                {
                    GlobalProperty.Instance.FeedProps(); Global.Visibility = Visibility.Visible;
                }
                else if (groupe.Name == GroupNames.Shadow.ToString())
                {
                    ShadowPanel.Instance.FeedProps(); Shadow.Visibility = Visibility.Visible;
                }
            }
        }

        public void ReactToProps(GroupNames groupName, PropertyNames propertyName, string value)
        {
            //if(i == 2) TextPage.Instance.setProperty(i, j, value);
            //else PageView.Instance.setProperty(i, j, value);
            PageView.Instance.setProperty(groupName, propertyName, value);
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button)!.Tag.ToString()!;
            var button = (sender as Button)!;
            switch (tag)
            {
                case "Entity": SetPanel(true); break;
                case "Form": SetPanel(false); break;
            }
        }

        public void SetPanel(bool showEntity = true)
        {
            if (showEntity)
            {
                SForm.Visibility = Visibility.Collapsed;
                SEntity.Visibility = Visibility.Visible;
                BEntity.Background = new BrushConverter().ConvertFrom("#f4f6f8") as SolidColorBrush;
                BEntity.Foreground = new BrushConverter().ConvertFrom("#1960ea") as SolidColorBrush;
                BForm.Background = Brushes.Transparent;
                BForm.Foreground = new BrushConverter().ConvertFrom("#000000") as SolidColorBrush;
            }
            else
            {
                SForm.Visibility = Visibility.Visible;
                SEntity.Visibility = Visibility.Collapsed;
                BForm.Background = new BrushConverter().ConvertFrom("#f4f6f8") as SolidColorBrush;
                BForm.Foreground = new BrushConverter().ConvertFrom("#1960ea") as SolidColorBrush;
                BEntity.Background = Brushes.Transparent;
                BEntity.Foreground = new BrushConverter().ConvertFrom("#000000") as SolidColorBrush;
            }
        }
    }
}
