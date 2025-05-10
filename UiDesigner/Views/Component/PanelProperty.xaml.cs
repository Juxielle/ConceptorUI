using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ConceptorUI.ViewModels.Components;
using ConceptorUI.ViewModels.Components.GroupProperty;
using UiDesigner.Inputs;
using UiDesigner.Models;
using UiDesigner.Views.Component;
using TransformGroup = ConceptorUI.ViewModels.Components.GroupProperty.TransformGroup;

namespace ConceptorUI.Views.Component
{
    partial class PanelProperty
    {
        public ICommand? MouseDownCommand;
        private List<GroupProperties>? _groupProperties;
        private List<GroupProperties>? _oldGroups;

        public PanelProperty()
        {
            InitializeComponent();

            InitGroups();
        }

        public void FeedProps(object value, ComponentList componentName)
        {
            var global = new GlobalProperty();
            var alignment = new AlignmentProperty();
            var selfAlignment = new AlignmentProperty();
            var transform = new TransformProperty();
            var text = new TextProperty();
            var appearance = new AppearanceProperty();
            var grid = new GridProperty();
            var shadow = new ShadowPanel();
            
            SForm.Children.Clear();
            SForm.Children.Add(global);
            SForm.Children.Add(alignment);
            SForm.Children.Add(selfAlignment);
            SForm.Children.Add(transform);
            SForm.Children.Add(text);
            SForm.Children.Add(appearance);
            SForm.Children.Add(grid);
            SForm.Children.Add(shadow);
            
            global.Visibility = alignment.Visibility = selfAlignment.Visibility = transform.Visibility = grid.Visibility =
                text.Visibility = appearance.Visibility = shadow.Visibility = Visibility.Collapsed;
            
            _oldGroups = value as List<GroupProperties>;
            SetPropertyValues(_oldGroups!);
            
            foreach (var group in _groupProperties!.Where(group => group.Visibility == VisibilityValue.Visible.ToString()))
            {
                if(group.Name == GroupNames.Alignment.ToString())
                {
                    alignment.Refresh(true);
                    alignment.FeedProps(group, componentName);
                    alignment.Visibility = Visibility.Visible;
                    alignment.MouseDownCommand = new RelayCommand(OnValueChangedHandle);
                }
                else if (group.Name == GroupNames.SelfAlignment.ToString())
                {
                    selfAlignment.Refresh(false);
                    selfAlignment.FeedProps(group, componentName);
                    selfAlignment.Visibility = Visibility.Visible;
                    selfAlignment.MouseDownCommand = new RelayCommand(OnValueChangedHandle);
                }
                else if (group.Name == GroupNames.Transform.ToString())
                {
                    transform.FeedProps(group);
                    transform.Visibility = Visibility.Visible;
                    transform.MouseDownCommand = new RelayCommand(OnValueChangedHandle);
                }
                else if (group.Name == GroupNames.Text.ToString())
                {
                    text.FeedProps(group);
                    text.Visibility = Visibility.Visible;
                    text.MouseDownCommand = new RelayCommand(OnValueChangedHandle);
                }
                else if (group.Name == GroupNames.Appearance.ToString())
                {
                    appearance.FeedProps(group);
                    appearance.Visibility = Visibility.Visible;
                    
                    appearance.MouseDownCommand = new RelayCommand(OnValueChangedHandle);
                }
                else if (group.Name == GroupNames.GridProperty.ToString())
                {
                    grid.FeedProps(group);
                    grid.Visibility = Visibility.Visible;
                    //Grid.MouseDownCommand = new RelayCommand(OnValueChangedHandle);
                }
                else if (group.Name == GroupNames.Global.ToString())
                {
                    global.FeedProps(group, componentName);
                    global.Visibility = Visibility.Visible;
                    global.MouseDownCommand = new RelayCommand(OnValueChangedHandle);
                }
                else if (group.Name == GroupNames.Shadow.ToString())
                {
                    shadow.FeedProps(group);
                    shadow.Visibility = Visibility.Visible;
                    shadow.MouseDownCommand = new RelayCommand(OnValueChangedHandle);
                }
            }
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

        private void OnValueChangedHandle(object sender)
        {
            MouseDownCommand?.Execute(sender);
            SetPropertyValues(_oldGroups!);
        }

        private void InitGroups()
        {
            _groupProperties =
            [
                new AlignmentGroup().GetContentAlignment(),
                new TransformGroup().GetTransformGroup(),
                new TextGroup().GetTextGroup(),
                new AppearanceGroup().GetAppearanceGroup(),
                new SelfAlignmentGroup().GetSelfAlignmentGroup(),
                new GridPropertyGroup().GetGridPropertyGroup(),
                new GlobalGroup().GetGlobalGroup(),
                new ShadowGroup().GetShadowGroup()
            ];
        }

        private void SetPropertyValues(List<GroupProperties> groups)
        {
            for (var i = 0; i < _groupProperties?.Count; i++)
            {
                if(i >= groups.Count) continue;
                
                if (ComponentHelper.IsMultiSelectionEnable)
                {
                    _groupProperties[i].Visibility =
                        _groupProperties[i].Visibility == VisibilityValue.Collapsed.ToString() ||
                        groups[i].Visibility == VisibilityValue.Collapsed.ToString()
                            ? VisibilityValue.Collapsed.ToString()
                            : VisibilityValue.Visible.ToString();
                }
                else
                {
                    _groupProperties[i].Visibility = $"{groups[i].Visibility}";
                }
                
                for (var j = 0; j < _groupProperties[i].Properties.Count; j++)
                {
                    if(j >= groups[i].Properties.Count) continue;

                    if (ComponentHelper.IsMultiSelectionEnable)
                    {
                        _groupProperties[i].Properties[j].Visibility =
                            _groupProperties[i].Properties[j].Visibility == VisibilityValue.Collapsed.ToString() ||
                            groups[i].Properties[j].Visibility == VisibilityValue.Collapsed.ToString()
                                ? VisibilityValue.Collapsed.ToString()
                                : VisibilityValue.Visible.ToString();
                    }
                    else
                    {
                        _groupProperties[i].Properties[j].Visibility = $"{groups[i].Properties[j].Visibility}";
                    }
                    
                    _groupProperties[i].Properties[j].Value = $"{groups[i].Properties[j].Value}";
                }
            }
        }
    }
}
