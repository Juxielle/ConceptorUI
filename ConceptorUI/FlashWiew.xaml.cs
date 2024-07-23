using ConceptorUI.Constants;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;


namespace ConceptorUI
{
    /// <summary>
    /// Logique d'interaction pour FlashWiew.xaml
    /// </summary>
    public partial class FlashWiew
    {
        public FlashWiew()
        {
            InitializeComponent();
            DoWork();
            //CreateFileStructure();
        }

        private bool CreateStructrure()
        {
            var b = false;
            var dirBase = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var dirInfo = new DirectoryInfo(dirBase + @"\UIConceptor");
            if (!dirInfo.Exists)
            {
                var dib = Directory.CreateDirectory(dirBase + @"\UIConceptor");
                if (dib.Exists)
                {
                    var dic = Directory.CreateDirectory(dirBase + @"\UIConceptor\Configs");
                    Directory.CreateDirectory(dirBase + @"\UIConceptor\Projects");
                    Directory.CreateDirectory(dirBase + @"\UIConceptor\Medias");
                    Directory.CreateDirectory(dirBase + @"\UIConceptor\Icons");
                    Directory.CreateDirectory(dirBase + @"\UIConceptor\Fonts");
                    if (dic.Exists)
                    {
                        File.Create(dirBase + @"\UIConceptor\Configs\config.json").Dispose();
                    }
                }
            }
            else
            {

            }
            return b;
        }

        public async void CreateFileStructure()
        {
            var finish = await Task.Run(() => CreateStructrure());

            var dirBase = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            var ls = new List<WeatherForecast>();
            for (var i = 0; i < 20; i++)
            {
                ls.Add(new WeatherForecast
                {
                    Date = DateTime.Parse("2019-08-01"),
                    TemperatureCelsius = 25,
                    Summary = "Hot"
                });
            }

            var fileName = dirBase + @"\UIConceptor\Configs\config.json";
            var jsonString = JsonSerializer.Serialize(ls);
            File.WriteAllText(fileName, jsonString);
            //Console.WriteLine(File.ReadAllText(fileName));

            var zipName = dirBase + @"\UIConceptor.zip";
            //if (File.Exists(zipName))
            //{
            //    File.Delete(zipName);
            //}
            //ZipFile.CreateFromDirectory(dirBase, zipName);

            var delayTimer = new System.Timers.Timer();
            delayTimer.Interval = 5000;
            delayTimer.Elapsed += (o, e) => {
                
            };
            delayTimer.Start();
            new PreviewPage().Show();
            this.Close();
        }

        private void DoWork()
        {
            var pv = new PreviewModel();
            var sc = SynchronizationContext.Current;
            ThreadPool.QueueUserWorkItem(delegate
            {
                var dirBase = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (!new DirectoryInfo(dirBase + @"\UIConceptor").Exists)
                {
                    Directory.CreateDirectory(dirBase + @"\UIConceptor");
                    Directory.CreateDirectory(dirBase + @"\UIConceptor\Configs");
                    Directory.CreateDirectory(dirBase + @"\UIConceptor\Projects");
                    Directory.CreateDirectory(dirBase + @"\UIConceptor\Medias");
                    Directory.CreateDirectory(dirBase + @"\UIConceptor\Icons");
                    Directory.CreateDirectory(dirBase + @"\UIConceptor\Fonts");
                    File.Create(dirBase + @"\UIConceptor\Configs\config.json").Dispose();
                    sc!.Post(delegate
                    {
                        pv = new PreviewModel
                        {
                            RecentApps = new ObservableCollection<Applicat>(),
                            LocalApps = new ObservableCollection<Applicat>(),
                            OnLineApps = new ObservableCollection<Applicat>(),
                            DataDetails = new Applicat()
                        };
                        File.WriteAllText(
                            dirBase + @"\UIConceptor\Configs\config.json",
                            JsonSerializer.Serialize(pv)
                        );
                    }, null);
                }
                else
                {
                    var dircf = new DirectoryInfo(dirBase + @"\UIConceptor\Configs");
                    var dirpj = new DirectoryInfo(dirBase + @"\UIConceptor\Projects");
                    var dirmd = new DirectoryInfo(dirBase + @"\UIConceptor\Medias");
                    if(!dircf.Exists)
                    {
                        var dic = Directory.CreateDirectory(dirBase + @"\UIConceptor\Configs");
                    }
                    if (!dirpj.Exists)
                    {
                        Directory.CreateDirectory(dirBase + @"\UIConceptor\Projects");
                    }
                    if (!dirmd.Exists)
                    {
                        Directory.CreateDirectory(dirBase + @"\UIConceptor\Medias");
                    }
                }
                sc!.Post(delegate
                {
                    var fileName = dirBase + @"\UIConceptor\Configs\config.json";
                    if (File.Exists(fileName))
                    {
                        pv = JsonSerializer.Deserialize<PreviewModel>(File.ReadAllText(fileName))!;
                        for (int i = 0; i < pv.LocalApps!.Count; i++)
                        {
                            pv.LocalApps[i].Image = $@"{Env.dirMedia}\{pv.LocalApps![i].Image}";
                        }
                    }
                    PreviewPage.Instance.Show(pv);
                    this.Close();
                }, null);
            });
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }

    public class WeatherForecast
    {
        public DateTimeOffset Date { get; set; }
        public int TemperatureCelsius { get; set; }
        public string? Summary { get; set; }
    }
}
