﻿#pragma checksum "..\..\..\..\..\Views\Component\RightPanel.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7B7C2936462104F2730D6C87788E859B3F3621F5"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
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


namespace ConceptorUI.Views.Component {
    
    
    /// <summary>
    /// RightPanel
    /// </summary>
    public partial class RightPanel : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\..\..\..\Views\Component\RightPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border BProps;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\..\Views\Component\RightPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.PackIcon Props;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\..\Views\Component\RightPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ComponentB;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\..\..\Views\Component\RightPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.PackIcon ComponentButton;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\..\Views\Component\RightPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border BSetting;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\..\..\Views\Component\RightPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.PackIcon Setting;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/UiDesigner;component/views/component/rightpanel.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\Component\RightPanel.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.BProps = ((System.Windows.Controls.Border)(target));
            
            #line 18 "..\..\..\..\..\Views\Component\RightPanel.xaml"
            this.BProps.MouseEnter += new System.Windows.Input.MouseEventHandler(this.BtnMouseEnter);
            
            #line default
            #line hidden
            
            #line 19 "..\..\..\..\..\Views\Component\RightPanel.xaml"
            this.BProps.MouseLeave += new System.Windows.Input.MouseEventHandler(this.BtnMouseLeave);
            
            #line default
            #line hidden
            
            #line 23 "..\..\..\..\..\Views\Component\RightPanel.xaml"
            this.BProps.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.BtnClick);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Props = ((MaterialDesignThemes.Wpf.PackIcon)(target));
            return;
            case 3:
            this.ComponentB = ((System.Windows.Controls.Border)(target));
            
            #line 39 "..\..\..\..\..\Views\Component\RightPanel.xaml"
            this.ComponentB.MouseEnter += new System.Windows.Input.MouseEventHandler(this.BtnMouseEnter);
            
            #line default
            #line hidden
            
            #line 40 "..\..\..\..\..\Views\Component\RightPanel.xaml"
            this.ComponentB.MouseLeave += new System.Windows.Input.MouseEventHandler(this.BtnMouseLeave);
            
            #line default
            #line hidden
            
            #line 44 "..\..\..\..\..\Views\Component\RightPanel.xaml"
            this.ComponentB.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.BtnClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ComponentButton = ((MaterialDesignThemes.Wpf.PackIcon)(target));
            return;
            case 5:
            this.BSetting = ((System.Windows.Controls.Border)(target));
            
            #line 60 "..\..\..\..\..\Views\Component\RightPanel.xaml"
            this.BSetting.MouseEnter += new System.Windows.Input.MouseEventHandler(this.BtnMouseEnter);
            
            #line default
            #line hidden
            
            #line 61 "..\..\..\..\..\Views\Component\RightPanel.xaml"
            this.BSetting.MouseLeave += new System.Windows.Input.MouseEventHandler(this.BtnMouseLeave);
            
            #line default
            #line hidden
            
            #line 65 "..\..\..\..\..\Views\Component\RightPanel.xaml"
            this.BSetting.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.BtnClick);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Setting = ((MaterialDesignThemes.Wpf.PackIcon)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

