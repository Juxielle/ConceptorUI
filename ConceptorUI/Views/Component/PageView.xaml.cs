using ConceptorUI.Models;
using ConceptorUi.ViewModels;
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
using ConceptorUI.Inputs;
using ConceptorUI.Utils;


namespace ConceptorUI.Views.Component
{
    public partial class PageView
    {
        private static PageView? _obj;
        public readonly object Component = null!;

        private Project _project;
        private Dictionary<string, ConceptorUi.ViewModels.Component> _components;
        public int SelectedReport;
        private string _selectedKey;

        private string _copiedComponent;
        private int _clickCount;
        private string? _componentId;

        public ICommand? RefreshPropertyPanelCommand;
        public ICommand? DisplayTextTypingCommand;
        public ICommand? DisplayLoadingCommand;

        public PageView()
        {
            InitializeComponent();

            _obj = this;
            var manageEnums = new ManageEnums();
            _project = new Project();
            _components = new Dictionary<string, ConceptorUi.ViewModels.Component>();
            SelectedReport = 0;
            _clickCount = 0;

            _copiedComponent = string.Empty;
        }

        public static PageView Instance => _obj == null! ? new PageView() : _obj;

        public void Refresh(object projectObject)
        {
            _project = (projectObject as Project)!;

            #region Init Space

            var configFile = $"{_project.FolderPath}/config.json";
            ComponentHelper.ProjectPath = _project.FolderPath;

            try
            {
                _project = File.Exists(configFile)
                    ? JsonSerializer.Deserialize<Project>(File.ReadAllText(configFile))!
                    : new Project();
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
            var structuralElement = _components[_project.Space.Reports[SelectedReport].Code].AddToStructuralView();

            StructuralView.Instance.Panel.Children.Clear();
            StructuralView.Instance.StructuralElement = structuralElement;
            StructuralView.Instance.BuildView(structuralElement, 0, structuralElement.IsSimpleElement);
            PanelStructuralView.Instance.Refresh();
        }

        private void LoadSpace()
        {
            #region Load spage

            Page.Children.Clear();

            var reports = _project.Space.Reports;
            var counter = 0;
            DisplayLoadingCommand?.Execute(true);
            for (var i = 0; i < reports.Count; i++)
            {
                var filePath = $"{_project.FolderPath}/pages/{reports[i].Code}.json";

                if (!File.Exists(filePath)) continue;

                var sc = SynchronizationContext.Current;
                var i1 = i;
                ThreadPool.QueueUserWorkItem(delegate
                {
                    var jsonString = File.ReadAllText(filePath);
                    var component = JsonSerializer.Deserialize<CompSerializer>(jsonString)!;

                    var k = i1;
                    sc!.Post(delegate
                    {
                        ConceptorUi.ViewModels.Component windowModel;

                        if (component.Name == ComponentList.Window.ToString())
                            windowModel = new WindowModel(true);
                        else windowModel = new ComponentModel(true);

                        windowModel.SelectedCommand = new RelayCommand(OnSelectedHandle);
                        windowModel.RefreshPropertyPanelCommand = new RelayCommand(OnRefreshPropertyPanelHandle);
                        windowModel.RefreshStructuralViewCommand = new RelayCommand(OnRefreshStructuralViewHandle);

                        _components.Add(reports[k].Code, windowModel);

                        windowModel.OnDeserializer(component);
                        
                        counter++;
                        if (counter != reports.Count) return;

                        DisplayLoadingCommand?.Execute(false);

                        for (var p = 0; p < reports.Count; p++)
                        {
                            var componentSx = _components[reports[p].Code].GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.X);
                            var componentX = Helper.ConvertToDouble(componentSx) - 200;

                            var componentSy = _components[reports[p].Code].GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Y);
                            var componentY = Helper.ConvertToDouble(componentSy);

                            var content = new StackPanel
                            {
                                Width = 400,
                                VerticalAlignment = VerticalAlignment.Top,
                                HorizontalAlignment = HorizontalAlignment.Left,
                                Margin = new Thickness(componentX, componentY, 0, 30)
                            };

                            var title = new TextBlock
                            {
                                Text = reports[p].Name,
                                FontSize = 14,
                                Margin = new Thickness(0, 0, 0, 6),
                                Foreground = new BrushConverter().ConvertFrom("#666666") as SolidColorBrush,
                                HorizontalAlignment = HorizontalAlignment.Stretch,
                                TextAlignment = TextAlignment.Center,
                                Tag = reports[p].Code
                            };
                            title.MouseDown += OnSelectedHandle;
                            title.MouseEnter += OnMouseEnterHandle;
                            title.PreviewMouseUp += OnPreviewMouseUp;
                            title.MouseLeave += OnPreviewMouseLeave;
                            title.PreviewMouseMove += OnPreviewMouseMove;

                            content.Children.Add(title);
                            content.Children.Add(_components[reports[p].Code].ComponentView);
                            Page.Children.Add(content);
                        }
                    }, null);
                });
            }

