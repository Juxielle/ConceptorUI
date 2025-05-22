using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ConceptorUI.Enums;
using ConceptorUI.Senders;
using ConceptorUi.ViewModels;
using ConceptorUI.ViewModels.Components;
using ConceptorUi.ViewModels.Operations;
using ConceptorUI.ViewModels.ReusableComponent;
using ConceptorUI.ViewModels.Window;
using ConceptorUI.Views.Modals;
using UiDesigner;
using UiDesigner.Application.Configs;
using UiDesigner.Application.Dto.UiDto;
using UiDesigner.Application.Project;
using UiDesigner.Application.Reports;
using UiDesigner.Classes;
using UiDesigner.Enums;
using UiDesigner.Inputs;
using UiDesigner.Models;
using UiDesigner.Utils;
using UiDesigner.Views.Component;

namespace ConceptorUI.Views.Component
{
    public partial class PageView
    {
        private static PageView? _obj;

        private ProjectUiDto _project;
        private readonly Dictionary<string, ConceptorUI.ViewModels.Components.Component> _components;
        private int _selectedReport;
        private string? _selectedKey;

        private string _copiedComponent;

        public ICommand? RefreshPropertyPanelCommand;
        public ICommand? DisplayLoadingCommand;

        /*
            A. Mécanisme d'annulation et restauration des actions
                1. Structure de données:
                    - Id du composant sur lequel l'action est effectuée
                    - Nom du groupe de propriété
                    - Nom de la propriété
                    - Valeur de la propriété
                2. On peut effectuer des enregistrements individuels ou par groupe

            B. Reussir la multi-sélection des composants
                1. Il suffit de dire au composant à sélectionner que la multi-sélection est activée, afin qu'il ne
                    desélectionne pas les autres;
                2. Créer une fonction capable de faire l'intersection entre les propriétés des composants;

            C. Sélection, desélection et déplacement par balayage;
            D. Composant reutilisable;
            E. Margin et Padding horizontal/vertical;
            F. Déplacement des composants avec la souris;
            G. Drag and Drop des composants;
            H. Fichier multi-onglets;

            I. Remplacement des mises en page:
                - Définir les types de composants;
                - Définir les critères de remplacement entre mises en page;
         */
        public PageView()
        {
            InitializeComponent();

            _obj = this;
            var manageEnums = new ManageEnums();
            _project = new ProjectUiDto
            {
                ReportInfos = []
            };

            _components = new Dictionary<string, ConceptorUI.ViewModels.Components.Component>();
            _selectedReport = 0;

            _copiedComponent = string.Empty;
            TextContextMenu.Command = new RelayCommand(OnChangeText);
        }

        public static PageView Instance => _obj == null! ? new PageView() : _obj;

