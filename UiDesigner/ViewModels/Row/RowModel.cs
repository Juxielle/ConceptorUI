using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ConceptorUI.Models;
using ConceptorUI.Utils;
using ConceptorUI.ViewModels.Components;
using ConceptorUi.ViewModels.Operations;
using ConceptorUi.ViewModels.Row;
using UiDesigner.Models;
using UiDesigner.ViewModels.Row;

namespace ConceptorUI.ViewModels.Row
{
    internal class RowModel : Component
    {
        public readonly Grid Grid;
        private bool _fromWidth;

        public RowModel(bool isVertical = true, bool allowConstraints = false)
        {
            OnInit();

            Grid = new Grid();
            IsVertical = isVertical;
            CanAddIntoChildContent = true;

            Content.Child = Grid;
            Name = isVertical ? ComponentList.Row : ComponentList.Column;

            SelfConstraints();
            if (allowConstraints) return;
            OnInitialize();
        }

        public override void WhenTextChanged(string propertyName, string value, bool isInitialize = false)
        {
        }

        protected override void WhenFileLoaded(string value)
        {
        }

        public sealed override void SelfConstraints()
        {
            /* Global */
            this.SetPropertyVisibility(GroupNames.Global, PropertyNames.FilePicker, false);
            this.SetPropertyVisibility(GroupNames.Global, PropertyNames.Focus);
            /* Content Alignment */
            this.SetGroupVisibility(GroupNames.Alignment);
            /* Self Alignment */
            /* Transform */
            this.SetGroupVisibility(GroupNames.Transform);
            this.SetPropertyVisibility(GroupNames.Transform, PropertyNames.Gap);
            this.SetPropertyValue(GroupNames.Transform, PropertyNames.PreviewWidth, "300");
            this.SetPropertyValue(GroupNames.Transform, PropertyNames.PreviewHeight, "100");
            /* Text */
            this.SetGroupVisibility(GroupNames.Text, false);
            /* Appearance */
            /* Shadow */
            this.SetGroupVisibility(GroupNames.Shadow);
        }

        public override void WhenAlignmentChanged(PropertyNames propertyName, string value)
        {
            if (Children.Count == 0) return;

            #region When Alignment Changed

            if ((IsVertical && propertyName is PropertyNames.Hl or PropertyNames.Hc or PropertyNames.Hr) ||
                (!IsVertical && propertyName is PropertyNames.Vt or PropertyNames.Vc or PropertyNames.Vb))
            {
                if (value == "0")
                {
                    WhenAlignmentChanged(IsVertical ? PropertyNames.Hl : PropertyNames.Vt, "1");
                    return;
                }

                this.SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Hl : PropertyNames.Vt, "0");
                this.SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Hc : PropertyNames.Vc, "0");
                this.SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Hr : PropertyNames.Vb, "0");
                this.SetPropertyValue(GroupNames.Alignment, propertyName, value);

