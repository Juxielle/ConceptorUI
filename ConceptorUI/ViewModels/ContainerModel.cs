using System;
using ConceptorUI.Models;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using ConceptorUI.Utils;
using ConceptorUi.ViewModels.Operations;


namespace ConceptorUi.ViewModels
{
    internal class ContainerModel : Component
    {
        public ContainerModel(bool allowConstraints = false)
        {
            OnInit();

            Content.Child = new Border();

            Name = ComponentList.Container;
            ChildContentLimit = 1;
            CanAddIntoChildContent = true;

            if (allowConstraints) return;
            SelfConstraints();
            OnInitialize();
        }

        public override void WhenTextChanged(string propertyName, string value, bool isInitialize = false)
        {
        }

        protected override void InitChildContent()
        {
        }

        protected override void AddIntoChildContent(FrameworkElement child, int k = -1)
        {
            (Content.Child as Border)!.Child ??= child;
        }

        public override bool AllowExpanded(bool isWidth = true)
        {
            return true;
        }

        public override bool AllowAuto(bool isWidth = true)
        {
            return true;
        }

        public sealed override void SelfConstraints()
        {
            /* Global */
            SetPropertyVisibility(GroupNames.Global, PropertyNames.FilePicker, false);
            /* Content Alignment */
            SetGroupVisibility(GroupNames.Alignment);
            SetPropertyVisibility(GroupNames.Alignment, PropertyNames.SpaceBetween, false);
            SetPropertyVisibility(GroupNames.Alignment, PropertyNames.SpaceAround, false);
            SetPropertyVisibility(GroupNames.Alignment, PropertyNames.SpaceEvery, false);
            /* Self Alignment */
            /* Transform */
            SetGroupVisibility(GroupNames.Transform);
            /* Text */
            SetGroupVisibility(GroupNames.Text, false);
            /* Appearance */
            /* Shadow */
            SetGroupVisibility(GroupNames.Shadow);
        }

