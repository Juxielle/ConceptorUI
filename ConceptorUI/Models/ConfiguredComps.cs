using System;
using System.Collections.Generic;



namespace ConceptorUI.Models
{
    class ConfiguredComps
    {
        public List<ComponentOut> Text;
        public List<ComponentOut> TextSimple;
        public List<ComponentOut> Image;
        public List<ComponentOut> Icon;
        public List<ComponentOut> TextButton;
        public List<ComponentOut> IconButton;
        public List<ComponentOut> Container;
        public List<ComponentOut> Stack;
        public List<ComponentOut> ListV;
        public List<ComponentOut> ListH;
        public List<ComponentOut> Column;
        public List<ComponentOut> Row;
        public List<ComponentOut> Grid;
        public List<ComponentOut> Table;

        private static ConfiguredComps? _obj;

        public ConfiguredComps()
        {
            _obj = this;
            configTextSimple();
            configImage();
            configTextButton();
            configText();
            configIcon();
            configContainer();
            configStack();
            configListV();
            configListH();
            configColunm();
            configRow();
            configGrid();
            configTable();
        }

        public static ConfiguredComps Instance
        {
            get { return _obj ?? (_obj = new ConfiguredComps()); }
            set { value = _obj!; }
        }

        private void configTextSimple()
        {
            #region
            TextSimple = new List<ComponentOut>
            {
                new ComponentOut()
                {
                    ParentId = 0,
                    Name = ComponentList.TextSingle.ToString(),
                    Component = new List<GroupProperties>
                    {
                        new GroupProperties()
                        {
                            Name = GroupNames.Alignment.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceBetween.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceAround.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceEvery.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Transform.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Width.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Expand.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Height.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Auto.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.X.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Y.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ROT.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HVE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Text.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.FontFamily.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "Calibri",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FontWeight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FontStyle.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FontSize.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "12",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.AlignLeft.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.AlignCenter.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.AlignRight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.AlignJustify.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ListOrd.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ListNOrd.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.TabLeft.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.TabRight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.TextUnderline.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.TextOverline.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.TextThrough.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.TextError.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.TextIndex.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.TextExpo.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Color.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "#FF000000",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Text.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "My text",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.DisplayText.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.TextWrap.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.LineSpacing.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Appearance.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.FillColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CMargin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.MarginLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Margin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CPadding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.PaddingLeft.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Padding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorder.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "BorderLeft",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Opacity.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.SelfAlignment.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Global.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.SelectedMode.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = ESelectedMode.Single.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FilePicker.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveLeft.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveRight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveTop.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveBottom.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Focus.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveParentToChild.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveChildToParent.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Trash.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CanSelect.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = CanSelectValues.None.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        }
                    }
                }
            };
            #endregion
        }

        private void configText()
        {

        }

        private void configImage()
        {
            #region
            Image = new List<ComponentOut>
            {
                new ComponentOut()
                {
                    ParentId = 0,
                    Name = ComponentList.Image.ToString(),
                    Component = new List<GroupProperties>
                    {
                        new GroupProperties()
                        {
                            Name = GroupNames.Alignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceBetween.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceAround.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceEvery.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Transform.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Width.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "50",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Height.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "50",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.X.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Y.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ROT.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HVE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Stretch.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "Uniform",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Appearance.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.FillColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CMargin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.MarginLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Margin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CPadding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.PaddingLeft.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Padding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorder.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "BorderLeft",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Opacity.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.SelfAlignment.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Global.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.SelectedMode.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = ESelectedMode.Single.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FilePicker.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "/Assets/image.png",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveLeft.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveRight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveTop.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveBottom.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Focus.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveParentToChild.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveChildToParent.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Trash.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CanSelect.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = CanSelectValues.None.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        }
                    }
                }
            };
            #endregion
        }

