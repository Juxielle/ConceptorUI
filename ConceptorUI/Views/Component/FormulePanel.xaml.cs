using ConceptorUI.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;




namespace ConceptorUI.Views.Component
{
    /// <summary>
    /// Logique d'interaction pour FormulePanel.xaml
    /// </summary>
    public partial class FormulePanel
    {
        private static FormulePanel? _obj;
        public FormulePanel()
        {
            InitializeComponent();
            _obj = this;
            Refresh();
        }

        public static FormulePanel Instance => _obj == null! ? new FormulePanel() : _obj;

        public void Refresh()
        {
            //TReportName.Text = Properties.Instance.SpaceReportModels[Properties.Instance.SelectedSpace][Properties.Instance.SelectedReport].Name!;
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button)!.Tag.ToString()!;
            switch (tag)
            {
                case "Save": PageView.Instance.OnSaved(0, Properties.Instance.SelectedReport); break;
                case "PDF":
                    Console.WriteLine(Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportMns[Properties.Instance.SelectedReport].
                        BuildReactComponent("", 0, "0").ToString());
                    break;
            }
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var tag = (sender as FrameworkElement)!.Tag.ToString()!;
            switch (tag)
            {
                case "ReportName":
                    var name = TReportName.Text;
                    if (name != string.Empty && name != Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportModels[Properties.Instance.SelectedReport].Name)
                    {
                        var code = Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportModels[Properties.Instance.SelectedReport].Code!;
                        var eds = Properties.Instance.ConfigAppInfo.Spaces[Properties.Instance.SelectedSpace].Reports!.Where(e => e.Code == code).ToList();
                        if (eds.Count > 0)
                        {
                            code = name;//EntityProps.Instance.GetCode(name);
                            Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportModels[Properties.Instance.SelectedReport].Name = name;
                            Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportModels[Properties.Instance.SelectedReport].Code = code;
                            var k = Properties.Instance.ConfigAppInfo.Spaces[Properties.Instance.SelectedSpace].Reports.IndexOf(eds[0]);
                            Properties.Instance.ConfigAppInfo.Spaces[Properties.Instance.SelectedSpace].Reports[k].Name = name;
                            Properties.Instance.ConfigAppInfo.Spaces[Properties.Instance.SelectedSpace].Reports[k].Code = code;
                            LeftCompPanel.Instance.SetItemInfos(1, k, name, code);
                            PageView.Instance.OnSaved(0, Properties.Instance.SelectedReport);
                            PageView.Instance.OnSaved(2);
                        }
                    }
                    break;
            }
        }
    }
}
