using ConceptorUI.Models;
using ConceptorUI.Views.Component;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ConceptorUI.Classes;
using ConceptorUI.ViewModels;


namespace ConceptorUI
{
    public partial class MainWindow
    {
        private static MainWindow? _obj;
        private List<Project> _projects;
        private int _selectedProject;

        public MainWindow()
        {
            InitializeComponent();
            
            _obj = this;
            _selectedProject = 0;
            _projects = new List<Project>();

            ComponentButtons.PreMouseDownEvent += OnComponentButtonMouseClick!;
            ContentPages.PreviewKeyDown += OnKeyDown;
            ContentPages.PreviewMouseDown += OnMouseDown;

            ComponentButtons.PreMouseDownEvent += OnAddComponentHandle!;
            RightPanel.OnValueChangedEvent += OnSetPropertyHandle!;
        }

        private void OnComponentButtonMouseClick(object sender, EventArgs e)
        {
            Console.WriteLine(@"Button name: "+ sender);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.C when (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control:
                    PageView.OnCopyOrPaste();
                    Console.WriteLine("Ctrl + C is pressed.");
                    break;
                case Key.V when (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control:
                    PageView.OnCopyOrPaste(isPaste: true);
                    Console.WriteLine("Ctrl + V is pressed.");
                    break;
                case Key.S when (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control:
                    PageView.OnSaved(
                        Properties.Instance.SelectedLeftOnglet == 1 ? 0 : 1,
                        Properties.Instance.SelectedLeftOnglet == 1 ? Properties.Instance.SelectedReport : Properties.Instance.SelectedComponent
                    );
                    Console.WriteLine("Ctrl + S is pressed.");
                    break;
            }
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!e.OriginalSource.Equals(ContentPages) && !e.OriginalSource.Equals(Pages)) return;
            CompPage.Visibility = Visibility.Collapsed;
            DisplayColorPalette(Brushes.DodgerBlue, false, "FillColor");
        }

        public static MainWindow Instance => _obj != null! ? _obj : new MainWindow();

        public void Show(Applicat projectPath)
        {
            Show();
            
            if(projectPath == null!)
                _projects.Add(new Project
                {
                    Name = "Unknown Project",
                    Created = DateTime.Now.ToShortTimeString(),
                });
            
            PageView.Refresh();
        }

        public void DisplayColorPalette(Brush color, bool display, string propOrigin, double opacity = 1.0)
        {
            if (display)
            {
                ColorPalette.propOriginColor = propOrigin;
                ColorPalette.LoadColorValue(color, false, opacity);
            }
            else ColorPalette.IsOpened = false;
            ColorPalette.Visibility = display ? Visibility.Visible : Visibility.Collapsed;
        }

        public void DisplayTextPage(bool display)
        {
            if (display)
                TextPage.LoadText((PageView.Component as Component)!.GetGroupProperties(GroupNames.Text).GetValue(PropertyNames.Text));
            TextPage.Visibility = display ? Visibility.Visible : Visibility.Collapsed;
        }

        public void DisplayCompPage()
        {
            CompPage.Visibility = CompPage.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        public void DisplayPage(bool isReport = true)
        {
            PageView.Visibility = isReport ? Visibility.Visible : Visibility.Collapsed;
            PageComponent.Visibility = !isReport ? Visibility.Visible : Visibility.Collapsed;
        }

        public void OpenRightPanel(bool isStructuralView = false)
        {
            RightPanel.Visibility = !isStructuralView ? (RightPanel.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible) : Visibility.Collapsed;
            StructuralView.Visibility = isStructuralView ? (StructuralView.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible) : Visibility.Collapsed;
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button)!.Tag.ToString()!;
            switch (tag)
            {
                case "Save":
                    if(Properties.Instance.SelectedLeftOnglet == 1)
                        PageView.Instance.OnSaved(0, Properties.Instance.SelectedReport);
                    else if (Properties.Instance.SelectedLeftOnglet == 2)
                        PageView.Instance.OnSaved(1, Properties.Instance.SelectedComponent);
                    break;
                case "PDF":
                    break;
                case "ADD":
                    PageView.Instance.NewReport();
                    break;
                case "Trash":
                    PageView.Instance.DeleteReport();
                    break;
            }
        }

        private void OnStateChanged(object sender, EventArgs e)
        {
            var wind = sender as Window;
            Console.WriteLine(@"Window state: "+ wind!.WindowState);
            switch (wind.WindowState)
            {
                case WindowState.Maximized:
                    break;
                case WindowState.Minimized:
                    // if(ColorPicker.Instance.IsVisible)
                    //     ColorPicker.Instance.Hide();
                    break;
                case WindowState.Normal:
                    try
                    {
                        // if(!ColorPicker.Instance.IsVisible)
                        //     ColorPicker.Instance.Show();
                    }
                    catch (Exception)
                    {
                        //
                    }
                    break;
            }
        }
        
        private void OnAddComponentHandle(object sender, EventArgs e)
        {
            PageView.AddComponent(sender.ToString()!);
        }
        
        private void OnSetPropertyHandle(object sender, EventArgs e)
        {
            var infos = sender as dynamic[];
            PageView.SetProperty((GroupNames)infos![0], (PropertyNames)infos[1], infos[2]);
        }
    }
}