        private void configIcon()
        {
            #region
            Icon = new List<ComponentOut>
            {
                new ComponentOut()
                {
                    ParentId = 0,
                    Name = ComponentList.Icon.ToString(),
                    Component = new List<GroupProperties>
                    {
                        new GroupProperties()
                        {
                            Name = GroupNames.Alignment.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceBetween.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceAround.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceEvery.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Transform.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Width.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "30",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Height.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "30",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.X.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Y.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ROT.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HVE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Appearance.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.FillColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "#FF26C6DA",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CMargin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.MarginLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Margin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CPadding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.PaddingLeft.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Padding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorder.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "BorderLeft",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Opacity.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.SelfAlignment.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Global.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.SelectedMode.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = ESelectedMode.Single.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FilePicker.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "Music",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveLeft.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveRight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveTop.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveBottom.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Focus.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveParentToChild.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveChildToParent.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Trash.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CanSelect.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = CanSelectValues.None.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        }
                    }
                }
            };
            #endregion
        }

        private void configTextButton()
        {
            #region
            TextButton = new List<ComponentOut>
            {
                new ComponentOut()
                {
                    ParentId = 0,
                    Name = ComponentList.Button.ToString(),
                    Component = new List<GroupProperties>
                    {
                        new GroupProperties()
                        {
                            Name = GroupNames.Alignment.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceBetween.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceAround.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceEvery.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Transform.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Width.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Auto.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Height.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Auto.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.X.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Y.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ROT.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HVE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Text.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.FontFamily.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FontWeight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FontStyle.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FontSize.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "10",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.AlignLeft.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.AlignCenter.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.AlignRight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.AlignJustify.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ListOrd.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ListNOrd.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.TabLeft.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.TabRight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.TextUnderline.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.TextOverline.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.TextThrough.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.TextError.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.TextIndex.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.TextExpo.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Color.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "#6739b7",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Text.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "Text",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.DisplayText.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Appearance.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.FillColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "#FF26C6DA",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CMargin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.MarginLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Margin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CPadding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.PaddingLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Padding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "10",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "8",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "10",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "8",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorder.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "BorderLeft",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.SelfAlignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Global.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.SelectedMode.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = ESelectedMode.Single.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FilePicker.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveLeft.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveRight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveTop.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveBottom.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Focus.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveParentToChild.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveChildToParent.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Trash.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CanSelect.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = CanSelectValues.None.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        }
                    }
                },
                new ComponentOut()
                {
                    ParentId = 0,
                    Name = "Text",
                    Component = new List<GroupProperties>
                    {
                        new GroupProperties()
                        {
                            Name = GroupNames.Alignment.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceBetween.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceAround.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceEvery.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Transform.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Width.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Auto.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Height.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Auto.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.X.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Y.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ROT.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HVE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Text.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.FontFamily.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FontWeight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FontStyle.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FontSize.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "12",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.AlignLeft.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.AlignCenter.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.AlignRight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.AlignJustify.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ListOrd.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ListNOrd.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.TabLeft.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.TabRight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.TextUnderline.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.TextOverline.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.TextThrough.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.TextError.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.TextIndex.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.TextExpo.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Color.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "#6739b7",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Text.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "BUTTON",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.DisplayText.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Appearance.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.FillColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CMargin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.MarginLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Margin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CPadding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.PaddingLeft.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Padding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorder.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "BorderLeft",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.SelfAlignment.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Global.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.SelectedMode.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = ESelectedMode.Single.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FilePicker.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveLeft.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveRight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveTop.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveBottom.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Focus.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveParentToChild.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveChildToParent.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Trash.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CanSelect.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = CanSelectValues.None.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        }
                    }
                }
            };
            #endregion
        }

