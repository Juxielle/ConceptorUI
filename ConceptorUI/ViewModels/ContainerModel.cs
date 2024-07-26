using ConceptorUI.Models;
using ConceptorUI.Views.ComponentP;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using System.Globalization;
using System.Windows.Media.Effects;
using ConceptorUI.Utils;


namespace ConceptorUI.ViewModels
{
    internal class ContainerModel : Component
    {
        public ContainerModel()
        {
            Name = ComponentList.Container;
        }
        
        protected override void LayoutConstraints(int id, bool isDeserialize = false, bool existExpand = false)
        {
            #region Contraintes de mise en page des enfants
            Children[0].Parent = this;
            
            Children[0].SetGroupVisibility(GroupNames.SelfAlignment, false);
            Children[0].OnInitialize();

            var hl = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.HL);
            var hc = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.HC);
            var hr = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.HR);

            var vt = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.VT);
            var vc = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.VC);
            var vb = GetGroupProperties(GroupNames.Alignment).GetValue(PropertyNames.VB);
            
            var w = GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
            var h = GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
            
            if(hl == "0" && hc == "0" && hr == "0" && w != SizeValue.Expand.ToString())
                OnUpdated(GroupNames.Alignment, PropertyNames.HL, "1", true);
            if (vt == "0" && vc == "0" && vb == "0" && h != SizeValue.Expand.ToString())
                OnUpdated(GroupNames.Alignment, PropertyNames.VT, "1", true);

            if (w == SizeValue.Expand.ToString())
            {
                OnUpdated(GroupNames.Alignment, PropertyNames.HL, "0", true);
                return;
            }
            if (h == SizeValue.Expand.ToString())
            {
                OnUpdated(GroupNames.Alignment, PropertyNames.VT, "0", true);
                return;
            }
            
            Children[0].OnUpdated(GroupNames.SelfAlignment, PropertyNames.HL, hl, true);
            Children[0].OnUpdated(GroupNames.SelfAlignment, PropertyNames.HC, hc, true);
            Children[0].OnUpdated(GroupNames.SelfAlignment, PropertyNames.HR, hr, true);

            Children[0].OnUpdated(GroupNames.SelfAlignment, PropertyNames.VT, vt, true);
            Children[0].OnUpdated(GroupNames.SelfAlignment, PropertyNames.VC, vc, true);
            Children[0].OnUpdated(GroupNames.SelfAlignment, PropertyNames.VB, vb, true);
            #endregion
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
                            child.OnUpdated(GroupNames.Transform, PropertyNames.Height, SizeValue.Auto.ToString(), true);
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
            Children.RemoveAt(0);
            ChildrenContent.Children.RemoveAt(0);

            SetPropertyValue(GroupNames.Alignment, PropertyNames.HL, "0");
            SetPropertyValue(GroupNames.Alignment, PropertyNames.HC, "0");
            SetPropertyValue(GroupNames.Alignment, PropertyNames.HR, "0");

            SetPropertyValue(GroupNames.Alignment, PropertyNames.VT, "0");
            SetPropertyValue(GroupNames.Alignment, PropertyNames.VC, "0");
            SetPropertyValue(GroupNames.Alignment, PropertyNames.VB, "0");

            OnSelected();
        }
        
        protected override void WhenWidthChanged()
        {
            var value = GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Width);
            if ((Children.Count > 0 && Children[0].Selected) || value != SizeValue.Expand.ToString()) return;
            OnUpdated(GroupNames.Alignment, PropertyNames.HL, "0", true);
        }
        
        protected override void WhenHeightChanged()
        {
            var value = GetGroupProperties(GroupNames.Transform).GetValue(PropertyNames.Height);
            if ((Children.Count > 0 && Children[0].Selected) || value != SizeValue.Expand.ToString()) return;
            OnUpdated(GroupNames.Alignment, PropertyNames.VT, "0", true);
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
