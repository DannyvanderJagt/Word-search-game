﻿

#pragma checksum "C:\Users\Danny van der Jagt\Documents\GitHub\Word-search-game\Word-search-game\Word-search-game.Windows\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "04FC6CA59ED8B62EA6690141388F3508"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Word_search_game
{
    partial class MainPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 52 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.Levels_Tapped;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 53 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.Statistics_Tapped;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 54 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.Players_Tapped;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