        private void configContainer()
        {
            #region
            Container = new List<ComponentOut>
            {
                new ComponentOut()
                {
                    ParentId = 0,
                    Name = ComponentList.Container.ToString(),
                    Component = new List<GroupProperties>
                    {
                        new GroupProperties()
                        {
                            Name = GroupNames.Alignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceBetween.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceAround.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceEvery.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Transform.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Width.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Expand.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Height.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Expand.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.X.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Y.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ROT.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HVE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Appearance.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.FillColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CMargin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.MarginLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Margin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CPadding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.PaddingLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Padding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorder.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "BorderLeft",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Opacity.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.SelfAlignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.GridProperty.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Row.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Column.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Fusion.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Merged.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ColumnSpan.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.RowSpan.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SelectedElement.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ESelectedElement.Cell.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HideBorder.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Global.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.SelectedMode.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = ESelectedMode.Single.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FilePicker.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveLeft.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveRight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveTop.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveBottom.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Focus.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveParentToChild.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveChildToParent.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Trash.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CanSelect.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = CanSelectValues.None.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        },
                        new ()
                        {
                            Name = GroupNames.Shadow.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new ()
                                {
                                    Name = PropertyNames.ShadowDepth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new ()
                                {
                                    Name = PropertyNames.BlurRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new ()
                                {
                                    Name = PropertyNames.ShadowDirection.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new()
                                {
                                    Name = PropertyNames.ShadowColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                    }
                }
            };
            #endregion
        }

        private void configStack()
        {
            #region
            Stack = new List<ComponentOut>
            {
                new ComponentOut()
                {
                    ParentId = 0,
                    Name = ComponentList.Stack.ToString(),
                    Component = new List<GroupProperties>
                    {
                        new GroupProperties()
                        {
                            Name = GroupNames.Alignment.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceBetween.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceAround.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceEvery.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Transform.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Width.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Expand.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Height.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Expand.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.X.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Y.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ROT.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HVE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Appearance.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.FillColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CMargin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.MarginLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Margin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CPadding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.PaddingLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Padding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorder.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "BorderLeft",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Opacity.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.SelfAlignment.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Global.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.SelectedMode.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = ESelectedMode.Single.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FilePicker.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveLeft.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveRight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveTop.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveBottom.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Focus.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveParentToChild.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveChildToParent.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Trash.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CanSelect.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = CanSelectValues.None.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        }
                    }
                }
            };
            #endregion
        }

        private void configListV()
        {
            #region
            ListV = new List<ComponentOut>
            {
                new ComponentOut()
                {
                    ParentId = 0,
                    Name = ComponentList.ListV.ToString(),
                    Component = new List<GroupProperties>
                    {
                        new GroupProperties()
                        {
                            Name = GroupNames.Transform.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Width.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Expand.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Height.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Expand.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.X.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Y.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ROT.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HVE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Appearance.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.FillColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CMargin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.MarginLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Margin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CPadding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.PaddingLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Padding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorder.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "BorderLeft",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Opacity.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.SelfAlignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Global.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.SelectedMode.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = ESelectedMode.Single.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FilePicker.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveLeft.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveRight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveTop.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveBottom.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Focus.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveParentToChild.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveChildToParent.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Trash.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CanSelect.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = CanSelectValues.None.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        }
                    }
                }
            };
            #endregion
        }

        private void configListH()
        {
            #region
            ListH = new List<ComponentOut>
            {
                new ComponentOut()
                {
                    ParentId = 0,
                    Name = ComponentList.ListH.ToString(),
                    Component = new List<GroupProperties>
                    {
                        new GroupProperties()
                        {
                            Name = GroupNames.Transform.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Width.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Expand.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Height.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Expand.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.X.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Y.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ROT.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HVE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Appearance.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.FillColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CMargin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.MarginLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Margin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CPadding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.PaddingLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Padding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorder.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "BorderLeft",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Opacity.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.SelfAlignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Global.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.SelectedMode.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = ESelectedMode.Single.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FilePicker.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveLeft.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveRight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveTop.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveBottom.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Focus.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveParentToChild.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveChildToParent.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Trash.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CanSelect.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = CanSelectValues.None.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        }
                    }
                }
            };
            #endregion
        }

        private void configColunm()
        {
            #region
            Column = new List<ComponentOut>
            {
                new ComponentOut()
                {
                    ParentId = 0,
                    Name = ComponentList.Column.ToString(),
                    Component = new List<GroupProperties>
                    {
                        new GroupProperties()
                        {
                            Name = GroupNames.Alignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceBetween.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceAround.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceEvery.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Transform.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Width.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Expand.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Height.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Expand.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.X.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Y.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ROT.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HVE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Appearance.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.FillColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CMargin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.MarginLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Margin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CPadding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.PaddingLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Padding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorder.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "BorderLeft",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Opacity.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.SelfAlignment.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Global.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.SelectedMode.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = ESelectedMode.Single.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FilePicker.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveLeft.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveRight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveTop.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveBottom.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Focus.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveParentToChild.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveChildToParent.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Trash.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CanSelect.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = CanSelectValues.None.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        }
                    }
                }
            };
            #endregion
        }

        private void configRow()
        {
            #region
            Row = new List<ComponentOut>
            {
                new ComponentOut()
                {
                    ParentId = 0,
                    Name = ComponentList.Row.ToString(),
                    Component = new List<GroupProperties>
                    {
                        new GroupProperties()
                        {
                            Name = GroupNames.Alignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceBetween.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceAround.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceEvery.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Transform.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Width.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Expand.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Height.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Expand.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.X.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Y.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ROT.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HVE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Appearance.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.FillColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CMargin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.MarginLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Margin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CPadding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.PaddingLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Padding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorder.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "BorderLeft",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.SelfAlignment.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Global.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.SelectedMode.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = ESelectedMode.Single.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FilePicker.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveLeft.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveRight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveTop.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveBottom.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Focus.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveParentToChild.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveChildToParent.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Trash.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CanSelect.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = CanSelectValues.None.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        }
                    }
                }
            };
            #endregion
        }

        private void configGrid()
        {
            #region
            Grid = new List<ComponentOut>
            {
                new ComponentOut()
                {
                    ParentId = 0,
                    Name = ComponentList.Grid.ToString(),
                    Component = new List<GroupProperties>
                    {
                        new GroupProperties()
                        {
                            Name = GroupNames.Alignment.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceBetween.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceAround.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceEvery.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Transform.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Width.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Expand.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Height.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Auto.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.X.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Y.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ROT.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HVE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Appearance.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.FillColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "Transparent",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CMargin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.MarginLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Margin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CPadding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.PaddingLeft.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Padding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorder.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "BorderLeft",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.SelfAlignment.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.GridProperty.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.SelectedElement.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ESelectedElement.Cell.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HideBorder.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Global.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.SelectedMode.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = ESelectedMode.Single.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FilePicker.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveLeft.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveRight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveTop.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveBottom.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Focus.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveParentToChild.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveChildToParent.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Trash.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CanSelect.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = CanSelectValues.None.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        }
                    }
                },
                new ComponentOut()
                {
                    ParentId = 0,
                    Name = ComponentList.Container.ToString(),
                    Component = new List<GroupProperties>
                    {
                        new GroupProperties()
                        {
                            Name = GroupNames.Alignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceBetween.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceAround.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceEvery.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Transform.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Width.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Expand.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Height.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Expand.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.X.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Y.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ROT.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HVE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Appearance.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.FillColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "#FFE0E0E0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CMargin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.MarginLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Margin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CPadding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.PaddingLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Padding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorder.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "BorderLeft",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.SelfAlignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.GridProperty.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Row.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Column.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Fusion.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Merged.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ColumnSpan.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.RowSpan.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SelectedElement.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ESelectedElement.Cell.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HideBorder.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Global.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.SelectedMode.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = ESelectedMode.Single.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FilePicker.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveLeft.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveRight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveTop.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveBottom.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Focus.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveParentToChild.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveChildToParent.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Trash.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CanSelect.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = CanSelectValues.None.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        }
                    }
                },
                new ComponentOut()
                {
                    ParentId = 0,
                    Name = ComponentList.Container.ToString(),
                    Component = new List<GroupProperties>
                    {
                        new GroupProperties()
                        {
                            Name = GroupNames.Alignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceBetween.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceAround.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceEvery.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Transform.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Width.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Expand.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Height.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Expand.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.X.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Y.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ROT.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HVE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Appearance.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.FillColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "#FFE0E0E0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CMargin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.MarginLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Margin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CPadding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.PaddingLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Padding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorder.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "BorderLeft",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.SelfAlignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.GridProperty.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Row.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Column.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Fusion.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Merged.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ColumnSpan.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.RowSpan.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SelectedElement.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ESelectedElement.Cell.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HideBorder.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Global.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.SelectedMode.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = ESelectedMode.Single.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FilePicker.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveLeft.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveRight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveTop.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveBottom.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Focus.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveParentToChild.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveChildToParent.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Trash.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CanSelect.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = CanSelectValues.None.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        }
                    }
                },
                new ComponentOut()
                {
                    ParentId = 0,
                    Name = ComponentList.Container.ToString(),
                    Component = new List<GroupProperties>
                    {
                        new GroupProperties()
                        {
                            Name = GroupNames.Alignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceBetween.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceAround.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceEvery.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Transform.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Width.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Expand.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Height.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Expand.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.X.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Y.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ROT.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HVE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Appearance.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.FillColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "Transparent",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CMargin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.MarginLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Margin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CPadding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.PaddingLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Padding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorder.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "BorderLeft",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.SelfAlignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.GridProperty.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Row.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Column.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Fusion.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Merged.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ColumnSpan.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.RowSpan.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SelectedElement.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ESelectedElement.Cell.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HideBorder.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Global.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.SelectedMode.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = ESelectedMode.Single.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FilePicker.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveLeft.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveRight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveTop.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveBottom.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Focus.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveParentToChild.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveChildToParent.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Trash.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CanSelect.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = CanSelectValues.None.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        }
                    }
                },
                new ComponentOut()
                {
                    ParentId = 0,
                    Name = ComponentList.Container.ToString(),
                    Component = new List<GroupProperties>
                    {
                        new GroupProperties()
                        {
                            Name = GroupNames.Alignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceBetween.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceAround.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceEvery.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Transform.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Width.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Expand.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Height.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Expand.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.X.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Y.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ROT.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HVE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Appearance.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.FillColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "Transparent",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CMargin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.MarginLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Margin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CPadding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.PaddingLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Padding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorder.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "BorderLeft",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.SelfAlignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.GridProperty.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Row.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Column.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Fusion.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Merged.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ColumnSpan.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.RowSpan.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SelectedElement.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ESelectedElement.Cell.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HideBorder.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Global.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.SelectedMode.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = ESelectedMode.Single.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FilePicker.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveLeft.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveRight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveTop.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveBottom.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Focus.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveParentToChild.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveChildToParent.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Trash.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CanSelect.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = CanSelectValues.None.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        }
                    }
                },
            };
            #endregion
        }

        private void configTable()
        {
            #region
            Table = new List<ComponentOut>
            {
                new ComponentOut()
                {
                    ParentId = 0,
                    Name = ComponentList.Table.ToString(),
                    Component = new List<GroupProperties>
                    {
                        new GroupProperties()
                        {
                            Name = GroupNames.Alignment.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceBetween.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceAround.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceEvery.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Transform.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Width.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Expand.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Height.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Auto.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.X.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Y.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ROT.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HVE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Appearance.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.FillColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "Transparent",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CMargin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.MarginLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Margin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CPadding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.PaddingLeft.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Padding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorder.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "BorderLeft",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.SelfAlignment.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.GridProperty.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.SelectedElement.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ESelectedElement.Cell.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Global.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.SelectedMode.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = ESelectedMode.Single.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FilePicker.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveLeft.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveRight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveTop.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveBottom.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Focus.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveParentToChild.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveChildToParent.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Trash.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CanSelect.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = CanSelectValues.None.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        }
                    }
                },
                new ComponentOut()
                {
                    ParentId = 0,
                    Name = ComponentList.Container.ToString(),
                    Component = new List<GroupProperties>
                    {
                        new GroupProperties()
                        {
                            Name = GroupNames.Alignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceBetween.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceAround.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceEvery.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Transform.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Width.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Expand.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Height.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "100",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.X.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Y.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ROT.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HVE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Appearance.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.FillColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "#FFE0E0E0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CMargin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.MarginLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Margin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CPadding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.PaddingLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Padding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorder.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "BorderLeft",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.SelfAlignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.GridProperty.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Row.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Column.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Fusion.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Merged.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ColumnSpan.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.RowSpan.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SelectedElement.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ESelectedElement.Cell.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Global.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.SelectedMode.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = ESelectedMode.Single.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FilePicker.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveLeft.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveRight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveTop.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveBottom.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Focus.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveParentToChild.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveChildToParent.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Trash.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CanSelect.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = CanSelectValues.None.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        }
                    }
                },
                new ComponentOut()
                {
                    ParentId = 0,
                    Name = ComponentList.Container.ToString(),
                    Component = new List<GroupProperties>
                    {
                        new GroupProperties()
                        {
                            Name = GroupNames.Alignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceBetween.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceAround.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceEvery.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Transform.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Width.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Expand.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Height.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "100",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.X.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Y.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ROT.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HVE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Appearance.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.FillColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "#FFE0E0E0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CMargin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.MarginLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Margin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CPadding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.PaddingLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Padding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorder.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "BorderLeft",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.SelfAlignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.GridProperty.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Row.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Column.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Fusion.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Merged.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ColumnSpan.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.RowSpan.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SelectedElement.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ESelectedElement.Cell.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Global.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.SelectedMode.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = ESelectedMode.Single.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FilePicker.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveLeft.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveRight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveTop.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveBottom.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Focus.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveParentToChild.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveChildToParent.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Trash.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CanSelect.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = CanSelectValues.None.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        }
                    }
                },
                new ComponentOut()
                {
                    ParentId = 0,
                    Name = ComponentList.Container.ToString(),
                    Component = new List<GroupProperties>
                    {
                        new GroupProperties()
                        {
                            Name = GroupNames.Alignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceBetween.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceAround.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceEvery.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Transform.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Width.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Expand.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Height.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "100",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.X.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Y.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ROT.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HVE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Appearance.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.FillColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "Transparent",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CMargin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.MarginLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Margin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CPadding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.PaddingLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Padding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorder.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "BorderLeft",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.SelfAlignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.GridProperty.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Row.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Column.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Fusion.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Merged.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ColumnSpan.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.RowSpan.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SelectedElement.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ESelectedElement.Cell.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Global.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.SelectedMode.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = ESelectedMode.Single.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FilePicker.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveLeft.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveRight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveTop.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveBottom.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Focus.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveParentToChild.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveChildToParent.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Trash.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CanSelect.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = CanSelectValues.None.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        }
                    }
                },
                new ComponentOut()
                {
                    ParentId = 0,
                    Name = ComponentList.Container.ToString(),
                    Component = new List<GroupProperties>
                    {
                        new GroupProperties()
                        {
                            Name = GroupNames.Alignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceBetween.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceAround.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceEvery.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Transform.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Width.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Expand.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Height.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "100",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.X.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Y.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ROT.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HVE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Appearance.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.FillColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "Transparent",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CMargin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.MarginLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Margin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CPadding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.PaddingLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Padding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorder.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "BorderLeft",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.SelfAlignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.GridProperty.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Row.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Column.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Fusion.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Merged.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ColumnSpan.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.RowSpan.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SelectedElement.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ESelectedElement.Cell.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Global.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.SelectedMode.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = ESelectedMode.Single.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FilePicker.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveLeft.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveRight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveTop.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveBottom.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Focus.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveParentToChild.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveChildToParent.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Trash.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CanSelect.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = CanSelectValues.None.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        }
                    }
                },
                new ComponentOut()
                {
                    ParentId = 0,
                    Name = ComponentList.Container.ToString(),
                    Component = new List<GroupProperties>
                    {
                        new GroupProperties()
                        {
                            Name = GroupNames.Alignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceBetween.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceAround.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceEvery.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Transform.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Width.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Expand.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Height.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "100",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.X.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Y.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ROT.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HVE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Appearance.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.FillColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "Transparent",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CMargin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.MarginLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Margin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CPadding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.PaddingLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Padding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorder.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "BorderLeft",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.SelfAlignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.GridProperty.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Row.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "2",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Column.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Fusion.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Merged.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ColumnSpan.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.RowSpan.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SelectedElement.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ESelectedElement.Cell.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Global.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.SelectedMode.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = ESelectedMode.Single.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FilePicker.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveLeft.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveRight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveTop.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveBottom.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Focus.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveParentToChild.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveChildToParent.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Trash.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CanSelect.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = CanSelectValues.None.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        }
                    }
                },
                new ComponentOut()
                {
                    ParentId = 0,
                    Name = ComponentList.Container.ToString(),
                    Component = new List<GroupProperties>
                    {
                        new GroupProperties()
                        {
                            Name = GroupNames.Alignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceBetween.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceAround.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceEvery.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Transform.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Width.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Expand.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Height.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "100",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.X.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Y.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ROT.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HVE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Appearance.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.FillColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "Transparent",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CMargin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.MarginLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Margin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CPadding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.PaddingLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Padding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorder.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "BorderLeft",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.SelfAlignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.GridProperty.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Row.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "2",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Column.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Fusion.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Merged.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ColumnSpan.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.RowSpan.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SelectedElement.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ESelectedElement.Cell.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Global.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.SelectedMode.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = ESelectedMode.Single.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FilePicker.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveLeft.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveRight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveTop.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveBottom.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Focus.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveParentToChild.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveChildToParent.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Trash.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CanSelect.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = CanSelectValues.None.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        }
                    }
                },
                new ComponentOut()
                {
                    ParentId = 0,
                    Name = ComponentList.Container.ToString(),
                    Component = new List<GroupProperties>
                    {
                        new GroupProperties()
                        {
                            Name = GroupNames.Alignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceBetween.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceAround.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceEvery.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Transform.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Width.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Expand.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Height.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "100",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.X.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Y.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ROT.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HVE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Appearance.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.FillColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "Transparent",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CMargin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.MarginLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Margin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CPadding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.PaddingLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Padding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorder.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "BorderLeft",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.SelfAlignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.GridProperty.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Row.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "3",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Column.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Fusion.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Merged.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ColumnSpan.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.RowSpan.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SelectedElement.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ESelectedElement.Cell.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Global.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.SelectedMode.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = ESelectedMode.Single.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FilePicker.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveLeft.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveRight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveTop.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveBottom.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Focus.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveParentToChild.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveChildToParent.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Trash.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CanSelect.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = CanSelectValues.None.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        }
                    }
                },
                new ComponentOut()
                {
                    ParentId = 0,
                    Name = ComponentList.Container.ToString(),
                    Component = new List<GroupProperties>
                    {
                        new GroupProperties()
                        {
                            Name = GroupNames.Alignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceBetween.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceAround.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SpaceEvery.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Transform.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Width.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = SizeValue.Expand.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Height.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "100",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.X.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Y.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ROT.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HVE.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Appearance.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.FillColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "Transparent",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CMargin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.MarginLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Margin.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MarginBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CPadding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.PaddingLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Padding.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingTop.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.PaddingBottom.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorder.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "BorderLeft",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderLeftStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderTopStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRightStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomWidth.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomColor.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ColorValue.Transparent.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderBottomStyle.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CBorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadBtnActif.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadius.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomLeft.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusTopRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.BorderRadiusBottomRight.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.SelfAlignment.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.HL.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.HR.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VT.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VC.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.VB.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                }
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.GridProperty.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.Row.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "3",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Column.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Fusion.ToString(),//Indique que cette cellule résulte de plusieurs cellules en fusion.
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Merged.ToString(),//Indique que cette cellules est en fusion avec d'autres cellules.
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.ColumnSpan.ToString(),//nombre de colonnes à fusionner.
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.RowSpan.ToString(),//nombre de lignes à fusionner.
                                    Type = PropertyTypes.String.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.SelectedElement.ToString(),
                                    Type = PropertyTypes.String.ToString(),
                                    Value = ESelectedElement.Cell.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                            }
                        },
                        new GroupProperties()
                        {
                            Name = GroupNames.Global.ToString(),
                            Visibility = VisibilityValue.Visible.ToString(),
                            Properties = new List<Property>
                            {
                                new Property()
                                {
                                    Name = PropertyNames.SelectedMode.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = ESelectedMode.Single.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.FilePicker.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveLeft.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveRight.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveTop.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveBottom.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Focus.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "1",
                                    Visibility = VisibilityValue.Collapsed.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveParentToChild.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.MoveChildToParent.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.Trash.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = "0",
                                    Visibility = VisibilityValue.Visible.ToString()
                                },
                                new Property()
                                {
                                    Name = PropertyNames.CanSelect.ToString(),
                                    Type = PropertyTypes.Action.ToString(),
                                    Value = CanSelectValues.None.ToString(),
                                    Visibility = VisibilityValue.Visible.ToString()
                                }
                            }
                        }
                    }
                },
            };
            #endregion
        }

    }
}
