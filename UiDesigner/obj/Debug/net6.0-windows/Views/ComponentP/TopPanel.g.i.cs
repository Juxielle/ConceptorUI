﻿#pragma checksum "..\..\..\..\..\Views\ComponentP\TopPanel.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "13183ADACEE601D64659FB6FB96BB752BF5944A2"
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
    /// TopPanel
    /// </summary>
    public partial class TopPanel : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\..\..\Views\ComponentP\TopPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.PackIcon MaxIcon;
        
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
            System.Uri resourceLocater = new System.Uri("/ConceptorUI;component/views/componentp/toppanel.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\ComponentP\TopPanel.xaml"
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
            
            #line 11 "..\..\..\..\..\Views\ComponentP\TopPanel.xaml"
            ((System.Windows.Controls.DockPanel)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.PnMouseUp);
            
            #line default
            #line hidden
            
            #line 11 "..\..\..\..\..\Views\ComponentP\TopPanel.xaml"
            ((System.Windows.Controls.DockPanel)(target)).MouseMove += new System.Windows.Input.MouseEventHandler(this.PnMouseMove);
            
            #line default
            #line hidden
            
            #line 11 "..\..\..\..\..\Views\ComponentP\TopPanel.xaml"
            ((System.Windows.Controls.DockPanel)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.PnMouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 13 "..\..\..\..\..\Views\ComponentP\TopPanel.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnScreenClick);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 17 "..\..\..\..\..\Views\ComponentP\TopPanel.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnScreenClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.MaxIcon = ((MaterialDesignThemes.Wpf.PackIcon)(target));
            return;
            case 5:
            
            #line 21 "..\..\..\..\..\Views\ComponentP\TopPanel.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BtnScreenClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

