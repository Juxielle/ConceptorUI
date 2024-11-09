using System;
using ConceptorUI.Models;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;


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

            Children[id].OnInitialize();

            var w = Children[id].GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
            if (w != SizeValue.Expand.ToString() &&
                Children[id].IsNullAlignment(GroupNames.SelfAlignment, "Horizontal"))
                OnUpdated(GroupNames.Alignment, PropertyNames.Hl, "1", true);
            else
            {
                var hc = Children[id].GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Hc);
                var hr = Children[id].GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Hr);
                var hl = Children[id].GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Hl);
                if (hl == "1" || hc == "1" || hr == "1")
                {
                    var horizontal = hr == "1" ? PropertyNames.Hr : hc == "1" ? PropertyNames.Hc : PropertyNames.Hl;
                    SetPropertyValue(GroupNames.Alignment, horizontal, "1");
                    WhenAlignmentChanged(horizontal, "0");
                    WhenAlignmentChanged(horizontal, "1");
                }
            }

            var h = Children[id].GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
            if (h != SizeValue.Expand.ToString() && Children[id].IsNullAlignment(GroupNames.SelfAlignment, "Vertical"))
                OnUpdated(GroupNames.Alignment, PropertyNames.Vt, "1", true);
            else
            {
                var vc = Children[id].GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Vc);
                var vb = Children[id].GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Vb);
                var vt = Children[id].GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.Vt);

                if (vt != "1" && vc != "1" && vb != "1") return;
                var vertical = vb == "1" ? PropertyNames.Vb : vc == "1" ? PropertyNames.Vc : PropertyNames.Vt;
                SetPropertyValue(GroupNames.Alignment, vertical, "1");
                WhenAlignmentChanged(vertical, "0");
                WhenAlignmentChanged(vertical, "1");
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

                var alignment = propertyName switch
                {
                    PropertyNames.Hl => PropertyNames.Hl,
                    PropertyNames.Hc => PropertyNames.Hc,
                    _ => PropertyNames.Hr
                };

                foreach (var child in Children)
                {
                    var w = child.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
                    if (value == "1")
                    {
                        child.OnUpdated(GroupNames.SelfAlignment, alignment, value, true);
                        if (w == SizeValue.Expand.ToString())
                            child.OnUpdated(GroupNames.Transform, PropertyNames.Width, SizeValue.Auto.ToString(), true);
                    }
                    // else
                    // {
                    //     child.OnUpdated(GroupNames.SelfAlignment, alignment, value, true);
                    // }
                }
            }
            else if (propertyName is PropertyNames.Vt or PropertyNames.Vc or PropertyNames.Vb)
            {
                SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.Vc, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.Vb, "0");
                SetPropertyValue(GroupNames.Alignment, propertyName, value);

                var alignment = propertyName switch
                {
                    PropertyNames.Vt => PropertyNames.Vt,
                    PropertyNames.Vc => PropertyNames.Vc,
                    _ => PropertyNames.Vb
                };

                foreach (var child in Children)
                {
                    var h = child.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
                    if (value == "1")
                    {
                        child.OnUpdated(GroupNames.SelfAlignment, alignment, value, true);
                        // if (h == SizeValue.Expand.ToString())
                        //     child.OnUpdated(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString(),
                        //         true);
                    }
                    // else
                    // {
                    //     child.OnUpdated(GroupNames.SelfAlignment, alignment, value, true);
                    // }
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
            if ((Children.Count > 0 && Children[0].Selected) || value != SizeValue.Expand.ToString())
                SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, "1");
        }

        protected override void WhenHeightChanged(string value)
        {
            if ((Children.Count > 0 && Children[0].Selected) || value != SizeValue.Expand.ToString())
                SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, "1");
        }

        protected override object GetPropertyGroups()
        {
            return PropertyGroups!;
        }

        protected override void ContinueToUpdate(GroupNames groupName, PropertyNames propertyName, string value)
        {
            if (groupName == GroupNames.Transform && propertyName is PropertyNames.He or PropertyNames.Hve)
            {
                Parent.SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, value == "1" ? "0" : "1");
                Parent.SetPropertyValue(GroupNames.Alignment, PropertyNames.Hc, "0");
                Parent.SetPropertyValue(GroupNames.Alignment, PropertyNames.Hr, "0");
            }

            if (groupName == GroupNames.Transform && propertyName is PropertyNames.Ve or PropertyNames.Hve)
            {
                Parent.SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, value == "1" ? "0" : "1");
                Parent.SetPropertyValue(GroupNames.Alignment, PropertyNames.Vc, "0");
                Parent.SetPropertyValue(GroupNames.Alignment, PropertyNames.Vb, "0");
            }
        }

        protected override void CallBack(GroupNames groupName, PropertyNames propertyName, string value)
        {
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
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, "1");
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
                    else if (Children.Count > 0 && !Children[0].AllowExpanded())
                    {
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, "1");
                    }
                }
            }

            #endregion

            #region Transform

            if (groupName == GroupNames.Transform)
            {
                if (propertyName == PropertyNames.Width)
                {
                    var hl = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hl);
                    var hc = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hc);
                    var hr = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hr);
                    var isHorizontal = hl == "1" || hc == "1" || hr == "1";

                    if (value == SizeValue.Expand.ToString() && isHorizontal)
                    {
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, "0");
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Hc, "0");
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Hr, "0");
                    }
                    else if (value == SizeValue.Auto.ToString() ||
                             value != SizeValue.Expand.ToString() && !isHorizontal)
                    {
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, "1");
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Hc, "0");
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Hr, "0");
                    }
                }
                else if (propertyName == PropertyNames.Height)
                {
                    var vt = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vt);
                    var vc = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vc);
                    var vb = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Vb);
                    var isVertical = vt == "1" || vc == "1" || vb == "1";

                    if (value == SizeValue.Expand.ToString() && isVertical)
                    {
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, "0");
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Vc, "0");
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Vb, "0");
                    }
                    else if (value == SizeValue.Auto.ToString() || value != SizeValue.Expand.ToString() && !isVertical)
                    {
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, "1");
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Vc, "0");
                        SetPropertyValue(GroupNames.Alignment, PropertyNames.Vb, "0");
                    }
                }
                else if (propertyName == PropertyNames.He)
                {
                    var hl = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hl);
                    var hc = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hc);
                    var hr = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.Hr);
                    var isHorizontal = hl == "1" || hc == "1" || hr == "1";

                    if (value == "1" && isHorizontal)
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

                    if (value == "1" && isVertical)
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

            /*---------------------*/
            var activationCount = 0;
            if (hl == "1") activationCount++;
            if (hc == "1") activationCount++;
            if (hr == "1") activationCount++;
            if (activationCount >= 2)
            {
                SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, Children.Count > 0 ? "1" : "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.Hc, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.Hr, "0");
            }

            activationCount = 0;
            if (vt == "1") activationCount++;
            if (vc == "1") activationCount++;
            if (vb == "1") activationCount++;
            if (activationCount >= 2)
            {
                SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, Children.Count > 0 ? "1" : "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.Vc, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.Vb, "0");
            }

            /*---------------------*/
            foreach (var child in Children)
            {
                var height = child.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
                var width = child.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);

                if (vt == "1")
                {
                    child.SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, "1");
                    child.SetPropertyValue(GroupNames.Alignment, PropertyNames.Vc, "0");
                    child.SetPropertyValue(GroupNames.Alignment, PropertyNames.Vb, "0");

                    child.SetPropertyValue(GroupNames.Transform, PropertyNames.Ve, "0");
                    child.SetPropertyValue(GroupNames.Transform, PropertyNames.Hve, "0");

                    var h = height != SizeValue.Expand.ToString() && height != SizeValue.Auto.ToString()
                        ? height
                        : SizeValue.Auto.ToString();
                    child.SetPropertyValue(GroupNames.Transform, PropertyNames.Height, h);
                }
                else if (vt == "0" && vc == "0" && vb == "0" && height != SizeValue.Expand.ToString())
                {
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, "1");
                }

                if (hl == "1")
                {
                    child.SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, "1");
                    child.SetPropertyValue(GroupNames.Alignment, PropertyNames.Hc, "0");
                    child.SetPropertyValue(GroupNames.Alignment, PropertyNames.Hr, "0");

                    child.SetPropertyValue(GroupNames.Transform, PropertyNames.He, "0");
                    child.SetPropertyValue(GroupNames.Transform, PropertyNames.Hve, "0");

                    var w = width != SizeValue.Expand.ToString() && width != SizeValue.Auto.ToString()
                        ? width
                        : SizeValue.Auto.ToString();
                    child.SetPropertyValue(GroupNames.Transform, PropertyNames.Width, w);
                }
                else if (hl == "0" && hc == "0" && hr == "0" && width != SizeValue.Expand.ToString())
                {
                    SetPropertyValue(GroupNames.Alignment, PropertyNames.Width, "1");
                }
            }
        }
    }
}