using System;
using System.Collections.Generic;
using ConceptorUI.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ConceptorUI.Classes;
using ConceptorUI.Interfaces;


namespace ConceptorUI.Views.Component
{
    public partial class LeftCompPanel : ISpace
    {
        private static LeftCompPanel? _obj;
        private List<Space> _spaces;
        
        public event EventHandler? OnAddSpaceEvent;
        public event EventHandler? OnSelectSpaceEvent;
        private readonly object _addSpaceLock = new();
        private readonly object _selectSpaceLock = new();
        
        public LeftCompPanel()
        {
            InitializeComponent();
            
            _obj = this;
            _spaces = new List<Space>();
            
            SContentComponent.Visibility = SContentReport.Visibility = SPersonnel.Visibility = Visibility.Collapsed;
            SContentReport.Visibility = Visibility.Visible;
            
            Refresh();
        }

        public static LeftCompPanel Instance => _obj == null! ? new LeftCompPanel() : _obj;
        
        event EventHandler ISpace.OnAddSpace
        {
            add
            {
                lock (_addSpaceLock)
                {
                    OnAddSpaceEvent += value;
                }
            }
            remove
            {
                lock (_addSpaceLock)
                {
                    OnAddSpaceEvent -= value;
                }
            }
        }
        
        event EventHandler ISpace.OnAddSelectSpace
        {
            add
            {
                lock (_selectSpaceLock)
                {
                    OnSelectSpaceEvent += value;
                }
            }
            remove
            {
                lock (_selectSpaceLock)
                {
                    OnSelectSpaceEvent -= value;
                }
            }
        }

        public void Refresh(bool withAdd = true)
        {
            if(withAdd) SContentReport.Children.Clear();
            foreach (var space in _spaces)
            {
                var item = withAdd ? new ItemReport() : null;
                var k = _spaces.IndexOf(space);
                if(withAdd) item!.Refresh(space.Code!, (k + 1).ToString(), space.Name!, space.Date, true);
                if (k == Properties.Instance.SelectedReport && Properties.Instance.SelectedLeftOnglet == 1 && withAdd) item!.SetItemForm();
                else if (k == Properties.Instance.SelectedReport && !withAdd) (SContentReport.Children[k] as ItemReport)!.SetItemForm();
                if (withAdd) SContentReport.Children.Add(item!);
            }
        }

        public void ActiveItem(int i, int onglet = 0)
        {
            foreach (var space in _spaces)
            {
                var k = _spaces.IndexOf(space);
                if (i == k) (SContentReport.Children[k] as ItemReport)!.SetItemForm();
                else (SContentReport.Children[k] as ItemReport)!.SetItemForm(false);
            }
        }

