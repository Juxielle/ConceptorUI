using System;
using System.Linq;
using System.Windows;
using ConceptorUI.Models;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ConceptorUI.Interfaces;


namespace ConceptorUI.Views.Component
{
    public partial class AlignmentProperty : IAlignment
    {
        private static AlignmentProperty? _obj;
        private bool _isContentAlignment;
        private GroupProperties _properties;
        
        public event EventHandler? PreMouseDownEvent;
        private readonly object _mouseDownLock = new();

        public AlignmentProperty()
        {
            InitializeComponent();
            
            _obj = this;
            _isContentAlignment = true;
            _properties = new GroupProperties();
        }
        
        event EventHandler IAlignment.OnMouseDown
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

        public void Refresh(bool value)
        {
            _isContentAlignment = value;
            Title.Text = value ? "CONTENT ALIGNMENT" : "SELF ALIGNMENT";
            
            if (value) return;
            gridSpace.Visibility = Visibility.Collapsed;
        }

        public void FeedProps(object properties)
        {
            BHL.Visibility = gridSpace.Visibility = BHC.Visibility = BHR.Visibility = BVT.Visibility = 
            BVC.Visibility = BVB.Visibility = Visibility.Collapsed;
            _properties = (properties as GroupProperties)!;
            var allowSpaceAlignment = false;

            foreach (var prop in _properties.Properties.Where(prop => prop.Visibility == VisibilityValue.Visible.ToString()))
            {
                if (prop.Name == PropertyNames.HL.ToString())
                {
                    BHL.Visibility = Visibility.Visible;
                    HL.Foreground = new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    HL.Tag = prop.Value;
                }
                else if (prop.Name == PropertyNames.HC.ToString())
                {
                    BHC.Visibility = Visibility.Visible;
                    HC.Foreground = new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    HC.Tag = prop.Value;
                }
                else if (prop.Name == PropertyNames.HR.ToString())
                {
                    BHR.Visibility = Visibility.Visible;
                    HR.Foreground = new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    HR.Tag = prop.Value;
                }
                else if (prop.Name == PropertyNames.VT.ToString())
                {
                    BVT.Visibility = Visibility.Visible;
                    VT.Foreground = new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    VT.Tag = prop.Value;
                }
                else if (prop.Name == PropertyNames.VC.ToString())
                {
                    BVC.Visibility = Visibility.Visible;
                    VC.Foreground = new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    VC.Tag = prop.Value;
                }
                else if (prop.Name == PropertyNames.VB.ToString())
                {
                    BVB.Visibility = Visibility.Visible;
                    VB.Foreground = new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    VB.Tag = prop.Value;
                }
                else if (prop.Name == PropertyNames.SpaceBetween.ToString())
                {
                    BSB.Visibility = Visibility.Visible;
                    gridSpace.Visibility = Visibility.Visible;
                    BSB.BorderBrush = BSB1.Foreground = BSB2.Foreground = BSB3.Foreground = BSB4.Foreground = 
                        new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    BSB1.Tag = prop.Value;
                    allowSpaceAlignment = true;
                }
                else if (prop.Name == PropertyNames.SpaceAround.ToString())
                {
                    BSA.Visibility = Visibility.Visible;
                    BSA.BorderBrush = BSA1.Foreground = BSA2.Foreground = BSA3.Foreground = BSA4.Foreground =
                        new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    BSA1.Tag = prop.Value;
                    allowSpaceAlignment = true;
                }
                else if (prop.Name == PropertyNames.SpaceEvery.ToString())
                {
                    BSE.Visibility = Visibility.Visible;
                    BSE.BorderBrush = BSE1.Foreground = BSE2.Foreground = BSE3.Foreground = BSE4.Foreground =
                        new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    BSE1.Tag = prop.Value;
                    allowSpaceAlignment = true;
                }
            }
            
            if (allowSpaceAlignment) return;
            gridSpace.Visibility = Visibility.Collapsed;
        }

        private void BtnClick(object sender, MouseButtonEventArgs e)
        {
            var tag = (sender as Border)!.Tag.ToString()!;
            var propertyName = PropertyNames.None;
            var sendValue = string.Empty;
            
            switch (tag)
            {
                case "HL": 
                    propertyName = PropertyNames.HL;
                    sendValue = HL.Tag.ToString() == "0" ? "1" : "0";
                    HL.Tag = sendValue;
                    break;
                case "HC":
                    propertyName = PropertyNames.HC;
                    sendValue = HC.Tag.ToString() == "0" ? "1" : "0";
                    HC.Tag = sendValue;
                    break;
                case "HR":
                    propertyName = PropertyNames.HR;
                    sendValue = HR.Tag.ToString() == "0" ? "1" : "0";
                    HR.Tag = sendValue;
                    break;
                case "VT":
                    propertyName = PropertyNames.VT;
                    sendValue = VT.Tag.ToString() == "0" ? "1" : "0";
                    VT.Tag = sendValue;
                    break;
                case "VC":
                    propertyName = PropertyNames.VC;
                    sendValue = VC.Tag.ToString() == "0" ? "1" : "0";
                    VC.Tag = sendValue;
                    break;
                case "VB":
                    propertyName = PropertyNames.VB;
                    sendValue = VB.Tag.ToString() == "0" ? "1" : "0";
                    VB.Tag = sendValue;
                    break;
                case "SB":
                    propertyName = PropertyNames.SpaceBetween;
                    sendValue = BSB1.Tag.ToString() == "0" ? "1" : "0";
                    BSB1.Tag = sendValue;
                    break;
                case "SA":
                    propertyName = PropertyNames.SpaceAround;
                    sendValue = BSA1.Tag.ToString() == "0" ? "1" : "0";
                    BSA1.Tag = sendValue;
                    break;
                case "SE":
                    propertyName = PropertyNames.SpaceEvery;
                    sendValue = BSE1.Tag.ToString() == "0" ? "1" : "0";
                    BSE1.Tag = sendValue;
                    break;
            }

            LoadValue(tag, sendValue);
            PreMouseDownEvent!.Invoke(
                new dynamic[]{_isContentAlignment ? GroupNames.Alignment : GroupNames.SelfAlignment, propertyName, sendValue}, 
                EventArgs.Empty
            );
        }

