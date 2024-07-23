using ConceptorUI.Constants;
using ConceptorUI.Models;
using ConceptorUI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace ConceptorUI.Views.ComponentP
{
    /// <summary>
    /// Logique d'interaction pour PageView.xaml
    /// </summary>
    public partial class PageView
    {
        private static PageView? _obj;
        private WindowModel window;
        public Applicat application;

        public PageView()
        {
            InitializeComponent();
            _obj = this;
            var manageEnums = new ManageEnums();
            var configuredComps = new ConfiguredComps();

            application = PreviewPage.CurrentApp!;
            #region Init Space
            var configFile = Env.configPFile($"Project{application.ID}", "config.json");
            Properties.Instance.ConfigAppInfo = File.Exists(configFile) ? JsonSerializer.Deserialize<ConfigAppInfo>(File.ReadAllText(configFile))! : new ConfigAppInfo();
            
            if (Properties.Instance.ConfigAppInfo.Spaces != null!)
            {
                foreach (var space in Properties.Instance.ConfigAppInfo.Spaces)
                {
                    if(space.Reports.Count <= 0) continue;
                    LoadSpace(0);
                    break;
                }
            }
            
            if(Properties.Instance.ConfigAppInfo.Spaces == null || Properties.Instance.ConfigAppInfo.Spaces!.Count <= 0)
            {
                NewSpace();
            }
            #endregion
            //Refresh();
        }

        public static PageView Instance => _obj == null! ? new PageView() : _obj;
        
        public void Refresh(bool added = true)
        {
            page.Children.Clear();
            //Properties.Instance.ReportMNS[Properties.Instance.SelectedReport].OnSelected();
            //page.Children.Add(Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportMns[Properties.Instance.SelectedReport].ComponentView!);
            LoadSpace(Properties.Instance.SelectedSpace);
            Console.WriteLine(@"Selected Space in refresh: "+ Properties.Instance.SelectedSpace);
            
            var structuralElement = Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportMns[Properties.Instance.SelectedReport].AddToStructuralView();
            
            StructuralView.Instance.Panel.Children.Clear();
            StructuralView.Instance.StructuralElement = structuralElement;
            StructuralView.Instance.BuildView(structuralElement, 0, structuralElement.IsSimpleElement);
            PanelStructuralView.Instance.Refresh();
        }

        public void LoadSpace(int spaceIndex)
        {
            #region Load spage
            page.Children.Clear();
            
            Properties.Instance.SpaceReports.Add(new SpaceReportModel
            {
                Name = Properties.Instance.ConfigAppInfo.Spaces[spaceIndex].Name!,
                Code = Properties.Instance.ConfigAppInfo.Spaces[spaceIndex].Code!,
                Date = Properties.Instance.ConfigAppInfo.Spaces[spaceIndex].Date,
                ReportModels = new List<ReportModel>(),
                ReportMns = new List<WindowModel>()
            });
            
            foreach (var report in Properties.Instance.ConfigAppInfo.Spaces[spaceIndex].Reports)
            {
                var fileName = Env.pemcFile($"Project{PreviewPage.CurrentApp!.ID}", "Pages", $"{report.Code}.json");
                if (!File.Exists(fileName)) continue;
                
                var pagem = new WindowModel(true);
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
                content.Children.Add(pagem.ComponentView!);
                page.Children.Add(content);
                
                var sc = SynchronizationContext.Current;
                ThreadPool.QueueUserWorkItem(delegate
                {
                    var jsonString = File.ReadAllText(fileName);
                    var component = JsonSerializer.Deserialize<ReportModel>(jsonString)!;
                    
                    Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportModels.Add(new ReportModel
                    {
                        Name = report.Name,
                        Code = report.Code,
                        Date = report.Date
                    });
                    
                    sc!.Post(delegate
                    {
                        pagem.OnDeserialiser(component.Report!);
                    }, null);
                    Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportMns.Add(pagem);
                });
            }
            #endregion
        }

        public void NewSpace()
        {
            #region Adding new Space
            var pagem = new WindowModel();
            if (Properties.Instance.ConfigAppInfo.Spaces == null!)
                Properties.Instance.ConfigAppInfo.Spaces = new List<SpaceModel>();
            Properties.Instance.SpaceReports.Add(
                new SpaceReportModel
                {
                    Name = "Space "+ (Properties.Instance.ConfigAppInfo.Spaces.Count + 1),
                    Code = "space"+ (Properties.Instance.ConfigAppInfo.Spaces.Count + 1),
                    Date = DateTime.Now,
                    ReportModels = new List<ReportModel>(),
                    ReportMns = new List<WindowModel>()
                }
            );
            
            Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportModels.Add(
                new ReportModel {
                    Name = $"ReportSpace{(Properties.Instance.ConfigAppInfo.Spaces.Count + 1)} 1",
                    Code = $"report_space{(Properties.Instance.ConfigAppInfo.Spaces.Count + 1)}1",
                    Report = pagem.OnSerialiser(),
                    Date = DateTime.Now
                }
            );
            
            Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportMns.Add(pagem);
                
            var content = new StackPanel
            {
                Width = 400,
                Margin = new Thickness(0, 0, 0, 30)
            };
            var title = new TextBlock
            {
                Text = "Space "+ (Properties.Instance.ConfigAppInfo.Spaces.Count + 1),
                FontSize = 14,
                Margin = new Thickness(0, 0, 0, 6),
                Foreground = new BrushConverter().ConvertFrom("#666666") as SolidColorBrush,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            content.Children.Add(title);
            content.Children.Add(pagem.ComponentView!);
            page.Children.Add(content);

            if (Properties.Instance.ConfigAppInfo.Spaces == null!)
                Properties.Instance.ConfigAppInfo.Spaces = new List<SpaceModel>();
            
            Properties.Instance.ConfigAppInfo.Spaces.Add
            (
                new SpaceModel
                {
                    Name = "Space "+ (Properties.Instance.ConfigAppInfo.Spaces.Count + 1),
                    Code = "space"+ (Properties.Instance.ConfigAppInfo.Spaces.Count + 1),
                    Date = DateTime.Now,
                    Reports = new List<ReportInfo>
                    {
                        new ()
                        {
                            Name = Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportModels[
                                Properties.Instance.SelectedReport].Name!,
                            Code = Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportModels[
                                Properties.Instance.SelectedReport].Code!,
                            Date = Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportModels[
                                Properties.Instance.SelectedReport].Date!
                        }
                    }
                }
            );

            OnSaved();
            OnSaved(2);
            #endregion
        }
        
        public void NewReport()
        {
            #region Adding new Report
            var pagem = new WindowModel();
            var indexReport = NextReportIndex();
            
            var name = $"ReportSpace{Properties.Instance.SelectedSpace + 1} {indexReport}";
            var code = $"report_space{Properties.Instance.SelectedSpace + 1}{indexReport}";
            var date = DateTime.Now;
            var index = Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportModels.Count;

            Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportModels.Add(
                new ReportModel {
                    Name = name,
                    Code = code,
                    Report = pagem.OnSerialiser(),
                    Date = date
                }
            );
            
            Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportMns.Add(pagem);
            
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
            content.Children.Add(pagem.ComponentView!);
            page.Children.Add(content);

            Properties.Instance.ConfigAppInfo.Spaces[Properties.Instance.SelectedSpace].Reports.Add
            (
                new ReportInfo
                {
                    Name = name,
                    Code = code,
                    Date = date
                }
            );

            OnSaved(0, index);
            OnSaved(2);
            #endregion
        }

        public void DeleteReport()
        {
            Console.WriteLine(@"SelectedSpace: " + Properties.Instance.SelectedSpace);
            Console.WriteLine(@"SelectedSpace code: " + Properties.Instance.ConfigAppInfo.Spaces[Properties.Instance.SelectedSpace].Code);
            Console.WriteLine(@"SelectedReport: " + Properties.Instance.SelectedReport);
            Console.WriteLine(@"ConfigAppInfo Space Report count: " + Properties.Instance.ConfigAppInfo.Spaces[Properties.Instance.SelectedSpace].Reports.Count);
            
            Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportMns.RemoveAt(Properties.Instance.SelectedReport);
            File.Delete(Env.pemcFile($"Project{application.ID}", "Pages",
                                $"{Properties.Instance.ConfigAppInfo.Spaces[Properties.Instance.SelectedSpace].Reports[Properties.Instance.SelectedReport].Code!}.json"));
            Properties.Instance.ConfigAppInfo.Spaces[Properties.Instance.SelectedSpace].Reports.RemoveAt(Properties.Instance.SelectedReport);
            Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportModels.RemoveAt(Properties.Instance.SelectedReport);
            
            var n = page.Children.Count;
            page.Children.RemoveAt(Properties.Instance.SelectedReport);
            
            if (n > 1)
            {
                //Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportMns[0].OnSelected();
                Properties.Instance.SelectedReport = 0;
            }

            OnSaved(2);
        }

        public void AddComponent(int id, ComponentList name)
        {
            if (Properties.Instance.SelectedLeftOnglet == 1)
            {
                Properties.InitComps(name);
                Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportMns[Properties.Instance.SelectedReport].OnConfiguOut(id);
            }
            if (Properties.Instance.SelectedLeftOnglet == 2)
            {
                Properties.InitComps(name);
                Properties.Instance.ComponentMNS[Properties.Instance.SelectedComponent].OnConfiguOut(id);
            }
        }

        public void setProperty(GroupNames groupName, PropertyNames propertyName, string value)
        {
            if (Properties.Instance.SelectedLeftOnglet == 1)
                Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportMns[Properties.Instance.SelectedReport].OnUpdated(groupName, propertyName, value);
            else if (Properties.Instance.SelectedLeftOnglet == 2)
                Properties.Instance.ComponentMNS[Properties.Instance.SelectedComponent].OnUpdated(groupName, propertyName, value);
        }

        public void OnUnselected(bool isInterne = false)
        {
            switch (Properties.Instance.SelectedLeftOnglet)
            {
                case 1:
                    for (var i = 0; i < Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportMns.Count; i++)
                    {
                        Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportMns[i].OnUnselected(isInterne);
                    }
                    break;
                case 2:
                    Properties.Instance.ComponentMNS[Properties.Instance.SelectedComponent].OnUnselected(isInterne);
                    break;
            }
        }

        public void OnSelected()
        {
            for (var i = 0; i < Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportMns.Count; i++)
            {
                if (Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportMns[i].OnChildSelected())
                    Properties.Instance.SelectedReport = i;
            }
        }

        public void IsDeepSelect()
        {
            
        }

        public void RefreshStructuralView()
        {
            var structuralElement = Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportMns[Properties.Instance.SelectedReport].AddToStructuralView();
            
            StructuralView.Instance.Panel.Children.Clear();
            StructuralView.Instance.StructuralElement = structuralElement;
            StructuralView.Instance.BuildView(structuralElement, 0, structuralElement.IsSimpleElement);
            PanelStructuralView.Instance.Refresh();
        }

        public void SelectFromStructuralView()
        {
            Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].
                ReportMns[Properties.Instance.SelectedReport].SelectFromStructuralView(StructuralView.Instance.StructuralElement);
        }

        public void OnCopyOrPaste(bool isPaste = false)
        {
            if (Properties.Instance.SelectedLeftOnglet == 1)
            {
                if(isPaste)
                    Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].
                        ReportMns[Properties.Instance.SelectedReport].OnCopyOrPaste(Properties.CopiedComponent, isPaste);
                else
                    Properties.CopiedComponent =
                        Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].
                            ReportMns[Properties.Instance.SelectedReport].OnCopyOrPaste(isPaste: isPaste);
            }
            else if (Properties.Instance.SelectedLeftOnglet == 2)
            {
                if (isPaste)
                    Properties.Instance.ComponentMNS[Properties.Instance.SelectedComponent].OnCopyOrPaste(Properties.CopiedComponent, isPaste);
                else
                    Properties.CopiedComponent =
                        Properties.Instance.ComponentMNS[Properties.Instance.SelectedComponent].OnCopyOrPaste(isPaste: isPaste);
            }
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
                            Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportModels[index].Report = 
                                Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportMns[index].OnSerialiser();
                            File.Create(Env.pemcFile($"Project{application.ID}", "Pages",
                                $"{Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportModels[index].Code!}.json")).Dispose();
                            var jsonString = JsonSerializer.Serialize(Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportModels[index]);
                            File.WriteAllText(
                                Env.pemcFile($"Project{application.ID}", "Pages",
                                    $"{Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportModels[index].Code!}.json"),
                                jsonString
                            );
                            break;
                        }
                        case 1:
                        {
                            Properties.Instance.Components[index].Report = Properties.Instance.ComponentMNS[index].OnSerialiser();
                            File.Create(Env.pemcFile($"Project{application.ID}", "Components",
                                $"{Properties.Instance.Components[index].Code!}.json")).Dispose();
                            string jsonString = JsonSerializer.Serialize(Properties.Instance.Components[index]);
                            File.WriteAllText(
                                Env.pemcFile($"Project{application.ID}", "Components",
                                    $"{Properties.Instance.Components[index].Code!}.json"),
                                jsonString
                            );
                            break;
                        }
                        case 2:
                        {
                            File.Create(Env.configPFile($"Project{application.ID}", "config.json")).Dispose();
                            string jsonString = JsonSerializer.Serialize(Properties.Instance.ConfigAppInfo);
                            File.WriteAllText(
                                Env.configPFile($"Project{application.ID}", "config.json"),
                                jsonString
                            );
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
                        var pm = JsonSerializer.Deserialize<ReportModel>(
                            File.ReadAllText(Env.pemcFile($"Project{application.ID}", "Pages", $"{fileName}.json"))
                        );
                        Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportModels.Add(pm!);
                        var pagem = new WindowModel(true);
                        pagem.OnDeserialiser(pm!.Report!);
                        Properties.Instance.SpaceReports[Properties.Instance.SelectedSpace].ReportMns.Add(pagem);
                        break;
                    }
                    case 1:
                    {
                        var component = JsonSerializer.Deserialize<ReportModel>(
                            File.ReadAllText(Env.pemcFile($"Project{application.ID}", "Components", $"{fileName}.json"))
                        );
                        Properties.Instance.Components.Add(component!);
                        var componentm = new ComponentModel(true);
                        componentm.OnDeserialiser(component!.Report!);
                        Properties.Instance.ComponentMNS.Add(componentm);
                        break;
                    }
                    case 2:
                        Properties.Instance.ConfigAppInfo = JsonSerializer.Deserialize<ConfigAppInfo>(File.ReadAllText(
                            Env.configPFile($"Project{application.ID}", "config.json")
                        ))!;
                        break;
                }
            });
            #endregion
        }

        private static int NextReportIndex()
        {
            var n = Properties.Instance.ConfigAppInfo.Spaces[Properties.Instance.SelectedSpace].Reports.Count;
            var indexSpace = Properties.Instance.SelectedSpace + 1;
            var index = -1;
            for(var i = 1; i <= n; i++)
            {
                var found = false;
                foreach(var report in Properties.Instance.ConfigAppInfo.Spaces[indexSpace - 1].Reports)
                {
                    Console.WriteLine("report code out: " + report.Code + "target report code: "+ ($"report_space{indexSpace}{i}"));
                    if ($"report_space{indexSpace}{i}" == report.Code)
                    {
                        Console.WriteLine("report code in: "+ report.Code);
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    index = i;
                    break;
                }
            }

            return index == -1 ? n + 1 : index;
        }

        public static void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("You clicked me at ");
        }
    }
}