                foreach (var child in Children)
                {
                    var d = child.GetGroupProperties(GroupNames.Transform)
                        .GetValue(IsVertical ? PropertyNames.Width : PropertyNames.Height);
                    if (value == "1")
                    {
                        child.OnUpdated(GroupNames.SelfAlignment, propertyName, "1", true);
                        if (d == SizeValue.Expand.ToString())
                            child.OnUpdated(GroupNames.Transform,
                                IsVertical ? PropertyNames.Width : PropertyNames.Height, SizeValue.Auto.ToString(),
                                true);
                    }
                    else if (d != SizeValue.Expand.ToString())
                        child.OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.Hl : PropertyNames.Vt, "1",
                            true);
                }
            }
            else if ((IsVertical && propertyName == PropertyNames.Vt) ||
                     (!IsVertical && propertyName == PropertyNames.Hl))
            {
                if (value == "1")
                {
                    for (var i = 0; i < GetSpaceCount(); i++)
                    {
                        if (i == GetSpaceCount() - 1)
                            SetDimension(i, new GridLength(1, GridUnitType.Star));
                        else SetDimension(i, new GridLength(0, GridUnitType.Auto));
                    }

                    this.SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vt : PropertyNames.Hl, "0");
                    this.SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vc : PropertyNames.Hc, "0");
                    this.SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vb : PropertyNames.Hr, "0");
                    this.SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceBetween, "0");
                    this.SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceAround, "0");
                    this.SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceEvery, "0");
                    this.SetPropertyValue(GroupNames.Alignment, propertyName, value);
                }
                else WhenAlignmentChanged(IsVertical ? PropertyNames.Vt : PropertyNames.Hl, "1");
            }
            else if ((IsVertical && propertyName == PropertyNames.Vc) ||
                     (!IsVertical && propertyName == PropertyNames.Hc))
            {
                if (value == "1")
                {
                    for (var i = 0; i < GetSpaceCount(); i++)
                    {
                        if (i == 0 || i == GetSpaceCount() - 1)
                            SetDimension(i, new GridLength(1, GridUnitType.Star));
                        else SetDimension(i, new GridLength(0, GridUnitType.Auto));
                    }

                    this.SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vt : PropertyNames.Hl, "0");
                    this.SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vc : PropertyNames.Hc, "0");
                    this.SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vb : PropertyNames.Hr, "0");
                    this.SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceBetween, "0");
                    this.SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceAround, "0");
                    this.SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceEvery, "0");
                    this.SetPropertyValue(GroupNames.Alignment, propertyName, value);
                }
                else WhenAlignmentChanged(IsVertical ? PropertyNames.Vt : PropertyNames.Hl, "1");
            }
            else if ((IsVertical && propertyName == PropertyNames.Vb) ||
                     (!IsVertical && propertyName == PropertyNames.Hr))
            {
                if (value == "1")
                {
                    for (var i = 0; i < GetSpaceCount(); i++)
                    {
                        if (i == 0)
                            SetDimension(i, new GridLength(1, GridUnitType.Star));
                        else SetDimension(i, new GridLength(0, GridUnitType.Auto));
                    }

                    this.SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vt : PropertyNames.Hl, "0");
                    this.SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vc : PropertyNames.Hc, "0");
                    this.SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vb : PropertyNames.Hr, "0");
                    this.SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceBetween, "0");
                    this.SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceAround, "0");
                    this.SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceEvery, "0");
                    this.SetPropertyValue(GroupNames.Alignment, propertyName, value);
                }
                else WhenAlignmentChanged(IsVertical ? PropertyNames.Vt : PropertyNames.Hl, "1");
            }
            else if (propertyName == PropertyNames.SpaceBetween)
            {
                if (value == "1")
                {
                    for (var i = 0; i < GetSpaceCount(); i++)
                    {
                        if (i == 0 || i == GetSpaceCount() - 1)
                            SetDimension(i, new GridLength(0, GridUnitType.Auto));
                        else if (i % 2 == 0)
                            SetDimension(i, new GridLength(1, GridUnitType.Star));
                        else SetDimension(i, new GridLength(0, GridUnitType.Auto));
                    }

                    this.SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vt : PropertyNames.Hl, "0");
                    this.SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vc : PropertyNames.Hc, "0");
                    this.SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vb : PropertyNames.Hr, "0");
                    this.SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceBetween, "0");
                    this.SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceAround, "0");
                    this.SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceEvery, "0");
                    this.SetPropertyValue(GroupNames.Alignment, propertyName, value);
                }
                else WhenAlignmentChanged(IsVertical ? PropertyNames.Vt : PropertyNames.Hl, "1");
            }
            else if (propertyName == PropertyNames.SpaceAround)
            {
                if (value == "1")
                {
                    for (var i = 0; i < GetSpaceCount(); i++)
                    {
                        if (i % 2 == 0)
                            SetDimension(i, new GridLength(1, GridUnitType.Star));
                        else SetDimension(i, new GridLength(0, GridUnitType.Auto));
                    }

                    this.SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vt : PropertyNames.Hl, "0");
                    this.SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vc : PropertyNames.Hc, "0");
                    this.SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vb : PropertyNames.Hr, "0");
                    this.SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceBetween, "0");
                    this.SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceAround, "0");
                    this.SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceEvery, "0");
                    this.SetPropertyValue(GroupNames.Alignment, propertyName, value);
                }
                else WhenAlignmentChanged(IsVertical ? PropertyNames.Vt : PropertyNames.Hl, "1");
            }
            else if (propertyName == PropertyNames.SpaceEvery)
            {
                if (value == "1")
                {
                    var vt = this.GetGroupProperties(GroupNames.Alignment)
                        .GetValue(IsVertical ? PropertyNames.Vt : PropertyNames.Hl);
                    var vc = this.GetGroupProperties(GroupNames.Alignment)
                        .GetValue(IsVertical ? PropertyNames.Vc : PropertyNames.Hc);
                    var vb = this.GetGroupProperties(GroupNames.Alignment)
                        .GetValue(IsVertical ? PropertyNames.Vb : PropertyNames.Hr);

                    this.SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vt : PropertyNames.Hl, "0");
                    this.SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vc : PropertyNames.Hc, "0");
                    this.SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vb : PropertyNames.Hr, "0");
                    this.SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceBetween, "0");
                    this.SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceAround, "0");
                    this.SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceEvery, "0");
                    this.SetPropertyValue(GroupNames.Alignment, propertyName, value);

                    for (var i = 0; i < GetSpaceCount(); i++)
                    {
                        if (i % 2 == 0)
                            SetDimension(i, new GridLength(1, GridUnitType.Star));
                        else SetDimension(i, new GridLength(0, GridUnitType.Auto));
                    }
                }
                else WhenAlignmentChanged(IsVertical ? PropertyNames.Vt : PropertyNames.Hl, "1");
            }

            #endregion
        }

        protected override void InitChildContent()
        {
        }

        public override void AddIntoChildContent(FrameworkElement child, int k = -1)
        {
            if (k == -1)
                Grid.Children.Add(child);
            else Grid.Children.Insert(k, child);
        }

        public override bool AllowExpanded(bool isWidth = true)
        {
            return true;
        }

        public override bool AllowAuto(bool isWidth = true)
        {
            return true;
        }

        public override void Delete(int j = -1)
        {
            var i = j;
            if (i == -1)
            {
                foreach (var child in Children.Where(child => child.Selected))
                {
                    i = Children.IndexOf(child);
                    break;
                }
            }

            if (i == -1) return;

            var nc = Children.Count;

            if (nc == 1)
            {
                if (IsVertical) Grid.RowDefinitions.Clear();
                else Grid.ColumnDefinitions.Clear();

                this.SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, "0");
                this.SetPropertyValue(GroupNames.Alignment, PropertyNames.Vc, "0");
                this.SetPropertyValue(GroupNames.Alignment, PropertyNames.Vb, "0");
                this.SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, "0");
                this.SetPropertyValue(GroupNames.Alignment, PropertyNames.Hc, "0");
                this.SetPropertyValue(GroupNames.Alignment, PropertyNames.Hr, "0");
                this.SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceBetween, "0");
                this.SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceAround, "0");
                this.SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceEvery, "0");
            }
            else if (IsVertical)
            {
                Grid.RowDefinitions.RemoveAt(2 * i);
                Grid.RowDefinitions.RemoveAt(2 * i);
            }
            else
            {
                Grid.ColumnDefinitions.RemoveAt(2 * i);
                Grid.ColumnDefinitions.RemoveAt(2 * i);
            }

            //Ecrire la methode permettant de restorer l'alignment
            Grid.Children.RemoveAt(i);
            Children.RemoveAt(i);

            for (var k = i; k < Children.Count; k++)
                SetPosition(2 * k + 1, Children[k].ComponentView);

            if (j == -1) OnSelected();
        }

        protected override void WhenWidthChanged(string value)
        {
            _fromWidth = true;
            WhenHeightChanged(value);
        }

        protected override void WhenHeightChanged(string value)
        {
            if (!IsVertical && !_fromWidth) return;
            _fromWidth = false;

            var i = -1;
            var found = false;
            var expandCount = 0;

            foreach (var child in Children)
            {
                if (child.Selected) i = Children.IndexOf(child);
                var d = child.GetGroupProperties(GroupNames.Transform)
                    .GetValue(IsVertical ? PropertyNames.Height : PropertyNames.Width);
                found = found || d == SizeValue.Expand.ToString();
                expandCount = d == SizeValue.Expand.ToString() ? expandCount + 1 : expandCount;
            }

            if (i != -1 && value == SizeValue.Expand.ToString())
            {
                this.SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vt : PropertyNames.Hl, "0");
                this.SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vc : PropertyNames.Hc, "0");
                this.SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vb : PropertyNames.Hr, "0");
                this.SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceBetween, "0");
                this.SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceAround, "0");
                this.SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceEvery, "0");

                SetDimension(0, new GridLength(0, GridUnitType.Auto));
                SetDimension(GetSpaceCount() - 1, new GridLength(0, GridUnitType.Auto));

                SetDimension(2 * i + 1, new GridLength(1, GridUnitType.Star));
            }
            else if (i != -1 && value == SizeValue.Auto.ToString())
            {
                SetDimension(2 * i + 1, new GridLength(0, GridUnitType.Auto));
            }
            else if (i != -1 && value != SizeValue.Old.ToString())
            {
                SetDimension(2 * i + 1, new GridLength(0, GridUnitType.Auto));
            }

            if (i != -1 && expandCount == 1)
            {
                var d = Children[i].GetGroupProperties(GroupNames.Transform)
                    .GetValue(IsVertical ? PropertyNames.Height : PropertyNames.Width);
                if (d == SizeValue.Expand.ToString())
                    this.SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vt : PropertyNames.Hl, "1");
            }
        }

        protected override void OnMoveLeft()
        {
            OnMoveTop();
        }

        protected override void OnMoveRight()
        {
            OnMoveBottom();
        }

        protected override void OnMoveTop()
        {
            var focus = this.GetGroupProperties(GroupNames.Global).GetValue(PropertyNames.Focus) == "1";
            var k = -1;

            foreach (var i in from child in Children let i = Children.IndexOf(child) where child.Selected select i)
            {
                k = i;
                break;
            }

            if (k != -1 && k > 0)
            {
                if (focus)
                {
                    var child = Children[k];

                    var resizeType = IsVertical
                        ? Grid.RowDefinitions[2 * (k - 1) + 1].Height.GridUnitType
                        : Grid.ColumnDefinitions[2 * (k - 1) + 1].Width.GridUnitType;
                    var resizeType2 = IsVertical
                        ? Grid.RowDefinitions[2 * k + 1].Height.GridUnitType
                        : Grid.ColumnDefinitions[2 * k + 1].Width.GridUnitType;

                    SetDimension(2 * (k - 1) + 1,
                        new GridLength(resizeType2 == GridUnitType.Auto ? 0 : 1, resizeType2));
                    SetDimension(2 * k + 1, new GridLength(resizeType == GridUnitType.Auto ? 0 : 1, resizeType));
                    SetPosition(2 * (k - 1) + 1, Children[k].ComponentView);
                    SetPosition(2 * k + 1, Children[k - 1].ComponentView);

                    Children.RemoveAt(k);
                    Children.Insert(k - 1, child);
                    Grid.Children.RemoveAt(k);
                    Grid.Children.Insert(k - 1, child.ComponentView);
                }
                else
                {
                    Children[k].OnUnselected();
                    Children[k - 1].OnSelected();
                }
            }
        }

        protected override void OnMoveBottom()
        {
            var focus = this.GetGroupProperties(GroupNames.Global).GetValue(PropertyNames.Focus) == "1";
            var k = -1;
            foreach (var i in from child in Children let i = Children.IndexOf(child) where child.Selected select i)
            {
                k = i;
                break;
            }
            
            if (k != -1 && k < Children.Count - 1)
            {
                if (focus)
                {
                    var child = Children[k];

                    var resizeType = IsVertical
                        ? Grid.RowDefinitions[2 * k + 1].Height.GridUnitType
                        : Grid.ColumnDefinitions[2 * k + 1].Width.GridUnitType;
                    var resizeType2 = IsVertical
                        ? Grid.RowDefinitions[2 * (k + 1) + 1].Height.GridUnitType
                        : Grid.ColumnDefinitions[2 * (k + 1) + 1].Width.GridUnitType;

                    SetDimension(2 * k + 1, new GridLength(resizeType2 == GridUnitType.Auto ? 0 : 1, resizeType2));
                    SetDimension(2 * (k + 1) + 1, new GridLength(resizeType == GridUnitType.Auto ? 0 : 1, resizeType));
                    SetPosition(2 * (k + 1) + 1, Children[k].ComponentView);
                    SetPosition(2 * k + 1, Children[k + 1].ComponentView);

                    Children.RemoveAt(k);
                    Children.Insert(k + 1, child);
                    Grid.Children.RemoveAt(k);
                    Grid.Children.Insert(k + 1, child.ComponentView);
                }
                else
                {
                    Children[k].OnUnselected();
                    Children[k + 1].OnSelected();
                }
            }
        }

        public override void LayoutConstraints(int id, bool isDeserialize = false, bool existExpand = false)
        {
            #region Contraintes de mise en page

            if (!isDeserialize)
            {
                Children[id].Parent = this;
                /* Global */
                Children[id].SetPropertyVisibility(GroupNames.Global,
                    !IsVertical ? PropertyNames.MoveLeft : PropertyNames.MoveTop);
                Children[id].SetPropertyVisibility(GroupNames.Global,
                    !IsVertical ? PropertyNames.MoveRight : PropertyNames.MoveBottom);

                /* Content Alignment */
                /* Self Alignment */
                Children[id].SetGroupVisibility(GroupNames.SelfAlignment);
                Children[id].SetPropertyVisibility(GroupNames.SelfAlignment,
                    IsVertical ? PropertyNames.Vt : PropertyNames.Hl, false);
                Children[id].SetPropertyVisibility(GroupNames.SelfAlignment,
                    IsVertical ? PropertyNames.Vc : PropertyNames.Hc, false);
                Children[id].SetPropertyVisibility(GroupNames.SelfAlignment,
                    IsVertical ? PropertyNames.Vb : PropertyNames.Hr, false);

                /* Transform */
                Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.Rot, false);
                Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.X, false);
                Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.Y, false);
                Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.Stretch, false);

                /* Appearance */
                /* Shadow */

                Children[id].OnInitialize();
                // Children[id].OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.VT : PropertyNames.HL, "1",
                //     true);
            }

            var component = Children[id];

            var vt = this.GetGroupProperties(GroupNames.Alignment)
                .GetValue(IsVertical ? PropertyNames.Vt : PropertyNames.Hl);
            var vc = this.GetGroupProperties(GroupNames.Alignment)
                .GetValue(IsVertical ? PropertyNames.Vc : PropertyNames.Hc);
            var vb = this.GetGroupProperties(GroupNames.Alignment)
                .GetValue(IsVertical ? PropertyNames.Vb : PropertyNames.Hr);

            var sb = this.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceBetween);
            var sa = this.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceAround);
            var se = this.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceEvery);

            var hl = this.GetGroupProperties(GroupNames.Alignment)
                .GetValue(IsVertical ? PropertyNames.Hl : PropertyNames.Vt);
            var hc = this.GetGroupProperties(GroupNames.Alignment)
                .GetValue(IsVertical ? PropertyNames.Hc : PropertyNames.Vc);
            var hr = this.GetGroupProperties(GroupNames.Alignment)
                .GetValue(IsVertical ? PropertyNames.Hr : PropertyNames.Vb);

            var hlc = component.GetGroupProperties(GroupNames.SelfAlignment)
                .GetValue(IsVertical ? PropertyNames.Hl : PropertyNames.Vt);
            var hcc = component.GetGroupProperties(GroupNames.SelfAlignment)
                .GetValue(IsVertical ? PropertyNames.Hc : PropertyNames.Vc);
            var hrc = component.GetGroupProperties(GroupNames.SelfAlignment)
                .GetValue(IsVertical ? PropertyNames.Hr : PropertyNames.Vb);

            var hasAlignment = hlc == "1" || hcc == "1" || hrc == "1";

            var w = component.GetGroupProperties(GroupNames.Transform)
                .GetValue(IsVertical ? PropertyNames.Width : PropertyNames.Height);
            var h = component.GetGroupProperties(GroupNames.Transform)
                .GetValue(IsVertical ? PropertyNames.Height : PropertyNames.Width);

            if (hl == "1" && !hasAlignment && w != SizeValue.Expand.ToString())
                component.OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.Hl : PropertyNames.Vt, hl,
                    true);
            else if (hc == "1" && !hasAlignment && w != SizeValue.Expand.ToString())
                component.OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.Hc : PropertyNames.Vc, hc,
                    true);
            else if (hr == "1" && !hasAlignment && w != SizeValue.Expand.ToString())
                component.OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.Hr : PropertyNames.Vb, hr,
                    true);
            else if (!isDeserialize && hl == "0" && hc == "0" && hr == "0" && w != SizeValue.Expand.ToString() &&
                     !hasAlignment)
                component.OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.Hl : PropertyNames.Vt, "1",
                    true);

            if (GetSpaceCount() > 0)
                SetDimension(GetSpaceCount() - 1, new GridLength(0, GridUnitType.Auto));
            else AddSpace(new GridLength(0, GridUnitType.Auto), 0);

            AddSpace(new GridLength(0, GridUnitType.Auto), 0);
            AddSpace(new GridLength(0, GridUnitType.Auto), 0);
            SetPosition(GetSpaceCount() - 2, component.ComponentView);

            SetDimension(0, new GridLength(0, GridUnitType.Auto));
            SetDimension(GetSpaceCount() - 1, new GridLength(0, GridUnitType.Auto));

            if (vt == "1")
            {
                SetDimension(GetSpaceCount() - 1, new GridLength(1, GridUnitType.Star));
                if (!isDeserialize && h == SizeValue.Expand.ToString())
                    component.OnUpdated(GroupNames.Transform, IsVertical ? PropertyNames.Height : PropertyNames.Width,
                        SizeValue.Auto.ToString(), true);
            }
            else if (vb == "1")
            {
                SetDimension(0, new GridLength(1, GridUnitType.Star));
                if (!isDeserialize && h == SizeValue.Expand.ToString())
                    component.OnUpdated(GroupNames.Transform, IsVertical ? PropertyNames.Height : PropertyNames.Width,
                        SizeValue.Auto.ToString(), true);
            }
            else if (vc == "1")
            {
                SetDimension(0, new GridLength(1, GridUnitType.Star));
                SetDimension(GetSpaceCount() - 1, new GridLength(1, GridUnitType.Star));
                if (!isDeserialize && h == SizeValue.Expand.ToString())
                    component.OnUpdated(GroupNames.Transform, IsVertical ? PropertyNames.Height : PropertyNames.Width,
                        SizeValue.Auto.ToString(), true);
            }
            else if (sb == "1")
            {
                for (var i = 0; i < GetSpaceCount(); i++)
                {
                    if (i == 0 || i == GetSpaceCount() - 1)
                        SetDimension(i, new GridLength(0, GridUnitType.Auto));
                    else if (i % 2 == 0)
                        SetDimension(i, new GridLength(1, GridUnitType.Star));
                }
            }
            else if (sa == "1")
            {
                for (var i = 0; i < GetSpaceCount(); i++)
                {
                    if (i % 2 == 0)
                        SetDimension(i, new GridLength(1, GridUnitType.Star));
                }
            }
            else if (se == "1")
            {
                for (var i = 0; i < GetSpaceCount(); i++)
                {
                    if (i % 2 == 0)
                        SetDimension(i, new GridLength(1, GridUnitType.Star));
                }
            }
            else
            {
                if (h == SizeValue.Expand.ToString())
                {
                    SetDimension(GetSpaceCount() - 2, new GridLength(1, GridUnitType.Star));
                }
                else if ((h == SizeValue.Auto.ToString() || h != SizeValue.Old.ToString()) && existExpand)
                {
                    SetDimension(GetSpaceCount() - 2, new GridLength(0, GridUnitType.Auto));
                }
                else if (!existExpand)
                {
                    SetDimension(GetSpaceCount() - 1, new GridLength(1, GridUnitType.Star));
                    this.SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vt : PropertyNames.Hl, "1");
                }
            }

            if (vt == "1" && (h == SizeValue.Auto.ToString() || h == SizeValue.Expand.ToString()))
                Children[id].OnUpdated(GroupNames.Transform, IsVertical ? PropertyNames.Height : PropertyNames.Width,
                    SizeValue.Auto.ToString(), true);

            #endregion
        }

        private void AddSpace(GridLength dimension, int i, bool isAdding = true)
        {
            if (IsVertical)
            {
                if (isAdding) Grid.RowDefinitions.Add(new RowDefinition { Height = dimension });
                else Grid.RowDefinitions.Insert(i, new RowDefinition { Height = dimension });
            }
            else
            {
                if (isAdding) Grid.ColumnDefinitions.Add(new ColumnDefinition { Width = dimension });
                else Grid.ColumnDefinitions.Insert(i, new ColumnDefinition { Width = dimension });
            }
        }

        private void RemoveSpace(int i)
        {
            if (IsVertical) Grid.RowDefinitions.RemoveAt(i);
            else Grid.ColumnDefinitions.RemoveAt(i);
        }

        private int GetSpaceCount()
        {
            return IsVertical ? Grid.RowDefinitions.Count : Grid.ColumnDefinitions.Count;
        }

        private void SetPosition(int i, UIElement view)
        {
            if (IsVertical) Grid.SetRow(view, i);
            else Grid.SetColumn(view, i);
        }

        private void SetDimension(int i, GridLength dimension)
        {
            if (IsVertical)
                Grid.RowDefinitions[i].Height = dimension;
            else Grid.ColumnDefinitions[i].Width = dimension;
        }

        protected override bool IsSelected(MouseButtonEventArgs e)
        {
            return false;
        }

        protected override List<GroupProperties> GetPropertyGroups()
        {
            return PropertyGroups!;
        }

        protected override void ContinueToUpdate(GroupNames groupName, PropertyNames propertyName, string value)
        {
            if (groupName == GroupNames.Transform && propertyName == PropertyNames.Gap)
            {
                //Prendre en compte les deplacements et les suppression d'éléments
                if (Children.Count <= 1) return;
                var vd = Helper.ConvertToDouble(value);
                var oldGap =
                    Helper.ConvertToDouble(this.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Gap));

                for (var i = 0; i < Children.Count; i++)
                {
                    if (i == Children.Count - 1) break;
                    SetChildGap(i, oldGap, vd);
                }

                this.SetPropertyValue(GroupNames.Transform, PropertyNames.Gap, value);
            }
        }

        protected override void ContinueToInitialize(string groupName, string propertyName, string value)
        {
        }

        protected override bool CanSetProperty(GroupNames groupName, PropertyNames propertyName, string value)
        {
            return true;
        }

        protected override bool CanChildSetProperty(GroupNames groupName, PropertyNames propertyName, string value)
        {
            return true;
        }

        protected override void RestoreProperties()
        {
            RowRestoreProperties.RestoreProperties(this);
        }

        protected override void CheckVisibilities()
        {
            this.SetVisibilities();
        }

        protected override void CallBack(GroupNames groupName, PropertyNames propertyName, string value)
        {
            if (Children.Count == 0) return;
            /*var vt1 = this.GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vt);
            Console.WriteLine($"Vt before: {vt1}");

            if (groupName == GroupNames.SelfAlignment)
            {
                var isChildHorizontal = SelfAlignment.IsHorizontal(Children[0]);
                var isHorizontal = Alignment.IsHorizontal(this);

                var ah = Alignment.Horizontal(this);
                var ahc = SelfAlignment.Horizontal(Children[0]);

                var isChildVertical = SelfAlignment.IsVertical(Children[0]);
                var isVertical = Alignment.IsVertical(this);

                var av = Alignment.Vertical(this);
                var avc = SelfAlignment.Vertical(Children[0]);

                if (isChildHorizontal && !isHorizontal)
                    Alignment.SetHorizontalValue(this, ahc, "1");
                else if (!isChildHorizontal && isHorizontal)
                    Alignment.SetHorizontalOnNull(this);

                if (isChildVertical && !isVertical)
                    Alignment.SetVerticalValue(this, avc, "1");
                else if (!isChildVertical && isVertical)
                    Alignment.SetVerticalOnNull(this);
            }
            else if (groupName == GroupNames.Transform)
            {
                var isHorizontal = Alignment.IsHorizontal(this);
                var isVertical = Alignment.IsVertical(this);
                var widthChild = Children[0].GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
                var heightChild = Children[0].GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);

                if (widthChild == SizeValue.Expand.ToString() && isHorizontal)
                    Alignment.SetHorizontalOnNull(this);
                else if (widthChild != SizeValue.Expand.ToString() && !isHorizontal)
                    Alignment.SetHorizontalValue(this, PropertyNames.Hl, "1");

                if (heightChild == SizeValue.Expand.ToString() && isVertical)
                    Alignment.SetVerticalOnNull(this);
                else if (heightChild != SizeValue.Expand.ToString() && !isVertical)
                    Alignment.SetVerticalValue(this, PropertyNames.Vt, "1");
            }*/
        }
    }
}