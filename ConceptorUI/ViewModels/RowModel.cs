﻿using ConceptorUI.Models;
using System.Linq;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System;


namespace ConceptorUi.ViewModels
{
    internal class RowModel : Component
    {
        private readonly Grid _grid;

        public RowModel(bool isVertical = true, bool allowConstraints = false)
        {
            OnInit();

            _grid = new Grid();
            IsVertical = isVertical;

            Content.Child = _grid;
            Name = isVertical ? ComponentList.Row : ComponentList.Column;

            if (allowConstraints) return;
            SelfConstraints();
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
            SetPropertyVisibility(GroupNames.Global, PropertyNames.FilePicker, false);
            SetPropertyVisibility(GroupNames.Global, PropertyNames.Focus);
            /* Content Alignment */
            SetGroupVisibility(GroupNames.Alignment);
            /* Self Alignment */
            /* Transform */
            SetGroupVisibility(GroupNames.Transform);
            /* Text */
            SetGroupVisibility(GroupNames.Text, false);
            /* Appearance */
            /* Shadow */
            SetGroupVisibility(GroupNames.Shadow);
        }

        protected override void WhenAlignmentChanged(PropertyNames propertyName, string value)
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

                SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Hl : PropertyNames.Vt, "0");
                SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Hc : PropertyNames.Vc, "0");
                SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Hr : PropertyNames.Vb, "0");
                SetPropertyValue(GroupNames.Alignment, propertyName, value);

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

                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vt : PropertyNames.Hl, "0");
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vc : PropertyNames.Hc, "0");
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vb : PropertyNames.Hr, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceBetween, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceAround, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceEvery, "0");
                    SetPropertyValue(GroupNames.Alignment, propertyName, value);
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

                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vt : PropertyNames.Hl, "0");
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vc : PropertyNames.Hc, "0");
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vb : PropertyNames.Hr, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceBetween, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceAround, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceEvery, "0");
                    SetPropertyValue(GroupNames.Alignment, propertyName, value);
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

                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vt : PropertyNames.Hl, "0");
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vc : PropertyNames.Hc, "0");
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vb : PropertyNames.Hr, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceBetween, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceAround, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceEvery, "0");
                    SetPropertyValue(GroupNames.Alignment, propertyName, value);
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

                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vt : PropertyNames.Hl, "0");
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vc : PropertyNames.Hc, "0");
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vb : PropertyNames.Hr, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceBetween, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceAround, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceEvery, "0");
                    SetPropertyValue(GroupNames.Alignment, propertyName, value);
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

                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vt : PropertyNames.Hl, "0");
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vc : PropertyNames.Hc, "0");
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vb : PropertyNames.Hr, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceBetween, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceAround, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceEvery, "0");
                    SetPropertyValue(GroupNames.Alignment, propertyName, value);
                }
                else WhenAlignmentChanged(IsVertical ? PropertyNames.Vt : PropertyNames.Hl, "1");
            }
            else if (propertyName == PropertyNames.SpaceEvery)
            {
                if (value == "1")
                {
                    var vt = GetGroupProperties(GroupNames.Alignment)
                        .GetValue(IsVertical ? PropertyNames.Vt : PropertyNames.Hl);
                    var vc = GetGroupProperties(GroupNames.Alignment)
                        .GetValue(IsVertical ? PropertyNames.Vc : PropertyNames.Hc);
                    var vb = GetGroupProperties(GroupNames.Alignment)
                        .GetValue(IsVertical ? PropertyNames.Vb : PropertyNames.Hr);

                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vt : PropertyNames.Hl, "0");
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vc : PropertyNames.Hc, "0");
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vb : PropertyNames.Hr, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceBetween, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceAround, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceEvery, "0");
                    SetPropertyValue(GroupNames.Alignment, propertyName, value);

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

        protected override void AddIntoChildContent(FrameworkElement child, int k = -1)
        {
            if (k == -1)
                _grid.Children.Add(child);
            else _grid.Children.Insert(k, child);
        }

        protected override bool AllowExpanded(bool isWidth = true)
        {
            return true;
        }

        protected override void Delete(int j = -1)
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

            var vc = GetGroupProperties(GroupNames.Alignment)
                .GetValue(IsVertical ? PropertyNames.Vc : PropertyNames.Hc);
            var vb = GetGroupProperties(GroupNames.Alignment)
                .GetValue(IsVertical ? PropertyNames.Vb : PropertyNames.Hr);
            var sb = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceBetween);
            var sa = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceAround);
            var se = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceEvery);

            if (nc == 1)
            {
                if (IsVertical) _grid.RowDefinitions.Clear();
                else _grid.ColumnDefinitions.Clear();

                SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.Vc, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.Vb, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.Hc, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.Hr, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceBetween, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceAround, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceEvery, "0");
            }
            else if (IsVertical)
            {
                _grid.RowDefinitions.RemoveAt(i - 1);
                _grid.RowDefinitions.RemoveAt(i - 1);
            }
            else
            {
                _grid.ColumnDefinitions.RemoveAt(i - 1);
                _grid.ColumnDefinitions.RemoveAt(i - 1);
            }

            //Ecrire la methode permettant de restorer l'alignment
            _grid.Children.RemoveAt(i);
            Children.RemoveAt(i);

            if (j == -1) OnSelected();
        }

        protected override void WhenWidthChanged(string value)
        {
            WhenHeightChanged(value);
        }

        protected override void WhenHeightChanged(string value)
        {
            var i = -1;
            var found = false;

            foreach (var child in Children)
            {
                if (child.Selected) i = Children.IndexOf(child);
                found = found || GetGroupProperties(GroupNames.Transform)
                    .GetValue(IsVertical ? PropertyNames.Height : PropertyNames.Width) == SizeValue.Expand.ToString();
            }

            if (i != -1 && value == SizeValue.Expand.ToString())
            {
                SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vt : PropertyNames.Hl, "0");
                SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vc : PropertyNames.Hc, "0");
                SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vb : PropertyNames.Hr, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceBetween, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceAround, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceEvery, "0");

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

            if (!found && i != -1 && value != SizeValue.Expand.ToString())
            {
                SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vt : PropertyNames.Hl, "1");
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
            var nl = GetSpaceCount();
            var nc = Children.Count;

            var vt = GetGroupProperties(GroupNames.Alignment)
                .GetValue(IsVertical ? PropertyNames.Vt : PropertyNames.Hl);
            var vc = GetGroupProperties(GroupNames.Alignment)
                .GetValue(IsVertical ? PropertyNames.Vc : PropertyNames.Hc);
            var vb = GetGroupProperties(GroupNames.Alignment)
                .GetValue(IsVertical ? PropertyNames.Vb : PropertyNames.Hr);

            var focus = GetGroupProperties(GroupNames.Global).GetValue(PropertyNames.Focus) == "1";
            var k = -1;

            foreach (var i in from child in Children let i = Children.IndexOf(child) where child.Selected select i)
            {
                k = i;
                break;
            }

            if (k != -1 && k > 0 &&
                (nl == nc || (nl == nc + 1 && vt == "1") || (nl == nc + 1 && vb == "1") || (nl == nc + 2 && vc == "1")))
            {
                if (focus)
                {
                    var fvbc = vb == "1" || vc == "1";
                    var child = Children[k];

                    var rd = new RowDefinition();
                    var cd = new ColumnDefinition();
                    if (IsVertical) rd = _grid.RowDefinitions[fvbc ? k + 1 : k];
                    else cd = _grid.ColumnDefinitions[fvbc ? k + 1 : k];

                    Children.RemoveAt(k);
                    Children.Insert(k - 1, child);
                    _grid.Children.RemoveAt(k);
                    _grid.Children.Insert(k - 1, child.ComponentView);

                    RemoveSpace(fvbc ? k + 1 : k);
                    if (IsVertical) _grid.RowDefinitions.Insert(fvbc ? k : k - 1, rd);
                    else _grid.ColumnDefinitions.Insert(fvbc ? k : k - 1, cd);

                    SetPosition(fvbc ? k : k - 1, Children[k - 1].ComponentView);
                    SetPosition(fvbc ? k + 1 : k, Children[k].ComponentView);
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
            var nl = _grid.RowDefinitions.Count;
            var nc = Children.Count;

            var vt = GetGroupProperties(GroupNames.Alignment)
                .GetValue(IsVertical ? PropertyNames.Vt : PropertyNames.Hl);
            var vc = GetGroupProperties(GroupNames.Alignment)
                .GetValue(IsVertical ? PropertyNames.Vc : PropertyNames.Hc);
            var vb = GetGroupProperties(GroupNames.Alignment)
                .GetValue(IsVertical ? PropertyNames.Vb : PropertyNames.Hr);

            var focus = GetGroupProperties(GroupNames.Global).GetValue(PropertyNames.Focus) == "1";
            var k = -1;
            foreach (var i in from child in Children let i = Children.IndexOf(child) where child.Selected select i)
            {
                k = i;
                break;
            }

            if (k != -1 && k < nc - 1 &&
                (nl == nc || (nl == nc + 1 && vb == "1") || (nl == nc + 1 && vt == "1") || (nl == nc + 2 && vc == "1")))
            {
                if (focus)
                {
                    var fvbc = vb == "1" || vc == "1";
                    var child = Children[k];

                    var rd = new RowDefinition();
                    var cd = new ColumnDefinition();
                    if (IsVertical) rd = _grid.RowDefinitions[fvbc ? k + 1 : k];
                    else cd = _grid.ColumnDefinitions[fvbc ? k + 1 : k];

                    Children.RemoveAt(k);
                    Children.Insert(k + 1, child);
                    _grid.Children.RemoveAt(k);
                    _grid.Children.Insert(k + 1, child.ComponentView);

                    RemoveSpace(fvbc ? k + 1 : k);

                    if (IsVertical) _grid.RowDefinitions.Insert(fvbc ? k + 2 : k + 1, rd);
                    else _grid.ColumnDefinitions.Insert(fvbc ? k + 2 : k + 1, cd);

                    SetPosition(fvbc ? k + 2 : k + 1, Children[k + 1].ComponentView);
                    SetPosition(fvbc ? k + 1 : k, Children[k].ComponentView);
                }
                else
                {
                    Children[k].OnUnselected();
                    Children[k + 1].OnSelected();
                }
            }
        }

        protected override void LayoutConstraints(int id, bool isDeserialize = false, bool existExpand = false)
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

            var vt = GetGroupProperties(GroupNames.Alignment)
                .GetValue(IsVertical ? PropertyNames.Vt : PropertyNames.Hl);
            var vc = GetGroupProperties(GroupNames.Alignment)
                .GetValue(IsVertical ? PropertyNames.Vc : PropertyNames.Hc);
            var vb = GetGroupProperties(GroupNames.Alignment)
                .GetValue(IsVertical ? PropertyNames.Vb : PropertyNames.Hr);

            var sb = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceBetween);
            var sa = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceAround);
            var se = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceEvery);

            var hl = GetGroupProperties(GroupNames.Alignment)
                .GetValue(IsVertical ? PropertyNames.Hl : PropertyNames.Vt);
            var hc = GetGroupProperties(GroupNames.Alignment)
                .GetValue(IsVertical ? PropertyNames.Hc : PropertyNames.Vc);
            var hr = GetGroupProperties(GroupNames.Alignment)
                .GetValue(IsVertical ? PropertyNames.Hr : PropertyNames.Vb);

            var hlc = component.GetGroupProperties(GroupNames.Alignment)
                .GetValue(IsVertical ? PropertyNames.Hl : PropertyNames.Vt);
            var hcc = component.GetGroupProperties(GroupNames.Alignment)
                .GetValue(IsVertical ? PropertyNames.Hc : PropertyNames.Vc);
            var hrc = component.GetGroupProperties(GroupNames.Alignment)
                .GetValue(IsVertical ? PropertyNames.Hr : PropertyNames.Vb);

            var hasAlignment = hlc == "1" || hcc == "1" || hrc == "1";

            var w = component.GetGroupProperties(GroupNames.Transform)
                .GetValue(IsVertical ? PropertyNames.Width : PropertyNames.Height);
            var h = component.GetGroupProperties(GroupNames.Transform)
                .GetValue(IsVertical ? PropertyNames.Height : PropertyNames.Width);

            if (hl == "1" && !hasAlignment)
                component.OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.Hl : PropertyNames.Vt, hl,
                    true);
            else if (hc == "1" && !hasAlignment)
                component.OnUpdated(GroupNames.SelfAlignment, IsVertical ? PropertyNames.Hc : PropertyNames.Vc, hc,
                    true);
            else if (hr == "1" && !hasAlignment)
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
                else
                {
                    SetDimension(GetSpaceCount() - 1, new GridLength(1, GridUnitType.Star));
                    SetPropertyValue(GroupNames.Alignment, IsVertical ? PropertyNames.Vt : PropertyNames.Hl, "1");
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
                if (isAdding) _grid.RowDefinitions.Add(new RowDefinition { Height = dimension });
                else _grid.RowDefinitions.Insert(i, new RowDefinition { Height = dimension });
            }
            else
            {
                if (isAdding) _grid.ColumnDefinitions.Add(new ColumnDefinition { Width = dimension });
                else _grid.ColumnDefinitions.Insert(i, new ColumnDefinition { Width = dimension });
            }
        }

        private void RemoveSpace(int i)
        {
            if (IsVertical) _grid.RowDefinitions.RemoveAt(i);
            else _grid.ColumnDefinitions.RemoveAt(i);
        }

        private int GetSpaceCount()
        {
            return IsVertical ? _grid.RowDefinitions.Count : _grid.ColumnDefinitions.Count;
        }

        private void SetPosition(int i, UIElement view)
        {
            if (IsVertical) Grid.SetRow(view, i);
            else Grid.SetColumn(view, i);
        }

        private void SetDimension(int i, GridLength dimension)
        {
            if (IsVertical)
                _grid.RowDefinitions[i].Height = dimension;
            else _grid.ColumnDefinitions[i].Width = dimension;
        }

        protected override bool IsSelected(MouseButtonEventArgs e)
        {
            return false;
        }

        protected override object GetPropertyGroups()
        {
            return PropertyGroups!;
        }

        protected override void ContinueToUpdate(GroupNames groupName, PropertyNames propertyName, string value)
        {
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
            var hl = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hl);
            var hc = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hc);
            var hr = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hr);

            var vt = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vt);
            var vc = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vc);
            var vb = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vb);

            var sb = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceBetween);
            var sa = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceAround);
            var se = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.SpaceEvery);

            /*---------------------*/
            var activationCount = 0;
            if (hl == "1") activationCount++;
            if (hc == "1") activationCount++;
            if (hr == "1") activationCount++;
            if (sb == "1" && !IsVertical) activationCount++;
            if (sa == "1" && !IsVertical) activationCount++;
            if (se == "1" && !IsVertical) activationCount++;
            if (activationCount >= 2)
            {
                SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, Children.Count > 0 ? "1" : "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.Hc, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.Hr, "0");
                if (!IsVertical)
                {
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceBetween, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceAround, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceEvery, "0");
                }
            }

            activationCount = 0;
            if (vt == "1") activationCount++;
            if (vc == "1") activationCount++;
            if (vb == "1") activationCount++;
            if (sb == "1" && IsVertical) activationCount++;
            if (sa == "1" && IsVertical) activationCount++;
            if (se == "1" && IsVertical) activationCount++;
            if (activationCount >= 2)
            {
                SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, Children.Count > 0 ? "1" : "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.Vc, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.Vb, "0");
                if (IsVertical)
                {
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceBetween, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceAround, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.SpaceEvery, "0");
                }
            }

            /*---------------------*/
        }
    }
}