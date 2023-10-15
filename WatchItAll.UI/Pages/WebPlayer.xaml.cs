using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchItAll.Core.Interfaces;
using WatchItAll.Core.Services;
using WatchItAll.DataLayer.Constants;
using WatchItAll.DataLayer.Entities;
using Windows.System.Profile;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace WatchItAll.UI.Pages
{
    public sealed partial class WebPlayer : Page
    {
        IMovieService _movieService = new MovieService();
        EpisodeItemAndServersMovieItem selectedMV;

        public WebPlayer()
        {
            this.InitializeComponent();
        }

        #region WEBVIEW
        string embeddedIframeUrl = "";
        bool firstNav = true;
        List<ServerItem> servers = new List<ServerItem>();
        int indexTvServer = 0;
        private async void InitializeWebView()
        {
            try
            {
                if (selectedMV.MovieStreamURls != null)
                {
                    EpisodeTextBlock.Text = $"{selectedMV.MovieItem.Name}";
                    embeddedIframeUrl = Staticants.baseUrl + selectedMV.MovieItem.Url.Replace("/movie/", "watch-movie/") + "." + selectedMV.MovieStreamURls.ServerId;
                    ChangeServerButton.Visibility = Visibility.Collapsed;
                }
                else
                {
                    EpisodeTextBlock.Text = $"{selectedMV.SelectedEpisode.EpisodeNumber}: {selectedMV.SelectedEpisode.Title}";
                    servers = await _movieService.GetServerIdsAsync(selectedMV.SelectedEpisode.ServerId);
                    embeddedIframeUrl = Staticants.baseUrl + selectedMV.SelectedEpisode.MovieURl.Replace("/tv/", "watch-tv/") + "." + servers[indexTvServer].ServerURL;
                    ChangeServerButton.Visibility = Visibility.Visible;
                }

                WebView2CN.CanGoBack = false;
                WebView2CN.CanGoForward = false;
                WebView2CN.Source = new Uri(embeddedIframeUrl);
                await WebView2CN.EnsureCoreWebView2Async();
                WebView2CN.CoreWebView2.NewWindowRequested += CoreWebView2_NewWindowRequested;
                WebView2CN.NavigationCompleted += WebView2CN_NavigationCompleted;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CoreWebView2_NewWindowRequested(CoreWebView2 sender, CoreWebView2NewWindowRequestedEventArgs args)
        {
            args.Handled = true;
        }


        private async void WebView2CN_NavigationCompleted(WebView2 sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs args)
        {
            if (firstNav)
            {
                await RunScriptAsync("window.location.href = document.getElementById('iframe-embed').src");
            }
            else
            {
                await RunScriptAsync("setInterval(function() {document.querySelectorAll('a[rel=\"nofollow\"]').forEach(function(element) { element.parentNode.parentNode.remove(); });}, 800);");
                await Task.Delay(1000);
                await RunScriptAsync("document.getElementsByClassName('jw-icon jw-icon-display jw-button-color jw-reset')[0].click();");
                await RunScriptAsync("document.getElementsByClassName('jw-icon jw-icon-inline jw-button-color jw-reset jw-icon-fullscreen')[1].click();");
                LoadingIndicator.Visibility = Visibility.Collapsed;
                WebView2CN.Visibility = Visibility.Visible;
            }
            firstNav = false;
        }

        private async Task RunScriptAsync(string script)
        {
            try
            {
                await WebView2CN.ExecuteScriptAsync(script);
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region Navigation

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                bool isXbox = AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Xbox";
                if (!isXbox)
                    ApplicationView.GetForCurrentView().TryEnterFullScreenMode();
                LoadingIndicator.Visibility = Visibility.Visible;
                WebView2CN.Visibility = Visibility.Collapsed;
                selectedMV = (EpisodeItemAndServersMovieItem)e.Parameter;
                InitializeWebView();
            }
            catch (System.Exception)
            {
            }
            base.OnNavigatedTo(e);

        }

        private async void BackButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                }
            }
            catch (Exception)
            {
            }
        }

        protected override async void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            WebView2CN.Close();
            bool isXbox = AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Xbox";
            if (!isXbox)
                ApplicationView.GetForCurrentView().ExitFullScreenMode();
        }
        #endregion

        private async void ChangeServer_Click(object sender, RoutedEventArgs e)
        {
            if (servers.Count>1)
            {
                if (indexTvServer+1 ==servers.Count())
                    indexTvServer = 0;
                else
                    indexTvServer ++;

                LoadingIndicator.Visibility = Visibility.Visible;
                WebView2CN.Visibility = Visibility.Collapsed;

                firstNav = true;
                embeddedIframeUrl = Staticants.baseUrl + selectedMV.SelectedEpisode.MovieURl.Replace("/tv/", "watch-tv/") + "." + servers[indexTvServer].ServerURL;
                WebView2CN.Source = new Uri(embeddedIframeUrl);
            }
        }
    }
}
