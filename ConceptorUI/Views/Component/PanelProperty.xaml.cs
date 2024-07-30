using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ConceptorUI.Models;
using System.Windows.Controls;
using System.Windows.Media;


namespace ConceptorUI.Views.ComponentP
{
    partial class PanelProperty
    {
        private static PanelProperty? _obj;
        
        public PanelProperty()
        {
            InitializeComponent();
            _obj = this;
            AlignSelf.Refresh(false);

            Global.Visibility = Align.Visibility = AlignSelf.Visibility = Transform.Visibility =
            Grid.Visibility = Text.Visibility = Appearance.Visibility = Shadow.Visibility = Visibility.Collapsed;
        }

        public static PanelProperty Instance => _obj == null! ? new PanelProperty() : _obj;

        public void FeedProps(object value)
        {
            Global.Visibility = Align.Visibility = AlignSelf.Visibility = Transform.Visibility = Grid.Visibility =
            Text.Visibility = Appearance.Visibility = Shadow.Visibility = Visibility.Collapsed;
            var groups = value as List<GroupProperties>;
            
            foreach (var group in groups!.Where(group => group.Visibility == VisibilityValue.Visible.ToString()))
            {
                if(group.Name == GroupNames.Alignment.ToString())
                {
                    Align.FeedProps(group);
                    Align.Visibility = Visibility.Visible;
                }
                else if (group.Name == GroupNames.SelfAlignment.ToString())
                {
                    AlignSelf.FeedProps(group);
                    AlignSelf.Visibility = Visibility.Visible;
                }
                else if (group.Name == GroupNames.Transform.ToString())
                {
                    TransformProperty.Instance.FeedProps(group);
                    Transform.Visibility = Visibility.Visible;
                }
                else if (group.Name == GroupNames.Text.ToString())
                {
                    TextProperty.Instance.FeedProps();
                    Text.Visibility = Visibility.Visible;
                }
                else if (group.Name == GroupNames.Appearance.ToString())
                {
                    AppearanceProperty.Instance.FeedProps(group);
                    Appearance.Visibility = Visibility.Visible;
                }
                else if (group.Name == GroupNames.GridProperty.ToString())
                {
                    GridProperty.Instance.FeedProps(group);
                    Grid.Visibility = Visibility.Visible;
                }
                else if (group.Name == GroupNames.Global.ToString())
                {
                    GlobalProperty.Instance.FeedProps(group);
                    Global.Visibility = Visibility.Visible;
                }
                else if (group.Name == GroupNames.Shadow.ToString())
                {
                    ShadowPanel.Instance.FeedProps(group);
                    Shadow.Visibility = Visibility.Visible;
                }
            }
        }

        public static void ReactToProps(GroupNames groupName, PropertyNames propertyName, string value)
        {
            PageView.Instance.setProperty(groupName, propertyName, value);
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button)!.Tag.ToString()!;
            switch (tag)
            {
                case "Entity": SetPanel(); break;
                case "Form": SetPanel(false); break;
            }
        }

        private void SetPanel(bool showEntity = true)
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