        public void SetItemInfos(int onglet, int i, string name, string code)
        {
            if (onglet == 0)
            {

            }
            else if (onglet == 1)
            {
                foreach (var space in _spaces)
                {
                    var k = _spaces.IndexOf(space);
                    if (i == k) (SContentReport.Children[k] as ItemReport)!.Refresh(code, (k + 1).ToString(), name, space.Date);
                }
            }
            else if (onglet == 2)
            {
                // foreach (var component in Properties.Instance.ConfigAppInfo.components!)
                // {
                //     int k = Properties.Instance.ConfigAppInfo.components!.IndexOf(component);
                //     if (i == k) (SPersonnel.Children[k] as ItemReport)!.Refresh(code, (k + 1).ToString(), name, component.Date);
                // }
            }
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button)!.Tag.ToString()!;
            var button = (sender as Button)!;
            switch (tag)
            {
                case "AddText": break;
                case "Close":
                    MainWindow.Instance.DisplayCompPage();
                    break;
                case "Component":
                    button.Background = new BrushConverter().ConvertFrom("#f4f6f8") as SolidColorBrush;
                    button.Foreground = new BrushConverter().ConvertFrom("#1960ea") as SolidColorBrush;
                    BReport.Background = BPersonnel.Background = Brushes.Transparent;
                    BReport.Foreground = BPersonnel.Foreground = new BrushConverter().ConvertFrom("#000000") as SolidColorBrush;
                    SContentReport.Visibility = BtnAdd.Visibility = SPersonnel.Visibility = Visibility.Collapsed;
                    SContentComponent.Visibility = Visibility.Visible;
                    Properties.Instance.SelectedLeftOnglet = 0;
                    Refresh();
                    break;
                case "Report":
                    if (Properties.Instance.SelectedLeftOnglet != 1)
                    {
                        button.Background = new BrushConverter().ConvertFrom("#f4f6f8") as SolidColorBrush;
                        button.Foreground = new BrushConverter().ConvertFrom("#1960ea") as SolidColorBrush;
                        BComponent.Background = BPersonnel.Background = Brushes.Transparent;
                        BComponent.Foreground = BPersonnel.Foreground = new BrushConverter().ConvertFrom("#000000") as SolidColorBrush;
                        SContentComponent.Visibility = SPersonnel.Visibility = Visibility.Collapsed;
                        SContentReport.Visibility = BtnAdd.Visibility = Visibility.Visible;

                        Properties.Instance.SelectedLeftOnglet = 1;
                        //PageView.Instance.Refresh();
                        //ActiveItem(0, 1);
                        MainWindow.Instance.DisplayPage();
                        //MainWindow.Instance.DisplayCompPage();
                    }
                    break;
                case "Personnel":
                    if (Properties.Instance.SelectedLeftOnglet != 2)
                    {
                        button.Background = new BrushConverter().ConvertFrom("#f4f6f8") as SolidColorBrush;
                        button.Foreground = new BrushConverter().ConvertFrom("#1960ea") as SolidColorBrush;
                        BComponent.Background = BReport.Background = Brushes.Transparent;
                        BComponent.Foreground = BReport.Foreground = new BrushConverter().ConvertFrom("#000000") as SolidColorBrush;
                        SContentComponent.Visibility = SContentReport.Visibility = Visibility.Collapsed;
                        SPersonnel.Visibility = BtnAdd.Visibility = Visibility.Visible;

                        Properties.Instance.SelectedLeftOnglet = 2;
                        ComponentPage.Instance.Refresh();
                        ActiveItem(Properties.Instance.SelectedComponent, 2);
                        MainWindow.Instance.DisplayPage(false);
                    }
                    break;
            }
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var tag = (sender as Border)!.Tag.ToString()!;
            switch (tag)
            {
                case "Add":
                    switch (Properties.Instance.SelectedLeftOnglet)
                    {
                        case 1:
                            break;
                        case 2:
                            // new ConfirmDialogBox(
                            //     delegate () {
                            //         var componentm = new ComponentModel();
                            //         Properties.Instance.Components.Add(
                            //             new ReportModel
                            //             {
                            //                 Name = $"Component{Properties.Instance.ConfigAppInfo.components!.Count + 1}",
                            //                 Code = $"component{Properties.Instance.ConfigAppInfo.components!.Count + 1}",
                            //                 Report = componentm.OnSerialiser(),
                            //                 Date = DateTime.Now
                            //             });
                            //         Properties.Instance.ComponentMNS.Add(componentm);
                            //         Properties.Instance.ConfigAppInfo.components!.Add(new ReportInfo
                            //         {
                            //             Name = Properties.Instance.Components[Properties.Instance.Components.Count - 1].Name!,
                            //             Code = Properties.Instance.Components[Properties.Instance.Components.Count - 1].Code!,
                            //             Date = Properties.Instance.Components[Properties.Instance.Components.Count - 1].Date!
                            //         });
                            //
                            //         Refresh(2);
                            //         Properties.Instance.SelectedComponent = Properties.Instance.Components.Count - 1;
                            //         ComponentPage.Instance.Refresh();
                            //         ActiveItem(Properties.Instance.ConfigAppInfo.components.Count - 1, 2);
                            //
                            //         PageView.Instance.OnSaved(1, Properties.Instance.Components.Count - 1);
                            //         PageView.Instance.OnSaved(2);
                            //     },
                            //     delegate () {  },
                            //     "Ajout d'un composant",
                            //     "Êtes-vous sûr de vouloir ajouter un nouveau composant ? Le cas échéant, le composant se crée et s'enregistre automatiquement.",
                            //     0
                            // ).ShowDialog();
                            break;
                    }
                    break;
            }
        }
    }
}
