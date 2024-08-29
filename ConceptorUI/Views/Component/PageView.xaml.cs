using ConceptorUI.Models;
using ConceptorUi.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ConceptorUI.Classes;
using ConceptorUI.Interfaces;


namespace ConceptorUI.Views.Component
{
    public partial class PageView : IRefreshPropertiesPanel
    {
        private static PageView? _obj;
        public object Component;
        
        private Project _project;
        private Dictionary<string, ConceptorUi.ViewModels.Component> _components;
        public int SelectedReport;

        private string _copiedComponent;
        private int _clicCount;
        private ComponentList _componentName;
        
        public event EventHandler? OnRefreshPropertyPanelEvent;
        private readonly object _refreshPropertyPanelLock = new();

        public PageView()
        {
            InitializeComponent();
            
            _obj = this;
            var manageEnums = new ManageEnums();
            _project = new Project();
            _components = new Dictionary<string, ConceptorUi.ViewModels.Component>();
            SelectedReport = 0;
            _clicCount = 0;

            _copiedComponent = string.Empty;
        }

        public static PageView Instance => _obj == null! ? new PageView() : _obj;
        
        event EventHandler IRefreshPropertiesPanel.OnRefreshPropertyPanel
        {
            add
            {
                lock (_refreshPropertyPanelLock)
                {
                    OnRefreshPropertyPanelEvent += value;
                }
            }
            remove
            {
                lock (_refreshPropertyPanelLock)
                {
                    OnRefreshPropertyPanelEvent -= value;
                }
            }
        }
        
        public void Refresh(object projectObject)
        {
            _project = (projectObject as Project)!;
            
            #region Init Space
            var configFile = $"{_project.FolderPath}/config.json";
            ComponentHelper.ProjectPath = _project.FolderPath;
            
            try
            {
                _project = File.Exists(configFile) ? JsonSerializer.Deserialize<Project>(File.ReadAllText(configFile))! : new Project();
            }
            catch (Exception)
            {
                Console.WriteLine(@"Le fichier n'existe pas ou n'est pas de bon format.");
            }
            
            if (_project.Space != null!)
            {
                if (_project.Space.Reports.Count > 0)
                    LoadSpace();
            }
            else NewSpace();
            #endregion
        }

        private void InitStructuralView()
        {
            var structuralElement = _components[_project.Space!.Reports[SelectedReport].Code].AddToStructuralView();
            
            StructuralView.Instance.Panel.Children.Clear();
            StructuralView.Instance.StructuralElement = structuralElement;
            StructuralView.Instance.BuildView(structuralElement, 0, structuralElement.IsSimpleElement);
            PanelStructuralView.Instance.Refresh();
        }

