﻿#pragma checksum "..\..\..\..\..\Views\Component\ItemReport.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5221395F7322339522B6AC3A68676A9D000FE0C5"
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


namespace UiDesigner.Views.Component {
    
    
    /// <summary>
    /// ItemReport
    /// </summary>
    public partial class ItemReport : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\..\..\Views\Component\ItemReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border Content;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\..\..\Views\Component\ItemReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Number;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\..\Views\Component\ItemReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.PackIcon IconReport;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\..\Views\Component\ItemReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Name;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\..\Views\Component\ItemReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Type;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\..\Views\Component\ItemReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock DateLabel;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\..\Views\Component\ItemReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Date;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\..\Views\Component\ItemReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ContentPen;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\..\Views\Component\ItemReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.PackIcon Pen;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\..\..\Views\Component\ItemReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ContentTrash;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\..\Views\Component\ItemReport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.PackIcon Trash;
        
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
            System.Uri resourceLocater = new System.Uri("/UiDesigner;component/views/component/itemreport.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\Component\ItemReport.xaml"
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
            this.Content = ((System.Windows.Controls.Border)(target));
            
            #line 10 "..\..\..\..\..\Views\Component\ItemReport.xaml"
            this.Content.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.OnMouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Number = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.IconReport = ((MaterialDesignThemes.Wpf.PackIcon)(target));
            return;
            case 4:
            this.Name = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.Type = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.DateLabel = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.Date = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.ContentPen = ((System.Windows.Controls.Border)(target));
            return;
            case 9:
            this.Pen = ((MaterialDesignThemes.Wpf.PackIcon)(target));
            return;
            case 10:
            this.ContentTrash = ((System.Windows.Controls.Border)(target));
            return;
            case 11:
            this.Trash = ((MaterialDesignThemes.Wpf.PackIcon)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
