﻿#pragma checksum "..\..\..\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "123BD00324F4F3AC10546A3D466D487F8B674387"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ConceptorUI;
using ConceptorUI.Views.Component;
using ConceptorUI.Views.PropertyPanel;
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


namespace ConceptorUI {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 63 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Content;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal UiDesigner.Views.Component.LeftPanel ComponentButtons;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ConceptorUI.Views.Component.PanelProperty RightPanel;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ConceptorUI.Views.PropertyPanel.ComponentList ComponentList;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid ContentPages;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer Pages;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ConceptorUI.Views.Component.PageView PageView;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal UiDesigner.Views.Component.ComponentPage PageComponent;
        
        #line default
        #line hidden
        
        
        #line 119 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal UiDesigner.Views.Component.LeftCompPanel CompPage;
        
        #line default
        #line hidden
        
        
        #line 135 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar Loading;
        
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
            System.Uri resourceLocater = new System.Uri("/UiDesigner;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.Content = ((System.Windows.Controls.Grid)(target));
            
            #line 65 "..\..\..\MainWindow.xaml"
            this.Content.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.OnMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ComponentButtons = ((UiDesigner.Views.Component.LeftPanel)(target));
            return;
            case 3:
            this.RightPanel = ((ConceptorUI.Views.Component.PanelProperty)(target));
            return;
            case 4:
            this.ComponentList = ((ConceptorUI.Views.PropertyPanel.ComponentList)(target));
            return;
            case 5:
            this.ContentPages = ((System.Windows.Controls.Grid)(target));
            
            #line 97 "..\..\..\MainWindow.xaml"
            this.ContentPages.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.OnMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Pages = ((System.Windows.Controls.ScrollViewer)(target));
            
            #line 101 "..\..\..\MainWindow.xaml"
            this.Pages.PreviewMouseWheel += new System.Windows.Input.MouseWheelEventHandler(this.OnMouseWheel);
            
            #line default
            #line hidden
            
            #line 102 "..\..\..\MainWindow.xaml"
            this.Pages.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.OnMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.PageView = ((ConceptorUI.Views.Component.PageView)(target));
            return;
            case 8:
            this.PageComponent = ((UiDesigner.Views.Component.ComponentPage)(target));
            return;
            case 9:
            this.CompPage = ((UiDesigner.Views.Component.LeftCompPanel)(target));
            return;
            case 10:
            this.Loading = ((System.Windows.Controls.ProgressBar)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

