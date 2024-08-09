using ConceptorUI.Models;
using ConceptorUI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ConceptorUI.Classes;


namespace ConceptorUI.Views.Component
{
    public partial class PageView
    {
        private static PageView? _obj;
        public object Component;
        
        private Project _project;
        private Dictionary<string, WindowModel> _windows;
        private int _selectedReport;

        private string _copiedComponent;

        public PageView()
        {
            InitializeComponent();
            
            _obj = this;
            var manageEnums = new ManageEnums();
            
            _project = new Project();
            _windows = new Dictionary<string, WindowModel>();
            _selectedReport = 0;

            _copiedComponent = string.Empty;
            
            #region Init Space
            var configFile = $"{_project.FolderPath}config.json";
            _project = File.Exists(configFile) ? JsonSerializer.Deserialize<Project>(File.ReadAllText(configFile))! : new Project();
            
            if (_project.Space != null!)
            {
                if (_project.Space.Reports.Count > 0)
                    LoadSpace(0);
            }
            else NewSpace();
            #endregion
        }

        public static PageView Instance => _obj == null! ? new PageView() : _obj;
        
        public void Refresh(bool added = true)
        {
            page.Children.Clear();
            LoadSpace(Properties.Instance.SelectedSpace);
            
            var structuralElement = _windows[_project.Space.Reports[_selectedReport].Code].AddToStructuralView();
            
            StructuralView.Instance.Panel.Children.Clear();
            StructuralView.Instance.StructuralElement = structuralElement;
            StructuralView.Instance.BuildView(structuralElement, 0, structuralElement.IsSimpleElement);
            PanelStructuralView.Instance.Refresh();
        }

