﻿#pragma checksum "..\..\..\..\..\View\OnContextMenus.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C1FBCF2BBC72884E2C65DA522AD6E4A951E3E947"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Note.View;
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


namespace Note.View {
    
    
    /// <summary>
    /// OnContextMenus
    /// </summary>
    public partial class OnContextMenus : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 96 "..\..\..\..\..\View\OnContextMenus.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox listOfColumns;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\..\..\View\OnContextMenus.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox newCaptionTB;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\..\..\..\View\OnContextMenus.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button renameBtn;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.10.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Note;component/view/oncontextmenus.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\OnContextMenus.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.10.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.listOfColumns = ((System.Windows.Controls.ComboBox)(target));
            
            #line 98 "..\..\..\..\..\View\OnContextMenus.xaml"
            this.listOfColumns.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.listOfColumns_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.newCaptionTB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.renameBtn = ((System.Windows.Controls.Button)(target));
            
            #line 101 "..\..\..\..\..\View\OnContextMenus.xaml"
            this.renameBtn.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

