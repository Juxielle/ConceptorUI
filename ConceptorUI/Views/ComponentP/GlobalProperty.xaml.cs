using ConceptorUI.Models;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ConceptorUI.Constants;


namespace ConceptorUI.Views.ComponentP
{
    /// <summary>
    /// Logique d'interaction pour GlobalProperty.xaml
    /// </summary>
    public partial class GlobalProperty
    {
        static GlobalProperty? _obj;
        public GlobalProperty()
        {
            InitializeComponent();
            _obj = this;
        }

        public static GlobalProperty Instance => _obj == null! ? new GlobalProperty() : _obj;

        public void FeedProps()
        {
            CFocus.Visibility = BMoveParentToChild.Visibility = BMoveChildToParent.Visibility = BTrash.Visibility = 
            BMoveLeft.Visibility = BMoveTop.Visibility = BMoveRight.Visibility = BMoveBottom.Visibility =
            BFilePicker.Visibility = BSelectedMode.Visibility = Visibility.Collapsed;
            var pos = Properties.GetPosition(GroupNames.Global, PropertyNames.SelectedMode);
            foreach (var prop in Properties.groupProps![pos[0]].Properties)
            {
                if (prop.Visibility == VisibilityValue.Visible.ToString())
                {
                    if (prop.Name == PropertyNames.Trash.ToString())
                    {
                        BTrash.Visibility = Visibility.Visible;
                    }
                    else if (prop.Name == PropertyNames.SelectedMode.ToString())
                    {
                        BSelectedMode.Visibility = Visibility.Visible;
                        SelectedMode.Foreground =
                            BSelectedMode.BorderBrush = new BrushConverter().ConvertFrom(prop.Value == ESelectedMode.Single.ToString() ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
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
                        var value = 0;
                        if (prop.Value == ESelectedElement.Row.ToString()) value = 1;
                        else if (prop.Value == ESelectedElement.Column.ToString()) value = 2;
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
        }
        
        private void BtnClick(object sender, RoutedEventArgs e)
        {
            var tag = (sender as Button)!.Tag.ToString()!;
            var pos = Properties.GetPosition(GroupNames.Global, PropertyNames.SelectedMode);
            int idG = pos[0], idP = pos[1];
            switch (tag)
            {
                case "SelectedMode":
                    var value = Properties.groupProps![idG].Properties[idP].Value == ESelectedMode.Single.ToString() ? ESelectedMode.Multiple : ESelectedMode.Single;
                    PanelProperty.Instance.ReactToProps(GroupNames.Global, PropertyNames.SelectedMode, value.ToString());
                    SelectedMode.Foreground =
                        BSelectedMode.BorderBrush = new BrushConverter().ConvertFrom(value == ESelectedMode.Single ? "#8c8c8a" : "#6739b7") as SolidColorBrush; break;
                case "MoveLeft": PanelProperty.Instance.ReactToProps(GroupNames.Global, PropertyNames.MoveLeft, "0"); break;
                case "MoveRight": PanelProperty.Instance.ReactToProps(GroupNames.Global, PropertyNames.MoveRight, "0"); break;
                case "MoveTop": PanelProperty.Instance.ReactToProps(GroupNames.Global, PropertyNames.MoveTop, "0"); break;
                case "MoveBottom": PanelProperty.Instance.ReactToProps(GroupNames.Global, PropertyNames.MoveBottom, "0"); break;
                case "MoveParentToChild": PanelProperty.Instance.ReactToProps(GroupNames.Global, PropertyNames.MoveParentToChild, "0"); break;
                case "MoveChildToParent": PanelProperty.Instance.ReactToProps(GroupNames.Global, PropertyNames.MoveChildToParent, "0"); break;
                case "Trash": PanelProperty.Instance.ReactToProps(GroupNames.Global, PropertyNames.Trash, "0"); break;
                case "FilePicker":
                    if (Properties.ComponentName == ComponentList.Icon)
                    {
                        var dbIcon = new DbIcons(data =>
                        {
                            PanelProperty.Instance.ReactToProps(GroupNames.Global, PropertyNames.FilePicker, data);
                        });
                        dbIcon.ShowDialog();
                    }
                    else if (Properties.ComponentName == ComponentList.Image)
                    {
                        var filePath = PickFile();
                        if (filePath == string.Empty) return;
                        
                        var fileName = Path.GetFileName(filePath);
                        var path = Env.pemcFile($"Project{PageView.Instance.application.ID}", "Medias", fileName);
                        
                        if(!File.Exists(path))
                            File.Copy(filePath, path);
                        
                        PanelProperty.Instance.ReactToProps(GroupNames.Global, PropertyNames.FilePicker, fileName);
                    }
                    break;
                case "Copy":
                    PageView.Instance.OnCopyOrPaste();
                    break;
                case "Paste":
                    PageView.Instance.OnCopyOrPaste(true);
                    break;
            }
        }
        
        private void OnFocusChecked(object sender, RoutedEventArgs e)
        {
            var tag = (sender as CheckBox)!.Tag == null ? "" : (sender as CheckBox)!.Tag.ToString()!;
            try
            {
                var pos = Properties.GetPosition(GroupNames.Global, PropertyNames.Focus);

                if (Properties.groupProps == null) return;
                var value = Properties.groupProps![pos[0]].Properties[pos[1]].Value;
                switch (tag)
                {
                    case "Focus":
                        PanelProperty.Instance.ReactToProps(GroupNames.Global, PropertyNames.Focus, value == "0" ? "1" : "0");
                        break;
                }
            }
            catch (Exception ex)
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