        protected override void LayoutConstraints(int id, bool isDeserialize = false, bool existExpaned = false)
        {
            Children[id].Parent = this;
            /* Global */
            Children[id].SetPropertyVisibility(GroupNames.Global, PropertyNames.MoveLeft, false);
            Children[id].SetPropertyVisibility(GroupNames.Global, PropertyNames.MoveRight, false);
            Children[id].SetPropertyVisibility(GroupNames.Global, PropertyNames.MoveTop, false);
            Children[id].SetPropertyVisibility(GroupNames.Global, PropertyNames.MoveBottom, false);
            //Children[id].SetPropertyVisibility(GroupNames.Global, PropertyNames.FilePicker, false);

            /* Content Alignment */
            /* Self Alignment */
            Children[id].SetGroupVisibility(GroupNames.SelfAlignment, false);

            /* Transform */
            Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.Rot, false);
            Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.X, false);
            Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.Y, false);
            Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.Stretch, false);

            /* Appearance */
            /* Shadow */

            var w = Children[id].GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
            if (w != SizeValue.Expand.ToString() &&
                Children[id].IsNullAlignment(GroupNames.SelfAlignment, "Horizontal"))
            {
                WhenAlignmentChanged(PropertyNames.Hl, "1");
            }
            else
            {
                var hc = Children[id].GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Hc);
                var hr = Children[id].GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Hr);
                var hl = Children[id].GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Hl);
                if (hl == "1" || hc == "1" || hr == "1")
                {
                    var horizontal = hr == "1" ? PropertyNames.Hr : hc == "1" ? PropertyNames.Hc : PropertyNames.Hl;
                    SetPropertyValue(GroupNames.Alignment, horizontal, "1");
                    WhenAlignmentChanged(horizontal, "1");
                }
                else if (w != SizeValue.Expand.ToString())
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, "1");
            }

            var h = Children[id].GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
            if (h != SizeValue.Expand.ToString() && Children[id].IsNullAlignment(GroupNames.SelfAlignment, "Vertical"))
                WhenAlignmentChanged(PropertyNames.Vt, "1"); //Démander la permission du parent avant
            else
            {
                var vc = Children[id].GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Vc);
                var vb = Children[id].GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Vb);
                var vt = Children[id].GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Vt);

                if (vt == "1" || vc == "1" || vb == "1")
                {
                    var vertical = vb == "1" ? PropertyNames.Vb : vc == "1" ? PropertyNames.Vc : PropertyNames.Vt;
                    SetPropertyValue(GroupNames.Alignment, vertical, "1");
                    WhenAlignmentChanged(vertical, "1");
                }
                else if (h != SizeValue.Expand.ToString())
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, "1");
            }
        }

        protected override void WhenAlignmentChanged(PropertyNames propertyName, string value)
        {
            if (propertyName is PropertyNames.Hl or PropertyNames.Hc or PropertyNames.Hr)
            {
                SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.Hc, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.Hr, "0");
                SetPropertyValue(GroupNames.Alignment, propertyName, value);

                foreach (var child in Children)
                {
                    child.OnUpdated(GroupNames.SelfAlignment, propertyName, value, true);
                }
            }
            else if (propertyName is PropertyNames.Vt or PropertyNames.Vc or PropertyNames.Vb)
            {
                SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.Vc, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.Vb, "0");
                SetPropertyValue(GroupNames.Alignment, propertyName, value);

                foreach (var child in Children)
                {
                    child.OnUpdated(GroupNames.SelfAlignment, propertyName, value, true);
                }
            }
        }

        protected override void Delete(int k = -1)
        {
            if ((Children.Count == 0 || !Children[0].Selected) && k == -1) return;

            Children.RemoveAt(0);
            (Content.Child as Border)!.Child = null;

            if (k > 0) return;
            SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, "0");
            SetPropertyValue(GroupNames.Alignment, PropertyNames.Hc, "0");
            SetPropertyValue(GroupNames.Alignment, PropertyNames.Hr, "0");

            SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, "0");
            SetPropertyValue(GroupNames.Alignment, PropertyNames.Vc, "0");
            SetPropertyValue(GroupNames.Alignment, PropertyNames.Vb, "0");

            if (k == -1) OnSelected();
        }

        protected override void WhenWidthChanged(string value)
        {
            if (Children.Count == 0 || !Children[0].Selected) return;

            SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, "0");
            SetPropertyValue(GroupNames.Alignment, PropertyNames.Hc, "0");
            SetPropertyValue(GroupNames.Alignment, PropertyNames.Hr, "0");

            SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, value != SizeValue.Expand.ToString() ? "1" : "0");
        }

        protected override void WhenHeightChanged(string value)
        {
            if (Children.Count == 0 || !Children[0].Selected) return;

            SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, "0");
            SetPropertyValue(GroupNames.Alignment, PropertyNames.Vc, "0");
            SetPropertyValue(GroupNames.Alignment, PropertyNames.Vb, "0");

            SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, value != SizeValue.Expand.ToString() ? "1" : "0");
        }

        protected override object GetPropertyGroups()
        {
            return PropertyGroups!;
        }

        protected override void ContinueToUpdate(GroupNames groupName, PropertyNames propertyName, string value)
        {
        }

        protected override void CallBack(GroupNames groupName, PropertyNames propertyName, string value)
        {
            if (Children.Count == 0) return;

            #region SelfAlignment

            if (groupName == GroupNames.SelfAlignment)
            {
                if (propertyName is PropertyNames.Hl or PropertyNames.Hc or PropertyNames.Hr)
                {
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.Hc, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.Hr, "0");
                    if (value == "1")
                    {
                        SetPropertyValue(GroupNames.Alignment, propertyName, value);
                    }
                    else if (Children.Count > 0 && !Children[0].AllowExpanded())
                    {
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, "0");
                    }
                }
                else if (propertyName is PropertyNames.Vt or PropertyNames.Vc or PropertyNames.Vb)
                {
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.Vc, "0");
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.Vb, "0");
                    if (value == "1")
                    {
                        SetPropertyValue(GroupNames.Alignment, propertyName, value);
                    }
                    else if (!Children[0].AllowExpanded())
                    {
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, "0");
                    }
                }
            }

            #endregion

            #region Transform

            /*
                -- Vérifier aussi que le fils soit expandable avant de confirmer l'action;
                -- Ecrire aussi une autre fonction qui permet de restaurer la nature propre d'un composant;
                -------------------------------
                -- Créer une autre fonction comparable à initializer capable de restaurer sans agir, les valeurs des
                    propriétés: Avant d'initialiser un composant et Après une action. => Elle spécifique à un composant.
            */
            if (groupName == GroupNames.Transform)
            {
                if (propertyName == PropertyNames.Width)
                {
                    var hl = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hl);
                    var hc = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hc);
                    var hr = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hr);
                    var isHorizontal = hl == "1" || hc == "1" || hr == "1";

                    if (value == SizeValue.Expand.ToString() && isHorizontal && Children[0].AllowExpanded())
                    {
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, "0");
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Hc, "0");
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Hr, "0");
                    }
                    else if ((value == SizeValue.Auto.ToString() || value != SizeValue.Expand.ToString()) &&
                             !isHorizontal)
                    {
                        var hlc = Children[0].GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hl);
                        var hcc = Children[0].GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hc);
                        var hrc = Children[0].GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hr);
                        var isChildHorizontal = hlc == "1" || hcc == "1" || hrc == "1";

                        if (!isChildHorizontal)
                        {
                            Children[0].OnUpdated(GroupNames.SelfAlignment, PropertyNames.Hl, "1", true);
                            return;
                        }

                        var childHorizontal = hlc == "1" ? PropertyNames.Hl :
                            hcc == "1" ? PropertyNames.Hc : PropertyNames.Hr;

                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, "0");
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Hc, "0");
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Hr, "0");
                        SetPropertyValue(GroupNames.Alignment, childHorizontal, "1");
                    }
                }
                else if (propertyName == PropertyNames.Height)
                {
                    var vt = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vt);
                    var vc = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vc);
                    var vb = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vb);
                    var isVertical = vt == "1" || vc == "1" || vb == "1";

                    if (value == SizeValue.Expand.ToString() && isVertical && Children[0].AllowExpanded(false))
                    {
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, "0");
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Vc, "0");
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Vb, "0");
                    }
                    else if ((value == SizeValue.Auto.ToString() || value != SizeValue.Expand.ToString()) &&
                             !isVertical)
                    {
                        var vtc = Children[0].GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vt);
                        var vcc = Children[0].GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vc);
                        var vbc = Children[0].GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vb);
                        var isChildVertical = vtc == "1" || vcc == "1" || vbc == "1";

                        if (!isChildVertical)
                        {
                            Children[0].OnUpdated(GroupNames.SelfAlignment, PropertyNames.Vt, "1", true);
                            return;
                        }

                        var childVertical = vtc == "1" ? PropertyNames.Vt :
                            vcc == "1" ? PropertyNames.Vc : PropertyNames.Vb;

                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, "0");
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Vc, "0");
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Vb, "0");
                        SetPropertyValue(GroupNames.Alignment, childVertical, "1");
                    }
                }
                else if (propertyName == PropertyNames.He)
                {
                    var hl = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hl);
                    var hc = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hc);
                    var hr = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hr);
                    var isHorizontal = hl == "1" || hc == "1" || hr == "1";

                    if (value == "1" && isHorizontal && Children[0].AllowExpanded())
                    {
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, "0");
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Hc, "0");
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Hr, "0");
                    }
                    else if (value == "0" && !isHorizontal)
                    {
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, "1");
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Hc, "0");
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Hr, "0");
                    }
                }
                else if (propertyName == PropertyNames.Ve)
                {
                    var vt = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vt);
                    var vc = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vc);
                    var vb = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vb);
                    var isVertical = vt == "1" || vc == "1" || vb == "1";

                    if (value == "1" && isVertical && Children[0].AllowExpanded(false))
                    {
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, "0");
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Vc, "0");
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Vb, "0");
                    }
                    else if (value == "0" && !isVertical)
                    {
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, "1");
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Vc, "0");
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Vb, "0");
                    }
                }
            }

            #endregion
        }

        protected override void ContinueToInitialize(string groupName, string propertyName, string value)
        {
        }

        protected override bool IsSelected(MouseButtonEventArgs e)
        {
            return false;
        }

        protected override void WhenFileLoaded(string value)
        {
        }

        protected override void OnMoveLeft()
        {
        }

        protected override void OnMoveRight()
        {
        }

        protected override void OnMoveTop()
        {
        }

        protected override void OnMoveBottom()
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

            var ah = Alignment.Horizontal(this);
            var isHorizontal = Alignment.IsHorizontal(this);

            var av = Alignment.Vertical(this);
            var isVertical = Alignment.IsVertical(this);

            //Alignment
            var activationCount = 0;
            if (hl == "1") activationCount++;
            if (hc == "1") activationCount++;
            if (hr == "1") activationCount++;
            if (activationCount >= 2)
            {
                SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, Children.Count > 0 ? "1" : "0");
            }

            activationCount = 0;
            if (vt == "1") activationCount++;
            if (vc == "1") activationCount++;
            if (vb == "1") activationCount++;
            if (activationCount >= 2)
            {
                SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, Children.Count > 0 ? "1" : "0");
            }

            if (hl != "0" || hl != "1") SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, "0");
            if (hc != "0" || hc != "1") SetPropertyValue(GroupNames.Alignment, PropertyNames.Hc, "0");
            if (hr != "0" || hr != "1") SetPropertyValue(GroupNames.Alignment, PropertyNames.Hr, "0");

            if (vt != "0" || vt != "1") SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, "0");
            if (vc != "0" || vc != "1") SetPropertyValue(GroupNames.Alignment, PropertyNames.Vc, "0");
            if (vb != "0" || vb != "1") SetPropertyValue(GroupNames.Alignment, PropertyNames.Vb, "0");

            if (isHorizontal && Children.Count == 0)
            {
                Alignment.SetHorizontalOnNull(this);
            }

            if (isVertical && Children.Count == 0)
            {
                Alignment.SetVerticalOnNull(this);
            }

            foreach (var child in Children)
            {
                var ahc = SelfAlignment.Horizontal(child);
                var isChildHorizontal = SelfAlignment.IsHorizontal(child);

                var avc = SelfAlignment.Vertical(child);
                var isChildVertical = SelfAlignment.IsVertical(child);

                var heightChild = child.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
                var widthChild = child.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);

                if (isHorizontal)
                {
                    if (isChildHorizontal)
                    {
                        if (widthChild == SizeValue.Expand.ToString())
                        {
                            SelfAlignment.SetHorizontalValue(child, ah, "1");
                            child.SetPropertyValue(GroupNames.Transform, PropertyNames.Width,
                                SizeValue.Auto.ToString());
                        }
                        else if (ah != ahc)
                            SelfAlignment.SetHorizontalValue(child, ah, "1");
                    }
                    else
                    {
                        if (widthChild == SizeValue.Expand.ToString() && child.AllowExpanded())
                        {
                            Alignment.SetHorizontalOnNull(this);
                        }
                        else if (widthChild == SizeValue.Expand.ToString() && !child.AllowExpanded())
                        {
                            SelfAlignment.SetHorizontalValue(child, ah, "1");
                            child.SetPropertyValue(GroupNames.Transform, PropertyNames.Width,
                                SizeValue.Auto.ToString());
                        }
                        else SelfAlignment.SetHorizontalValue(child, ah, "1");
                    }
                }
                else
                {
                    if (isChildHorizontal)
                    {
                        if (widthChild == SizeValue.Expand.ToString() && child.AllowExpanded())
                        {
                            SelfAlignment.SetHorizontalOnNull(child);
                        }
                        else if (widthChild == SizeValue.Expand.ToString() && !child.AllowExpanded())
                        {
                            Alignment.SetHorizontalValue(this, ahc, "1");
                            child.SetPropertyValue(GroupNames.Transform, PropertyNames.Width,
                                SizeValue.Auto.ToString());
                        }
                        else SelfAlignment.SetHorizontalValue(child, ahc, "1");
                    }
                    else
                    {
                        if (widthChild == SizeValue.Expand.ToString() && !child.AllowExpanded())
                        {
                            SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, "1");
                            child.SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.Hl, "1");
                            child.SetPropertyValue(GroupNames.Transform, PropertyNames.Width,
                                SizeValue.Auto.ToString());
                        }
                        else if (widthChild != SizeValue.Expand.ToString())
                        {
                            SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, "1");
                            child.SetPropertyValue(GroupNames.SelfAlignment, PropertyNames.Hl, "1");
                        }
                    }
                }
                
                if (isVertical)
                {
                    if (isChildVertical)
                    {
                        if (heightChild == SizeValue.Expand.ToString())
                        {
                            //Ecrire une fonction pour faire une activation horizontal
                            child.SetPropertyValue(GroupNames.SelfAlignment, av, "1");
                            child.SetPropertyValue(GroupNames.Transform, PropertyNames.Height,
                                SizeValue.Auto.ToString());
                        }
                        else if (av != avc)
                            child.SetPropertyValue(GroupNames.SelfAlignment, av, "1");
                    }
                    else
                    {
                        if (heightChild == SizeValue.Expand.ToString() && child.AllowExpanded())
                        {
                            SetPropertyValue(GroupNames.Alignment, av, "0");
                        }
                        else if (heightChild == SizeValue.Expand.ToString() && !child.AllowExpanded())
                        {
                            child.SetPropertyValue(GroupNames.SelfAlignment, av, "1");
                            child.SetPropertyValue(GroupNames.Transform, PropertyNames.Height,
                                SizeValue.Auto.ToString());
                        }
                        else child.SetPropertyValue(GroupNames.SelfAlignment, av, "1");
                    }
                }

                child.Synchronize();
            }
            this.Synchronize();
        }
    }
}