        private void LoadValue(string tag, string value)
        {
            switch (tag)
            {
                case "HL":
                    HL.Foreground = new BrushConverter().ConvertFrom(value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    HC.Foreground = HR.Foreground = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    break;
                case "HC":
                    HL.Foreground = HR.Foreground = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    HC.Foreground = new BrushConverter().ConvertFrom(value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    break;
                case "HR":
                    HL.Foreground = HC.Foreground = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    HR.Foreground = new BrushConverter().ConvertFrom(value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    break;
                case "VT":
                    VT.Foreground = new BrushConverter().ConvertFrom(value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    VC.Foreground = VB.Foreground = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    BSB.BorderBrush = BSB1.Foreground = BSB2.Foreground = BSB3.Foreground = BSB4.Foreground = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    BSA.BorderBrush = BSA1.Foreground = BSA2.Foreground = BSA3.Foreground = BSA4.Foreground = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    BSE.BorderBrush = BSE1.Foreground = BSE2.Foreground = BSE3.Foreground = BSE4.Foreground = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    break;
                case "VC":
                    VT.Foreground = VB.Foreground = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    VC.Foreground = new BrushConverter().ConvertFrom(value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    BSB.BorderBrush = BSB1.Foreground = BSB2.Foreground = BSB3.Foreground = BSB4.Foreground = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    BSA.BorderBrush = BSA1.Foreground = BSA2.Foreground = BSA3.Foreground = BSA4.Foreground = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    BSE.BorderBrush = BSE1.Foreground = BSE2.Foreground = BSE3.Foreground = BSE4.Foreground = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    break;
                case "VB":
                    VT.Foreground = VC.Foreground = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    VB.Foreground = new BrushConverter().ConvertFrom(value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    BSB.BorderBrush = BSB1.Foreground = BSB2.Foreground = BSB3.Foreground = BSB4.Foreground = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    BSA.BorderBrush = BSA1.Foreground = BSA2.Foreground = BSA3.Foreground = BSA4.Foreground = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    BSE.BorderBrush = BSE1.Foreground = BSE2.Foreground = BSE3.Foreground = BSE4.Foreground = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    break;
                case "SB":
                    VT.Foreground = VC.Foreground = VB.Foreground = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    BSB.BorderBrush = BSB1.Foreground = BSB2.Foreground = BSB3.Foreground = BSB4.Foreground = new BrushConverter().ConvertFrom("#6739b7") as SolidColorBrush;
                    BSA.BorderBrush = BSA1.Foreground = BSA2.Foreground = BSA3.Foreground = BSA4.Foreground = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    BSE.BorderBrush = BSE1.Foreground = BSE2.Foreground = BSE3.Foreground = BSE4.Foreground = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    break;
                case "SA":
                    VT.Foreground = VC.Foreground = VB.Foreground = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    BSB.BorderBrush = BSB1.Foreground = BSB2.Foreground = BSB3.Foreground = BSB4.Foreground = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    BSA.BorderBrush = BSA1.Foreground = BSA2.Foreground = BSA3.Foreground = BSA4.Foreground = new BrushConverter().ConvertFrom("#6739b7") as SolidColorBrush;
                    BSE.BorderBrush = BSE1.Foreground = BSE2.Foreground = BSE3.Foreground = BSE4.Foreground = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    break;
                case "SE":
                    VT.Foreground = VC.Foreground = VB.Foreground = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    BSB.BorderBrush = BSB1.Foreground = BSB2.Foreground = BSB3.Foreground = BSB4.Foreground = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    BSA.BorderBrush = BSA1.Foreground = BSA2.Foreground = BSA3.Foreground = BSA4.Foreground = new BrushConverter().ConvertFrom("#8c8c8a") as SolidColorBrush;
                    BSE.BorderBrush = BSE1.Foreground = BSE2.Foreground = BSE3.Foreground = BSE4.Foreground = new BrushConverter().ConvertFrom("#6739b7") as SolidColorBrush;
                    break;
            }
        }
    }
}