        private void LoadSpace()
        {
            #region Load spage
            page.Children.Clear();
            
            foreach (var report in _project.Space.Reports)
            {
                var filePath = $"{_project.FolderPath}/pages/{report.Code}.json";
                
                if (!File.Exists(filePath)) continue;
                
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
                
                var sc = SynchronizationContext.Current;
                ThreadPool.QueueUserWorkItem(delegate
                {
                    var jsonString = File.ReadAllText(filePath);
                    var component = JsonSerializer.Deserialize<CompSerializer>(jsonString)!;
                    
                    sc!.Post(delegate
                    {
                        ConceptorUi.ViewModels.Component windowModel = null!;
                        
                        if(component.Name == ComponentList.Window.ToString())
                            windowModel = new WindowModel(true);
                        else windowModel = new ComponentModel(true);
                        
                        windowModel.OnSelectedEvent += OnSelectedHandle!;
                        windowModel.OnRefreshPropertyPanelEvent += OnRefreshPropertyPanelHandle!;
                        windowModel.OnRefreshStructuralViewEvent += OnRefreshStructuralViewHandle!;
                
                        content.Children.Add(title);
                        content.Children.Add(windowModel.ComponentView);
                        page.Children.Add(content);
                        _components.Add(report.Code, windowModel);
                        
                        windowModel.OnDeserializer(component);
                        
                        //InitStructuralView();
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
                _project.Space = new Space
                {
                    Name = "Space 1",
                    Code = "space1",
                    Date = DateTime.Now,
                    Reports = new List<Report>()
                };
                _components = new Dictionary<string, ConceptorUi.ViewModels.Component>();
            }
            
            _project.Space.Reports.Add(
                new Report
                {
                    Name = "Report 1",
                    Code = "report1",
                    Date = DateTime.Now
                }
            );
            
            _components.Add(_project.Space.Reports[^1].Code, windowModel);
                
            var content = new StackPanel
            {
                Width = 400,
                Margin = new Thickness(0, 0, 0, 30)
            };
            
            var title = new TextBlock
            {
                Text = "Report "+ (_project.Space.Reports.Count + 1),
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
                        
            InitStructuralView();
            #endregion
        }
        
        public void NewReport()
        {
            #region Adding new Report
            var windowModel = new ComponentModel();
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
            
            _components.Add(code, windowModel);
            
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
            _project.Space.Reports.RemoveAt(SelectedReport);
            
            File.Delete($"{_project.Space.Reports[SelectedReport].Code}.json");

            _components[_project.Space.Reports[SelectedReport].Code].OnSelectedEvent -= OnSelectedHandle!;
            _components[_project.Space.Reports[SelectedReport].Code].OnRefreshPropertyPanelEvent += OnRefreshPropertyPanelHandle!;
            _components[_project.Space.Reports[SelectedReport].Code].OnRefreshStructuralViewEvent += OnRefreshStructuralViewHandle!;
            
            _components.Remove(_project.Space.Reports[SelectedReport].Code);
            
            var n = page.Children.Count;
            page.Children.RemoveAt(SelectedReport);
            
            if (n > 1)
            {
                SelectedReport = 0;
            }

            OnSaved(2);
        }

        public void AddComponent(string componentName)
        {
            if (ComponentHelper.IsComponent(componentName))
            {
                var component = ComponentHelper.GetComponent(componentName);
                var compText = JsonSerializer.Serialize(component.OnSerializer());
                _components[_project.Space.Reports[SelectedReport].Code].OnCopyOrPaste(compText, true);
            }else{}
        }

        public void SetProperty(GroupNames groupName, PropertyNames propertyName, string value)
        {
            _components[_project.Space.Reports[SelectedReport].Code].OnUpdated(groupName, propertyName, value);
        }

        public void RefreshStructuralView()
        {
            var structuralElement = _components[_project.Space.Reports[SelectedReport].Code].AddToStructuralView();
            
            StructuralView.Instance.Panel.Children.Clear();
            StructuralView.Instance.StructuralElement = structuralElement;
            StructuralView.Instance.BuildView(structuralElement, 0, structuralElement.IsSimpleElement);
            PanelStructuralView.Instance.Refresh();
        }

        public void SelectFromStructuralView()
        {
            _components[_project.Space.Reports[SelectedReport].Code].SelectFromStructuralView(StructuralView.Instance.StructuralElement);
        }

        public void OnCopyOrPaste(bool isPaste = false)
        {
            if(isPaste)
                _components[_project.Space.Reports[SelectedReport].Code].OnCopyOrPaste(_copiedComponent, isPaste);
            else
                _copiedComponent = _components[_project.Space.Reports[SelectedReport].Code].OnCopyOrPaste(isPaste: isPaste);
        }
        
        private void OnSelectedHandle(object sender, EventArgs e)
        {
            var values = sender as Dictionary<string, dynamic>;
            
            foreach (var key in _components.Keys)
            {
                if (_components[key].OnChildSelected())
                    SelectedReport = _project.Space.Reports.FindIndex(r => r.Code == key);
                if(!values!["selected"])
                    _components[key].OnUnselected();
            }

            if (values!["selected"])
            {
                OnRefreshPropertyPanelEvent?.Invoke(
                    values,
                    EventArgs.Empty
                );
                RefreshStructuralView();

                _clicCount++;
                _componentName = values["componentName"];
                if (_componentName == values["componentName"] && _clicCount == 2)
                {
                    _clicCount = 0;
                }
            }
        }
        
        private void OnRefreshPropertyPanelHandle(object sender, EventArgs e)
        {
            
        }
        
        private void OnRefreshStructuralViewHandle(object sender, EventArgs e)
        {
            
        }
        
        public void OnUnSelect()
        {
            _components[_project.Space.Reports[SelectedReport].Code].OnUnselected();
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
                            var componentSerializer = _components[_project.Space.Reports[index].Code].OnSerializer();
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
            //var sc = SynchronizationContext.Current;
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
                        _components.Add("unknown", windowModel);
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
    }
}
