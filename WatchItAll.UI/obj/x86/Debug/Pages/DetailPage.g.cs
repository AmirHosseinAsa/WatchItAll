﻿#pragma checksum "C:\Users\Amirh\source\repos\WatchItAll\WatchItAll.UI\Pages\DetailPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "446F9E28E4FAB792CA8A6CFC2CB8D7A37B7971C3FEA91E50263B44041F40CCA7"
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
    partial class DetailPage : 
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
            case 2: // Pages\DetailPage.xaml line 16
                {
                    global::Windows.UI.Xaml.Controls.AppBarButton element2 = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)element2).Click += this.BackButton_Click;
                }
                break;
            case 3: // Pages\DetailPage.xaml line 21
                {
                    global::Windows.UI.Xaml.Controls.Grid element3 = (global::Windows.UI.Xaml.Controls.Grid)(target);
                    ((global::Windows.UI.Xaml.Controls.Grid)element3).SizeChanged += this.Grid_SizeChanged;
                }
                break;
            case 4: // Pages\DetailPage.xaml line 23
                {
                    this.LoadingIndicator = (global::Windows.UI.Xaml.Controls.ProgressRing)(target);
                }
                break;
            case 5: // Pages\DetailPage.xaml line 25
                {
                    this.GridShow = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 6: // Pages\DetailPage.xaml line 447
                {
                    this.PageLoad = (global::Windows.UI.Xaml.Controls.Border)(target);
                }
                break;
            case 7: // Pages\DetailPage.xaml line 329
                {
                    this.EpisodesPanel = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 8: // Pages\DetailPage.xaml line 354
                {
                    this.ServersPanel = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 9: // Pages\DetailPage.xaml line 402
                {
                    this.EPStack = (global::Windows.UI.Xaml.Controls.ScrollViewer)(target);
                }
                break;
            case 10: // Pages\DetailPage.xaml line 404
                {
                    this.EpisodesSource = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    ((global::Windows.UI.Xaml.Controls.ListView)this.EpisodesSource).ItemClick += this.Episode_ItemClick;
                }
                break;
            case 11: // Pages\DetailPage.xaml line 436
                {
                    this.ComingSoonTextBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 13: // Pages\DetailPage.xaml line 395
                {
                    this.SeasonsComboBox = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)this.SeasonsComboBox).SelectionChanged += this.SeasonsComboBox_SelectionChanged;
                }
                break;
            case 14: // Pages\DetailPage.xaml line 362
                {
                    global::Windows.UI.Xaml.Controls.ListView element14 = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    ((global::Windows.UI.Xaml.Controls.ListView)element14).ItemClick += this.Server_ItemClick;
                }
                break;
            case 16: // Pages\DetailPage.xaml line 339
                {
                    this.MenuTitle = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 17: // Pages\DetailPage.xaml line 306
                {
                    this.MOVIEDescription = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 18: // Pages\DetailPage.xaml line 293
                {
                    this.MOVIETitle = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 19: // Pages\DetailPage.xaml line 76
                {
                    this.FavoriteButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.FavoriteButton).Click += this.FavoriteButton_Click;
                }
                break;
            case 20: // Pages\DetailPage.xaml line 111
                {
                    global::Windows.UI.Xaml.Controls.Button element20 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element20).Click += this.PlayTrailerButtonClicked;
                }
                break;
            case 21: // Pages\DetailPage.xaml line 216
                {
                    this.FormatPanel = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 22: // Pages\DetailPage.xaml line 229
                {
                    global::Windows.UI.Xaml.Controls.ListView element22 = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    ((global::Windows.UI.Xaml.Controls.ListView)element22).ItemClick += this.Cast_ItemClick;
                }
                break;
            case 24: // Pages\DetailPage.xaml line 200
                {
                    global::Windows.UI.Xaml.Controls.ListView element24 = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    ((global::Windows.UI.Xaml.Controls.ListView)element24).ItemClick += this.Genre_ItemClick;
                }
                break;
            case 26: // Pages\DetailPage.xaml line 89
                {
                    this.favSymbol = (global::Windows.UI.Xaml.Controls.SymbolIcon)(target);
                }
                break;
            case 27: // Pages\DetailPage.xaml line 97
                {
                    this.favText = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 28: // Pages\DetailPage.xaml line 66
                {
                    this.MOVIEImg = (global::Windows.UI.Xaml.Media.ImageBrush)(target);
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

