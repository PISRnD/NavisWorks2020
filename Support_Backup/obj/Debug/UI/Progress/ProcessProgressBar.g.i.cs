#pragma checksum "..\..\..\..\UI\Progress\ProcessProgressBar.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4D08A3DB919DC827B81614494962E49DC1D30A0E57606370D347A6CF84AF54A3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PinnacleDockRevit.Support;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace PinnacleDockRevit.Support {
    
    
    /// <summary>
    /// ProcessProgressBar
    /// </summary>
    public partial class ProcessProgressBar : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 30 "..\..\..\..\UI\Progress\ProcessProgressBar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar processProgressBar;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\UI\Progress\ProcessProgressBar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtProgressdata;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\UI\Progress\ProcessProgressBar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancelProcess;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PiShuttringSupport;component/ui/progress/processprogressbar.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UI\Progress\ProcessProgressBar.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 11 "..\..\..\..\UI\Progress\ProcessProgressBar.xaml"
            ((PinnacleDockRevit.Support.ProcessProgressBar)(target)).Loaded += new System.Windows.RoutedEventHandler(this.ProcessProgressBar_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.processProgressBar = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 3:
            this.txtProgressdata = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.btnCancelProcess = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\..\UI\Progress\ProcessProgressBar.xaml"
            this.btnCancelProcess.Click += new System.Windows.RoutedEventHandler(this.btnCancelProcess_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