            #endregion
        }

        private void NewSpace()
        {
            #region Adding new Space

            var windowModel = new WindowModel();
            windowModel.SelectedCommand = new RelayCommand(OnSelectedHandle);
            windowModel.RefreshPropertyPanelCommand = new RelayCommand(OnRefreshPropertyPanelHandle);
            windowModel.RefreshStructuralViewCommand = new RelayCommand(OnRefreshStructuralViewHandle);

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
                Text = "Report " + (_project.Space.Reports.Count + 1),
                FontSize = 14,
                Margin = new Thickness(0, 0, 0, 6),
                Foreground = new BrushConverter().ConvertFrom("#666666") as SolidColorBrush,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            content.Children.Add(title);
            content.Children.Add(windowModel.ComponentView);
            Page.Children.Add(content);

            OnSaved();
            OnSaved(2);

            InitStructuralView();

            #endregion
        }

        public void NewReport(bool isComponent = false)
        {
            #region Adding new Report

            ConceptorUi.ViewModels.Component windowModel;
            if (isComponent) windowModel = new ComponentModel();
            else windowModel = new WindowModel();

            windowModel.SelectedCommand = new RelayCommand(OnSelectedHandle);
            windowModel.RefreshPropertyPanelCommand = new RelayCommand(OnRefreshPropertyPanelHandle);
            windowModel.RefreshStructuralViewCommand = new RelayCommand(OnRefreshStructuralViewHandle);

            var indexReport = NextReportIndex();

            var name = $"Report {indexReport}";
            var code = $"report{indexReport}";
            var date = DateTime.Now;
            var index = _project.Space.Reports.Count;

            _project.Space.Reports.Add(
                new Report
                {
                    Name = name,
                    Code = code,
                    Date = date
                }
            );

            _components.Add(code, windowModel);

            var content = new StackPanel
            {
                Width = 400,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(0, 0, 0, 30)
            };

            var title = new TextBlock
            {
                Text = name,
                FontSize = 14,
                Margin = new Thickness(0, 0, 0, 6),
                Foreground = new BrushConverter().ConvertFrom("#666666") as SolidColorBrush,
                HorizontalAlignment = HorizontalAlignment.Center,
                Tag = code
            };
            title.MouseDown += OnSelectedHandle;

            content.Children.Add(title);
            content.Children.Add(windowModel.ComponentView);
            Page.Children.Add(content);

            OnSaved(0, index);
            OnSaved(2);

            #endregion
        }

