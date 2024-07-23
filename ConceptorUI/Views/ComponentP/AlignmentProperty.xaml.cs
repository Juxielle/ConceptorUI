using System.Windows;
using ConceptorUI.Models;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Runtime.Intrinsics.Arm;
using System;

namespace ConceptorUI.Views.ComponentP
{
    /// <summary>
    /// Logique d'interaction pour AlignmentProperty.xaml
    /// </summary>
    public partial class AlignmentProperty : UserControl
    {
        private string tag;
        private int index;
        static AlignmentProperty? _obj;
        private bool isContentAlignment;

        public AlignmentProperty()
        {
            InitializeComponent();
            _obj = this;
            tag = "HV";
            index = 0;
            isContentAlignment = true;
        }

        public static AlignmentProperty Instance => _obj == null! ? new AlignmentProperty() : _obj;

        public void Refresh(bool value)
        {
            isContentAlignment = value;
            Title.Text = value ? "CONTENT ALIGNMENT" : "SELF ALIGNMENT";
            if (!value)
            {
                gridSpace.Visibility = Visibility.Collapsed;
                content.Height = 65;
            }
        }

        public void FeedProps()
        {
            BHL.Visibility = gridSpace.Visibility = BHC.Visibility = BHR.Visibility = BVT.Visibility = 
            BVC.Visibility = BVB.Visibility = Visibility.Collapsed;

            var pos = Properties.GetPosition(isContentAlignment ? GroupNames.Alignment : GroupNames.SelfAlignment, PropertyNames.HL);
            foreach (var prop in Properties.groupProps![pos[0]].Properties)
            {
                if (prop.Visibility == VisibilityValue.Visible.ToString())
                {
                    if (prop.Name == PropertyNames.HL.ToString())
                    {
                        BHL.Visibility = Visibility.Visible;
                        HL.Foreground = new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.HC.ToString())
                    {
                        BHC.Visibility = Visibility.Visible;
                        HC.Foreground = new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.HR.ToString())
                    {
                        BHR.Visibility = Visibility.Visible;
                        HR.Foreground = new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.VT.ToString())
                    {
                        BVT.Visibility = Visibility.Visible;
                        VT.Foreground = new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.VC.ToString())
                    {
                        BVC.Visibility = Visibility.Visible;
                        VC.Foreground = new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.VB.ToString())
                    {
                        BVB.Visibility = Visibility.Visible;
                        VB.Foreground = new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.SpaceBetween.ToString())
                    {
                        BSB.Visibility = Visibility.Visible; gridSpace.Visibility = Visibility.Visible;
                        BSB.BorderBrush = BSB1.Foreground = BSB2.Foreground = BSB3.Foreground = BSB4.Foreground = 
                            new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.SpaceAround.ToString())
                    {
                        BSA.Visibility = Visibility.Visible;
                        BSA.BorderBrush = BSA1.Foreground = BSA2.Foreground = BSA3.Foreground = BSA4.Foreground =
                            new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.SpaceEvery.ToString())
                    {
                        BSE.Visibility = Visibility.Visible;
                        BSE.BorderBrush = BSE1.Foreground = BSE2.Foreground = BSE3.Foreground = BSE4.Foreground =
                            new BrushConverter().ConvertFrom(prop.Value == "0" ? "#8c8c8a" : "#6739b7") as SolidColorBrush;
                    }
                }
            }
        }

        private void BtnClick(object sender, MouseButtonEventArgs e)
        {
            var tag = (sender as Border)!.Tag.ToString()!;
            PropertyNames idP = PropertyNames.None, idE = PropertyNames.None;
            var idEN = -1;
            switch (tag)
            {
                case "HL": idP = PropertyNames.HL; idE = PropertyNames.Width; idEN = 0; break;
                case "HC": idP = PropertyNames.HC; idE = PropertyNames.Width; idEN = 0; break;
                case "HR": idP = PropertyNames.HR; idE = PropertyNames.Width; idEN = 0; break;
                case "VT": idP = PropertyNames.VT; idE = PropertyNames.Height; idEN = 1; break;
                case "VC": idP = PropertyNames.VC; idE = PropertyNames.Height; idEN = 1; break;
                case "VB": idP = PropertyNames.VB; idE = PropertyNames.Height; idEN = 1; break;
                case "SB": idP = PropertyNames.SpaceBetween; break;
                case "SA": idP = PropertyNames.SpaceAround; break;
                case "SE": idP = PropertyNames.SpaceEvery; break;
            }
            if (idP != PropertyNames.None)
            {
                var pos = Properties.GetPosition(isContentAlignment ? GroupNames.Alignment : GroupNames.SelfAlignment, idP);
                int idG = pos[0], idPS = pos[1];

                var value = "1";
                if(isContentAlignment) value = Properties.groupProps![idG].Properties[idPS].Value == "0" ? "1" : "0";
                PanelProperty.Instance.ReactToProps(isContentAlignment ? GroupNames.Alignment : GroupNames.SelfAlignment, idP, value);
                LoadValue(tag!, value);
                if(!isContentAlignment && value == "1" && Properties.groupProps![idG].Properties[idEN].Value == SizeValue.Expand.ToString())
                {
                    PanelProperty.Instance.ReactToProps(GroupNames.Transform, idE, SizeValue.Auto.ToString());
                    PanelProperty.Instance.FeedProps();
                }
            }
        }

        public void LoadValue(string tag, string value)
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
