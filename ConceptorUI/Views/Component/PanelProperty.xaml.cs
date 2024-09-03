using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ConceptorUI.Models;
using System.Windows.Controls;
using System.Windows.Media;
using ConceptorUI.Interfaces;


namespace ConceptorUI.Views.Component
{
    partial class PanelProperty : IValue
    {
        private static PanelProperty? _obj;
        
        public event EventHandler? OnValueChangedEvent;
        private readonly object _valueChangedLock = new();
        
        public PanelProperty()
        {
            InitializeComponent();
            _obj = this;
            AlignSelf.Refresh(false);

            Global.Visibility = Align.Visibility = AlignSelf.Visibility = Transform.Visibility =
            Grid.Visibility = Text.Visibility = Appearance.Visibility = Shadow.Visibility = Visibility.Collapsed;
        }
        
        event EventHandler IValue.OnValueChanged
        {
            add
            {
                lock (_valueChangedLock)
                {
                    OnValueChangedEvent += value;
                }
            }
            remove
            {
                lock (_valueChangedLock)
                {
                    OnValueChangedEvent -= value;
                }
            }
        }

        public void FeedProps(object value, ComponentList componentName)
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
                    Align.PreMouseDownEvent -= OnValueChangedHandle!;
                    Align.PreMouseDownEvent += OnValueChangedHandle!;
                }
                else if (group.Name == GroupNames.SelfAlignment.ToString())
                {
                    AlignSelf.FeedProps(group);
                    AlignSelf.Visibility = Visibility.Visible;
                    AlignSelf.PreMouseDownEvent -= OnValueChangedHandle!;
                    AlignSelf.PreMouseDownEvent += OnValueChangedHandle!;
                }
                else if (group.Name == GroupNames.Transform.ToString())
                {
                    Transform.FeedProps(group);
                    Transform.Visibility = Visibility.Visible;
                    Transform.OnValueChangedEvent -= OnValueChangedHandle!;
                    Transform.OnValueChangedEvent += OnValueChangedHandle!;
                }
                else if (group.Name == GroupNames.Text.ToString())
                {
                    Text.FeedProps(group);
                    Text.Visibility = Visibility.Visible;
                    Text.OnValueChangedEvent -= OnValueChangedHandle!;
                    Text.OnValueChangedEvent += OnValueChangedHandle!;
                }
                else if (group.Name == GroupNames.Appearance.ToString())
                {
                    Appearance.FeedProps(group);
                    Appearance.Visibility = Visibility.Visible;
                    Appearance.PreMouseDownEvent -= OnValueChangedHandle!;
                    Appearance.PreMouseDownEvent += OnValueChangedHandle!;
                }
                else if (group.Name == GroupNames.GridProperty.ToString())
                {
                    Grid.FeedProps(group);
                    Grid.Visibility = Visibility.Visible;
                    //Grid.PreMouseDownEvent += OnValueChangedHandle!;
                }
                else if (group.Name == GroupNames.Global.ToString())
                {
                    Global.FeedProps(group, componentName);
                    Global.Visibility = Visibility.Visible;
                    Global.PreMouseDownEvent -= OnValueChangedHandle!;
                    Global.PreMouseDownEvent += OnValueChangedHandle!;
                }
                else if (group.Name == GroupNames.Shadow.ToString())
                {
                    Shadow.FeedProps(group);
                    Shadow.Visibility = Visibility.Visible;
                    Shadow.OnValueChangedEvent -= OnValueChangedHandle!;
                    Shadow.OnValueChangedEvent += OnValueChangedHandle!;
                }
            }
        }

        public static void ReactToProps(GroupNames groupName, PropertyNames propertyName, string value)
        {
            //PageView.Instance.SetProperty(groupName, propertyName, value);
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

        private void OnValueChangedHandle(object sender, EventArgs e)
        {
            OnValueChangedEvent!.Invoke(
                sender,
                EventArgs.Empty
            );
        }
    }
}