        public void DeleteReport()
        {
            _project.Space.Reports.RemoveAt(SelectedReport);

            File.Delete($"{_project.Space.Reports[SelectedReport].Code}.json");

            _components[_project.Space.Reports[SelectedReport].Code].SelectedCommand =
                new RelayCommand(OnSelectedHandle);
            _components[_project.Space.Reports[SelectedReport].Code].RefreshPropertyPanelCommand =
                new RelayCommand(OnRefreshPropertyPanelHandle);
            _components[_project.Space.Reports[SelectedReport].Code].RefreshStructuralViewCommand =
                new RelayCommand(OnRefreshStructuralViewHandle);

            _components.Remove(_project.Space.Reports[SelectedReport].Code);

            var n = Page.Children.Count;
            Page.Children.RemoveAt(SelectedReport);

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
            }
        }

        public void AddReusableComponent(string id)
        {
            var component = _components.Values.First(c => c.Id == id);
            if (component == null! || component.Children.Count == 0 ||
                component.Children[0].Children.Count == 0) return;

            component.CheckIsExistId();
            var compText = JsonSerializer.Serialize(component.Children[0].Children[0].OnSerializer());
            _components[_project.Space.Reports[SelectedReport].Code].OnCopyOrPaste(compText, true, true);
        }

        public void RefreshReusableComponent()
        {
            try
            {
                foreach (var key in _components.Keys)
                {
                    if (_components[key].GetType().Name != nameof(ComponentModel)) continue;
                    var serializer = _components[key].Children[0].Children[0].OnSerializer();
                    foreach (var key2 in _components.Keys.Where(key2 => key != key2))
                        _components[key2].OnUpdateComponent(serializer);
                    break;
                }
            }
            catch (Exception)
            {
                //
            }
        }

        public void SetProperty(GroupNames groupName, PropertyNames propertyName, string value)
        {
            _components[_project.Space.Reports[SelectedReport].Code].OnUpdated(groupName, propertyName, value);
        }

        private void RefreshStructuralView()
        {
            var structuralElement = _components[_project.Space.Reports[SelectedReport].Code].AddToStructuralView();

            StructuralView.Instance.Panel.Children.Clear();
            StructuralView.Instance.StructuralElement = structuralElement;
            StructuralView.Instance.BuildView(structuralElement, 0, structuralElement.IsSimpleElement);
            PanelStructuralView.Instance.Refresh();
        }

        public void SelectFromStructuralView()
        {
            _components[_project.Space.Reports[SelectedReport].Code]
                .SelectFromStructuralView(StructuralView.Instance.StructuralElement);
        }

        public void OnCopyOrPaste(bool isPaste = false)
        {
            if (isPaste)
                _components[_project.Space.Reports[SelectedReport].Code].OnCopyOrPaste(_copiedComponent, isPaste);
            else
                _copiedComponent = _components[_project.Space.Reports[SelectedReport].Code]
                    .OnCopyOrPaste(isPaste: isPaste);
        }

        private void OnSelectedHandle(object sender)
        {
            var values = sender as Dictionary<string, dynamic>;

            foreach (var key in _components.Keys.Where(_ => !values!["selected"]))
                _components[key].OnUnselected();

            if (!values!["selected"]) return;

            foreach (var key in _components.Keys.Where(key => _components[key].OnChildSelected()))
                SelectedReport = _project.Space.Reports.FindIndex(r => r.Code == key);

            RefreshPropertyPanelCommand?.Execute(values);

            RefreshStructuralView();

            _clickCount++;
            if (_componentId == values["Id"] && _clickCount == 2)
            {
                _clickCount = 0;
                if (values["componentName"] == ComponentList.Text)
                    DisplayTextTypingCommand?.Execute(values["propertyGroups"]);
            }

            _clickCount = _clickCount >= 2 ? 0 : _clickCount;
            _componentId = values["Id"];
        }

        private void OnRefreshPropertyPanelHandle(object sender)
        {
        }

        private void OnRefreshStructuralViewHandle(object sender)
        {
        }

        public void OnUnSelect()
        {
            _components[_project.Space.Reports[SelectedReport].Code].OnUnselected();
        }

        public object SendComponent()
        {
            List<ConceptorUi.ViewModels.Component> components = [];
            components.AddRange(from key in _components.Keys
                where _components[key].GetType().Name == nameof(ComponentModel)
                select _components[key]);

            return components;
        }

        public void OnSaved(int isPage = 0, int index = 0)
        {
            DisplayLoadingCommand?.Execute(true);
            var sc = SynchronizationContext.Current;
            ThreadPool.QueueUserWorkItem(delegate
            {
                #region

                switch (isPage)
                {
                    case 0:
                    {
                        foreach (var key in _components.Keys)
                        {
                            var componentSerializer = _components[key].OnSerializer();
                            var filePath = @$"{_project.FolderPath}\pages\{key}.json";

                            File.Create(filePath).Dispose();

                            var jsonString = JsonSerializer.Serialize(componentSerializer);
                            File.WriteAllText(filePath, jsonString);
                        }

                        sc!.Post(delegate { RefreshReusableComponent(); }, null);
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

                sc!.Post(delegate { DisplayLoadingCommand?.Execute(false); }, null);

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

                        ConceptorUi.ViewModels.Component windowModel;

                        if (compSerializer!.Name == ComponentList.Container.ToString())
                            windowModel = new ComponentModel();
                        else windowModel = new WindowModel();

                        windowModel.SelectedCommand = new RelayCommand(OnSelectedHandle);
                        windowModel.RefreshPropertyPanelCommand = new RelayCommand(OnRefreshPropertyPanelHandle);
                        windowModel.RefreshStructuralViewCommand = new RelayCommand(OnRefreshStructuralViewHandle);

                        windowModel.OnDeserializer(compSerializer);
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
            for (var i = 1; i <= n; i++)
            {
                var found = false;
                foreach (var report in _project.Space.Reports)
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

        private void OnSelectedHandle(object sender, MouseButtonEventArgs e)
        {
            var tag = (sender as FrameworkElement)!.Tag as string;

            foreach (var component in _components)
            {
                if (component.Key == tag)
                {
                    _components[component.Key].OnUnselected();
                    _components[component.Key].OnSelected();

                    var point = e.GetPosition(Page);
                    _selectedKey = tag;
                    _components[_selectedKey].SetPropertyValue(GroupNames.Transform, PropertyNames.X, $"{point.X}");
                    _components[_selectedKey].SetPropertyValue(GroupNames.Transform, PropertyNames.Y, $"{point.Y}");
                }
                else _components[component.Key].OnUnselected();
            }
        }

        private void OnMouseEnterHandle(object sender, MouseEventArgs e)
        {
            var textBlock = (TextBlock)sender;
            textBlock.Cursor = Cursors.Hand;
        }

        private void OnPreviewMouseUp(object sender, MouseEventArgs e)
        {
            _selectedKey = string.Empty;
        }

        private void OnPreviewMouseLeave(object sender, MouseEventArgs e)
        {
            _selectedKey = string.Empty;
        }

        private void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            var textBlock = (TextBlock)sender;
            if (_selectedKey != textBlock.Tag.ToString()) return;

            var componentSx = _components[_selectedKey]
                .GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.X);
            var componentX = Helper.ConvertToDouble(componentSx);

            var componentSy = _components[_selectedKey]
                .GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Y);
            var componentY = Helper.ConvertToDouble(componentSy);

            var child = Page.Children[SelectedReport] as StackPanel;
            var point = e.GetPosition(Page);

            var toRight = componentX < point.X;
            var toBottom = componentY < point.Y;

            var dx = Math.Abs(point.X - componentX);
            var dy = Math.Abs(point.Y - componentY);

            var x = toRight ? componentX + dx : componentX - dx;
            var y = toBottom ? componentY + dy : componentY - dy;

            var xn = toRight ? child!.Margin.Left + dx : child!.Margin.Left - dx;
            var yn = toBottom ? child.Margin.Top + dy : child.Margin.Top - dy;

            _components[_selectedKey].SetPropertyValue(GroupNames.Transform, PropertyNames.X, $"{x}");
            _components[_selectedKey].SetPropertyValue(GroupNames.Transform, PropertyNames.Y, $"{y}");

            Console.WriteLine($@"----------------------------------");
            Console.WriteLine($@"xn = {xn}");
            Console.WriteLine($@"yn = {yn}");
            child.Margin = new Thickness(xn, yn, 0, 0);
        }

        private void OnPageClickHandle(object sender, MouseButtonEventArgs e)
        {
            if(!e.OriginalSource.Equals(Page)) return;
            _components[_project.Space.Reports[SelectedReport].Code].OnUnselected();
        }
    }
}