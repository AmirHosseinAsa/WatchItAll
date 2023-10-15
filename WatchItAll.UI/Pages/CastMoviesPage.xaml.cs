using System.Collections.Generic;
using System.Threading.Tasks;
using WatchItAll.Core.Interfaces;
using WatchItAll.Core.Services;
using WatchItAll.DataLayer.Entities;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace WatchItAll.UI.Pages
{
    public sealed partial class CastMoviesPage : Page
    {
        IMovieService _movieService = new MovieService();
        public ItemCastORGenre Cast { get; set; }
        public List<MovieItem> Movies { get; set; }
        int page = 1;
        public CastMoviesPage()
        {
            InitializeComponent();
        }

        async Task LoadCastAsync()
        {
            CastResultsList.Visibility = Visibility.Collapsed;
            string url = Cast.Url + $"?page={page}";
            Movies = await _movieService.GetMovieListItemAsync(await _movieService.GetPageHtmlAsync(url));

            if (page != 1 || Movies.Count > 20)
            {
                PaggingContainer.Visibility = Visibility.Visible;
                CastResContainer.Margin = new Thickness(0);
            }
            else
            {
                PaggingContainer.Visibility = Visibility.Collapsed;
                CastResContainer.Margin = new Thickness(10);
            }

            if (Movies.Count == 0)
            {
                NoResultsTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                CastResultsList.ItemsSource = Movies;
                NoResultsTextBlock.Visibility = Visibility.Collapsed;
            }
            CurrentPage.Text = page.ToString();
            LoadingIndicator.Visibility = Visibility.Collapsed;
            CastResultsList.Visibility = Visibility.Visible;
        }

        #region View
        private async void LeftArrow_Click(object sender, RoutedEventArgs e)
        {
            if (page > 1)
            {
                page--;
                LoadingIndicator.Visibility = Visibility.Visible;
                await LoadCastAsync();
            }
        }

        private async void RightArrow_Click(object sender, RoutedEventArgs e)
        {
            page++;
            LoadingIndicator.Visibility = Visibility.Visible;
            await LoadCastAsync();
        }
        #endregion

        #region Navigation
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            LoadingIndicator.Visibility = Visibility.Visible;
            base.OnNavigatedTo(e);
            Cast = (ItemCastORGenre)e.Parameter;
            CurrentCast.Text = Cast.Title;
            page = 1;
            LoadCastAsync();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                this.NavigationCacheMode = NavigationCacheMode.Disabled;
                Frame.GoBack();
            }
        }

        private void MovieList_Clicked(object sender, ItemClickEventArgs e)
        {
            MovieItem selectedMovie = (MovieItem)e.ClickedItem;
            Frame.Navigate(typeof(DetailPage), selectedMovie);
        }

        #endregion
    }
}
