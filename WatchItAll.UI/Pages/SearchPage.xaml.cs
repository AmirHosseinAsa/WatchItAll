using System;
using WatchItAll.Core.Interfaces;
using WatchItAll.Core.Services;
using WatchItAll.DataLayer.Constants;
using WatchItAll.DataLayer.Entities;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Navigation;

namespace WatchItAll.UI.Pages
{
    public sealed partial class SearchPage : Page
    {
        private DispatcherTimer searchTimer;
        IMovieService _movieService = new MovieService();

        public SearchPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            searchTimer = new DispatcherTimer();
            searchTimer.Interval = TimeSpan.FromMilliseconds(1400);
            searchTimer.Tick += SearchTimer_Tick;
        }

        #region Animation
        public void MovieListView_Loaded(object sender, RoutedEventArgs e)
        {
            var MovieCardListView = sender as ListView;
            if (MovieCardListView != null)
            {
                var compositor = ElementCompositionPreview.GetElementVisual(MovieCardListView).Compositor;
                var animation = compositor.CreateScalarKeyFrameAnimation();
                animation.InsertKeyFrame(0, 0);
                animation.InsertKeyFrame(1, 1);
                animation.Duration = TimeSpan.FromMilliseconds(1000);

                var visual = ElementCompositionPreview.GetElementVisual(MovieCardListView);
                visual.Opacity = 0;
                visual.StartAnimation("Opacity", animation);
            }
        }

        #endregion

        #region Navigation And Button Click

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            DoActiveRecentSearch();
            base.OnNavigatedTo(e);
        }

        private void MovieList_Clicked(object sender, ItemClickEventArgs e)
        {
            MovieItem selectedMovie = (MovieItem)e.ClickedItem;
            Frame.Navigate(typeof(DetailPage), selectedMovie);
        }
        private void SearchItem_Clicked(object sender, ItemClickEventArgs e)
        {
            var search = (Search)e.ClickedItem;
            SearchBox.Text = search.Text;
            SearchProgressRing.Visibility = Visibility.Visible;
            RecentSearchesList.Visibility = Visibility.Collapsed;
        }
        private async void DeleteButton_Clicked(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                await _movieService.DeleteSearchAsync(clickedButton.Tag.ToString());
                DoActiveRecentSearch();
            }
        }
        #endregion

        #region Search

        private void SearchTimer_Tick(object sender, object e)
        {
            if (!String.IsNullOrEmpty(SearchBox.Text))
                DoSearch(SearchBox.Text);
            else
            {
                SearchResultsList.ItemsSource = null;
                NoResultsTextBlock.Visibility = Visibility.Collapsed;
                DoActiveRecentSearch();
            }
            searchTimer.Stop();
        }

        async void DoSearch(string text)
        {
            SearchProgressRing.IsActive = true;
            SearchProgressRing.Visibility = Visibility.Visible;
            RecentSearchesList.Visibility = Visibility.Collapsed;

            await _movieService.SaveSearchAsync(text);

            var SearchResults = await _movieService.GetMovieListItemAsync(await _movieService.GetPageHtmlAsync(Staticants.search + text.Trim().Replace(" ","-")));
            if (SearchResults.Count == 0)
            {
                SearchResultsList.Visibility = Visibility.Collapsed;
                NoResultsTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                SearchResultsList.ItemsSource = SearchResults;
                NoResultsTextBlock.Visibility = Visibility.Collapsed;
            }
            SearchProgressRing.Visibility = Visibility.Collapsed;
            SearchProgressRing.IsActive = false;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchTimer.Stop();
            searchTimer.Start();
        }

        async void DoActiveRecentSearch()
        {
            if (SearchResultsList.ItemsSource == null)
            {
                var recentSearches = await _movieService.LoadSavedSearchAsync();
                if (recentSearches != null)
                {
                    RecentSearchesList.ItemsSource = recentSearches;
                    RecentSearchesList.ItemsSource = recentSearches;
                    RecentSearchesList.Visibility = Visibility.Visible;
                }
                else
                    RecentSearchesList.Visibility = Visibility.Collapsed;
            }
        }

        #endregion
    }
}
