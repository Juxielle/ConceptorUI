using ConceptorUI.Constants;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ConceptorUI.Application.Configs;
using ConceptorUI.Application.Dto.UiDto;
using ConceptorUI.Application.PlatformSystem;
using ConceptorUI.Application.Project;
using ConceptorUI.Utils;


namespace ConceptorUI
{
    public partial class PreviewPage
    {
        private static PreviewPage? _obj;
        private ObservableCollection<ProjectInfoUiDto> _projects;
        private int _selectedProject;
        private FormStates _formState;

        private string _appId;
        private string _name;
        private string _version;
        private string _password;
        private string _repeatPassword;
        private string _image;
        private string _projectPath;

        private static readonly string DirBase = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public PreviewPage()
        {
            InitializeComponent();
            _obj = this;
            _projects = [];
            _selectedProject = -1;
            _formState = FormStates.Closed;

            PbPassword.Password = TbPassword.Text = string.Empty;
            PbrPassword.Password = TbrPassword.Text = string.Empty;
        }

        public static PreviewPage Instance => _obj == null! ? new PreviewPage() : _obj;

        public void Show(List<ProjectInfoUiDto> projects)
        {
            _projects = new ObservableCollection<ProjectInfoUiDto>(projects);
            LocalApps.ItemsSource = _projects;
            Show();
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var tag = (sender as FrameworkElement)!.Tag.ToString()!;

            switch (tag)
            {
                case "PASSWORD_EYE":
                    PbPassword.Visibility = PbPassword.Visibility == Visibility.Visible
                        ? Visibility.Collapsed
                        : Visibility.Visible;
                    TbPassword.Visibility = TbPassword.Visibility == Visibility.Visible
                        ? Visibility.Collapsed
                        : Visibility.Visible;
                    PVisible.Kind = TbPassword.Visibility != Visibility.Visible
                        ? PackIconKind.EyeOffOutline
                        : PackIconKind.EyeOutline;
                    //PbPassword.Password = TbPassword.Text = _projects[_selectedProject].Password;
                    break;
                case "RPASSWORD_EYE":
                    PbrPassword.Visibility = PbrPassword.Visibility == Visibility.Visible
                        ? Visibility.Collapsed
                        : Visibility.Visible;
                    TbrPassword.Visibility = TbrPassword.Visibility == Visibility.Visible
                        ? Visibility.Collapsed
                        : Visibility.Visible;
                    RpVisible.Kind = TbrPassword.Visibility != Visibility.Visible
                        ? PackIconKind.EyeOffOutline
                        : PackIconKind.EyeOutline;
                    //PbrPassword.Password = TbrPassword.Text = _projects[_selectedProject].Password;
                    break;
                case "UploadImage":
                    var filePath = Helper.PickFile();
                    if (filePath != string.Empty)
                    {
                        var bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(filePath, UriKind.Absolute);
                        bitmap.EndInit();
                        AppImage.Source = bitmap;
                        _image = filePath;
                    }

                    break;
                case "FolderOpen":
                    var filePath2 = Helper.PickFile(true);
                    if (filePath2 != string.Empty)
                    {
                    }

                    break;
                case "Add":
                    if (_formState is FormStates.Closed or FormStates.Opened)
                    {
                        TNameApp.Text = _name = "";
                        IdApp.Text = _appId = "A" + ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds();
                        TVersion.Text = _version = "1.0";
                        var dateString = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        CreatedDate.Text = dateString;
                        UpdatedDate.Text = dateString;
                        BCreate.Content = "CREER";
                        _formState = FormStates.Created;

                        //Begin image
                        try
                        {
                            const string path = "/Assets/mobile.png";
                            var bitmap = new BitmapImage();
                            bitmap.BeginInit();
                            bitmap.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
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

            _selectedProject = _projects.ToList().FindIndex(d => d.Id == id);
            var project = _projects[_selectedProject];
            TNameApp.Text = project.Name;
            IdApp.Text = project.Id;
            CreatedDate.Text = project.Created.ToString(CultureInfo.InvariantCulture);
            UpdatedDate.Text = project.Updated.ToString(CultureInfo.InvariantCulture);

            BCreate.Content = "EXECUTER";
            _formState = FormStates.Opened;
            Form.Visibility = Visibility.Visible;
        }

        private void OnTextChanged(object sender, RoutedEventArgs e)
        {
            var tag = (sender as FrameworkElement)!.Tag != null
                ? (sender as FrameworkElement)!.Tag.ToString()!
                : string.Empty;
            switch (tag)
            {
                case "NameApp":
                    _name = (sender as TextBox)!.Text;
                    SetFormState();
                    break;
                case "Version":
                    _version = (sender as TextBox)!.Text;
                    SetFormState();
                    break;
                case "PPassword":
                    _password = (sender as PasswordBox)!.Password;
                    SetFormState();
                    break;
                case "TPassword":
                    _password = (sender as TextBox)!.Text;
                    SetFormState();
                    break;
                case "PRPassword":
                    _repeatPassword = (sender as PasswordBox)!.Password;
                    SetFormState();
                    break;
                case "TRPassword":
                    _repeatPassword = (sender as TextBox)!.Text;
                    SetFormState();
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
                    PbPassword.Password = TbPassword.Text = string.Empty;
                    PbrPassword.Password = TbrPassword.Text = string.Empty;
                    _formState = FormStates.Closed;
                    Form.Visibility = Visibility.Collapsed;
                    break;
                case "Create":
                    if (_formState == FormStates.Created)
                    {
                        _projectPath = Helper.SelectFolder(_name);
                        if (_projectPath == null!) return;

                        var fileName = Path.GetFileName(_projectPath);
                        var projectName = fileName.Replace(".xui", "");
                        var path = _projectPath.Replace($@"\{fileName}", "");

                        var sc = SynchronizationContext.Current;
                        DoWork(async delegate
                        {
                            var createProjectResult = await new CreateProjectCommandHandler().Handle(
                                new CreateProjectCommand
                                {
                                    ProjectId = projectName,
                                    ProjectName = projectName,
                                    ProjectImage = _image,
                                    FolderPath = path
                                });

                            if (createProjectResult.IsFailure) return;
                            var project = createProjectResult.Value;
                            
                            await new SaveProjectMetaDataCommandHandler().Handle(new SaveProjectMetaDataCommand
                            {
                                Id = project.Id,
                                Name = project.Name,
                                Created = DateTime.Now,
                                Updated = DateTime.Now,
                                ZipPath = project.ZipPath,
                                Image = project.Image
                            });
                            
                            var projects = _projects.ToList();

                            projects.Add(project);
                            var json = JsonSerializer.Serialize(projects);
                            await new SaveSystemConfigCommandHandler().Handle(new SaveSystemConfigCommand
                            {
                                Path = Env.DirEnv,
                                Content = json
                            });

                            sc?.Post(delegate
                            {
                                _projects.Add(project);
                                BCreate.Content = "EXECUTER";
                                _formState = FormStates.Opened;
                            }, null);
                        });
                    }
                    else if (_formState == FormStates.Opened)
                    {
                        MainWindow.Instance.Show(_projects[_selectedProject]);
                        Close();
                    }

                    break;
            }
        }

        private static void DoWork(Action callback)
        {
            ThreadPool.QueueUserWorkItem(delegate { callback(); });
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