﻿#pragma checksum "C:\Users\Amirh\source\repos\WatchItAll\WatchItAll.UI\Pages\CastMoviesPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "37132A3760EF7825807856C6B853ED79EE079B72067F02FEAEF5766B42ABB57E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WatchItAll.UI.Pages
{
    partial class CastMoviesPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 0.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Pages\CastMoviesPage.xaml line 15
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element2 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element2).Click += this.BackButton_Click;
                }
                break;
            case 3: // Pages\CastMoviesPage.xaml line 17
                {
                    this.CurrentCast = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 4: // Pages\CastMoviesPage.xaml line 38
                {
                    this.PaggingContainer = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 5: // Pages\CastMoviesPage.xaml line 56
                {
                    this.CastResContainer = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 6: // Pages\CastMoviesPage.xaml line 68
                {
                    this.LoadingIndicator = (global::Windows.UI.Xaml.Controls.ProgressRing)(target);
                }
                break;
            case 7: // Pages\CastMoviesPage.xaml line 69
                {
                    this.NoResultsTextBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 8: // Pages\CastMoviesPage.xaml line 59
                {
                    this.CastResultsList = (global::Windows.UI.Xaml.Controls.GridView)(target);
                    ((global::Windows.UI.Xaml.Controls.GridView)this.CastResultsList).ItemClick += this.MovieList_Clicked;
                }
                break;
            case 10: // Pages\CastMoviesPage.xaml line 45
                {
                    global::Windows.UI.Xaml.Controls.Button element10 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element10).Click += this.LeftArrow_Click;
                }
                break;
            case 11: // Pages\CastMoviesPage.xaml line 49
                {
                    this.CurrentPage = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 12: // Pages\CastMoviesPage.xaml line 51
                {
                    global::Windows.UI.Xaml.Controls.Button element12 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element12).Click += this.RightArrow_Click;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 0.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

