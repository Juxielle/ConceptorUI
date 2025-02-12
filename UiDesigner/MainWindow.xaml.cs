using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ConceptorUI.Enums;
using ConceptorUI.Senders;
using ConceptorUI.ViewModels.Components;
using UiDesigner.Application.Dto.UiDto;
using UiDesigner.Enums;
using UiDesigner.Inputs;
using UiDesigner.Models;
using UiDesigner.Views.Modals;

namespace ConceptorUI
{
    public partial class MainWindow
    {
        private static MainWindow? _obj;
        private readonly List<ProjectInfoUiDto> _projects;
        private bool _isHorizontalScroll;
        private bool _allowMove;

        public MainWindow()
        {
            InitializeComponent();

            _obj = this;
            _projects = new List<ProjectInfoUiDto>();
            _allowMove = true;
            _isHorizontalScroll = false;
            Pages.Focus();

            ComponentButtons.OnPreMouseDownEvent += OnComponentButtonMouseClick!;
            ContentPages.PreviewKeyDown += OnKeyDown;
            ContentPages.PreviewKeyUp += OnKeyUp;
            ContentPages.PreviewMouseDown += OnMouseDown;

            ComponentButtons.OnPreMouseDownEvent += OnAddComponentHandle!;
            RightPanel.MouseDownCommand = new RelayCommand(OnSetPropertyHandle);

            PageView.RefreshPropertyPanelCommand = new RelayCommand(OnRefreshPropertyPanelHandle);
            PageView.DisplayTextTypingCommand = new RelayCommand(OnDisplayTextTypingHandle);
            PageView.DisplayLoadingCommand = new RelayCommand(OnDisplayLoadingHandle);

            TextTyping.Instance.TextChangedCommand = new RelayCommand(OnSetPropertyHandle);

            ComponentList.SendComponentCommand = new RelayCommand(OnSendComponentHandle);
            PanelActionCommand = new RelayCommand(OnExecuteAction);
        }
        
        public ICommand PanelActionCommand { get; }

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
                    PageView.OnSaved();
                    Console.WriteLine("Ctrl + S is pressed.");
                    break;
                case Key.Z when (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control:
                    PageView.GetTransferData(nameof(PageView), new PropertySender
                    {
                        SenderAction = SenderAction.CancelAction
                    });
                    Console.WriteLine("Ctrl + Z is pressed.");
                    break;
                case Key.Y when (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control:
                    PageView.GetTransferData(nameof(PageView), new PropertySender
                    {
                        SenderAction = SenderAction.RestoreAction
                    });
                    Console.WriteLine("Ctrl + Y is pressed.");
                    break;
                case Key.LeftShift:
                    _allowMove = false;
                    _isHorizontalScroll = true;
                    Pages.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
                    Console.WriteLine("LeftShift is pressed.");
                    break;
                case Key.RightShift:
                    _allowMove = false;
                    _isHorizontalScroll = true;
                    Pages.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
                    Console.WriteLine("RightShift is pressed.");
                    break;
                case Key.LeftCtrl:
                    ComponentHelper.IsMultiSelectionEnable = true;
                    Console.WriteLine("LeftCtrl is pressed.");
                    break;
                case Key.RightCtrl:
                    ComponentHelper.IsMultiSelectionEnable = true;
                    Console.WriteLine("RightCtrl is pressed.");
                    break;
            }
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (_isHorizontalScroll)
                Pages.ScrollToHorizontalOffset(Pages.HorizontalOffset - e.Delta);
            else Pages.ScrollToVerticalOffset(Pages.VerticalOffset - e.Delta);
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key is Key.LeftShift or Key.RightShift)
            {
                _isHorizontalScroll = false;
                Pages.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            }
            else if (e.Key is Key.LeftCtrl or Key.RightCtrl)
            {
                ComponentHelper.IsMultiSelectionEnable = false;
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
            
            var projectInfo = (ProjectInfoUiDto)project;
            Title = $"Ui Designer -- {projectInfo.Name}";
            
            _projects.Add(projectInfo);
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
                    //StructuralView.Visibility = Visibility.Collapsed;
                    ComponentList.Visibility = Visibility.Collapsed;

                    RightPanel.Visibility = RightPanel.Visibility == Visibility.Collapsed
                        ? Visibility.Visible
                        : Visibility.Collapsed;
                    break;
                case RightPanelAction.DisplayStructuralView:
                    RightPanel.Visibility = Visibility.Collapsed;
                    ComponentList.Visibility = Visibility.Collapsed;

                    // StructuralView.Visibility = StructuralView.Visibility == Visibility.Collapsed
                    //     ? Visibility.Visible
                    //     : Visibility.Collapsed;
                    break;
                case RightPanelAction.DisplayComponentPanel:
                    RightPanel.Visibility = Visibility.Collapsed;
                    //StructuralView.Visibility = Visibility.Collapsed;

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

        private void OnAddComponentHandle(object sender, EventArgs e)
        {
            PageView.AddComponent(sender.ToString()!);
        }

        private void OnSetPropertyHandle(object sender)
        {
            var infos = sender as dynamic[];
            var property = (PropertyNames)infos![1];

            if (property == PropertyNames.Add)
                PageView.AddComponent(UiDesigner.Models.ComponentList.TextSingle.ToString());
            else PageView.SetProperty((GroupNames)infos[0], (PropertyNames)infos[1], infos[2]);
        }

        private void OnRefreshPropertyPanelHandle(object sender)
        {
            var values = sender as Dictionary<string, dynamic>;
            List<GroupProperties> groups;
            if (((ComponentList)values!["componentName"]) == UiDesigner.Models.ComponentList.Text)
                groups = ((List<List<GroupProperties>>)values["propertyGroups"])[0];
            else groups = (List<GroupProperties>)values["propertyGroups"];

            RightPanel.FeedProps(groups, values["componentName"]);
        }

        private void OnDisplayTextTypingHandle(object sender)
        {
            TextTyping.Instance.Refresh(sender);
        }

        private void OnSendComponentHandle(object sender)
        {
            PageView.AddReusableComponent(sender.ToString()!);
        }

        private void OnExecuteAction(object sender)
        {
            PropertySender propertySender;
            if (sender is PropertySender pSender) propertySender = pSender;
            else
            {
                var senderAction = (SenderAction)Enum.Parse(typeof(SenderAction), $"{sender}");
                propertySender = new PropertySender
                {
                    SenderAction = senderAction,
                    Value = $"{sender}"
                };
            }
                
            PageView.GetTransferData(nameof(PageView), propertySender);
        }

        private void OnDisplayLoadingHandle(object sender)
        {
            Loading.Visibility = Loading.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}