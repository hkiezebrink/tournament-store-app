﻿

#pragma checksum "C:\Users\NATHAN\WindowsApps\Tournament\Tournament\Views\Fixtures.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C800587112F333BCC0DA8A3E9CF8CA8C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tournament.Views
{
    partial class Fixtures : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 52 "..\..\..\Views\Fixtures.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).GotFocus += this.TournamentName_GotFocus;
                 #line default
                 #line hidden
                #line 52 "..\..\..\Views\Fixtures.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).LostFocus += this.TournamentName_LostFocus;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 53 "..\..\..\Views\Fixtures.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Get_Fixtures;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 55 "..\..\..\Views\Fixtures.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).GotFocus += this.Players_GotFocus;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


