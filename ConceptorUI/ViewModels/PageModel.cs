using ConceptorUI.Models;
using ConceptorUI.Views.ComponentP;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls.Primitives;




namespace ConceptorUI.ViewModels
{
    class PageModel : Component
    {
        public Grid grid;
        public ContainerModel Header { get; set; }
        public ContainerModel Body { get; set; }
        public ContainerModel Footer { get; set; }
        public int Level { get; set; }
        Border borderP;
        Border border;

        public PageModel(bool isConstraints = false)
        {
            borderP = new Border();
            border = new Border();
            grid = new Grid();
            Level = 0;
            ComponentView = borderP;
            borderP.Child = border;
            border.Child = grid;
            EnumName = ComponentList.Page;

            RowDefinition rd0 = new RowDefinition(); rd0.Height = new GridLength(0, GridUnitType.Auto);
            RowDefinition rd1 = new RowDefinition(); rd1.Height = new GridLength(1, GridUnitType.Star);
            RowDefinition rd2 = new RowDefinition(); rd2.Height = new GridLength(0, GridUnitType.Auto);
            grid.RowDefinitions.Add(rd0); grid.RowDefinitions.Add(rd1); grid.RowDefinitions.Add(rd2);

            ComponentView.PreviewMouseDown += new MouseButtonEventHandler(OnMouseDown);
            OnConfigured();
            LoadIds();
            if (!isConstraints)
            {
                InitComponents();
                Grid.SetRow(Header!.ComponentView, 0); Grid.SetRow(Body!.ComponentView, 1); Grid.SetRow(Footer!.ComponentView, 2);
                grid.Children.Add(Header.ComponentView); grid.Children.Add(Body.ComponentView); grid.Children.Add(Footer.ComponentView);
                border.Child = grid;
                OnInitialized();
            }
        }

