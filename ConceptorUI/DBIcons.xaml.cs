using ConceptorUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using ConceptorUI.Constants;
using FontAwesome.WPF;
using MaterialDesignThemes.Wpf;
using Syncfusion.UI.Xaml.Controls.DataPager;


namespace ConceptorUI
{
    public partial class DbIcons
    {
        private readonly Action<string> _action;
        private string _selectedIcon;
        private int _selectedTab;

        private bool _allowChangingTab;
        private List<Icon> _icons;
        
        private const int PageSize = 200;
        //public string IconPath = $"{Env.dirEnv}/Icons/MaterialIcons-Regular.ttf#Material Icons";

        public DbIcons(Action<string> action)
        {
            _allowChangingTab = false;
            InitializeComponent();
            _action = action;
            _selectedTab = 0;
            _icons = new List<Icon>();
            DataContext = this;

            // var listIcons = Enum.GetValues(typeof(FontAwesomeIcon)).Cast<FontAwesomeIcon>();
            // var packIcons = new IconPack
            // {
            //     Label = "FontAwesome",
            //     Version = "v7.4.47",
            //     Icons = new List<Icon>()
            // };
            //
            // foreach (var icon in listIcons)
            // {
            //     var found = false;
            //     foreach (var iconExist in packIcons.Icons)
            //     {
            //         if (iconExist.Code != icon.ToString()) continue;
            //         found = true;
            //         break;
            //     }
            //
            //     if (found) continue;
            //     packIcons.Icons.Add(new Icon
            //     {
            //         Name = icon.ToString(),
            //         Code = icon.ToString()
            //     });
            // }
            //
            // var jsonString = System.Text.Json.JsonSerializer.Serialize(packIcons);
            // File.WriteAllText(
            //     $"{Env.dirEnv}/Icons/fontawesome.json",
            //     jsonString
            // );

            if (Properties.Instance.IconPacks.Count == 0)
            {
                Properties.Instance.IconPacks.Add(new IconPack());
                Properties.Instance.IconPacks.Add(new IconPack());
                Properties.Instance.IconPacks.Add(new IconPack());
            }
            _allowChangingTab = true;
            Tabs.SelectedIndex = 0;
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

            if (Properties.Instance.IconPacks[_selectedTab].Icons == null!)
            {
                var iconPack = System.Text.Json.JsonSerializer.Deserialize<IconPack>(
                    File.ReadAllText($"{Env.dirEnv}/Icons/{fileName}")
                );
                Properties.Instance.IconPacks[_selectedTab] = iconPack!;
            }

            _icons = Properties.Instance.IconPacks[_selectedTab].Icons;
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
                    if(_selectedIcon != string.Empty)
                    {
                        var package = _selectedTab == 0 ? "Material" : "FontAwesome";
                        var sendValue = new[] { _selectedIcon, package };
                        _action(System.Text.Json.JsonSerializer.Serialize(sendValue));
                    }
                    _selectedIcon = string.Empty;
                    Close();
                    break;
                case "Close":
                    Close();
                    break;
            }
        }

        private void OnMouseDownIcon(object sender, MouseButtonEventArgs e)
        {
            var tag = (sender as FrameworkElement)!.Tag.ToString()!;
            _selectedIcon = tag;
            
            var myDataObject = new Icon{Code = tag};
            var myBinding = new Binding("Code");
            myBinding.Source = myDataObject;

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
                    BindingOperations.SetBinding(GooglePreview, TextBlock.TextProperty, myBinding);
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
            _icons = Properties.Instance.IconPacks[_selectedTab].Icons;

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
