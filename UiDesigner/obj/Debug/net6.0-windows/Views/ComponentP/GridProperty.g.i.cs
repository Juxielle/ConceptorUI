﻿#pragma checksum "..\..\..\..\..\Views\ComponentP\GridProperty.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9EB067DA29723C9F34837902A4AE2DA48FEB6500"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using ConceptorUI.Views.ComponentP;
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


namespace ConceptorUI.Views.ComponentP {
    
    
    /// <summary>
    /// GridProperty
    /// </summary>
    public partial class GridProperty : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 43 "..\..\..\..\..\Views\ComponentP\GridProperty.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CSelectedElement;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\..\..\Views\ComponentP\GridProperty.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BAdd;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\..\..\Views\ComponentP\GridProperty.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BMerge;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\..\..\Views\ComponentP\GridProperty.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BHideBorder;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\..\..\Views\ComponentP\GridProperty.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.PackIcon HideBorder;
        
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
            System.Uri resourceLocater = new System.Uri("/ConceptorUI;component/views/componentp/gridproperty.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\ComponentP\GridProperty.xaml"
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
            this.CSelectedElement = ((System.Windows.Controls.ComboBox)(target));
            
            #line 43 "..\..\..\..\..\Views\ComponentP\GridProperty.xaml"
            this.CSelectedElement.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.OnSelectedChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.BAdd = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\..\..\..\Views\ComponentP\GridProperty.xaml"
            this.BAdd.Click += new System.Windows.RoutedEventHandler(this.BtnClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.BMerge = ((System.Windows.Controls.Button)(target));
            
            #line 57 "..\..\..\..\..\Views\ComponentP\GridProperty.xaml"
            this.BMerge.Click += new System.Windows.RoutedEventHandler(this.BtnClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.BHideBorder = ((System.Windows.Controls.Button)(target));
            
            #line 62 "..\..\..\..\..\Views\ComponentP\GridProperty.xaml"
            this.BHideBorder.Click += new System.Windows.RoutedEventHandler(this.BtnClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.HideBorder = ((MaterialDesignThemes.Wpf.PackIcon)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

