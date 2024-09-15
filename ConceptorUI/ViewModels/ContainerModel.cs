using System;
using ConceptorUI.Models;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


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

            if (allowConstraints) return;
            SelfConstraints();
            OnInitialize();
        }

        protected override void WhenTextChanged(string propertyName, string value)
        {
        }

        protected override void InitChildContent()
        {
        }

        protected override void AddIntoChildContent(FrameworkElement child)
        {
            (Content.Child as Border)!.Child ??= child;
            
            if (Children[0].GetType().Name == nameof(TextSingleModel))
            {
                Console.WriteLine($@"SelectContent: {(Content.Child as Border)!.Child.GetType()}");
                
                Console.WriteLine($@"Content: {((Content.Child as Border)!.Child as Border)!.Child.GetType()}");

                Console.WriteLine($@"Text: {(((Content.Child as Border)!.Child as Border)!.Child as Border)!.Child.GetType()}");
                
                // (Content.Child as Border)!.Child = new Border
                // {
                //     Background = Brushes.Yellow,
                //     HorizontalAlignment = HorizontalAlignment.Center,
                //     VerticalAlignment = VerticalAlignment.Center,
                //     Child = new Border { Child = new TextBlock { Text = "ENREGISTRER" } }
                // };
            }
        }

        protected override bool AllowExpanded(bool isWidth = true)
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
            Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.ROT, false);
            Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.X, false);
            Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.Y, false);
            Children[id].SetPropertyVisibility(GroupNames.Transform, PropertyNames.Stretch, false);

            /* Appearance */
            /* Shadow */

            Children[id].OnInitialize();

            var w = Children[id].GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
            if (w != SizeValue.Expand.ToString() &&
                Children[id].IsNullAlignment(GroupNames.SelfAlignment, "Horizontal"))
                OnUpdated(GroupNames.Alignment, PropertyNames.HL, "1", true);
            else
            {
                var hc = Children[id].GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.HC);
                var hr = Children[id].GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.HR);
                var hl = Children[id].GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.HL);
                if (hl == "1" || hc == "1" || hr == "1")
                {
                    var horizontal = hr == "1" ? PropertyNames.HR : (hc == "1" ? PropertyNames.HC : PropertyNames.HL);
                    SetPropertyValue(GroupNames.Alignment, horizontal, "1");
                }
            }

            var h = Children[id].GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
            if (h != SizeValue.Expand.ToString() && Children[id].IsNullAlignment(GroupNames.SelfAlignment, "Vertical"))
                OnUpdated(GroupNames.Alignment, PropertyNames.VT, "1", true);
            else
            {
                var vc = Children[id].GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.VC);
                var vb = Children[id].GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.VB);
                var vt = Children[id].GetGroupProperties(GroupNames.SelfAlignment).GetValue(PropertyNames.VT);
                
                if (vt != "1" && vc != "1" && vb != "1") return;
                var vertical = vb == "1" ? PropertyNames.VB : (vc == "1" ? PropertyNames.VC : PropertyNames.VT);
                SetPropertyValue(GroupNames.Alignment, vertical, "1");
            }
        }

        protected override void WhenAlignmentChanged(PropertyNames propertyName, string value)
        {
            if (propertyName is PropertyNames.HL or PropertyNames.HC or PropertyNames.HR)
            {
                SetPropertyValue(GroupNames.Alignment, PropertyNames.HL, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.HC, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.HR, "0");
                SetPropertyValue(GroupNames.Alignment, propertyName, value);

                var alignment = propertyName switch
                {
                    PropertyNames.HL => PropertyNames.HL,
                    PropertyNames.HC => PropertyNames.HC,
                    _ => PropertyNames.HR
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
                    else
                    {
                        child.OnUpdated(GroupNames.SelfAlignment, alignment, value, true);
                    }
                }
            }
            else if (propertyName is PropertyNames.VT or PropertyNames.VC or PropertyNames.VB)
            {
                SetPropertyValue(GroupNames.Alignment, PropertyNames.VT, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.VC, "0");
                SetPropertyValue(GroupNames.Alignment, PropertyNames.VB, "0");
                SetPropertyValue(GroupNames.Alignment, propertyName, value);

                var alignment = propertyName switch
                {
                    PropertyNames.VT => PropertyNames.VT,
                    PropertyNames.VC => PropertyNames.VC,
                    _ => PropertyNames.VB
                };

                foreach (var child in Children)
                {
                    var h = child.GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
                    if (value == "1")
                    {
                        child.OnUpdated(GroupNames.SelfAlignment, alignment, value, true);
                        if (h == SizeValue.Expand.ToString())
                            child.OnUpdated(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString(),
                                true);
                    }
                    else
                    {
                        child.OnUpdated(GroupNames.SelfAlignment, alignment, value, true);
                    }
                }
            }
        }

        protected override void Delete()
        {
            if (Children.Count == 0 || !Children[0].Selected) return;
            
            Children.RemoveAt(0);
            (Content.Child as Border)!.Child = null;
            
            SetPropertyValue(GroupNames.Alignment, PropertyNames.HL, "0");
            SetPropertyValue(GroupNames.Alignment, PropertyNames.HC, "0");
            SetPropertyValue(GroupNames.Alignment, PropertyNames.HR, "0");
            
            SetPropertyValue(GroupNames.Alignment, PropertyNames.VT, "0");
            SetPropertyValue(GroupNames.Alignment, PropertyNames.VC, "0");
            SetPropertyValue(GroupNames.Alignment, PropertyNames.VB, "0");
            
            OnSelected();
        }

        protected override void WhenWidthChanged(string value)
        {
            if ((Children.Count > 0 && Children[0].Selected) || value != SizeValue.Expand.ToString()) return;
            OnUpdated(GroupNames.Alignment, PropertyNames.HL, "0", true);
        }

        protected override void WhenHeightChanged(string value)
        {
            if ((Children.Count > 0 && Children[0].Selected) || value != SizeValue.Expand.ToString()) return;
            OnUpdated(GroupNames.Alignment, PropertyNames.VT, "0", true);
        }

        protected override void ContinueToUpdate(GroupNames groupName, PropertyNames propertyName, string value)
        {
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
    }
}