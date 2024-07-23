using ConceptorUI.ViewModels;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;


namespace ConceptorUI.Models
{
    class Properties
    {
        public static List<GroupProperties>? groupProps;
        public static ComponentList ComponentName = ComponentList.Window;
        public static List<ComponentOut>? ComponentOuts;
        public static List<ComponentOut>? ComponentCopy = null;
        public static List<ComponentOut>? ComponentOutsUsing;
        public static string CopiedComponent = null!;
        public int SelectedLeftOnglet;

        public List<ReportModel> Components;
        public List<ComponentModel> ComponentMNS;
        public int SelectedComponent;

        public List<SpaceReportModel> SpaceReports;
        public int SelectedSpace;
        public int SelectedReport;
        public ConfigAppInfo ConfigAppInfo;

        private static Properties? _obj;
        public bool EditionMode;

        public List<Icons> Icons;
        public DbIcons DialogBoxIcon;

        public List<Reference> References;

        public List<IconPack> IconPacks;

        public Properties()
        {
            _obj = this;
            SelectedLeftOnglet = 1;

            Components = new List<ReportModel>();
            ComponentMNS = new List<ComponentModel>();
            SelectedComponent = 0;

            SpaceReports = new List<SpaceReportModel>();
            SelectedSpace = 0;
            SelectedReport = 0;

            ConfigAppInfo = new ConfigAppInfo();
            EditionMode = true;

            References = new List<Reference>();

            IconPacks = new List<IconPack>();

            InitIcons();
        }

        public static Properties Instance => _obj == null! ? new Properties() : _obj;