        private void InitComponents()
        {
            #region
            //Configure Header
            Header = new ContainerModel(true); Header.Parent = this;
            Header.OnAddConfig(GroupNames.Alignment, PropertyNames.HL, VisibilityValue.Collapsed.ToString());
            Header.OnAddConfig(GroupNames.SelfAlignment, PropertyNames.HL, VisibilityValue.Collapsed.ToString());
            Header.OnAddConfig(GroupNames.Transform, PropertyNames.Width, VisibilityValue.Collapsed.ToString());
            Header.OnAddConfig(GroupNames.Transform, PropertyNames.Width, VisibilityValue.Visible.ToString());
            Header.OnAddConfig(GroupNames.Transform, PropertyNames.Height, VisibilityValue.Visible.ToString(), false);
            Header.OnAddConfig(GroupNames.Text, PropertyNames.FontFamily, VisibilityValue.Collapsed.ToString());
            Header.OnAddConfig(GroupNames.Appearance, PropertyNames.FillColor, VisibilityValue.Collapsed.ToString());
            Header.OnAddConfig(GroupNames.Appearance, PropertyNames.FillColor, VisibilityValue.Visible.ToString());
            Header.OnAddConfig(GroupNames.Appearance, PropertyNames.FillColor, VisibilityValue.Visible.ToString(), false);
            Header.OnAddConfig(GroupNames.Appearance, PropertyNames.Padding, VisibilityValue.Visible.ToString(), false);
            Header.OnUpdated(GroupNames.Transform, PropertyNames.Width, SizeValue.Expand.ToString(), true);
            Header.OnUpdated(GroupNames.Transform, PropertyNames.Height, "95", true);
            Header.OnUpdated(GroupNames.Appearance, PropertyNames.FillColor, "#FFFFFF", true);
            Header.OnInitialized();

            //Configure content
            Body = new ContainerModel(true); Body.Parent = this;
            Body.OnAddConfig(GroupNames.Alignment, PropertyNames.HL, VisibilityValue.Collapsed.ToString());
            Body.OnAddConfig(GroupNames.SelfAlignment, PropertyNames.HL, VisibilityValue.Collapsed.ToString());
            Body.OnAddConfig(GroupNames.Transform, PropertyNames.Width, VisibilityValue.Collapsed.ToString());
            Body.OnAddConfig(GroupNames.Text, PropertyNames.FontFamily, VisibilityValue.Collapsed.ToString());
            Body.OnAddConfig(GroupNames.Appearance, PropertyNames.FillColor, VisibilityValue.Collapsed.ToString());
            Body.OnAddConfig(GroupNames.Appearance, PropertyNames.FillColor, VisibilityValue.Visible.ToString());
            Body.OnAddConfig(GroupNames.Appearance, PropertyNames.Padding, VisibilityValue.Visible.ToString(), false);
            Body.OnAddConfig(GroupNames.Appearance, PropertyNames.PaddingLeft, VisibilityValue.Visible.ToString(), false);
            Body.OnAddConfig(GroupNames.Appearance, PropertyNames.PaddingRight, VisibilityValue.Visible.ToString(), false);
            Body.OnUpdated(GroupNames.Alignment, PropertyNames.HC, "0", true);
            Body.OnUpdated(GroupNames.Alignment, PropertyNames.VC, "0", true);
            Body.OnUpdated(GroupNames.Transform, PropertyNames.Width, SizeValue.Expand.ToString(), true);
            Body.OnUpdated(GroupNames.Transform, PropertyNames.Height, SizeValue.Expand.ToString(), true);
            Body.OnUpdated(GroupNames.Appearance, PropertyNames.FillColor, "#FFFFFF", true);
            Body.OnUpdated(GroupNames.Appearance, PropertyNames.CPadding, "0", true);
            Body.OnUpdated(GroupNames.Appearance, PropertyNames.PaddingLeft, "95", true);
            Body.OnUpdated(GroupNames.Appearance, PropertyNames.PaddingRight, "95", true);
            Body.OnInitialized();

            //Configure bottomBar
            Footer = new ContainerModel(true); Footer.Parent = this;
            Footer.OnAddConfig(GroupNames.Alignment, PropertyNames.HL, VisibilityValue.Collapsed.ToString());
            Footer.OnAddConfig(GroupNames.SelfAlignment, PropertyNames.HL, VisibilityValue.Collapsed.ToString());
            Footer.OnAddConfig(GroupNames.Transform, PropertyNames.Width, VisibilityValue.Collapsed.ToString());
            Header.OnAddConfig(GroupNames.Transform, PropertyNames.Width, VisibilityValue.Visible.ToString());
            Header.OnAddConfig(GroupNames.Transform, PropertyNames.Height, VisibilityValue.Visible.ToString(), false);
            Footer.OnAddConfig(GroupNames.Text, PropertyNames.FontFamily, VisibilityValue.Collapsed.ToString());
            Footer.OnAddConfig(GroupNames.Appearance, PropertyNames.FillColor, VisibilityValue.Collapsed.ToString());
            Header.OnAddConfig(GroupNames.Appearance, PropertyNames.FillColor, VisibilityValue.Visible.ToString());
            Header.OnAddConfig(GroupNames.Appearance, PropertyNames.FillColor, VisibilityValue.Visible.ToString(), false);
            Header.OnAddConfig(GroupNames.Appearance, PropertyNames.Padding, VisibilityValue.Visible.ToString(), false);
            Footer.OnUpdated(GroupNames.Transform, PropertyNames.Width, SizeValue.Expand.ToString(), true);
            Footer.OnUpdated(GroupNames.Transform, PropertyNames.Height, "95", true);
            Footer.OnUpdated(GroupNames.Appearance, PropertyNames.FillColor, "#FFFFFF", true);
            Footer.OnInitialized();
            #endregion
        }

        public override bool OnSelected(bool isInterne = false)
        {
            borderP.BorderBrush = Brushes.DodgerBlue;
            borderP.BorderThickness = new Thickness(1);
            Properties.groupProps = groupProps;
            Properties.ComponentName = ComponentList.Page;
            PanelProperty.Instance.FeedProps();
            Selected = true;
            return true;
        }

        public override void OnUnselected(bool isInterne = false)
        {
            Selected = false;
            borderP.BorderBrush = Brushes.Black;
            borderP.BorderThickness = new Thickness(0.4);
            if (Header != null!) Header.OnUnselected(isInterne);
            if (Body != null!) Body.OnUnselected(isInterne);
            if (Footer != null!) Footer.OnUnselected(isInterne);
        }

