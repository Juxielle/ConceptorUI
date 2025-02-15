﻿using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ConceptorUI.Services;
using ConceptorUI.ViewModels.Components;
using ConceptorUI.Views.Modals;
using UiDesigner;
using UiDesigner.Application.Images;
using UiDesigner.Models;
using UiDesigner.Views.Component;

namespace ConceptorUI.Views.Component
{
    public partial class GlobalProperty
    {
        private GroupProperties _properties;
        private ComponentList _componentName;
        private bool _allowSetField;

        public ICommand? MouseDownCommand;

        public GlobalProperty()
        {
            InitializeComponent();

            _properties = new GroupProperties();
            _componentName = ComponentList.Window;
        }

        public void FeedProps(object properties, ComponentList componentName)
        {
            CFocus.Visibility = BMoveParentToChild.Visibility = BMoveChildToParent.Visibility = BTrash.Visibility =
                BMoveLeft.Visibility = BMoveTop.Visibility = BMoveRight.Visibility = BMoveBottom.Visibility =
                    BFilePicker.Visibility = BSelectedMode.Visibility = Visibility.Collapsed;
            _properties = (properties as GroupProperties)!;
            _componentName = componentName;
            _allowSetField = false;

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

            _allowSetField = true;
        }

        private async void BtnClick(object sender, RoutedEventArgs e)
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
                            MouseDownCommand?.Execute(
                                new dynamic[] { GroupNames.Global, PropertyNames.FilePicker, (data! as string)! }
                            );
                        };

                        dbIcon.ShowDialog();
                        allowSend = false;
                    }
                    else if (_componentName == ComponentList.Image)
                    {
                        var filePath = PickFile();
                        if (filePath == string.Empty) return;

                        var extension = Path.GetExtension(filePath);
                        var fileName = $"img_{((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds()}{extension}";

                        var saveImageResult = await new SaveImageCommandHandler().Handle(new SaveImageCommand
                        {
                            ZipPath = ComponentHelper.ProjectPath!,
                            ProjectName = ComponentHelper.ProjectName!,
                            FilePath = filePath,
                            FileName = fileName,
                        });
                        
                        if(saveImageResult.IsFailure) return;

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
            MouseDownCommand?.Execute(
                new dynamic[] { GroupNames.Global, propertyName, sendValue }
            );
        }

        private void OnFocusChecked(object sender, RoutedEventArgs e)
        {
            if (!_allowSetField) return;

            var tag = (sender as CheckBox)!.Tag == null ? "" : (sender as CheckBox)!.Tag.ToString()!;
            try
            {
                if (_properties == null!) return;
                var value = _properties.GetValue(PropertyNames.Focus);
                switch (tag)
                {
                    case "Focus":
                        MouseDownCommand?.Execute(
                            new dynamic[] { GroupNames.Global, PropertyNames.Focus, value == "0" ? "1" : "0" }
                        );
                        break;
                }
            }
            catch (Exception)
            {
                //
            }
        }

        private void OnSettingClick(object sender, RoutedEventArgs e)
        {
            var componentSetting = PropertiesConfigService.GetConfigs(_properties);
            ComponentPropertyConfig.Instance.Refresh(componentSetting, "GLOBAL PROPERTIES", GroupNames.Global);
        }

        private static string PickFile()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                FileName = "Download",
                DefaultExt = ".png",
                Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg"
            };
            return dialog.ShowDialog() == true ? dialog.FileName : string.Empty;
        }
    }
}