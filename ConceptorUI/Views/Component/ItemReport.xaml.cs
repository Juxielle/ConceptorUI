using ConceptorUI.Models;
using MaterialDesignThemes.Wpf;
using System;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;



namespace ConceptorUI.Views.ComponentP
{
    /// <summary>
    /// Logique d'interaction pour ItemReport.xaml
    /// </summary>
    public partial class ItemReport
    {
        private string _code;
        private int _index;
        private static ItemReport? _obj;
        public ItemReport()
        {
            InitializeComponent();
            _obj = this;
            _code = string.Empty;
            _index = 0;
        }
        public static ItemReport Instance => _obj == null! ? new ItemReport() : _obj;

        public void Refresh(
            string code,
            string number,
            string name,
            DateTime date,
            bool isReport = true)
        {
            this._code = code;
            _index = int.Parse(number) - 1;
            Number.Text = number;
            IconReport.Kind = PackIconKind.FileCsv;
            Type.Text = isReport ? "Report type" : "Entity type";
            Name.Text = name;
            Date.Text = date.ToShortDateString();//EntityProps.Instance.FormatedDate(date.ToString(), "j Ms, aaaa");
        }

        public void SetItemForm(bool actived = true)
        {
            Content.Background = new BrushConverter().ConvertFrom(actived ? "#f4f6fa" : "#ffffff") as SolidColorBrush;
            Number.Foreground = new BrushConverter().ConvertFrom(actived ? "#000000" : "#c2c2c2") as SolidColorBrush;
            IconReport.Foreground = new BrushConverter().ConvertFrom(actived ? "#1960ea" : "#c2c2c2") as SolidColorBrush;
            Name.Foreground = new BrushConverter().ConvertFrom(actived ? "#000000" : "#c2c2c2") as SolidColorBrush;
            Type.Foreground = new BrushConverter().ConvertFrom(actived ? "#000000" : "#c2c2c2") as SolidColorBrush;
            DateLabel.Foreground = new BrushConverter().ConvertFrom(actived ? "#000000" : "#c2c2c2") as SolidColorBrush;
            Date.Foreground = new BrushConverter().ConvertFrom(actived ? "#000000" : "#c2c2c2") as SolidColorBrush;
            ContentPen.Background = new BrushConverter().ConvertFrom(actived ? "#ffffff" : "#f4f6fa") as SolidColorBrush;
            ContentTrash.Background = new BrushConverter().ConvertFrom(actived ? "#ffffff" : "#f4f6fa") as SolidColorBrush;
            Pen.Foreground = new BrushConverter().ConvertFrom(actived ? "#1960ea" : "#c2c2c2") as SolidColorBrush;
            Trash.Foreground = new BrushConverter().ConvertFrom(actived ? "#1960ea" : "#c2c2c2") as SolidColorBrush;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            LoadData();
        }

        public void LoadData(string code = "", bool withBascule = true, bool withDisplay = true)
        {
            if(code == "") code = _code;
            Console.WriteLine(@"Space code: "+ code);
            switch (Properties.Instance.SelectedLeftOnglet)
            {
                case 1:
                {
                    var spaces = Properties.Instance.SpaceReports.Where(e => e.Code == code).ToList();
                    Console.WriteLine(@"Spaces count: "+ spaces.Count);
                    if (spaces.Count == 0)
                    {
                        var index = Properties.Instance.ConfigAppInfo.Spaces.FindIndex(s => s.Code == code);
                        Properties.Instance.SelectedSpace = Properties.Instance.SpaceReports.Count;
                        PageView.Instance.LoadSpace(index);
                        Console.WriteLine(@"Selected space: "+ Properties.Instance.SelectedSpace);
                        //PageView.Instance.Refresh();
                        if (withBascule) LeftCompPanel.Instance.ActiveItem(_index, 1);
                        FormulePanel.Instance.Refresh();
                    }
                    else
                    {
                        Console.WriteLine(@"Space code2: "+ spaces[0].Code);
                        if (withDisplay) Properties.Instance.SelectedSpace = Properties.Instance.ConfigAppInfo.Spaces.FindIndex(s => s.Code == code);
                        Console.WriteLine(@"Selected space: "+ Properties.Instance.SelectedSpace);
                        PageView.Instance.Refresh();
                        if (withBascule) LeftCompPanel.Instance.ActiveItem(_index, 1);
                        FormulePanel.Instance.Refresh();
                    }
                    break;
                }
                case 2:
                {
                    var components = Properties.Instance.Components.Where(e => e.Code == code).ToList();
                    if (components.Count == 0)
                    {
                        PageView.Instance.OnLoaded(code, 1);
                        Properties.Instance.SelectedComponent = Properties.Instance.Components.Count - 1;
                        ComponentPage.Instance.Refresh();
                        if (withBascule) LeftCompPanel.Instance.ActiveItem(_index, 2);
                        FormulePanel.Instance.Refresh();
                    }
                    else
                    {
                        if (withDisplay) Properties.Instance.SelectedComponent = Properties.Instance.Components.IndexOf(components[0]);
                        ComponentPage.Instance.Refresh();
                        if (withBascule) LeftCompPanel.Instance.ActiveItem(_index, 2);
                        FormulePanel.Instance.Refresh();
                    }
                    break;
                }
            }
        }
    }
}
