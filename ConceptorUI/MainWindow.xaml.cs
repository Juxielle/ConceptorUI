using ConceptorUI.Models;
using ConceptorUI.Views.ComponentP;
using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ConceptorUI.Views.Modals;


namespace ConceptorUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        private static MainWindow? _obj;
        private readonly HttpClient _httpClient = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();
            _obj = this;

            ComponentButtons.PreMouseDownEvent += OnComponentButtonMouseClick!;
            ContentPages.PreviewKeyDown += OnKeyDown;
            ContentPages.PreviewMouseDown += OnMouseDown;
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
                    pageView.OnCopyOrPaste();
                    Console.WriteLine("Ctrl + C is pressed.");
                    break;
                case Key.V when (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control:
                    pageView.OnCopyOrPaste(isPaste: true);
                    Console.WriteLine("Ctrl + V is pressed.");
                    break;
                case Key.S when (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control:
                    pageView.OnSaved(
                        Properties.Instance.SelectedLeftOnglet == 1 ? 0 : 1,
                        Properties.Instance.SelectedLeftOnglet == 1 ? Properties.Instance.SelectedReport : Properties.Instance.SelectedComponent
                    );
                    Console.WriteLine("Ctrl + S is pressed.");
                    break;
            }
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource.Equals(ContentPages) || e.OriginalSource.Equals(Pages))
            {
                compPage.Visibility = Visibility.Collapsed;
                DisplayColorPalette(Brushes.DodgerBlue, false, "FillColor");
            }
        }

        public async void TestMethod()
        {
            var stringData = await _httpClient.GetStringAsync("http://127.0.0.1:8000/api/users-list");
            Console.WriteLine("Datas: " + stringData);
        }

        public static MainWindow Instance
        {
            get { return _obj != null ? _obj : new MainWindow(); }
        }

        public void Show(Applicat app)
        {
            Show();
            pageView.application = app;
        }

        public void FillFontComboBox(ComboBox comboBoxFonts)
        {
            foreach (FontFamily fontFamily in Fonts.SystemFontFamilies)
            {
                //comboBoxFonts.Items.Add(fontFamily.Source);
            }
            //comboBoxFonts.SelectedIndex = 0;
        }

        public void DisplayColorPalette(Brush color, bool display, string propOrigin, double opacity = 1.0)
        {
            if (display)
            {
                colorPalette.propOriginColor = propOrigin;
                colorPalette.LoadColorValue(color, false, opacity);
            }
            else colorPalette.IsOpened = false;
            colorPalette.Visibility = display ? Visibility.Visible : Visibility.Collapsed;
        }

        public void DisplayTextPage(bool display)
        {
            if (display) textPage.LoadText(Properties.groupProps![2].Properties[19].Value);
            textPage.Visibility = display ? Visibility.Visible : Visibility.Collapsed;
        }

        public void DisplayCompPage()
        {
            compPage.Visibility = compPage.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        public void DisplayPage(bool isReport = true)
        {
            formulePanel.Visibility = Visibility.Collapsed;
            pageView.Visibility = isReport ? Visibility.Visible : Visibility.Collapsed;
            pageComponent.Visibility = !isReport ? Visibility.Visible : Visibility.Collapsed;
        }

        public void OpenRightPanel(bool isStructuralView = false)
        {
            RightPanel.Visibility = !isStructuralView ? (RightPanel.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible) : Visibility.Collapsed;
            structuralView.Visibility = isStructuralView ? (structuralView.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible) : Visibility.Collapsed;
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
                    Console.WriteLine(Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportMns[Properties.Instance.SelectedReport].
                        BuildReactComponent("", 0, "0").ToString());
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
            switch (wind!.WindowState)
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
    }
}