        private void LoadSpace(int spaceIndex)
        {
            #region Load spage
            page.Children.Clear();
            
            _windows.Add("", new WindowModel());
            
            foreach (var report in _project.Space.Reports)
            {
                var fileName = $"{_project.FolderPath}pages/{report.Code}.json";
                if (!File.Exists(fileName)) continue;
                
                var windowModel = new WindowModel();
                windowModel.OnSelectedEvent += OnSelectedHandle!;
                windowModel.OnRefreshPropertyPanelEvent += OnRefreshPropertyPanelHandle!;
                windowModel.OnRefreshStructuralViewEvent += OnRefreshStructuralViewHandle!;
                
                var content = new StackPanel
                {
                    Width = 400,
                    Margin = new Thickness(0, 0, 0, 30)
                };
                
                var title = new TextBlock
                {
                    Text = report.Name,
                    FontSize = 14,
                    Margin = new Thickness(0, 0, 0, 6),
                    Foreground = new BrushConverter().ConvertFrom("#666666") as SolidColorBrush,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                
                content.Children.Add(title);
                content.Children.Add(windowModel.ComponentView);
                page.Children.Add(content);
                _windows.Add(report.Code, windowModel);
                
                var sc = SynchronizationContext.Current;
                ThreadPool.QueueUserWorkItem(delegate
                {
                    var jsonString = File.ReadAllText(fileName);
                    var component = JsonSerializer.Deserialize<CompSerializer>(jsonString)!;
                    
                    sc!.Post(delegate
                    {
                        windowModel.OnDeserializer(component);
                    }, null);
                });
            }
            #endregion
        }

        public void NewSpace()
        {
            #region Adding new Space
            var windowModel = new WindowModel();
            windowModel.OnSelectedEvent += OnSelectedHandle!;
            windowModel.OnRefreshPropertyPanelEvent += OnRefreshPropertyPanelHandle!;
            windowModel.OnRefreshStructuralViewEvent += OnRefreshStructuralViewHandle!;
            
            if (_project.Space == null!)
            {
                _project.Space = new Space();
                _windows = new Dictionary<string, WindowModel>();
            }
            
            _project.Space.Reports.Add(
                new Report
                {
                    Name = "Space 1",
                    Code = "space1",
                    Date = DateTime.Now
                }
            );
            
            _windows.Add(_project.Space.Reports[^1].Code, windowModel);
                
            var content = new StackPanel
            {
                Width = 400,
                Margin = new Thickness(0, 0, 0, 30)
            };
            
            var title = new TextBlock
            {
                Text = "Space "+ (_project.Space.Reports.Count + 1),
                FontSize = 14,
                Margin = new Thickness(0, 0, 0, 6),
                Foreground = new BrushConverter().ConvertFrom("#666666") as SolidColorBrush,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            
            content.Children.Add(title);
            content.Children.Add(windowModel.ComponentView);
            page.Children.Add(content);

            OnSaved();
            OnSaved(2);
            #endregion
        }
        
        public void NewReport()
        {
            #region Adding new Report
            var windowModel = new WindowModel();
            windowModel.OnSelectedEvent += OnSelectedHandle!;
            windowModel.OnRefreshPropertyPanelEvent += OnRefreshPropertyPanelHandle!;
            windowModel.OnRefreshStructuralViewEvent += OnRefreshStructuralViewHandle!;
            
            var indexReport = NextReportIndex();
            
            var name = $"Report {indexReport}";
            var code = $"report{indexReport}";
            var date = DateTime.Now;
            var index = _project.Space.Reports.Count;

            _project.Space.Reports.Add(
                new Report {
                    Name = name,
                    Code = code,
                    Date = date
                }
            );
            
            _windows.Add(code, windowModel);
            
            var content = new StackPanel
            {
                Width = 400,
                Margin = new Thickness(0, 0, 0, 30)
            };
            
            var title = new TextBlock
            {
                Text = name,
                FontSize = 14,
                Margin = new Thickness(0, 0, 0, 6),
                Foreground = new BrushConverter().ConvertFrom("#666666") as SolidColorBrush,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            
            content.Children.Add(title);
            content.Children.Add(windowModel.ComponentView);
            page.Children.Add(content);

            OnSaved(0, index);
            OnSaved(2);
            #endregion
        }

        public void DeleteReport()
        {
            _project.Space.Reports.RemoveAt(_selectedReport);
            
            File.Delete($"{_project.Space.Reports[_selectedReport].Code}.json");

            _windows[_project.Space.Reports[_selectedReport].Code].OnSelectedEvent -= OnSelectedHandle!;
            _windows[_project.Space.Reports[_selectedReport].Code].OnRefreshPropertyPanelEvent += OnRefreshPropertyPanelHandle!;
            _windows[_project.Space.Reports[_selectedReport].Code].OnRefreshStructuralViewEvent += OnRefreshStructuralViewHandle!;
            
            _windows.Remove(_project.Space.Reports[_selectedReport].Code);
            
            var n = page.Children.Count;
            page.Children.RemoveAt(Properties.Instance.SelectedReport);
            
            if (n > 1)
            {
                Properties.Instance.SelectedReport = 0;
            }

            OnSaved(2);
        }

        public void AddComponent(string componentName)
        {
            if (componentName == ComponentList.Container.ToString())
            {
                var component = JsonSerializer.Serialize(new ContainerModel());
                _windows[_project.Space.Reports[_selectedReport].Code].OnCopyOrPaste(component, true);
            }
        }

        public void SetProperty(GroupNames groupName, PropertyNames propertyName, string value)
        {
            _windows[_project.Space.Reports[_selectedReport].Code].OnUpdated(groupName, propertyName, value);
        }

        public void OnUnselected(bool isInterne = false)
        {
            switch (Properties.Instance.SelectedLeftOnglet)
            {
                case 1:
                    foreach (var key in _windows.Keys)
                        _windows[key].OnUnselected(isInterne);
                    break;
                case 2:
                    _windows[_project.Space.Reports[_selectedReport].Code].OnUnselected(isInterne);
                    break;
            }
        }

        public void OnSelected()
        {
            foreach (var key in _windows.Keys)
            {
                if (_windows[key].OnChildSelected())
                    _selectedReport = _project.Space.Reports.FindIndex(r => r.Code == key);
            }
        }

        public void RefreshStructuralView()
        {
            var structuralElement = _windows[_project.Space.Reports[_selectedReport].Code].AddToStructuralView();
            
            StructuralView.Instance.Panel.Children.Clear();
            StructuralView.Instance.StructuralElement = structuralElement;
            StructuralView.Instance.BuildView(structuralElement, 0, structuralElement.IsSimpleElement);
            PanelStructuralView.Instance.Refresh();
        }

        public void SelectFromStructuralView()
        {
            _windows[_project.Space.Reports[_selectedReport].Code].SelectFromStructuralView(StructuralView.Instance.StructuralElement);
        }

        public void OnCopyOrPaste(bool isPaste = false)
        {
            if(isPaste)
                _windows[_project.Space.Reports[_selectedReport].Code].OnCopyOrPaste(_copiedComponent, isPaste);
            else
                _copiedComponent = _windows[_project.Space.Reports[_selectedReport].Code].OnCopyOrPaste(isPaste: isPaste);
        }
        
        private void OnSelectedHandle(object sender, EventArgs e)
        {
            
        }
        
        private void OnRefreshPropertyPanelHandle(object sender, EventArgs e)
        {
            
        }
        
        private void OnRefreshStructuralViewHandle(object sender, EventArgs e)
        {
            
        }

        public void OnSaved(int isPage = 0, int index = 0)
        {
            var sc = SynchronizationContext.Current;
            ThreadPool.QueueUserWorkItem(delegate
            {
                #region
                sc!.Post(delegate
                {
                    switch (isPage)
                    {
                        case 0:
                        {
                            var componentSerializer = _windows[_project.Space.Reports[index].Code].OnSerializer();
                            var filePath = $"{_project.FolderPath}/pages/{_project.Space.Reports[index].Code}.json";
                            File.Create(filePath).Dispose();
                            
                            var jsonString = JsonSerializer.Serialize(componentSerializer);
                            File.WriteAllText(filePath, jsonString);
                            break;
                        }
                        case 2:
                        {
                            var filePath = $"{_project.FolderPath}/config.json";
                            File.Create(filePath).Dispose();
                            var jsonString = JsonSerializer.Serialize(_project);
                            
                            File.WriteAllText(filePath, jsonString);
                            break;
                        }
                    }
                }, null);
                #endregion
            });
        }

        public void OnLoaded(string fileName, int isPage = 0)
        {
            #region Chargement des Composants
            var sc = SynchronizationContext.Current;
            ThreadPool.QueueUserWorkItem(delegate
            {
                switch (isPage)
                {
                    case 0:
                    {
                        var filePath = $"{_project.FolderPath}/pages/{fileName}.json";
                        var compSerializer = JsonSerializer.Deserialize<CompSerializer>(
                            File.ReadAllText(filePath)
                        );
                        
                        var windowModel = new WindowModel();
                        windowModel.OnSelectedEvent += OnSelectedHandle!;
                        
                        windowModel.OnDeserializer(compSerializer!);
                        _windows.Add("unknown", windowModel);
                        break;
                    }
                    case 2:
                        _project = JsonSerializer.Deserialize<Project>(File.ReadAllText(
                            $"{_project.FolderPath}/config.json"
                        ))!;
                        break;
                }
            });
            #endregion
        }

        private int NextReportIndex()
        {
            var n = _project.Space.Reports.Count;
            var index = -1;
            for(var i = 1; i <= n; i++)
            {
                var found = false;
                foreach(var report in _project.Space.Reports)
                {
                    if ($"report{i}" != report.Code) continue;
                    found = true;
                    break;
                }

                if (found) continue;
                index = i;
                break;
            }

            return index == -1 ? n + 1 : index;
        }

        public static void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("You clicked me at ");
        }
    }
}
