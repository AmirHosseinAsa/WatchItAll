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
    public sealed partial class GenresPage : Page
    {
        IMovieService _movieService = new MovieService();
        public Genre Genre { get; set; }
        public List<MovieItem> Movies{ get; set; }
        int page = 1;
        public GenresPage()
        {
            InitializeComponent();
        }

        async Task LoadGenreAsync()
        {
            GenreResultsList.Visibility = Visibility.Collapsed;   
            string url = Genre.Link + $"?page={page}";
            Movies = await _movieService.GetMovieListItemAsync(await _movieService.GetPageHtmlAsync(url));

            if (Movies.Count == 0)
            {
                NoResultsTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                GenreResultsList.ItemsSource = Movies;
                NoResultsTextBlock.Visibility = Visibility.Collapsed;
            }
            CurrentPage.Text = page.ToString();
            LoadingIndicator.Visibility = Visibility.Collapsed;
            GenreResultsList.Visibility = Visibility.Visible;
        }

        #region View
        private async void LeftArrow_Click(object sender, RoutedEventArgs e)
        {
            if (page > 1)
            {
                page--;
                LoadingIndicator.Visibility = Visibility.Visible;
                await LoadGenreAsync();
            }
        }

        private async void RightArrow_Click(object sender, RoutedEventArgs e)
        {
            page++;
            LoadingIndicator.Visibility = Visibility.Visible;
            await LoadGenreAsync();
        }
        #endregion

        #region Navigation
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            LoadingIndicator.Visibility = Visibility.Visible;
            base.OnNavigatedTo(e);
            Genre = (Genre)e.Parameter;
            CurrentGenre.Text = Genre.Name;
            page = 1;
            LoadGenreAsync();
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
