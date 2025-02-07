using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ConceptorUI.ViewModels.Components;
using ConceptorUi.ViewModels.Container;
using ConceptorUi.ViewModels.Operations;
using UiDesigner.Models;

namespace ConceptorUI.ViewModels.Container
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
            this.SetPropertyVisibility(GroupNames.Global, PropertyNames.FilePicker, false);
            /* Content Alignment */
            this.SetGroupVisibility(GroupNames.Alignment);
            this.SetPropertyVisibility(GroupNames.Alignment, PropertyNames.SpaceBetween, false);
            this.SetPropertyVisibility(GroupNames.Alignment, PropertyNames.SpaceAround, false);
            this.SetPropertyVisibility(GroupNames.Alignment, PropertyNames.SpaceEvery, false);
            /* Self Alignment */
            /* Transform */
            this.SetGroupVisibility(GroupNames.Transform);
            /* Text */
            this.SetGroupVisibility(GroupNames.Text, false);
            /* Appearance */
            /* Shadow */
            this.SetGroupVisibility(GroupNames.Shadow);
        }

        protected override void LayoutConstraints(int id, bool isDeserialize = false, bool existExpaned = false)
        {
            Children[id].Parent = this;
            /* Global */
            Children[id].SetPropertyVisibility(GroupNames.Global, PropertyNames.MoveLeft, false);
            Children[id].SetPropertyVisibility(GroupNames.Global, PropertyNames.MoveRight, false);
            Children[id].SetPropertyVisibility(GroupNames.Global, PropertyNames.MoveTop, false);
            Children[id].SetPropertyVisibility(GroupNames.Global, PropertyNames.MoveBottom, false);
            //Children[id].this.SetPropertyVisibility(GroupNames.Global, PropertyNames.FilePicker, false);

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
                    this.SetPropertyValue(GroupNames.Alignment, horizontal, "1");
                    WhenAlignmentChanged(horizontal, "1");
                }
                else if (w != SizeValue.Expand.ToString())
                    this.SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, "1");
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
                    this.SetPropertyValue(GroupNames.Alignment, vertical, "1");
                    WhenAlignmentChanged(vertical, "1");
                }
                else if (h != SizeValue.Expand.ToString())
                    this.SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, "1");
            }
        }

        public override void WhenAlignmentChanged(PropertyNames propertyName, string value)
        {
            if (propertyName is PropertyNames.Hl or PropertyNames.Hc or PropertyNames.Hr)
            {
                this.SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, "0");
                this.SetPropertyValue(GroupNames.Alignment, PropertyNames.Hc, "0");
                this.SetPropertyValue(GroupNames.Alignment, PropertyNames.Hr, "0");
                this.SetPropertyValue(GroupNames.Alignment, propertyName, value);

                foreach (var child in Children)
                {
                    child.OnUpdated(GroupNames.SelfAlignment, propertyName, value, true);
                }
            }
            else if (propertyName is PropertyNames.Vt or PropertyNames.Vc or PropertyNames.Vb)
            {
                this.SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, "0");
                this.SetPropertyValue(GroupNames.Alignment, PropertyNames.Vc, "0");
                this.SetPropertyValue(GroupNames.Alignment, PropertyNames.Vb, "0");
                this.SetPropertyValue(GroupNames.Alignment, propertyName, value);

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
            this.SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, "0");
            this.SetPropertyValue(GroupNames.Alignment, PropertyNames.Hc, "0");
            this.SetPropertyValue(GroupNames.Alignment, PropertyNames.Hr, "0");

            this.SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, "0");
            this.SetPropertyValue(GroupNames.Alignment, PropertyNames.Vc, "0");
            this.SetPropertyValue(GroupNames.Alignment, PropertyNames.Vb, "0");

            if (k == -1) OnSelected();
        }

        protected override void WhenWidthChanged(string value)
        {
            if (Children.Count == 0 || !Children[0].Selected) return;

            this.SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, "0");
            this.SetPropertyValue(GroupNames.Alignment, PropertyNames.Hc, "0");
            this.SetPropertyValue(GroupNames.Alignment, PropertyNames.Hr, "0");

            this.SetPropertyValue(GroupNames.Alignment, PropertyNames.Hl, value != SizeValue.Expand.ToString() ? "1" : "0");
        }

        protected override void WhenHeightChanged(string value)
        {
            if (Children.Count == 0 || !Children[0].Selected) return;

            this.SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, "0");
            this.SetPropertyValue(GroupNames.Alignment, PropertyNames.Vc, "0");
            this.SetPropertyValue(GroupNames.Alignment, PropertyNames.Vb, "0");

            this.SetPropertyValue(GroupNames.Alignment, PropertyNames.Vt, value != SizeValue.Expand.ToString() ? "1" : "0");
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
                
                if(widthChild == SizeValue.Expand.ToString() && isHorizontal)
                    Alignment.SetHorizontalOnNull(this);
                else if(widthChild != SizeValue.Expand.ToString() && !isHorizontal)
                    Alignment.SetHorizontalValue(this, PropertyNames.Hl, "1");
                
                if(heightChild == SizeValue.Expand.ToString() && isVertical)
                    Alignment.SetVerticalOnNull(this);
                else if(heightChild != SizeValue.Expand.ToString() && !isVertical)
                    Alignment.SetVerticalValue(this, PropertyNames.Vt, "1");
            }
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
            ContainerRestoreProperties.RestoreProperties(this);
        }
        
        protected override void CheckVisibilities()
        {
            this.SetVisibilities();
        }
    }
}