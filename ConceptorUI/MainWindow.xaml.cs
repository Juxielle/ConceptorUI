using ConceptorUI.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ConceptorUI.Classes;
using ConceptorUI.Enums;
using ConceptorUI.Inputs;
using ConceptorUI.Views.Modals;


namespace ConceptorUI
{
    public partial class MainWindow
    {
        private static MainWindow? _obj;
        private readonly List<Project> _projects;

        public MainWindow()
        {
            InitializeComponent();

            _obj = this;
            _projects = new List<Project>();

            ComponentButtons.PreMouseDownEvent += OnComponentButtonMouseClick!;
            ContentPages.PreviewKeyDown += OnKeyDown;
            ContentPages.PreviewMouseDown += OnMouseDown;

            ComponentButtons.PreMouseDownEvent += OnAddComponentHandle!;
            RightPanel.MouseDownCommand = new RelayCommand(OnSetPropertyHandle);

            PageView.RefreshPropertyPanelCommand = new RelayCommand(OnRefreshPropertyPanelHandle);
            PageView.DisplayTextTypingCommand = new RelayCommand(OnDisplayTextTypingHandle);

            TextTyping.Instance.TextChangedCommand = new RelayCommand(OnSetPropertyHandle);
        }

        private void OnComponentButtonMouseClick(object sender, EventArgs e)
        {
            Console.WriteLine(@"Button name: " + sender);
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
                        Properties.Instance.SelectedLeftOnglet == 1
                            ? Properties.Instance.SelectedReport
                            : Properties.Instance.SelectedComponent
                    );
                    Console.WriteLine("Ctrl + S is pressed.");
                    break;
            }
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!e.OriginalSource.Equals(Content) && !e.OriginalSource.Equals(ContentPages) &&
                !e.OriginalSource.Equals(Pages)) return;
            PageView.OnUnSelect();
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!e.OriginalSource.Equals(ContentPages) && !e.OriginalSource.Equals(Pages)) return;
            CompPage.Visibility = Visibility.Collapsed;
        }

        public static MainWindow Instance => _obj != null! ? _obj : new MainWindow();

        public void Show(object project)
        {
            Show();

            _projects.Add((Project)project);
            PageView.Refresh(_projects[0]);
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

        public void OpenRightPanel(RightPanelAction action)
        {
            switch (action)
            {
                case RightPanelAction.DisplayPropertyPanel:
                    StructuralView.Visibility = Visibility.Collapsed;
                    ComponentList.Visibility = Visibility.Collapsed;

                    RightPanel.Visibility = RightPanel.Visibility == Visibility.Collapsed
                        ? Visibility.Visible
                        : Visibility.Collapsed;
                    break;
                case RightPanelAction.DisplayStructuralView:
                    RightPanel.Visibility = Visibility.Collapsed;
                    ComponentList.Visibility = Visibility.Collapsed;

                    StructuralView.Visibility = StructuralView.Visibility == Visibility.Collapsed
                        ? Visibility.Visible
                        : Visibility.Collapsed;
                    break;
                case RightPanelAction.DisplayComponentPanel:
                    RightPanel.Visibility = Visibility.Collapsed;
                    StructuralView.Visibility = Visibility.Collapsed;

                    ComponentList.Visibility = ComponentList.Visibility == Visibility.Collapsed
                        ? Visibility.Visible
                        : Visibility.Collapsed;

                    if (ComponentList.Visibility == Visibility.Visible)
                    {
                        var components = PageView.SendComponent();
                        ComponentList.Refresh(components);
                    }
                    break;
            }
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button)!.Tag.ToString()!;
            switch (tag)
            {
                case "Save":
                    PageView.OnSaved(0, PageView.SelectedReport);
                    break;
                case "AddReport":
                    new ConfirmDialogBox(
                        "Confirmation",
                        "Confirmer la création de la page",
                        AlertType.Confirm,
                        () => PageView.NewReport()
                    ).ShowDialog();
                    break;
                case "AddComponent":
                    new ConfirmDialogBox(
                        "Confirmation",
                        "Confirmer la création du composant",
                        AlertType.Confirm,
                        () => PageView.NewReport(true)
                    ).ShowDialog();
                    break;
                case "Trash":
                    PageView.DeleteReport();
                    break;
            }
        }

        private void OnStateChanged(object sender, EventArgs e)
        {
            switch (WindowState)
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

        private void OnSetPropertyHandle(object sender)
        {
            var infos = sender as dynamic[];
            PageView.SetProperty((GroupNames)infos![0], (PropertyNames)infos[1], infos[2]);
        }

        private void OnRefreshPropertyPanelHandle(object sender)
        {
            var values = sender as Dictionary<string, dynamic>;

            RightPanel.FeedProps(values!["propertyGroups"], values["componentName"]);
        }

        private void OnDisplayTextTypingHandle(object sender)
        {
            var group = (sender as List<GroupProperties>)!.Find(g => g.Name == GroupNames.Text.ToString());
            var text = group!.GetValue(PropertyNames.Text);

            TextTyping.Instance.Refresh(text);
        }
    }
}