﻿#pragma checksum "C:\Users\Amirh\source\repos\WatchItAll\WatchItAll.UI\Pages\GenresPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5D3B1D0D37F4346ABD1B4147D87EFCAFAD7E3FA7F914C0B6D6E2376D334AC5AF"
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
    partial class GenresPage : 
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
            case 2: // Pages\GenresPage.xaml line 15
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element2 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element2).Click += this.BackButton_Click;
                }
                break;
            case 3: // Pages\GenresPage.xaml line 17
                {
                    this.CurrentGenre = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 4: // Pages\GenresPage.xaml line 68
                {
                    this.LoadingIndicator = (global::Windows.UI.Xaml.Controls.ProgressRing)(target);
                }
                break;
            case 5: // Pages\GenresPage.xaml line 69
                {
                    this.NoResultsTextBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 6: // Pages\GenresPage.xaml line 59
                {
                    this.GenreResultsList = (global::Windows.UI.Xaml.Controls.GridView)(target);
                    ((global::Windows.UI.Xaml.Controls.GridView)this.GenreResultsList).ItemClick += this.MovieList_Clicked;
                }
                break;
            case 8: // Pages\GenresPage.xaml line 45
                {
                    global::Windows.UI.Xaml.Controls.Button element8 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element8).Click += this.LeftArrow_Click;
                }
                break;
            case 9: // Pages\GenresPage.xaml line 49
                {
                    this.CurrentPage = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 10: // Pages\GenresPage.xaml line 51
                {
                    global::Windows.UI.Xaml.Controls.Button element10 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element10).Click += this.RightArrow_Click;
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

