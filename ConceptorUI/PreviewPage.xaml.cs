using ConceptorUI.Constants;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ConceptorUI.Classes;
using ConceptorUI.Utils;


namespace ConceptorUI
{
    public partial class PreviewPage
    {
        private static PreviewPage? _obj;
        private List<Project> _projects;
        private int _selectedProject;
        private FormStates _formState;

        private string _appId;
        private string _name;
        private string _version;
        private string _password;
        private string _repeatPassword;
        private string _image;
        private string _projectPath;
        
        private static readonly string DirBase = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public PreviewPage()
        {
            InitializeComponent();
            _obj = this;
            _projects = new List<Project>();
            _selectedProject = -1;
            _formState = FormStates.Closed;

            PBPassword.Password = TBPassword.Text = string.Empty;
            PBRPassword.Password = TBRPassword.Text = string.Empty;
        }

        public static PreviewPage Instance => _obj == null! ? new PreviewPage() : _obj;

        public void Show(List<Project> projects)
        {
            _projects = projects;
            LocalApps.ItemsSource = _projects;
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
                    PBPassword.Password = TBPassword.Text = _projects[_selectedProject].Password;
                    break;
                case "RPASSWORD_EYE":
                    PBRPassword.Visibility = PBRPassword.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                    TBRPassword.Visibility = TBRPassword.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                    RPVisible.Kind = TBRPassword.Visibility != Visibility.Visible ? PackIconKind.EyeOffOutline : PackIconKind.EyeOutline;
                    PBRPassword.Password = TBRPassword.Text = _projects[_selectedProject].Password;
                    break;
                case "UploadImage":
                    var fileName = Helper.PickFile();
                    if(fileName != string.Empty)
                    {
                        var bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(fileName, UriKind.Absolute);
                        bitmap.EndInit();
                        AppImage.Source = bitmap;
                        _projects[_selectedProject].Image = fileName;
                    }
                    break;
                case "FolderOpen":
                    var fileName2 = Helper.PickFile(true);
                    if (fileName2 != string.Empty)
                    {

                    }
                    break;
                case "Add":
                    _projectPath = Helper.SelectFolder();
                    if (_projectPath != null! && _formState is FormStates.Closed or FormStates.Opened)
                    {
                        TNameApp.Text = _name = "";
                        IDApp.Text = _appId = "A" + ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds();
                        TVersion.Text = _version = "1.0";
                        var dateString = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        CreatedDate.Text = dateString;
                        UpdatedDate.Text = dateString;
                        BCreate.Content = "CREER";
                        _formState = FormStates.Created;
                        
                        //Begin image
                        try
                        {
                            var path = @$"{DirBase}\UIConceptor\Medias\mobile.png";
                            var bitmap = new BitmapImage();
                            bitmap.BeginInit();
                            bitmap.UriSource = new Uri(path, UriKind.Absolute);
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
            
            if (_formState is not (FormStates.Closed or FormStates.Opened)) return;

            _selectedProject = _projects.FindIndex(d => d.ID == id);
            var project = _projects[_selectedProject];
            TNameApp.Text = project.Name;
            IDApp.Text = project.ID;
            TVersion.Text = project.Version;
            CreatedDate.Text = project.Created.ToString(CultureInfo.InvariantCulture);
            UpdatedDate.Text = project.Updated.ToString(CultureInfo.InvariantCulture);
            
            //Load image
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri($"{project.FolderPath}/{project.Image}", UriKind.Absolute);
            bitmap.EndInit();
            AppImage.Source = bitmap;
            //End loading image
            
            BCreate.Content = "EXECUTER";
            _formState = FormStates.Opened;
            Form.Visibility = Visibility.Visible;
        }

        private void OnTextChanged(object sender, RoutedEventArgs e)
        {
            var tag = (sender as FrameworkElement)!.Tag != null ? (sender as FrameworkElement)!.Tag.ToString()! : string.Empty;
            switch (tag)
            {
                case "NameApp": 
                    _name = (sender as TextBox)!.Text; SetFormState();
                    break;
                case "Version":
                    _version = (sender as TextBox)!.Text; SetFormState();
                    break;
                case "PPassword": 
                    _password = (sender as PasswordBox)!.Password; SetFormState();
                    break;
                case "TPassword": 
                    _password = (sender as TextBox)!.Text; SetFormState();
                    break;
                case "PRPassword": 
                    _repeatPassword = (sender as PasswordBox)!.Password; SetFormState();
                    break;
                case "TRPassword":
                    _repeatPassword = (sender as TextBox)!.Text; SetFormState();
                    break;
            }
        }

        private void SetFormState()
        {
            if (_formState != FormStates.Opened) return;
            BCreate.Content = "METTRE A JOUR";
            _formState = FormStates.Updated;
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
                    _formState = FormStates.Closed;
                    Form.Visibility = Visibility.Collapsed; 
                    break;
                case "Create":
                    if(_formState == FormStates.Created)
                    {
                        var sc = SynchronizationContext.Current;
                        DoWork(delegate
                        {
                            sc!.Post(delegate
                            {
                                // TMessage.Text = "Enregistrement du projet dans les configurations.";
                                // var imageName = $"appImage{appID}{imageApp.Substring(imageApp.Length - 4)}";
                                // var imgPath = $"{Env.dirMedia}/{imageName}";
                                // File.Copy(_image, imgPath);
                                
                                var project = new Project
                                {
                                    ID = _appId,
                                    Name = _name,
                                    Password = _password,
                                    Color = "transparent",
                                    Created = DateTime.Now,
                                    Updated = DateTime.Now,
                                    FolderPath = "",
                                    Image = "",
                                };
                                
                                // dataContext.LocalApps!.Add(createdApp);
                                // dataContext.DataDetails = createdApp;
                                // FormatForSave(dataContext);
                                // var jsonString = JsonSerializer.Serialize(dataContext);
                                // File.WriteAllText(Env.fileConfig, jsonString);
                                // FormatForSave(dataContext, true);
                            }, null);
                            sc!.Post(delegate
                            {
                                //Structure du projet
                                TMessage.Text = "Créatio de la structure du projet.";
                                var dirProject = $@"{Env.dirProject}\Project{_appId}";
                                Directory.CreateDirectory($@"{dirProject}\Pages");
                                Directory.CreateDirectory($@"{dirProject}\Components");
                                Directory.CreateDirectory($@"{dirProject}\Medias");
                                Directory.CreateDirectory($@"{dirProject}\Caches");
                                Directory.CreateDirectory($@"{dirProject}\Datas");
                                //File.Create($@"{dirProject}\config.json").Dispose();
                                BCreate.Content = "EXECUTER";
                                _formState = FormStates.Opened;
                            }, null);
                        });
                    }
                    else if(_formState == FormStates.Opened)
                    {
                        MainWindow.Instance.Show(_projects[_selectedProject]);
                        Close();
                    }
                    break;
            }
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
    }

    public enum FormStates
    {
        Closed = 0,
        Updated = 1,
        Created = 2,
        Opened = 3
    }
}
