﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using UiDesigner.Assets.GoogleFontIcons;
using UiDesigner.Constants;
using FontAwesome.WPF;
using MaterialDesignThemes.Wpf;
using Syncfusion.UI.Xaml.Controls.DataPager;
using UiDesigner.Interfaces;
using UiDesigner.Models;


namespace UiDesigner
{
    public partial class DbIcons : IValue
    {
        private string _selectedIcon;
        private int _selectedTab;
        private int _clickCount;

        private bool _allowChangingTab;
        private List<Icon> _icons;

        private const int PageSize = 200;
        private static List<IconPack>? _iconPacks;
        //public string IconPath = $"{Env.dirEnv}/Icons/MaterialIcons-Regular.ttf#Material Icons";

        public event EventHandler? OnValueChangedEvent;
        private readonly object _valueChangedLock = new();

        public DbIcons()
        {
            _allowChangingTab = false;
            InitializeComponent();

            _selectedTab = 0;
            _clickCount = 0;
            _selectedIcon = string.Empty;
            _icons = new List<Icon>();
            DataContext = this;

            var listIcons = Enum.GetValues(typeof(FontAwesomeIcon)).Cast<FontAwesomeIcon>();
            var packIcons = new IconPack
            {
                Label = "FontAwesome",
                Version = "v7.4.47",
                Icons = []
            };

            foreach (var icon in listIcons)
            {
                var found = packIcons.Icons.Any(iconExist => iconExist.Code == icon.ToString());

                if (found) continue;
                packIcons.Icons.Add(new Icon
                {
                    Name = icon.ToString(),
                    Code = icon.ToString()
                });
            }

            var jsonString = System.Text.Json.JsonSerializer.Serialize(packIcons);
            File.WriteAllText(
                $"{Env.DirEnv}/Icons/fontawesome.json",
                jsonString
            );

            if (_iconPacks == null || _iconPacks.Count == 0)
            {
                _iconPacks = new List<IconPack>();
                _iconPacks =
                [
                    new IconPack(),
                    new IconPack(),
                    new IconPack()
                ];
            }

            _allowChangingTab = true;
            Tabs.SelectedIndex = 0;
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

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_allowChangingTab)
            {
                _allowChangingTab = true;
                return;
            }

            var index = (sender as TabControl)!.SelectedIndex;
            var found = false;
            var fileName = string.Empty;
            var items = Items;

            switch (index)
            {
                case 0:
                    _selectedTab = index;
                    found = true;
                    fileName = "material_icons.json";
                    break;
                case 1:
                    _selectedTab = index;
                    found = true;
                    fileName = "fontawesome.json";
                    items = FontAwesonItems;
                    break;
                case 2:
                    _selectedTab = index;
                    found = true;
                    fileName = "google_font_icons.json";
                    items = GoogleIconItems;
                    break;
            }

            if (!found) return;

            if (_iconPacks![_selectedTab].Icons == null!)
            {
                if (_selectedTab != 2)
                {
                    var iconPack = System.Text.Json.JsonSerializer.Deserialize<IconPack>(
                        File.ReadAllText($"{Env.DirEnv}/Icons/{fileName}")
                    );
                    _iconPacks[_selectedTab] = iconPack!;
                }
                else
                {
                    _iconPacks[_selectedTab] = GoogleFontIcons.GetIconPack();
                }
            }

            _icons = _iconPacks[_selectedTab].Icons;
            var count = _icons.Count >= PageSize ? PageSize : _icons.Count;
            var icons = _icons.GetRange(0, count);

            items.ItemsSource = icons;
            DataPager.Source = _icons;
            DataPager.PageIndex = 0;
            _selectedIcon = string.Empty;
            TbSearch.Text = string.Empty;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var tag = (sender as FrameworkElement)!.Tag.ToString()!;
            switch (tag)
            {
                case "Select":
                    OnIconSelected();
                    break;
                case "Close":
                    Close();
                    break;
            }
        }

        private void OnMouseDownIcon(object sender, MouseButtonEventArgs e)
        {
            var tag = (sender as FrameworkElement)!.Tag.ToString()!;

            var myDataObject = new Icon { Code = tag };
            var myBinding = new System.Windows.Data.Binding("Code")
            {
                Source = myDataObject
            };

            if (_selectedIcon == tag && _clickCount >= 1)
                OnIconSelected();
            else if (_clickCount >= 1)
                _clickCount = 0;
            _clickCount++;
            _selectedIcon = tag;

            switch (_selectedTab)
            {
                case 0:
                    FontAwesomePreview.Visibility = GooglePreview.Visibility = Visibility.Collapsed;
                    MaterialPreview.Visibility = Visibility.Visible;
                    BindingOperations.SetBinding(MaterialPreview, PackIcon.KindProperty, myBinding);
                    break;
                case 1:
                    MaterialPreview.Visibility = GooglePreview.Visibility = Visibility.Collapsed;
                    FontAwesomePreview.Visibility = Visibility.Visible;
                    BindingOperations.SetBinding(FontAwesomePreview, ImageAwesome.IconProperty, myBinding);
                    break;
                case 2:
                    MaterialPreview.Visibility = FontAwesomePreview.Visibility = Visibility.Collapsed;
                    GooglePreview.Visibility = Visibility.Visible;
                    BindingOperations.SetBinding(GooglePreview, GoogleFontIcon.IconNameProperty, myBinding);
                    break;
            }
        }

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            var itemIcon = (sender as StackPanel)!;
            itemIcon.Background = Brushes.AliceBlue;
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            var itemIcon = (sender as StackPanel)!;
            itemIcon.Background = Brushes.Transparent;
        }

        private void OnPageIndexChanged(object sender, PageIndexChangedEventArgs e)
        {
            _loadPageData();
        }

        private void OnSearch(object sender, RoutedEventArgs e)
        {
            var text = (sender as TextBox)!.Text;

            //if(text.Length < 3) return;
            _icons = _iconPacks![_selectedTab].Icons;

            if (text.Length != 0)
            {
                var icons = new List<Icon>();
                foreach (var icon in _icons)
                {
                    if (!icon.Code.ToLower().Contains(text.ToLower())) continue;
                    icons.Add(icon);
                }

                _icons = icons;
            }

            DataPager.PageIndex = 0;
            _loadPageData();
        }

        private void OnIconSelected()
        {
            if (_selectedIcon != string.Empty)
            {
                var package = _selectedTab switch
                {
                    0 => "Material",
                    1 => "FontAwesome",
                    _ => "GoogleFontIcons"
                };
                var sendValue = new[] { _selectedIcon, package };

                OnValueChangedEvent!.Invoke(
                    System.Text.Json.JsonSerializer.Serialize(sendValue),
                    EventArgs.Empty
                );
            }

            _selectedIcon = string.Empty;
            Close();
        }

        private void _loadPageData()
        {
            var items = Items;
            switch (_selectedTab)
            {
                case 0:
                    break;
                case 1:
                    items = FontAwesonItems;
                    break;
                case 2:
                    items = GoogleIconItems;
                    break;
            }

            items.ItemsSource = null;
            var selectedIndex = DataPager.PageIndex;
            var index = selectedIndex * PageSize;
            var last = _icons.Count - index - PageSize;
            var count = last > PageSize ? PageSize : PageSize + last;

            items.ItemsSource = _icons.GetRange(index, count);
            DataPager.Source = _icons;
        }
    }
}