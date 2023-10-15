using WatchItAll.Core.Interfaces;
using WatchItAll.Core.Services;
using WatchItAll.DataLayer.Entities;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace WatchItAll.UI.Pages
{
    public sealed partial class HomePage : Page
    {
        IMovieService _movieService = new MovieService();
        HomePageData HomePageMovies;
        public HomePage()
        {
            this.InitializeComponent();
            LoadingIndicator.Visibility = Visibility.Visible;
            GridShow.Visibility = Visibility.Collapsed;
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Required;
            LoadMovieTitlesAsync();
        }
        private async void LoadMovieTitlesAsync()
        {
            HomePageMovies = await _movieService.GetHomeItemsAsync(await _movieService.GetPageHtmlAsync("home"));

            TrendingMoviesList.ItemsSource = HomePageMovies.TrendingMovieItems;
            TrendingTvShowsList.ItemsSource = HomePageMovies.TrendingTvItems;
            LatestMoviesList.ItemsSource = HomePageMovies.LatestMoviesItems;
            LatestTvShowsList.ItemsSource = HomePageMovies.LatestTvShowsItems;

            LoadingIndicator.Visibility = Visibility.Collapsed;
            GridShow.Visibility = Visibility.Visible;
        }


        #region Navigation
        private void MovieList_Clicked(object sender, ItemClickEventArgs e)
        {
            MovieItem selectedMovie = (MovieItem)e.ClickedItem;
            Frame.Navigate(typeof(DetailPage), selectedMovie);
        }

        #endregion
    }
}
