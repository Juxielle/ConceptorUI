using ConceptorUI.Models;
using ConceptorUi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ConceptorUI.Application.Configs;
using ConceptorUI.Application.Dto.UiDto;
using ConceptorUI.Application.Project;
using ConceptorUI.Application.Reports;
using ConceptorUI.Classes;
using ConceptorUI.Inputs;
using ConceptorUI.Utils;


namespace ConceptorUI.Views.Component
{
    public partial class PageView
    {
        private static PageView? _obj;

        private ProjectUiDto _project;
        private readonly Dictionary<string, ConceptorUi.ViewModels.Component> _components;
        private int _selectedReport;
        private string? _selectedKey;

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
            _project = new ProjectUiDto
            {
                ReportInfos = []
            };

            _components = new Dictionary<string, ConceptorUi.ViewModels.Component>();
            _selectedReport = 0;
            _clickCount = 0;

            _copiedComponent = string.Empty;
        }

        public static PageView Instance => _obj == null! ? new PageView() : _obj;

        public async void Refresh(object projectObject)
        {
            var projectInfoUiDto = (projectObject as ProjectInfoUiDto)!;
            _project = new ProjectUiDto
            {
                ZipPath = projectInfoUiDto.ZipPath,
                Id = projectInfoUiDto.Id,
                Name = projectInfoUiDto.Name,
                Image = projectInfoUiDto.Image,
                Created = projectInfoUiDto.Created,
                Updated = projectInfoUiDto.Updated,
                ReportInfos = []
            };
            ComponentHelper.ProjectPath = projectInfoUiDto.ZipPath;
            ComponentHelper.ProjectName = projectInfoUiDto.Id;

            #region Init Space

            var configResult = await new GetConfigsQueryHandler().Handle(new GetConfigsQuery
            {
                ZipPath = projectInfoUiDto.ZipPath,
                ProjectName = projectInfoUiDto.Id
            });

            if (configResult.IsSuccess)
            {
                _project = configResult.Value;
                LoadSpace();
            }
            else NewReport();

            #endregion
        }

        private void InitStructuralView()
        {
            var structuralElement = _components[_project.ReportInfos[_selectedReport].Code!].AddToStructuralView();

            StructuralView.Instance.Panel.Children.Clear();
            StructuralView.Instance.StructuralElement = structuralElement;
            StructuralView.Instance.BuildView(structuralElement, 0, structuralElement.IsSimpleElement);
            PanelStructuralView.Instance.Refresh();
        }

        private async void LoadSpace()
        {
            #region Load spage

            var reportsResult = await new GetReportsQueryHandler().Handle(new GetReportsQuery
            {
                ZipPath = ComponentHelper.ProjectPath!,
                ProjectName = _project.Id,
                ReportInfos = _project.ReportInfos
            });

            if (!reportsResult.IsSuccess) return;
            var reports = reportsResult.Value.ToList();

            Page.Children.Clear();
            var counter = 0;
            var failCounter = 0;
            
            DisplayLoadingCommand?.Execute(true);
            for (var i = 0; i < reports.Count; i++)
            {
                var sc = SynchronizationContext.Current;
                var j = i;
                ThreadPool.QueueUserWorkItem(delegate
                {
                    try
                    {
                        var component = JsonSerializer.Deserialize<CompSerializer>(reports[j].Json)!;
                        
                        var k = j;
                        var counter1 = failCounter;
                        sc!.Post(delegate
                        {
                            ConceptorUi.ViewModels.Component windowModel;

                            if (component.Name == ComponentList.Window.ToString())
                                windowModel = new WindowModel(true);
                            else windowModel = new ComponentModel(true);

                            windowModel.SelectedCommand = new RelayCommand(OnSelectedHandle);
                            windowModel.RefreshPropertyPanelCommand = new RelayCommand(OnRefreshPropertyPanelHandle);
                            windowModel.RefreshStructuralViewCommand = new RelayCommand(OnRefreshStructuralViewHandle);

                            _components.Add(reports[k].Code!, windowModel);

                            windowModel.OnDeserializer(component);

                            counter++;
                            //Récupérer uniquement les éléments bien serialisés
                            if (counter != reports.Count - counter1) return;

                            DisplayLoadingCommand?.Execute(false);

                            for (var p = 0; p < reports.Count - counter1; p++)
                            {
                                if(!_components.ContainsKey(reports[p].Code!)) continue;
                                
                                var componentSx = _components[reports[p].Code!].GetGroupProperties(GroupNames.Transform)
                                    .GetValue(PropertyNames.X);
                                var componentX = 0.0;
                                if (Helper.IsDeserializable<WindowPosition>(componentSx))
                                    componentX = Helper.Deserialize<WindowPosition>(componentSx).ForWindow;
                                else if (Helper.IsNumber(componentSx))
                                    componentX = Helper.ConvertToDouble(componentSx) - 200;

                                var componentSy = _components[reports[p].Code!].GetGroupProperties(GroupNames.Transform)
                                    .GetValue(PropertyNames.Y);
                                var componentY = 0.0;
                                if (Helper.IsDeserializable<WindowPosition>(componentSy))
                                    componentY = Helper.Deserialize<WindowPosition>(componentSy).ForWindow;
                                else if (Helper.IsNumber(componentSy))
                                    componentY = Helper.ConvertToDouble(componentSy);

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
                                content.Children.Add(_components[reports[p].Code!].ComponentView);
                                Page.Children.Add(content);
                            }
                        }, null);
                    }
                    catch (Exception e)
                    {
                        failCounter++;
                        Console.WriteLine($"Error: {e.Message}");
                    }
                });
            }

            #endregion
        }