        public static void InitProps()
        {
            #region
            groupProps = new List<GroupProperties>
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
                    Visibility = VisibilityValue.Collapsed.ToString(),
                    Properties = new List<Property>
                    {
                        new Property()
                        {
                            Name = PropertyNames.Width.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.Height.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
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
                            Visibility = VisibilityValue.Visible.ToString()
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
                    Visibility = VisibilityValue.Collapsed.ToString(),
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
                            Value = "1",
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
                    Name = GroupNames.GridProperty.ToString(),
                    Visibility = VisibilityValue.Collapsed.ToString(),
                    Properties = new List<Property>
                    {
                        new Property()
                        {
                            Name = PropertyNames.SelectedMode.ToString(),
                            Type = PropertyTypes.List.ToString(),
                            Value = ESelectedMode.Single.ToString(),
                            Items = new string[] { "Single", "Multiple" },
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.SelectedElement.ToString(),
                            Type = PropertyTypes.List.ToString(),
                            Value = ESelectedElement.Cell.ToString(),
                            Items = new string[] { "Row", "Column", "Cell" },
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.ElementSize.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = SizeValue.Auto.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = GridActions.Add.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = GridActions.Delete.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = GridActions.MoveToStart.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = GridActions.MoveToEnd.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString()
                        }
                    }
                },
                new GroupProperties()
                {
                    Name = GroupNames.ParentProperty.ToString(),
                    Visibility = VisibilityValue.Collapsed.ToString(),
                    Properties = new List<Property>
                    {
                        new Property()
                        {
                            Name = PropertyNames.Row.ToString(),
                            Type = PropertyTypes.List.ToString(),
                            Value = "0",
                            Items = new string[] { "0" },
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.RowSpan.ToString(),
                            Type = PropertyTypes.List.ToString(),
                            Value = "1",
                            Items = new string[] { "1" },
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.Column.ToString(),
                            Type = PropertyTypes.List.ToString(),
                            Value = "0",
                            Items = new string[] { "0" },
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.ColumnSpan.ToString(),
                            Type = PropertyTypes.List.ToString(),
                            Value = "1",
                            Items = new string[] { "1" },
                            Visibility = VisibilityValue.Collapsed.ToString()
                        }
                    }
                },
                new GroupProperties()
                {
                    Name = GroupNames.Global.ToString(),
                    Visibility = VisibilityValue.Collapsed.ToString(),
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
                        }
                    }
                }
            };
            #endregion
        }

        public static void SetComponentList(int id)
        {
            Console.WriteLine("Name: " + ComponentOuts![id].Name);
            ComponentOutsUsing!.Add(ComponentOuts[id]);
            foreach (var child in ComponentOuts)
                if (child.ParentId == id) ComponentOutsUsing.Add(child);
        }

        public static void InitComps(ComponentList name)
        {
            if (name == ComponentList.TextSingle) ComponentOutsUsing = ConfiguredComps.Instance.TextSimple;
            else if (name == ComponentList.Image) ComponentOutsUsing = ConfiguredComps.Instance.Image;
            else if (name == ComponentList.Button) ComponentOutsUsing = ConfiguredComps.Instance.TextButton;
            else if (name == ComponentList.Icon) ComponentOutsUsing = ConfiguredComps.Instance.Icon;
            else if (name == ComponentList.Container) ComponentOutsUsing = ConfiguredComps.Instance.Container;
            else if (name == ComponentList.Stack) ComponentOutsUsing = ConfiguredComps.Instance.Stack;
            else if (name == ComponentList.ListV) ComponentOutsUsing = ConfiguredComps.Instance.ListV;
            else if (name == ComponentList.ListH) ComponentOutsUsing = ConfiguredComps.Instance.ListH;
            else if (name == ComponentList.Row) ComponentOutsUsing = ConfiguredComps.Instance.Row;
            else if (name == ComponentList.Column) ComponentOutsUsing = ConfiguredComps.Instance.Column;
            else if (name == ComponentList.Grid) ComponentOutsUsing = ConfiguredComps.Instance.Grid;
            else if (name == ComponentList.Table) ComponentOutsUsing = ConfiguredComps.Instance.Table;
            else ComponentOutsUsing = ConfiguredComps.Instance.TextSimple;
        }

        public static int[] GetPosition(GroupNames groupName, PropertyNames propertyName)
        {
            int[] position = { -1, -1 };
            if (groupProps != null)
            {
                foreach (var group in groupProps)
                {
                    if (group.Name == groupName.ToString())
                    {
                        position[0] = groupProps.IndexOf(group);
                        foreach (var prop in group.Properties)
                        {
                            if (prop.Name == propertyName.ToString())
                            {
                                position[1] = group.Properties.IndexOf(prop);
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            return position;
        }

        private void InitIcons()
        {
            Icons = new List<Icons> {
                #region
                new ()
                {
                    Name = "music",
                    StringIcon = "Music",
                    Icon = new PackIcon{ Kind = PackIconKind.Music },
                    IconPlateform = new List<IconP>
                    {
                        new IconP{ Name = "music", Component = "FontAwesome", Library = "react-native-vector-icons/FontAwesome", Plateform = Plateforms.ReactNative },
                    }
                },
                new ()
                {
                    Name = "search",
                    StringIcon = "Magnify",
                    Icon = new PackIcon{ Kind = PackIconKind.Magnify },
                    IconPlateform = new List<IconP>
                    {
                        new IconP{ Name = "search", Component = "FontAwesome", Library = "react-native-vector-icons/FontAwesome", Plateform = Plateforms.ReactNative },
                    }
                },
                new ()
                {
                    Name = "search plus",
                    StringIcon = "MagnifyPlusOutline",
                    Icon = new PackIcon{ Kind = PackIconKind.MagnifyPlusOutline },
                    IconPlateform = new List<IconP>
                    {
                        new IconP{ Name = "search-plus", Component = "FontAwesome", Library = "react-native-vector-icons/FontAwesome", Plateform = Plateforms.ReactNative },
                    }
                },
                new ()
                {
                    Name = "search minus",
                    StringIcon = "MagnifyMinusOutline",
                    Icon = new PackIcon{ Kind = PackIconKind.MagnifyMinusOutline },
                    IconPlateform = new List<IconP>
                    {
                        new IconP{ Name = "search-minus", Component = "FontAwesome", Library = "react-native-vector-icons/FontAwesome", Plateform = Plateforms.ReactNative },
                    }
                },
                new ()
                {
                    Name = "email outline",
                    StringIcon = "EmailOutline",
                    Icon = new PackIcon{ Kind = PackIconKind.EmailOutline },
                    IconPlateform = new List<IconP>
                    {
                        new IconP{ Name = "envelope-o", Component = "FontAwesome", Library = "react-native-vector-icons/FontAwesome", Plateform = Plateforms.ReactNative },
                    }
                },
                new ()
                {
                    Name = "heart",
                    StringIcon = "Heart",
                    Icon = new PackIcon{ Kind = PackIconKind.Heart },
                    IconPlateform = new List<IconP>
                    {
                        new IconP{ Name = "heart", Component = "FontAwesome", Library = "react-native-vector-icons/FontAwesome", Plateform = Plateforms.ReactNative },
                    }
                },
                new ()
                {
                    Name = "star",
                    StringIcon = "Star",
                    Icon = new PackIcon{ Kind = PackIconKind.Star },
                    IconPlateform = new List<IconP>
                    {
                        new IconP{ Name = "star", Component = "FontAwesome", Library = "react-native-vector-icons/FontAwesome", Plateform = Plateforms.ReactNative },
                    }
                },
                new ()
                {
                    Name = "star outline",
                    StringIcon = "StarOutline",
                    Icon = new PackIcon{ Kind = PackIconKind.StarOutline },
                    IconPlateform = new List<IconP>
                    {
                        new IconP{ Name = "star-o", Component = "FontAwesome", Library = "react-native-vector-icons/FontAwesome", Plateform = Plateforms.ReactNative },
                    }
                },
                new ()
                {
                    Name = "user",
                    StringIcon = "Account",
                    Icon = new PackIcon{ Kind = PackIconKind.Account },
                    IconPlateform = new List<IconP>
                    {
                        new IconP{ Name = "user", Component = "FontAwesome", Library = "react-native-vector-icons/FontAwesome", Plateform = Plateforms.ReactNative },
                    }
                },
                new ()
                {
                    Name = "view grid",
                    StringIcon = "ViewGrid",
                    Icon = new PackIcon{ Kind = PackIconKind.ViewGrid },
                    IconPlateform = new List<IconP>
                    {
                        new IconP{ Name = "th-large", Component = "FontAwesome", Library = "react-native-vector-icons/FontAwesome", Plateform = Plateforms.ReactNative },
                    }
                },
                new ()
                {
                    Name = "view comfy",
                    StringIcon = "ViewComfy",
                    Icon = new PackIcon{ Kind = PackIconKind.ViewComfy },
                    IconPlateform = new List<IconP>
                    {
                        new IconP{ Name = "th", Component = "FontAwesome", Library = "react-native-vector-icons/FontAwesome", Plateform = Plateforms.ReactNative },
                    }
                },
                new ()
                {
                    Name = "view list",
                    StringIcon = "ViewList",
                    Icon = new PackIcon{ Kind = PackIconKind.ViewList },
                    IconPlateform = new List<IconP>
                    {
                        new IconP{ Name = "th-list", Component = "FontAwesome", Library = "react-native-vector-icons/FontAwesome", Plateform = Plateforms.ReactNative },
                    }
                },
                new ()
                {
                    Name = "check",
                    StringIcon = "Check",
                    Icon = new PackIcon{ Kind = PackIconKind.Check },
                    IconPlateform = new List<IconP>
                    {
                        new IconP{ Name = "check", Component = "FontAwesome", Library = "react-native-vector-icons/FontAwesome", Plateform = Plateforms.ReactNative },
                    }
                },
                new ()
                {
                    Name = "close",
                    StringIcon = "Close",
                    Icon = new PackIcon{ Kind = PackIconKind.Close },
                    IconPlateform = new List<IconP>
                    {
                        new IconP{ Name = "remove", Component = "FontAwesome", Library = "react-native-vector-icons/FontAwesome", Plateform = Plateforms.ReactNative },
                    }
                },
                new ()
                {
                    Name = "power",
                    StringIcon = "Power",
                    Icon = new PackIcon{ Kind = PackIconKind.Power },
                    IconPlateform = new List<IconP>
                    {
                        new IconP{ Name = "power-off", Component = "FontAwesome", Library = "react-native-vector-icons/FontAwesome", Plateform = Plateforms.ReactNative },
                    }
                },
                new ()
                {
                    Name = "signal",
                    StringIcon = "Signal",
                    Icon = new PackIcon{ Kind = PackIconKind.Signal },
                    IconPlateform = new List<IconP>
                    {
                        new IconP{ Name = "signal", Component = "FontAwesome", Library = "react-native-vector-icons/FontAwesome", Plateform = Plateforms.ReactNative },
                    }
                },
                new ()
                {
                    Name = "cog",
                    StringIcon = "Cog",
                    Icon = new PackIcon{ Kind = PackIconKind.Cog },
                    IconPlateform = new List<IconP>
                    {
                        new IconP{ Name = "cog", Component = "FontAwesome", Library = "react-native-vector-icons/FontAwesome", Plateform = Plateforms.ReactNative },
                    }
                },
                new ()
                {
                    Name = "trash outline",
                    StringIcon = "TrashCanOutline",
                    Icon = new PackIcon{ Kind = PackIconKind.TrashCanOutline },
                    IconPlateform = new List<IconP>
                    {
                        new IconP{ Name = "trash-o", Component = "FontAwesome", Library = "react-native-vector-icons/FontAwesome", Plateform = Plateforms.ReactNative },
                    }
                },
                new ()
                {
                    Name = "chevron left",
                    StringIcon = "ChevronLeft",
                    Icon = new PackIcon{ Kind = PackIconKind.ChevronLeft },
                    IconPlateform = new List<IconP>
                    {
                        new IconP{ Name = "chevron-left", Component = "FontAwesome", Library = "react-native-vector-icons/FontAwesome", Plateform = Plateforms.ReactNative },
                    }
                },
                new ()
                {
                    Name = "chevron right",
                    StringIcon = "ChevronRight",
                    Icon = new PackIcon{ Kind = PackIconKind.ChevronRight },
                    IconPlateform = new List<IconP>
                    {
                        new IconP{ Name = "chevron-right", Component = "FontAwesome", Library = "react-native-vector-icons/FontAwesome", Plateform = Plateforms.ReactNative },
                    }
                },
                #endregion
            };
        }

        public Icons GetIcon(string sIcon)
        {
            foreach(var icon in Icons)
            {
                if (sIcon == icon.StringIcon) return icon;
            }
            return new Icons
            {
                Name = "CameraIris",
                StringIcon = "CameraIris",
                Icon = new PackIcon { Kind = PackIconKind.CameraIris },
                IconPlateform = new List<IconP>
                {
                    new IconP{ Name = "music", Component = "FontAwesome", Library = "react-native-vector-icons/FontAwesome", Plateform = Plateforms.ReactNative },
                }
            };
        }
    }
}