        public async void Refresh(object projectObject)
        {
            var projectInfoUiDto = (projectObject as ProjectInfoUiDto)!;

            _project = new ProjectUiDto
            {
                ZipPath = projectInfoUiDto.ZipPath,
                Id = projectInfoUiDto.Id,
                UniqueId = projectInfoUiDto.UniqueId,
                Name = projectInfoUiDto.Name,
                Image = projectInfoUiDto.Image,
                Created = projectInfoUiDto.Created,
                Updated = projectInfoUiDto.Updated,
                ReportInfos = []
            };

            ComponentHelper.ProjectPath = projectInfoUiDto.ZipPath;
            ComponentHelper.ProjectName = projectInfoUiDto.Id;
            ComponentHelper.ProjectTempId = projectInfoUiDto.UniqueId;

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
                            ConceptorUI.ViewModels.Components.Component windowModel;

                            if (component.Name == ComponentList.Window.ToString())
                                windowModel = new WindowModel(true);
                            else windowModel = new ComponentModel(true);

                            windowModel.SelectedCommand = new RelayCommand(OnSelectedHandle);
                            new RelayCommand(OnRefreshPropertyPanelHandle);
                            new RelayCommand(OnRefreshStructuralViewHandle);

                            _components.Add(reports[k].Code!, windowModel);

                            windowModel.OnDeserializer(component);

                            counter++;
                            //Récupérer uniquement les éléments bien serialisés
                            if (counter != reports.Count - counter1) return;

                            DisplayLoadingCommand?.Execute(false);

                            for (var p = 0; p < reports.Count - counter1; p++)
                            {
                                if (!_components.ContainsKey(reports[p].Code!)) continue;

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

                                var content = new Border
                                {
                                    Tag = reports[p].Code,
                                    Background = new BrushConverter().ConvertFrom("#e4e4e4") as SolidColorBrush,
                                };
                                content.MouseDown += OnSelectedHandle;
                                content.MouseEnter += OnMouseEnterHandle;
                                content.PreviewMouseUp += OnPreviewMouseUp;
                                content.MouseLeave += OnPreviewMouseLeave;
                                content.PreviewMouseMove += OnPreviewMouseMove;

                                _components[reports[p].Code!].ComponentView.Margin = new Thickness(15);

                                var grid = new Grid
                                {
                                    Tag = reports[p].Code,
                                    VerticalAlignment = VerticalAlignment.Top,
                                    HorizontalAlignment = HorizontalAlignment.Left,
                                    Margin = new Thickness(componentX, componentY, 0, 30),
                                };
                                grid.Children.Add(content);
                                grid.Children.Add(_components[reports[p].Code!].ComponentView);
                                Page.Children.Add(grid);
                            }
                        }, null);
                    }
                    catch (Exception)
                    {
                        failCounter++;
                    }
                });
            }

            AddSizeToScroll();

            #endregion
        }

        public async void NewReport(bool isComponent = false)
        {
            #region Adding new Report

            ConceptorUI.ViewModels.Components.Component windowModel;
            if (isComponent) windowModel = new ComponentModel();
            else windowModel = new WindowModel();

            windowModel.SelectedCommand = new RelayCommand(OnSelectedHandle);
            new RelayCommand(OnRefreshPropertyPanelHandle);
            new RelayCommand(OnRefreshStructuralViewHandle);

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

            var content = new Border
            {
                Tag = code,
                Background = new BrushConverter().ConvertFrom("#e4e4e4") as SolidColorBrush,
            };
            content.MouseDown += OnSelectedHandle;
            content.MouseEnter += OnMouseEnterHandle;
            content.PreviewMouseUp += OnPreviewMouseUp;
            content.MouseLeave += OnPreviewMouseLeave;
            content.PreviewMouseMove += OnPreviewMouseMove;

            windowModel.ComponentView.Margin = new Thickness(15);
            var point = GetNewPagePosition(300, 100);

            var grid = new Grid
            {
                Tag = code,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(point.X, point.Y, 0, 30),
            };
            grid.Children.Add(content);
            grid.Children.Add(windowModel.ComponentView);
            Page.Children.Add(grid);
            AddSizeToScroll();

            var componentSerializer = windowModel.OnSerializer();
            var jsonString = JsonSerializer.Serialize(componentSerializer);

            var result = await new CreateReportCommandHandler().Handle(new CreateReportCommand
            {
                ZipPath = ComponentHelper.ProjectPath!,
                ProjectName = _project.Id,
                Report = new UiDesigner.Domain.Entities.Report
                {
                    Name = code,
                    Json = jsonString
                }
            });

            if (result.IsFailure)
                return;

            await new SaveConfigCommandHandler().Handle(new SaveConfigCommand
            {
                ZipPath = ComponentHelper.ProjectPath!,
                ProjectName = _project.Id,
                Json = JsonSerializer.Serialize(_project)
            });

            #endregion
        }

        public async void DeleteReport()
        {
            var result = await new DeleteReportCommandHandler().Handle(new DeleteReportCommand
            {
                ZipPath = ComponentHelper.ProjectPath!,
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

            var index = -1;
            for (var i = 0; i < Page.Children.Count; i++)
                if (_project.ReportInfos[_selectedReport].Code == ((StackPanel)Page.Children[i]).Tag.ToString())
                    index = i;

            if (index == -1) return;

            Page.Children.RemoveAt(index);

            _components.Remove(_project.ReportInfos[_selectedReport].Code!);
            _project.ReportInfos.RemoveAt(_selectedReport);

            if (Page.Children.Count > 0) _selectedReport = 0;

            await new SaveConfigCommandHandler().Handle(new SaveConfigCommand
            {
                ZipPath = ComponentHelper.ProjectPath!,
                ProjectName = _project.Id,
                Json = JsonSerializer.Serialize(_project)
            });
        }

        public void ChangeScreen(object screen)
        {
            foreach (var key in _components.Keys)
            {
                if (_components[key].Name != ComponentList.Window) continue;
                ((WindowModel)_components[key]).ChangeScreen(screen);
            }
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
                }
            }
            catch (Exception)
            {
                //
            }
        }

        public void SetProperty(GroupNames groupName, PropertyNames propertyName, string value)
        {
            _components[_project.ReportInfos[_selectedReport].Code!]
                .OnUpdated(groupName, propertyName, value, allowSavingAction: true);
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
            var customSender = sender as SelectComponentSender;

            foreach (var key in _components.Keys.Where(_ =>
                         customSender!.SelectComponentAction == SelectComponentActions.Unselect))
                _components[key].OnUnselected();

            if (customSender!.SelectComponentAction == SelectComponentActions.Unselect) return;

            foreach (var key in _components.Keys.Where(key => _components[key].OnChildSelected()))
                _selectedReport = _project.ReportInfos.FindIndex(r => r.Code == key);

            RefreshPropertyPanelCommand?.Execute(customSender);

            RefreshStructuralView();

            if (customSender.SelectComponentAction == SelectComponentActions.DoubleClick)
            {
                if (customSender.ComponentName == ComponentList.Text)
                {
                    TextContextMenu.Sender = customSender.PropertyGroups;
                    TextContextMenu.IsOpen = true;
                }
            }
        }

        private void OnChangeText(object sender)
        {
            var infos = sender as dynamic[];
            if (infos?.Length < 3) return;
            SetProperty((GroupNames)infos![0], (PropertyNames)infos[1], infos[2]);
        }

        private void OnRefreshPropertyPanelHandle(object sender)
        {
        }

        private void OnRefreshStructuralViewHandle(object sender)
        {
        }

        public void OnUnSelect()
        {
            if (!_components.ContainsKey(_project.ReportInfos[_selectedReport].Code!)) return;
            _components[_project.ReportInfos[_selectedReport].Code!].OnUnselected();
        }

        public object SendComponent()
        {
            List<ConceptorUI.ViewModels.Components.Component> components = [];
            components.AddRange(from key in _components.Keys
                where _components[key].GetType().Name == nameof(ComponentModel)
                select _components[key]);

            return components;
        }

        public void OnSaved()
        {
            DisplayLoadingCommand?.Execute(true);
            RefreshReusableComponent();

            var sc = SynchronizationContext.Current;
            ThreadPool.QueueUserWorkItem(delegate
            {
                #region

                var reports = new List<UiDesigner.Domain.Entities.Report>();
                foreach (var key in _components.Keys)
                {
                    var componentSerializer = _components[key].OnSerializer();

                    var jsonString = JsonSerializer.Serialize(componentSerializer);
                    reports.Add(new UiDesigner.Domain.Entities.Report { Name = key, Json = jsonString });
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
            var content = (FrameworkElement)sender;
            content.Cursor = Cursors.SizeAll;
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
            var content = (FrameworkElement)sender;
            if (_selectedKey != content.Tag.ToString()) return;

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

            var index = -1;
            for (var i = 0; i < Page.Children.Count; i++)
                if (_selectedKey == ((FrameworkElement)Page.Children[i]).Tag.ToString())
                    index = i;

            if (index == -1) return;

            var child = Page.Children[index] as FrameworkElement; //Source potentielle du problème
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

        private void ZoomIn(double value)
        {
            ScaleTransform.ScaleX *= value;
            ScaleTransform.ScaleY *= value;
        }

        private void ZoomOut(double value)
        {
            ScaleTransform.ScaleX /= value;
            ScaleTransform.ScaleY /= value;
        }

        private void OnScreenChanged(object sender)
        {
            ChangeScreen(sender);
        }

        private void CreateComponent(bool isComponent = false)
        {
            var destination = isComponent ? "de la page" : "du composant";
            new ConfirmDialogBox(
                "Confirmation",
                $"Confirmer la création {destination}",
                AlertType.Confirm,
                () => NewReport(isComponent)
            ).ShowDialog();
        }

        public override void GetTransferData(object sender, object data)
        {
            if (data == null! || data is not PropertySender propertySender) return;

            if (propertySender.SenderAction == SenderAction.UpdatePropertyVisibility)
            {
                _components[_project.ReportInfos[_selectedReport].Code!]
                    .SetComponentVisibility(propertySender.GroupName, propertySender.propertyName,
                        propertySender.Value == VisibilityValue.Visible.ToString());
            }
            else if (propertySender.SenderAction == SenderAction.ZoomIn)
            {
                ZoomIn(1.2);
            }
            else if (propertySender.SenderAction == SenderAction.ZoomOut)
            {
                ZoomOut(1.2);
            }
            else if (propertySender.SenderAction == SenderAction.Save)
            {
                OnSaved();
            }
            else if (propertySender.SenderAction == SenderAction.AddPage)
            {
                CreateComponent();
            }
            else if (propertySender.SenderAction == SenderAction.AddComponent)
            {
                CreateComponent(true);
            }
            else if (propertySender.SenderAction == SenderAction.Refresh)
            {
                RefreshReusableComponent();
            }
            else if (propertySender.SenderAction == SenderAction.Delete)
            {
                DeleteReport();
            }
            else if (propertySender.SenderAction == SenderAction.Screens)
            {
                var screenModal = new ScreenModal
                {
                    ScreenChangedCommand = new RelayCommand(OnScreenChanged)
                };
                screenModal.ShowDialog();
            }
            else if (propertySender.SenderAction == SenderAction.CancelAction &&
                     ComponentHelper.UndoActions.Count > 0)
            {
                var index = ComponentHelper.UndoActions.Count - 1;

                ComponentHelper.UndoActions[index].Instance?.OnUpdated(
                    groupName: ComponentHelper.UndoActions[index].PreviousAction.GroupName,
                    propertyName: ComponentHelper.UndoActions[index].PreviousAction.PropertyName,
                    value: ComponentHelper.UndoActions[index].PreviousAction.Value,
                    allow: true
                );

                ComponentHelper.RedoActions.Add(ComponentHelper.UndoActions[index]);
                ComponentHelper.UndoActions.RemoveAt(index);
            }
            else if (propertySender.SenderAction == SenderAction.RestoreAction &&
                     ComponentHelper.RedoActions.Count > 0)
            {
                var index = ComponentHelper.RedoActions.Count - 1;

                ComponentHelper.RedoActions[index].Instance?.OnUpdated(
                    groupName: ComponentHelper.RedoActions[index].CurrentAction.GroupName,
                    propertyName: ComponentHelper.RedoActions[index].CurrentAction.PropertyName,
                    value: ComponentHelper.RedoActions[index].CurrentAction.Value,
                    allow: true
                );

                ComponentHelper.UndoActions.Add(ComponentHelper.RedoActions[index]);
                ComponentHelper.RedoActions.RemoveAt(index);
            }
        }

        private Point GetNewPagePosition(double previewWidth, double previewHeight)
        {
            //Chercher l'espace libre le plus proche
            double x = 0, y = 0;
            List<Point> emptySpaces = [];
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    var found = false;
                    foreach (var child in Page.Children)
                    {
                        var page = child as FrameworkElement;
                        var dx = page?.Margin.Left;
                        var dy = page?.Margin.Top;
                        var dw = page?.Width;
                        var dh = page?.Height;
                        if ((x >= dx && x + previewWidth <= dx + dw) &&
                            (y >= dy && y + previewHeight <= dy + dh))
                            found = true;
                    }
                    if (!found)
                    {
                        emptySpaces.Add(new Point(x, y));
                        break;
                    }
                    x = (j + 1) * (previewWidth + 40);
                }
                y = (i + 1) * (previewHeight + 40);
            }
            
            if(emptySpaces.Count == 0)
                return new Point(0, 0);
            Console.WriteLine(JsonSerializer.Serialize(emptySpaces));
            x = emptySpaces[0].X;
            y = emptySpaces[0].Y;
            foreach (var emptySpace in emptySpaces)
            {
                if (emptySpace.X < x && emptySpace.Y < y)
                {
                    x = emptySpace.X;
                    y = emptySpace.Y;
                }
            }
            
            return new Point(x, y);
        }
        
        private void AddSizeToScroll()
        {
            double w = 0, h = 0;
            foreach (var child in Page.Children)
            {
                var page = child as FrameworkElement;
                var dx = page?.Margin.Left;
                var dy = page?.Margin.Top;
                var dW = page?.Width;
                var dH = page?.Height;
                if (dx + dW > w)
                    w = (double)(dx + dW)!;
                if (dy + dH > h)
                    h = (double)(dy + dH)!;
            }

            Page.Width = 2000 + w;
            Page.Height = 2000 + h;
        }
    }
}