        public async void NewReport(bool isComponent = false)
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

            _project.ReportInfos.Add(
                new ReportInfoUiDto
                {
                    Name = name,
                    Code = code
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
            title.MouseEnter += OnMouseEnterHandle;
            title.PreviewMouseUp += OnPreviewMouseUp;
            title.MouseLeave += OnPreviewMouseLeave;
            title.PreviewMouseMove += OnPreviewMouseMove;

            content.Children.Add(title);
            content.Children.Add(windowModel.ComponentView);
            Page.Children.Add(content);

            var componentSerializer = windowModel.OnSerializer();
            var jsonString = JsonSerializer.Serialize(componentSerializer);

            await new CreateReportCommandHandler().Handle(new CreateReportCommand
            {
                ZipPath = _project.ZipPath,
                ProjectName = _project.Id,
                Report = new Domain.Entities.Report
                {
                    Name = code,
                    Json = jsonString
                }
            });

            await new SaveConfigCommandHandler().Handle(new SaveConfigCommand
            {
                ZipPath = _project.ZipPath,
                ProjectName = _project.Id,
                Json = JsonSerializer.Serialize(_project)
            });

            #endregion
        }

        public async void DeleteReport()
        {
            var result = await new DeleteReportCommandHandler().Handle(new DeleteReportCommand
            {
                ZipPath = _project.ZipPath,
                ProjectName = _project.Id,
                FileName = _project.ReportInfos[_selectedReport].Name,
                FileCode = _project.ReportInfos[_selectedReport].Code
            });
            if (!result.IsSuccess) return;

            var title = ((StackPanel)Page.Children[_selectedReport]).Children[0];
            title.MouseDown -= OnSelectedHandle;
            title.MouseEnter -= OnMouseEnterHandle;
            title.PreviewMouseUp -= OnPreviewMouseUp;
            title.MouseLeave -= OnPreviewMouseLeave;
            title.PreviewMouseMove -= OnPreviewMouseMove;

            _project.ReportInfos.RemoveAt(_selectedReport);
            _components.Remove(_project.ReportInfos[_selectedReport].Code!);
            Page.Children.RemoveAt(_selectedReport);

            if (Page.Children.Count > 0) _selectedReport = 0;

            await new SaveConfigCommandHandler().Handle(new SaveConfigCommand
            {
                ZipPath = _project.ZipPath,
                ProjectName = _project.Id,
                Json = JsonSerializer.Serialize(_project)
            });
        }

        public void AddComponent(string componentName)
        {
            if (!ComponentHelper.IsComponent(componentName)) return;

            var component = ComponentHelper.GetComponent(componentName);
            var compText = JsonSerializer.Serialize(component.OnSerializer());
            _components[_project.ReportInfos[_selectedReport].Code!].OnCopyOrPaste(compText, true);
        }

        public void AddReusableComponent(string id)
        {
            var component = _components.Values.First(c => c.Id == id);
            if (component == null! || component.Children.Count == 0 ||
                component.Children[0].Children.Count == 0) return;

            component.CheckIsExistId();
            var compText = JsonSerializer.Serialize(component.Children[0].Children[0].OnSerializer());
            _components[_project.ReportInfos[_selectedReport].Code!].OnCopyOrPaste(compText, true, true);
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
            _components[_project.ReportInfos[_selectedReport].Code!].OnUpdated(groupName, propertyName, value);
        }

        private void RefreshStructuralView()
        {
            var structuralElement = _components[_project.ReportInfos[_selectedReport].Code!].AddToStructuralView();

            StructuralView.Instance.Panel.Children.Clear();
            StructuralView.Instance.StructuralElement = structuralElement;
            StructuralView.Instance.BuildView(structuralElement, 0, structuralElement.IsSimpleElement);
            PanelStructuralView.Instance.Refresh();
        }

        public void SelectFromStructuralView()
        {
            _components[_project.ReportInfos[_selectedReport].Code!]
                .SelectFromStructuralView(StructuralView.Instance.StructuralElement);
        }

        public void OnCopyOrPaste(bool isPaste = false)
        {
            if (isPaste)
                _components[_project.ReportInfos[_selectedReport].Code!].OnCopyOrPaste(_copiedComponent, isPaste);
            else
                _copiedComponent = _components[_project.ReportInfos[_selectedReport].Code!]
                    .OnCopyOrPaste(isPaste: isPaste);
        }

        private void OnSelectedHandle(object sender)
        {
            var values = sender as Dictionary<string, dynamic>;

            foreach (var key in _components.Keys.Where(_ => !values!["selected"]))
                _components[key].OnUnselected();

            if (!values!["selected"]) return;

            foreach (var key in _components.Keys.Where(key => _components[key].OnChildSelected()))
                _selectedReport = _project.ReportInfos.FindIndex(r => r.Code == key);

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
            _components[_project.ReportInfos[_selectedReport].Code!].OnUnselected();
        }

        public object SendComponent()
        {
            List<ConceptorUi.ViewModels.Component> components = [];
            components.AddRange(from key in _components.Keys
                where _components[key].GetType().Name == nameof(ComponentModel)
                select _components[key]);

            return components;
        }

        public void OnSaved()
        {
            DisplayLoadingCommand?.Execute(true);
            var sc = SynchronizationContext.Current;
            ThreadPool.QueueUserWorkItem(delegate
            {
                #region

                var reports = new List<Domain.Entities.Report>();
                foreach (var key in _components.Keys)
                {
                    var componentSerializer = _components[key].OnSerializer();

                    var jsonString = JsonSerializer.Serialize(componentSerializer);
                    reports.Add(new Domain.Entities.Report { Name = key, Json = jsonString });
                }

                sc!.Post(async delegate
                {
                    await new SaveProjectCommandHandler().Handle(new SaveProjectCommand
                    {
                        ZipPath = ComponentHelper.ProjectPath!,
                        ProjectName = _project.Id,
                        Reports = reports
                    });

                    DisplayLoadingCommand?.Execute(false);
                }, null);

                #endregion
            });
        }

        private int NextReportIndex()
        {
            var n = _project.ReportInfos != null! ? _project.ReportInfos.Count : 0;
            var index = -1;
            for (var i = 1; i <= n; i++)
            {
                var found = false;
                foreach (var report in _project.ReportInfos!)
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
            Console.WriteLine($@"Selected Tag: {tag}");

            foreach (var component in _components)
            {
                if (component.Key == tag)
                {
                    _components[component.Key].OnUnselected();
                    _components[component.Key].OnSelected();

                    var point = e.GetPosition(Page);
                    _selectedKey = tag;
                    Console.WriteLine($@"Selected key 0: {_selectedKey}");
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
            Console.WriteLine($@"Selected key 1: {_selectedKey}");

            var componentSx = _components[_selectedKey!]
                .GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.X);
            var componentX = 0.0;
            if (Helper.IsDeserializable<WindowPosition>(componentSx))
                componentX = Helper.Deserialize<WindowPosition>(componentSx).ForMouse;
            else if (Helper.IsNumber(componentSx))
                componentX = Helper.ConvertToDouble(componentSx);

            var componentSy = _components[_selectedKey!]
                .GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Y);
            var componentY = 0.0;
            if (Helper.IsDeserializable<WindowPosition>(componentSy))
                componentY = Helper.Deserialize<WindowPosition>(componentSy).ForMouse;
            else if (Helper.IsNumber(componentSy))
                componentY = Helper.ConvertToDouble(componentSy);

            var child = Page.Children[_selectedReport] as StackPanel; //Source potentielle du problème
            var point = e.GetPosition(Page);

            var toRight = componentX < point.X;
            var toBottom = componentY < point.Y;

            var dx = Math.Abs(point.X - componentX);
            var dy = Math.Abs(point.Y - componentY);

            var x = toRight ? componentX + dx : componentX - dx;
            var y = toBottom ? componentY + dy : componentY - dy;

            var xn = toRight ? child!.Margin.Left + dx : child!.Margin.Left - dx;
            var yn = toBottom ? child.Margin.Top + dy : child.Margin.Top - dy;

            _components[_selectedKey!].SetPropertyValue(GroupNames.Transform, PropertyNames.X,
                JsonSerializer.Serialize(new WindowPosition { ForMouse = x, ForWindow = xn }));
            _components[_selectedKey!].SetPropertyValue(GroupNames.Transform, PropertyNames.Y,
                JsonSerializer.Serialize(new WindowPosition { ForMouse = y, ForWindow = yn }));

            child.Margin = new Thickness(xn, yn, 0, 0);
        }

        private void OnPageClickHandle(object sender, MouseButtonEventArgs e)
        {
            if (!e.OriginalSource.Equals(Page)) return;
            _components[_project.ReportInfos[_selectedReport].Code!].OnUnselected();
        }
    }
}