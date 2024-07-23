using ConceptorUI.Constants;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace ConceptorUI
{
    /// <summary>
    /// Logique d'interaction pour PreviewPage.xaml
    /// </summary>
    public partial class PreviewPage
    {
        private static PreviewPage? _obj;
        private string name;
        private string version;
        private string appID;
        private string password;
        private string rpassword;
        private string imageApp;
        public PreviewModel dataContext;
        private FormStates formState;
        public static Applicat? CurrentApp;
        string dirBase = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public PreviewPage()
        {
            InitializeComponent();
            _obj = this;

            name = version = appID = string.Empty;
            imageApp = string.Empty;
            password = rpassword = string.Empty;
            PBPassword.Password = TBPassword.Text = password;
            PBRPassword.Password = TBRPassword.Text = rpassword;
            formState = FormStates.Closed;
        }

        public static PreviewPage Instance
        {
            get { return _obj == null ? new PreviewPage() : _obj; }
        }

        public void Show(PreviewModel pv)
        {
            dataContext = pv;
            DataContext = dataContext;
            Show();
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var tag = (sender as FrameworkElement)!.Tag.ToString()!;

            switch (tag)
            {
                case "PASSWORD_EYE":
                    PBPassword.Visibility = PBPassword.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                    TBPassword.Visibility = TBPassword.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                    PVisible.Kind = TBPassword.Visibility != Visibility.Visible ? PackIconKind.EyeOffOutline : PackIconKind.EyeOutline;
                    PBPassword.Password = TBPassword.Text = password;
                    break;
                case "RPASSWORD_EYE":
                    PBRPassword.Visibility = PBRPassword.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                    TBRPassword.Visibility = TBRPassword.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                    RPVisible.Kind = TBRPassword.Visibility != Visibility.Visible ? PackIconKind.EyeOffOutline : PackIconKind.EyeOutline;
                    PBRPassword.Password = TBRPassword.Text = rpassword;
                    break;
                case "UploadImage":
                    var fileName = PickFile();
                    if(fileName != string.Empty)
                    {
                        var bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(fileName, UriKind.Absolute);
                        bitmap.EndInit();
                        AppImage.Source = bitmap;
                        imageApp = fileName;
                    }
                    break;
                case "FolderOpen":
                    var fileName2 = PickFile(true);
                    if (fileName2 != string.Empty)
                    {

                    }
                    break;
                case "Add":
                    if (formState == FormStates.Closed || formState == FormStates.Opened)
                    {
                        TNameApp.Text = name = "";
                        IDApp.Text = appID = "A" + ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds();
                        TVersion.Text = version = "1.0";
                        var date = DateTime.Now;
                        var dateString = date.Day + " " + GetMonth(date.Month) + " " + date.Year + " à " + DateTime.Now.ToShortTimeString().Replace(":", "h") + "min";
                        CreatedDate.Text = dateString;
                        UpdatedDate.Text = dateString;
                        BCreate.Content = "CREER";
                        formState = FormStates.Created;
                        //Begin image
                        try
                        {
                            imageApp = @$"{dirBase}\UIConceptor\Medias\mobile.png";
                            var bitmap = new BitmapImage();
                            bitmap.BeginInit();
                            bitmap.UriSource = new Uri(imageApp, UriKind.Absolute);
                            bitmap.EndInit();
                            AppImage.Source = bitmap;
                        }
                        catch (FileNotFoundException ex)
                        {
                            Console.WriteLine("Veuillez attendre le chargement de l'image par défaut.");
                        }
                        //End image
                        Form.Visibility = Visibility.Visible;
                    }
                    break;
            }
        }

        private void OnMouseDownApp(object sender, MouseButtonEventArgs e)
        {
            var id = (sender as FrameworkElement)!.Tag.ToString()!;
            var bitmap = new BitmapImage();
            
            if (formState is not (FormStates.Closed or FormStates.Opened)) return;
            var details = dataContext.LocalApps!.Where(d => d.ID == id).First();
            TNameApp.Text = details.Name!;
            IDApp.Text = details.ID;
            TVersion.Text = details.Version!;
            CreatedDate.Text = details.Created;
            UpdatedDate.Text = details.Updated;
            
            //Load image
            bitmap.BeginInit();
            bitmap.UriSource = new Uri($@"{details.Image!}", UriKind.Absolute);
            bitmap.EndInit();
            AppImage.Source = bitmap;
            //End loading image
            
            BCreate.Content = "EXECUTER";
            formState = FormStates.Opened;
            Form.Visibility = Visibility.Visible;
            dataContext.DataDetails = details;
        }

        private void OnTextChanged(object sender, RoutedEventArgs e)
        {
            var tag = (sender as FrameworkElement)!.Tag != null ? (sender as FrameworkElement)!.Tag.ToString()! : string.Empty;
            switch (tag)
            {
                case "NameApp": 
                    name = (sender as TextBox)!.Text; SetFormState();
                    break;
                case "Version":
                    version = (sender as TextBox)!.Text; SetFormState();
                    break;
                case "PPassword": 
                    password = (sender as PasswordBox)!.Password; SetFormState();
                    break;
                case "TPassword": 
                    password = (sender as TextBox)!.Text; SetFormState();
                    break;
                case "PRPassword": 
                    rpassword = (sender as PasswordBox)!.Password; SetFormState();
                    break;
                case "TRPassword":
                    rpassword = (sender as TextBox)!.Text; SetFormState();
                    break;
            }
        }

        private void SetFormState()
        {
            if (formState != FormStates.Opened) return;
            BCreate.Content = "METTRE A JOUR";
            formState = FormStates.Updated;
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            var tag = (sender as FrameworkElement)!.Tag.ToString()!;
            switch (tag)
            {
                case "Close":
                    TNameApp.Text = string.Empty;
                    TVersion.Text = string.Empty;
                    PBPassword.Password = TBPassword.Text = string.Empty;
                    PBRPassword.Password = TBRPassword.Text = string.Empty;
                    formState = FormStates.Closed;
                    Form.Visibility = Visibility.Collapsed; 
                    break;
                case "Create":
                    if(formState == FormStates.Created)
                    {
                        var sc = SynchronizationContext.Current;
                        Popup.Visibility = Visibility.Visible;
                        Popup.Refresh(new Views.ComponentP.PopupModel
                        {
                            Display = true,
                            Loading = true,
                            Message = "Création du project.",
                        });
                        DoWork(delegate ()
                        {
                            //Enregistrement et création de la structure du project
                            sc!.Post(delegate
                            {
                                TMessage.Text = "Enregistrement du projet dans les configurations.";
                                var imageName = $@"appImage{appID}{imageApp.Substring(imageApp.Length - 4)}";
                                var imgPath = $@"{Env.dirMedia}/{imageName}";
                                File.Copy(imageApp, imgPath);
                                imageApp = imgPath;
                                
                                var createdApp = new Applicat
                                {
                                    ID = appID,
                                    Name = name,
                                    Password = password,
                                    Color = "transparent",
                                    Created = "Mer 15 Août 2022 à 8h30",
                                    Updated = "Mer 15 Août 2022 à 8h30",
                                    Image = @imgPath,
                                };
                                dataContext.LocalApps!.Add(createdApp);
                                dataContext.DataDetails = createdApp;
                                FormatForSave(dataContext);
                                var jsonString = JsonSerializer.Serialize(dataContext);
                                File.WriteAllText(Env.fileConfig, jsonString);
                                FormatForSave(dataContext, true);
                            }, null);
                            sc!.Post(delegate
                            {
                                //Structure du projet
                                TMessage.Text = "Créatio de la structure du projet.";
                                var dirProject = $@"{Env.dirProject}\Project{appID}";
                                Directory.CreateDirectory($@"{dirProject}\Pages");
                                Directory.CreateDirectory($@"{dirProject}\Components");
                                Directory.CreateDirectory($@"{dirProject}\Medias");
                                Directory.CreateDirectory($@"{dirProject}\Caches");
                                Directory.CreateDirectory($@"{dirProject}\Datas");
                                //File.Create($@"{dirProject}\config.json").Dispose();
                                BCreate.Content = "EXECUTER";
                                formState = FormStates.Opened;
                            }, null);
                        });
                    }else if(formState == FormStates.Opened)
                    {
                        CurrentApp = dataContext.DataDetails!;
                        MainWindow.Instance.Show(dataContext.DataDetails!);
                        Close();
                    }
                    break;
            }
        }

        private string PickFile(bool openProject = false)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Download";
            dialog.DefaultExt = ".png";
            dialog.Filter = openProject ? "Zip files (*.zip;*.rar)|*.zip;*.rar" : "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";

            var result = dialog.ShowDialog();

            return result == true ? dialog.FileName : string.Empty;
        }

        private void FormatForSave(PreviewModel pv, bool reverse = false)
        {
            for (var i = 0; i < pv.LocalApps!.Count; i++)
            {
                var imageName = reverse ? pv.LocalApps[i].Image! : GetImageName(pv.LocalApps[i].Image!);
                pv.LocalApps[i].Image = $@"{(reverse ? Env.dirMedia + "/" : "")}{imageName}";
            }
        }

        private string GetImageName(string name)
        {
            string imageName, temp;
            imageName = temp = string.Empty;
            for (var i = name.Length - 1; i >= 0; i--)
            {
                if (name[i] == '\\' || name[i] == '/')
                    break;
                temp += name[i];
            }
            for (var i = 0; i < temp.Length; i++) 
                imageApp += temp[i];
            return imageName;
        }

        private string GetMonth(int n)
        {
            var month = "Janvier";
            switch (n)
            {
                case 1: month = "Janvier"; break;
                case 2: month = "Février"; break;
                case 3: month = "Mars"; break;
                case 4: month = "Avril"; break;
                case 5: month = "Main"; break;
                case 6: month = "Juin"; break;
                case 7: month = "Juillet"; break;
                case 8: month = "Août"; break;
                case 9: month = "Septembre"; break;
                case 10: month = "Octobre"; break;
                case 11: month = "Novembre"; break;
                case 12: month = "Decembre"; break;
            }
            return month;
        }

        private static void DoWork(Action callback)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                callback();
            });
        }

        public void HidePopup(bool hide)
        {
            Popup.Visibility = hide ? Visibility.Collapsed : Visibility.Visible;
        }

        private void SaveBitmapImage(Image image, string name)
        {
            var path = @$"{dirBase}\UIConceptor\Medias\{name}.png";
            PngBitmapEncoder png = new PngBitmapEncoder();
            if (!File.Exists(path))
            {
                var element = image as UIElement;
                double width, height, renderWidth, renderHeight;
                width = renderWidth = element.RenderSize.Width;
                height = renderHeight = element.RenderSize.Height;
                
                RenderTargetBitmap rtb = new RenderTargetBitmap((int)renderWidth, (int)renderHeight, 96, 96, PixelFormats.Pbgra32);
                var visualBrush = new VisualBrush(element);
                var drawingVisual = new DrawingVisual();
                using (var drawingContext = drawingVisual.RenderOpen())
                {
                    drawingContext.DrawRectangle(visualBrush, null, new Rect(new Point(0, 0), new Point(width, height)));
                }
                rtb.Render(drawingVisual);

                png.Frames.Add(BitmapFrame.Create(rtb));
                using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    png.Save(stream);
                }
            }
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            SaveBitmapImage(AppImage, "mobile");
            Console.WriteLine("Création d'image effectuée avec succès.");
        }

    }

    public class PreviewModel
    {
        public ObservableCollection<Applicat>? RecentApps { get; set; }
        public ObservableCollection<Applicat>? LocalApps { get; set; }
        public ObservableCollection<Applicat>? OnLineApps { get; set; }
        public Applicat? DataDetails { get; set; }
    }

    public class Applicat
    {
        public string? ID { get; set; }
        public string? Name { get; set; }
        public string? Version { get; set; } = "1.0";
        public string? Password { get; set; }
        public string? Color { get; set; }
        public string? Created { get; set; }
        public string? Updated { get; set; }
        public string? LastOpen { get; set; }
        public string? Image { get; set; }
    }

    public enum FormStates
    {
        Closed = 0,
        Updated = 1,
        Created = 2,
        Opened = 3
    }
}
