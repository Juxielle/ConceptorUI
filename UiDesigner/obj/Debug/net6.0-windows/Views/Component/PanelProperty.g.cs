﻿#pragma checksum "..\..\..\..\..\Views\Component\PanelProperty.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0A269160C36F3400CA5DCEE5CABAE342DE74516F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using UiDesigner.Views.Component;


namespace UiDesigner.Views.Component {
    
    
    /// <summary>
    /// PanelProperty
    /// </summary>
    public partial class PanelProperty : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 42 "..\..\..\..\..\Views\Component\PanelProperty.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BForm;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\..\..\Views\Component\PanelProperty.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BEntity;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\..\..\Views\Component\PanelProperty.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Property;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\..\..\Views\Component\PanelProperty.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel SForm;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\..\..\Views\Component\PanelProperty.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid SEntity;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\..\..\Views\Component\PanelProperty.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal UiDesigner.Views.Component.PanelStructuralView StructuralView;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/UiDesigner;component/views/component/panelproperty.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\Component\PanelProperty.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.BForm = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\..\..\..\Views\Component\PanelProperty.xaml"
            this.BForm.Click += new System.Windows.RoutedEventHandler(this.BtnClick);
            
            #line default
            #line hidden
            return;
            case 2:
            this.BEntity = ((System.Windows.Controls.Button)(target));
            
            #line 53 "..\..\..\..\..\Views\Component\PanelProperty.xaml"
            this.BEntity.Click += new System.Windows.RoutedEventHandler(this.BtnClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Property = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.SForm = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 5:
            this.SEntity = ((System.Windows.Controls.Grid)(target));
            return;
            case 6:
            this.StructuralView = ((UiDesigner.Views.Component.PanelStructuralView)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