        public override bool OnChildSelected()
        {
            return (Header.OnChildSelected() || Body.OnChildSelected() || Footer.OnChildSelected());
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            int[] pos = GetPosition(GroupNames.Global, PropertyNames.CanSelect);
            int idG = pos[0], idP = pos[1];
            if (groupProps![idG].Properties[idP].Value == CanSelectValues.None.ToString() && (e.OriginalSource.Equals(borderP) || e.OriginalSource.Equals(border) ||
                e.OriginalSource.Equals(Header) || e.OriginalSource.Equals(Body) || e.OriginalSource.Equals(Footer)))
            {
                PageView.Instance.OnUnselected(Name == ComponentList.Cell.ToString());
                OnSelected();
                PageView.Instance.OnSelected();
            }
        }

        public override void OnUpdated(GroupNames groupName, PropertyNames propertyName, string value, bool allow = false)
        {
            int[] pos = GetPosition(groupName, propertyName);
            if (pos[0] != -1 && pos[1] != -1 && (Selected || allow))
            {
                int idG = pos[0], idP = pos[1];
                string propName = "";
                try { propName = groupProps![idG].Properties[idP].Name; }
                catch (Exception e) { }

                if (Selected || allow)
                {
                    /* Alignement */
                    #region
                    #endregion
                    /* Transform */
                    #region
                    if (propName == PropertyNames.Width.ToString())
                    {
                        if (value == SizeValue.Expand.ToString())
                        {
                            borderP.Width = double.NaN; borderP.HorizontalAlignment = HorizontalAlignment.Stretch;
                            groupProps![idG].Properties[idP].Value = SizeValue.Expand.ToString();
                        }
                        else if (value == SizeValue.Auto.ToString())
                        {
                            borderP.Width = double.NaN;
                            groupProps![idG].Properties[idP].Value = SizeValue.Auto.ToString();
                            borderP.HorizontalAlignment = HorizontalAlignment.Left;
                        }
                        else if (value != SizeValue.Old.ToString())
                        {
                            double vd = 0; double.TryParse(value, out vd);
                            groupProps![idG].Properties[idP].Value = vd + "";
                            borderP.Width = vd;
                            borderP.HorizontalAlignment = HorizontalAlignment.Left;
                        }
                    }
                    else if (propName == PropertyNames.Height.ToString())
                    {
                        if (value == SizeValue.Expand.ToString())
                        {
                            borderP.Height = double.NaN; borderP.VerticalAlignment = VerticalAlignment.Stretch;
                            groupProps![idG].Properties[idP].Value = SizeValue.Expand.ToString();
                        }
                        else if (value == SizeValue.Auto.ToString())
                        {
                            borderP.Height = 10;
                            groupProps![idG].Properties[idP].Value = SizeValue.Auto.ToString();
                            borderP.VerticalAlignment = VerticalAlignment.Top;
                        }
                        else if (value != SizeValue.Old.ToString())
                        {
                            double vd = 0; double.TryParse(value, out vd);
                            groupProps![idG].Properties[idP].Value = vd + "";
                            borderP.Height = vd;
                        }
                    }
                    else if (propName == PropertyNames.HE.ToString() || propName == PropertyNames.VE.ToString())
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.MoveParentToChild.ToString())
                    {
                        if (Body != null)
                        {
                            OnUnselected();
                            Body.OnSelected();
                        }
                    }
                    #endregion
                    /* Text */
                    #region
                    #endregion
                    /* Appearance */
                    #region
                    else if (propName == PropertyNames.FillColor.ToString())
                    {
                        border.Background = value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                                                     new BrushConverter().ConvertFrom(value) as SolidColorBrush;
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.CBorder.ToString())
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.BorderBtnActif.ToString())
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.BorderWidth.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = groupProps[idG].Properties[idP + 3].Value = groupProps[idG].Properties[idP + 6].Value =
                        groupProps[idG].Properties[idP + 9].Value = groupProps[idG].Properties[idP + 12].Value = vd + "";
                        border.BorderThickness = new Thickness(vd);
                    }
                    else if (propName == PropertyNames.BorderColor.ToString())
                    {
                        border.BorderBrush = value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                                                          new BrushConverter().ConvertFrom(value) as SolidColorBrush;
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.BorderLeftWidth.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        border.BorderThickness = new Thickness(vd, border.BorderThickness.Top, border.BorderThickness.Right, border.BorderThickness.Bottom);
                    }
                    else if (propName == PropertyNames.BorderTopWidth.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        border.BorderThickness = new Thickness(border.BorderThickness.Left, vd * 0.65, border.BorderThickness.Right, border.BorderThickness.Bottom);
                    }
                    else if (propName == PropertyNames.BorderRightWidth.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        border.BorderThickness = new Thickness(border.BorderThickness.Left, border.BorderThickness.Top, vd, border.BorderThickness.Bottom);
                    }
                    else if (propName == PropertyNames.BorderBottomWidth.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        border.BorderThickness = new Thickness(border.BorderThickness.Left, border.BorderThickness.Top, border.BorderThickness.Right, vd * 0.65);
                    }
                    else if (propName == PropertyNames.CBorderRadius.ToString())
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.BorderRadBtnActif.ToString())
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    else if (propName == PropertyNames.BorderRadius.ToString())
                    {
                        //double vd = 0; double.TryParse(value, out vd);
                        //groupProps[idG].Properties[idP].Value = groupProps[idG].Properties[idP + 1].Value = groupProps[idG].Properties[idP + 2].Value =
                        //groupProps[idG].Properties[idP + 3].Value = groupProps[idG].Properties[idP + 4].Value = vd + "";
                        //border.CornerRadius = new CornerRadius(vd);
                    }
                    else if (propName == PropertyNames.BorderRadiusTopLeft.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        border.CornerRadius = new CornerRadius(vd, border.CornerRadius.TopRight,
                                                      border.CornerRadius.BottomRight, border.CornerRadius.BottomLeft);
                    }
                    else if (propName == PropertyNames.BorderRadiusBottomLeft.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        border.CornerRadius = new CornerRadius(border.CornerRadius.TopLeft, border.CornerRadius.TopRight,
                                                      border.CornerRadius.BottomRight, vd);
                    }
                    else if (propName == PropertyNames.BorderRadiusTopRight.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        border.CornerRadius = new CornerRadius(border.CornerRadius.TopLeft, vd,
                                                      border.CornerRadius.BottomRight, border.CornerRadius.BottomLeft);
                    }
                    else if (propName == PropertyNames.BorderRadiusBottomRight.ToString())
                    {
                        double vd = 0; double.TryParse(value, out vd);
                        groupProps![idG].Properties[idP].Value = vd + "";
                        border.CornerRadius = new CornerRadius(border.CornerRadius.TopLeft, border.CornerRadius.TopRight,
                                                      vd, border.CornerRadius.BottomLeft);
                    }
                    #endregion
                    /* Global */
                    #region
                    else if (propertyName == PropertyNames.CanSelect)
                    {
                        groupProps![idG].Properties[idP].Value = value;
                    }
                    #endregion
                }
            }
            else
            {
                if (Header != null! && Body != null! && Footer != null!)
                {
                    if ((Header.Selected || Body.Selected || Footer.Selected) && propertyName == PropertyNames.MoveChildToParent)
                    {
                        if (Header.Selected) Header.OnUnselected();
                        else if (Body.Selected) Body.OnUnselected();
                        else if (Footer.Selected) Footer.OnUnselected();
                        OnSelected();
                    }
                    else
                    {
                        Header.OnUpdated(groupName, propertyName, value);
                        Body.OnUpdated(groupName, propertyName, value);
                        Footer.OnUpdated(groupName, propertyName, value);
                    }
                }
            }
        }

        public override void OnInitialized()
        {
            foreach (var group in groupProps!)
            {
                foreach (var prop in group.Properties)
                {
                    /* Alignement */
                    #region
                    #endregion
                    /* Transform */
                    #region
                    if (prop.Name == PropertyNames.Width.ToString())
                    {
                        if (prop.Value == SizeValue.Expand.ToString())
                        {
                            borderP.Width = double.NaN; borderP.HorizontalAlignment = HorizontalAlignment.Stretch;
                        }
                        else if (prop.Value == SizeValue.Auto.ToString())
                        {
                            borderP.Width = double.NaN; borderP.HorizontalAlignment = HorizontalAlignment.Left;
                        }
                        else if (prop.Value != SizeValue.Old.ToString())
                        {
                            double vd = 0; double.TryParse(prop.Value, out vd);
                            borderP.Width = vd;
                        }
                    }
                    else if (prop.Name == PropertyNames.Height.ToString())
                    {
                        if (prop.Value == SizeValue.Expand.ToString())
                        {
                            borderP.Height = double.NaN; borderP.VerticalAlignment = VerticalAlignment.Stretch;
                        }
                        else if (prop.Value == SizeValue.Auto.ToString())
                        {
                            borderP.Height = double.NaN; borderP.VerticalAlignment = VerticalAlignment.Top;
                        }
                        else if (prop.Value != SizeValue.Old.ToString())
                        {
                            double vd = 0; double.TryParse(prop.Value, out vd);
                            borderP.Height = vd;
                        }
                    }
                    #endregion
                    /* Text */
                    #region
                    #endregion
                    /* Appearance */
                    #region
                    else if (prop.Name == PropertyNames.FillColor.ToString())
                    {
                        border.Background = prop.Value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                                                        new BrushConverter().ConvertFrom(prop.Value) as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.BorderWidth.ToString())
                    {
                        double vd = 0; double.TryParse(prop.Value, out vd);
                        border.BorderThickness = new Thickness(vd);
                    }
                    else if (prop.Name == PropertyNames.BorderColor.ToString())
                    {
                        border.BorderBrush = prop.Value == ColorValue.Transparent.ToString() ? Brushes.Transparent :
                                                                new BrushConverter().ConvertFrom(prop.Value) as SolidColorBrush;
                    }
                    else if (prop.Name == PropertyNames.BorderRadius.ToString())
                    {
                        double vd = 0; double.TryParse(prop.Value, out vd);
                        border.CornerRadius = new CornerRadius(vd);
                    }
                    else if (prop.Name == PropertyNames.BorderRadiusTopLeft.ToString() && groupProps[1].Properties[32].Value == "0")
                    {
                        double vd = 0; double.TryParse(prop.Value, out vd);
                        border.CornerRadius = new CornerRadius(vd, border.CornerRadius.TopRight,
                                                        border.CornerRadius.BottomRight, border.CornerRadius.BottomLeft);
                    }
                    else if (prop.Name == PropertyNames.BorderRadiusBottomLeft.ToString() && groupProps[1].Properties[32].Value == "0")
                    {
                        double vd = 0; double.TryParse(prop.Value, out vd);
                        border.CornerRadius = new CornerRadius(border.CornerRadius.TopLeft, border.CornerRadius.TopRight,
                                                        border.CornerRadius.BottomRight, vd);
                    }
                    else if (prop.Name == PropertyNames.BorderRadiusTopRight.ToString() && groupProps[1].Properties[32].Value == "0")
                    {
                        double vd = 0; double.TryParse(prop.Value, out vd);
                        border.CornerRadius = new CornerRadius(border.CornerRadius.TopLeft, vd,
                                                        border.CornerRadius.BottomRight, border.CornerRadius.BottomLeft);
                    }
                    else if (prop.Name == PropertyNames.BorderRadiusBottomRight.ToString() && groupProps[1].Properties[32].Value == "0")
                    {
                        double vd = 0; double.TryParse(prop.Value, out vd);
                        border.CornerRadius = new CornerRadius(border.CornerRadius.TopLeft, border.CornerRadius.TopRight,
                                                        vd, border.CornerRadius.BottomLeft);
                    }
                    #endregion
                }
            }
        }

        public override CompSerialiser OnSerialiser()
        {
            return new CompSerialiser
            {
                Name = EnumName.ToString(),
                Properties = groupProps,
                Children = new List<CompSerialiser> { Header.OnSerialiser(), Body.OnSerialiser(), Footer.OnSerialiser() }
            };
        }

        public override void OnDeserialiser(CompSerialiser compSerialiser)
        {
            groupProps = compSerialiser.Properties;
            if (compSerialiser.Children != null)
            {
                Header = new ContainerModel(true);
                Header.Parent = this; Header.Added = true;
                Header.OnDeserialiser(compSerialiser.Children[0]);
                Body = new ContainerModel(true);
                Body.Parent = this; Body.Added = true;
                Body.OnDeserialiser(compSerialiser.Children[1]);
                Footer = new ContainerModel(true);
                Footer.Parent = this; Footer.Added = true;
                Footer.OnDeserialiser(compSerialiser.Children[2]);

                Grid.SetRow(Header!.ComponentView!, 0); Grid.SetRow(Body!.ComponentView!, 1); Grid.SetRow(Footer!.ComponentView!, 2);
                grid.Children.Add(Header.ComponentView!); grid.Children.Add(Body.ComponentView!); grid.Children.Add(Footer.ComponentView!);
            }
            OnInitialized();
        }

        public override void OnAddConfig(GroupNames groupName, PropertyNames propertyName, string value, bool isGroup = true)
        {
            int[] pos = GetPosition(groupName, propertyName);
            if (pos[0] != -1 && pos[1] != -1)
            {
                int idG = pos[0], idP = pos[1];
                if (isGroup)
                {
                    groupProps![idG].Visibility = value;
                    if (value == VisibilityValue.Collapsed.ToString())
                        for (int i = 0; i < groupProps[idG].Properties.Count; i++)
                            groupProps[idG].Properties[i].Visibility = value;
                }
                else
                {
                    groupProps![idG].Properties[idP].Visibility = value;
                }
            }
        }

        public override void OnConfiguOut(int id)
        {
            if (Selected || Properties.ComponentOutsUsing![id].Name == ComponentList.Page.ToString())
            {
                if (id >= 0 && Properties.ComponentOutsUsing![id].Name == ComponentList.Page.ToString() && !Selected)
                {
                    foreach (var group in Properties.ComponentOutsUsing[id].Component)
                    {
                        int i = Properties.ComponentOutsUsing[id].Component.IndexOf(group);
                        groupProps![i].Visibility = group.Visibility;
                        foreach (var prop in group.Properties)
                        {
                            int j = group.Properties.IndexOf(prop);
                            groupProps[i].Properties[j].Visibility = prop.Visibility;
                            groupProps[i].Properties[j].Value = prop.Value;
                        }
                    }
                }

                if (Body == null)
                {
                    
                }
            }
            else
            {
                if (Header != null) Header.OnConfiguOut(id);
                if (Body != null)
                {
                    bool beforeSelectedValue = Body.Selected;
                    Body.OnConfiguOut(id);
                    if (beforeSelectedValue && !Body.Selected)
                    {
                        Body.Child.OnAddConfig(GroupNames.Transform, PropertyNames.Width, VisibilityValue.Collapsed.ToString());
                    }
                }
                if (Footer != null) Footer.OnConfiguOut(id);
            }
        }

        public override int[] GetPosition(GroupNames groupName, PropertyNames propertyName)
        {
            int[] position = { -1, -1 };
            foreach (var group in groupProps!)
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
            return position;
        }

        public override string OnCopyOrPaste(string value = null!, bool isPaste = false)
        {
            CompSerialiser valueD = null!;
            if (value != null)
                valueD = System.Text.Json.JsonSerializer.Deserialize<CompSerialiser>(value)!;

            if (Selected && isPaste && value != null && valueD.Children != null)
            {
                string name = valueD.Name!;
                Component component = ManageEnums.Instance.GetComponent(name);
                component.Parent = this; component.Added = true;
                component.OnDeserialiser(valueD);
                //Il faut enfin appliquer à ce composant, les contraintes de mise en page.
                //Child = component;
                //border.Child = Child.ComponentView;
            }
            else if (Selected && !isPaste)
                return System.Text.Json.JsonSerializer.Serialize(this.OnSerialiser());
            else if (!Selected)
                Body.OnCopyOrPaste(value!, isPaste);
            return null!;
        }

        public override StructuralElement AddToStructuralView()
        {
            var structuralElement = new StructuralElement();
            structuralElement.Node = EnumName.ToString()!;
            structuralElement.Selected = Selected;
            structuralElement.Children =
                new List<StructuralElement> { Header.AddToStructuralView(), Body.AddToStructuralView(), Footer.AddToStructuralView() };

            return structuralElement;
        }

        public override void SelectFromStructuralView(StructuralElement structuralElement)
        {
            if (structuralElement.Selected)
            {
                PageView.Instance.OnUnselected(Name == ComponentList.Cell.ToString());
                OnSelected();
            }
            else if (structuralElement.Children != null!)
            {
                foreach (var child in structuralElement.Children!)
                {
                    Body.SelectFromStructuralView(child);
                }
            }
        }

        public override void LayoutConstraints(int id, bool isDeserialize = false, bool existExpaned = false)
        {
            return;
        }

        public override bool AllowFixSize(bool isHeight = true)
        {
            return false;
        }

        public override void UpdateComponent(List<ComponentRef> refs, GroupNames groupName, PropertyNames propertyName, string value, int i = 0, bool found = false)
        {
            
        }

        public override void AddComponent(List<ComponentRef> refs, string data, int i = 0, bool found = false)
        {
            
        }

        public override void DeleteComponent(List<ComponentRef> refs, int i = 0, bool found = false)
        {
            
        }

        public override List<ComponentRef> BuildComponentRefs(int i)
        {
            return null!;
        }

        public override ReactComponent BuildReactComponent(string tab, int n, string id)
        {
            ReactComponent comp = new ReactComponent();
            //Ajout du Header
            string Header = $"<Header style='auto'/>";
            var body = Body.Child != null ? Body.Child.BuildReactComponent(Header, n, id) : Body.BuildReactComponent(Header, n, id);
            comp.Body = body.Body;
            comp.Styles = body.Styles;
            comp.Imports = body.Imports;
            comp.Imports!.Add("expo-status-bar", new Dictionary<string, bool> { { "Header", false } });
            return comp;
        }

        public override WebComponent BuildWebComponent(Dictionary<string, Dictionary<string, string>>? styles, string tab, int n, string id, List<string> lparams)
        {
            WebComponent comp = new WebComponent();
            return comp;
        }

        public override ReactComponent BuildFlutterComponent(string tab, int n, string id)
        {
            ReactComponent comp = new ReactComponent();
            return comp;
        }

        public override ReactComponent BuildAndroidXmlComponent(string tab, int n, string id)
        {
            ReactComponent comp = new ReactComponent();
            return comp;
        }

        public override ReactComponent BuildAndroidComposeComponent(string tab, int n, string id)
        {
            ReactComponent comp = new ReactComponent();
            return comp;
        }

        public override void LoadIds()
        {
            Ids = new Dictionary<string, PropStringToIndex>();
            foreach (var group in groupProps!)
            {
                Ids!.Add(group.Name, new PropStringToIndex() { IdGroup = groupProps.IndexOf(group), Props = new Dictionary<string, int>() });
                foreach (var prop in group.Properties)
                {
                    Ids[group.Name].Props!.Add(prop.Name, group.Properties.IndexOf(prop));
                }
            }
        }

        private string tabs(int n)
        {
            string tab = "";
            for (int i = 0; i < n; i++)
                tab += "    ";
            return tab;
        }

        private string tcolor(string color)
        {
            return color.Length == 9 ? "#" + color.Substring(3).ToLower() : color.ToLower();
        }

        public override double Width(Component component)
        {
            string paddingLeft = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.PaddingLeft.ToString()]].Value;
            string paddingRight = groupProps![Ids![GroupNames.Appearance.ToString()].IdGroup].Properties[Ids![GroupNames.Appearance.ToString()].Props![PropertyNames.PaddingRight.ToString()]].Value;
            return ComponentView!.Width - (Convert.ToDouble(paddingLeft) + Convert.ToDouble(paddingRight));
        }
        public override double Height(Component component)
        {
            double h = 0;
            if (component.Equals(this)) h = borderP!.Height;
            else if (component.Equals(Header)) h = Header.Height(null!);
            else if (component.Equals(Body)) h = borderP!.Height - (Header.Height(null!) + Footer.Height(null!));
            else if (component.Equals(Footer)) h = Footer.Height(null!);
            return h;
        }

        public override void OnConfigured()
        {
            #region
            groupProps = new List<GroupProperties>
            {
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
                            Value = "794",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.Height.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "1123",
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
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.MoveBottom.ToString(),
                            Type = PropertyTypes.Action.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Visible.ToString()
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
                            Value = "#FFFFFFFF",
                            Visibility = VisibilityValue.Visible.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.CMargin.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "1",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.MarginBtnActif.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = PropertyNames.MarginLeft.ToString(),
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.Margin.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.MarginLeft.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.MarginTop.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.MarginRight.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
                        },
                        new Property()
                        {
                            Name = PropertyNames.MarginBottom.ToString(),
                            Type = PropertyTypes.String.ToString(),
                            Value = "0",
                            Visibility = VisibilityValue.Collapsed.ToString()
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
                            Value = "20",
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
            };
            #endregion
        }
    }
}
