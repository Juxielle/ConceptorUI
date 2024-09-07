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
        public event EventHandler? OnValueChangedEvent;
        private readonly object _valueChangedLock = new();
        
        public PanelProperty()
        {
            InitializeComponent();
            // AlignSelf.Refresh(false);
            //
            // Global.Visibility = Align.Visibility = AlignSelf.Visibility = Transform.Visibility =
            // Grid.Visibility = Text.Visibility = Appearance.Visibility = Shadow.Visibility = Visibility.Collapsed;
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
            
            var groups = value as List<GroupProperties>;
            
            foreach (var group in groups!.Where(group => group.Visibility == VisibilityValue.Visible.ToString()))
            {
                if(group.Name == GroupNames.Alignment.ToString())
                {
                    alignment.FeedProps(group);
                    alignment.Visibility = Visibility.Visible;
                    alignment.PreMouseDownEvent += OnValueChangedHandle!;
                }
                else if (group.Name == GroupNames.SelfAlignment.ToString())
                {
                    selfAlignment.FeedProps(group);
                    selfAlignment.Visibility = Visibility.Visible;
                    selfAlignment.PreMouseDownEvent += OnValueChangedHandle!;
                }
                else if (group.Name == GroupNames.Transform.ToString())
                {
                    transform.FeedProps(group);
                    transform.Visibility = Visibility.Visible;
                    transform.OnValueChangedEvent += OnValueChangedHandle!;
                }
                else if (group.Name == GroupNames.Text.ToString())
                {
                    text.FeedProps(group);
                    text.Visibility = Visibility.Visible;
                    text.OnValueChangedEvent += OnValueChangedHandle!;
                }
                else if (group.Name == GroupNames.Appearance.ToString())
                {
                    appearance.FeedProps(group);
                    appearance.Visibility = Visibility.Visible;
                    
                    appearance.PreMouseDownEvent += OnValueChangedHandle!;
                }
                else if (group.Name == GroupNames.GridProperty.ToString())
                {
                    grid.FeedProps(group);
                    grid.Visibility = Visibility.Visible;
                    //Grid.PreMouseDownEvent += OnValueChangedHandle!;
                }
                else if (group.Name == GroupNames.Global.ToString())
                {
                    global.FeedProps(group, componentName);
                    global.Visibility = Visibility.Visible;
                    global.PreMouseDownEvent += OnValueChangedHandle!;
                }
                else if (group.Name == GroupNames.Shadow.ToString())
                {
                    shadow.FeedProps(group);
                    shadow.Visibility = Visibility.Visible;
                    shadow.OnValueChangedEvent += OnValueChangedHandle!;
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
            //Console.WriteLine($@"Event is sended here.");
            OnValueChangedEvent!.Invoke(
                sender,
                EventArgs.Empty
            );
        }
    }
}
