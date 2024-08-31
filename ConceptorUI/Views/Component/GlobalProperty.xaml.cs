using ConceptorUI.Models;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ConceptorUI.Interfaces;
using ConceptorUi.ViewModels;


namespace ConceptorUI.Views.Component
{
    public partial class GlobalProperty : IGlobal
    {
        private static GlobalProperty? _obj;
        private GroupProperties _properties;
        private ComponentList _componentName;

        public event EventHandler? PreMouseDownEvent;
        private readonly object _mouseDownLock = new();

        public GlobalProperty()
        {
            InitializeComponent();

            _obj = this;
            _properties = new GroupProperties();
            _componentName = ComponentList.Window;
        }

        public static GlobalProperty Instance => _obj == null! ? new GlobalProperty() : _obj;

        event EventHandler IGlobal.OnMouseDown
        {
            add
            {
                lock (_mouseDownLock)
                {
                    PreMouseDownEvent += value;
                }
            }
            remove
            {
                lock (_mouseDownLock)
                {
                    PreMouseDownEvent -= value;
                }
            }
        }

        public void FeedProps(object properties, ComponentList componentName)
        {
            CFocus.Visibility = BMoveParentToChild.Visibility = BMoveChildToParent.Visibility = BTrash.Visibility =
                BMoveLeft.Visibility = BMoveTop.Visibility = BMoveRight.Visibility = BMoveBottom.Visibility =
                    BFilePicker.Visibility = BSelectedMode.Visibility = Visibility.Collapsed;
            _properties = (properties as GroupProperties)!;
            _componentName = componentName;

            foreach (var prop in _properties.Properties.Where(prop =>
                         prop.Visibility == VisibilityValue.Visible.ToString()))
            {
                if (prop.Name == PropertyNames.Trash.ToString())
                {
                    BTrash.Visibility = Visibility.Visible;
                }
                else if (prop.Name == PropertyNames.SelectedMode.ToString())
                {
                    BSelectedMode.Visibility = Visibility.Visible;
                    SelectedMode.Foreground =
                        BSelectedMode.BorderBrush =
                            new BrushConverter().ConvertFrom(prop.Value == ESelectedMode.Single.ToString()
                                ? "#8c8c8a"
                                : "#6739b7") as SolidColorBrush;
                }
                else if (prop.Name == PropertyNames.FilePicker.ToString())
                {
                    BFilePicker.Visibility = Visibility.Visible;
                }
                else if (prop.Name == PropertyNames.Focus.ToString())
                {
                    CFocus.Visibility = Visibility.Visible;
                    CFocus.IsChecked = prop.Value == "1";
                }
                else if (prop.Name == PropertyNames.MoveLeft.ToString())
                {
                    BMoveLeft.Visibility = Visibility.Visible;
                }
                else if (prop.Name == PropertyNames.MoveTop.ToString())
                {
                    BMoveTop.Visibility = Visibility.Visible;
                }
                else if (prop.Name == PropertyNames.MoveRight.ToString())
                {
                    BMoveRight.Visibility = Visibility.Visible;
                }
                else if (prop.Name == PropertyNames.MoveBottom.ToString())
                {
                    BMoveBottom.Visibility = Visibility.Visible;
                }
                else if (prop.Name == PropertyNames.MoveParentToChild.ToString())
                {
                    BMoveParentToChild.Visibility = Visibility.Visible;
                }
                else if (prop.Name == PropertyNames.MoveChildToParent.ToString())
                {
                    BMoveChildToParent.Visibility = Visibility.Visible;
                }
            }
        }

        private void BtnClick(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button)!.Tag.ToString()!;
            var propertyName = PropertyNames.None;
            var sendValue = string.Empty;
            var allowSend = true;

            switch (tag)
            {
                case "SelectedMode":
                    sendValue = _properties.GetValue(PropertyNames.SelectedMode) == ESelectedMode.Single.ToString()
                        ? ESelectedMode.Multiple.ToString()
                        : ESelectedMode.Single.ToString();
                    propertyName = PropertyNames.SelectedMode;
                    SelectedMode.Foreground =
                        BSelectedMode.BorderBrush =
                            new BrushConverter().ConvertFrom(sendValue == ESelectedMode.Single.ToString()
                                ? "#8c8c8a"
                                : "#6739b7") as SolidColorBrush;
                    break;
                case "MoveLeft":
                    sendValue = "0";
                    propertyName = PropertyNames.MoveLeft;
                    break;
                case "MoveRight":
                    sendValue = "0";
                    propertyName = PropertyNames.MoveRight;
                    break;
                case "MoveTop":
                    sendValue = "0";
                    propertyName = PropertyNames.MoveTop;
                    break;
                case "MoveBottom":
                    sendValue = "0";
                    propertyName = PropertyNames.MoveBottom;
                    break;
                case "MoveParentToChild":
                    sendValue = "0";
                    propertyName = PropertyNames.MoveParentToChild;
                    break;
                case "MoveChildToParent":
                    sendValue = "0";
                    propertyName = PropertyNames.MoveChildToParent;
                    Console.WriteLine(@$"Passe Bien Ici.");
                    break;
                case "Trash":
                    sendValue = "0";
                    propertyName = PropertyNames.Trash;
                    break;
                case "FilePicker":
                    if (_componentName == ComponentList.Icon)
                    {
                        var dbIcon = new DbIcons();
                        dbIcon.OnValueChangedEvent += (data, _) =>
                        {
                            PreMouseDownEvent!.Invoke(
                                new dynamic[] { GroupNames.Global, PropertyNames.FilePicker, (data! as string)! },
                                EventArgs.Empty
                            );
                        };
                        
                        dbIcon.ShowDialog();
                        allowSend = false;
                    }
                    else if (_componentName == ComponentList.Image)
                    {
                        var filePath = PickFile();
                        if (filePath == string.Empty) return;

                        var fileName = Path.GetFileName(filePath);
                        var path = ComponentHelper.ProjectPath + "/Medias/" + fileName;

                        if (!File.Exists(path))
                            File.Copy(filePath, path);

                        sendValue = fileName;
                        propertyName = PropertyNames.FilePicker;
                    }

                    break;
                case "Copy":
                    PageView.Instance.OnCopyOrPaste();
                    sendValue = "0";
                    propertyName = PropertyNames.Copy;
                    break;
                case "Paste":
                    PageView.Instance.OnCopyOrPaste(true);
                    sendValue = "0";
                    propertyName = PropertyNames.Paste;
                    break;
            }

            if (!allowSend) return;
            PreMouseDownEvent!.Invoke(
                new dynamic[] { GroupNames.Global, propertyName, sendValue },
                EventArgs.Empty
            );
        }

        private void OnFocusChecked(object sender, RoutedEventArgs e)
        {
            var tag = (sender as CheckBox)!.Tag == null ? "" : (sender as CheckBox)!.Tag.ToString()!;
            try
            {
                if (_properties == null!) return;
                var value = _properties.GetValue(PropertyNames.Focus);
                switch (tag)
                {
                    case "Focus":
                        PanelProperty.ReactToProps(GroupNames.Global, PropertyNames.Focus, value == "0" ? "1" : "0");
                        break;
                }
            }
            catch (Exception)
            {
                //
            }
        }

        private static string PickFile()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Download";
            dialog.DefaultExt = ".png";
            dialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
            return dialog.ShowDialog() == true ? dialog.FileName : string.Empty;
        }
    }
}