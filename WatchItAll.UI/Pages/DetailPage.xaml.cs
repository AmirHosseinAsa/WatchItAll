using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Linq;
using System.Threading.Tasks;
using WatchItAll.Core.Interfaces;
using WatchItAll.Core.Services;
using WatchItAll.DataLayer.Constants;
using WatchItAll.DataLayer.Entities;
using WatchItAll.UI.UserControls;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Navigation;

namespace WatchItAll.UI.Pages
{
    public sealed partial class DetailPage : Page
    {
        IMovieService _movieService = new MovieService();
        public MovieItem SelectedMovie { get; set; }

        public DetailPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }
        private async void LoadDetailAsync()
        {
            var result = await _movieService.GetMovieDetailAsync(SelectedMovie.Url);
            SelectedMovie.Detail = result.mv;
            SelectedMovie.IMDBRate = result.imdbRate;
            DataContext = SelectedMovie;
            CalculateGrid();

            try
            {
                await DoFav(true);
            }
            catch (Exception)
            {
            }

            if (SelectedMovie.Detail.Seasons != null)
            {
                SeasonsComboBox.ItemsSource = SelectedMovie.Detail.Seasons.Select(s => s.Title).ToList();
                EpisodesSource.ItemsSource = SelectedMovie.Detail.Seasons.FirstOrDefault().Episodes.ToList();
                SeasonsComboBox.SelectedIndex = 0;

                ServersPanel.Visibility = Visibility.Collapsed;
                SeasonsComboBox.Visibility = Visibility.Visible;
                EpisodesSource.Visibility = Visibility.Visible;
            }
            else
            {
                SeasonsComboBox.Visibility = Visibility.Collapsed;
                EpisodesSource.Visibility = Visibility.Collapsed;
                EpisodesPanel.Visibility = Visibility.Collapsed;
                ServersPanel.Visibility = Visibility.Visible;
            }
            LoadingIndicator.Visibility = Visibility.Collapsed;
            GridShow.Visibility = Visibility.Visible;
            Page_Loaded();
        }

        async Task DoFav(bool inital)
        {
            if (inital)
            {
                if (await _movieService.IsMVSBookmarkedAsync(SelectedMovie, "bookmarks_mvs.json"))
                {
                    favText.Text = "Remove from Favorite";
                    favSymbol.Symbol = Symbol.UnFavorite;
                }
                else
                {
                    favText.Text = "Add to Favorites";
                    favSymbol.Symbol = Symbol.Favorite;
                }
            }
            else
            {
                if (favText.Text.ToString() == "Add to Favorites")
                {
                    favText.Text = "Remove from Favorites";
                    favSymbol.Symbol = Symbol.UnFavorite;
                    await _movieService.SaveMVSAsync(SelectedMovie, "bookmarks_mvs.json");
                }
                else
                {
                    favText.Text = "Add to Favorites";
                    favSymbol.Symbol = Symbol.Favorite;
                    await _movieService.DeleteMVSAsync(SelectedMovie, "bookmarks_mvs.json");
                }
            }
        }
        private void Page_Loaded()
        {
            var grid = GridShow as Grid;
            if (grid != null)
            {
                var compositor = ElementCompositionPreview.GetElementVisual(grid).Compositor;
                var animation = compositor.CreateScalarKeyFrameAnimation();
                animation.InsertKeyFrame(0, 0);
                animation.InsertKeyFrame(1, 1);
                animation.Duration = TimeSpan.FromMilliseconds(2000);

                var visual = ElementCompositionPreview.GetElementVisual(grid);
                visual.Opacity = 0;
                visual.StartAnimation("Opacity", animation);
            }
        }
        #region Navigation
        private async void FavoriteButton_Click(object sender, RoutedEventArgs e)
        {
            await DoFav(false);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                LoadingIndicator.Visibility = Visibility.Visible;
                GridShow.Visibility = Visibility.Collapsed;
                SelectedMovie = (MovieItem)e.Parameter;
                LoadDetailAsync();
            }
            catch (System.Exception)
            {
            }
            base.OnNavigatedTo(e);

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                this.NavigationCacheMode = NavigationCacheMode.Disabled;
                Frame.GoBack();
            }
        }

        private void Genre_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemCastORGenre selectedGenre = (ItemCastORGenre)e.ClickedItem;
            Genre gen = Staticants.GenreList.FirstOrDefault(g => selectedGenre.Url.ToLower().Trim().Contains(g.Link.ToLower().Trim()));
            Frame.Navigate(typeof(GenresPage), gen);
        }

        WebView webView;
        private async void PlayTrailerButtonClicked(object sender, RoutedEventArgs e)
        {
            webView = new WebView();
            Uri uri = new Uri(SelectedMovie.Detail.TrailerLink);
            webView.Source = uri;

            var windowBounds = Window.Current.Bounds;
            webView.Width = windowBounds.Width * .8;
            webView.Height = windowBounds.Height * .8;

            ContentDialog dialog = new ContentDialog();
            dialog.Content = webView;

            dialog.MinWidth = windowBounds.Width * .9999;
            dialog.Width = windowBounds.Width * .9999;
            dialog.MaxWidth = windowBounds.Width * .9999;
            dialog.Title = "Trailer: "+ SelectedMovie.Name;

            dialog.MinHeight = windowBounds.Height * .9999;
            dialog.Height = windowBounds.Height * .9999;
            dialog.MaxHeight = windowBounds.Height * .9999;
            dialog.PrimaryButtonText = "Close";
            var bst = new Windows.UI.Xaml.Style(typeof(Button));
            bst.Setters.Add(new Setter(Button.WidthProperty, 100));
            bst.Setters.Add(new Setter(Button.MarginProperty, new Thickness(0, 0, 200, 0)));
            dialog.PrimaryButtonStyle = bst;
            dialog.VerticalAlignment = VerticalAlignment.Center;
            dialog.HorizontalAlignment = HorizontalAlignment.Center;
            dialog.PrimaryButtonClick += Dialog_PrimaryButtonClick; ;
            await dialog.ShowAsync();
        }

        private void Dialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            try
            {
                webView.NavigateToString("SOO");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async void SeasonsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedValue != null)
            {
                var selectedSeason = SelectedMovie.Detail.Seasons.FirstOrDefault(s => s.Title == (sender as ComboBox).SelectedValue.ToString());
                await ChangeComboSeason(selectedSeason);
            }
        }

        async Task ChangeComboSeason(SeasonItem selectedSeason)
        {
            if (selectedSeason.Episodes == null)
            {
                LoadingIndicator.Visibility = Visibility.Visible;
                EpisodesSource.Visibility = Visibility.Collapsed;
                selectedSeason.Episodes = await _movieService.GetEpisodesOfASeasonAsync(selectedSeason.Title, selectedSeason.Id, SelectedMovie.Url);

                LoadingIndicator.Visibility = Visibility.Collapsed;
                EpisodesSource.Visibility = Visibility.Visible;
            }
            EpisodesSource.ItemsSource = selectedSeason.Episodes.ToList();
        }

        private async void Episode_ItemClick(object sender, ItemClickEventArgs e)
        {
            EpisodeItem selectedEpisode = (EpisodeItem)e.ClickedItem;
            var epOrMvs = new EpisodeItemAndServersMovieItem(selectedEpisode, null, SelectedMovie);
            await _movieService.SavePlayedAsync(epOrMvs);
            Frame.Navigate(typeof(WebPlayer), epOrMvs);
        }

        private async void Cast_ItemClick(object sender, ItemClickEventArgs e)
        {
            ItemCastORGenre selectedCast = (ItemCastORGenre)e.ClickedItem;
            Frame.Navigate(typeof(CastMoviesPage), selectedCast);
        }

        private async void Server_ItemClick(object sender, ItemClickEventArgs e)
        {
            StreamURlForMovieItem clickedItem = (StreamURlForMovieItem)e.ClickedItem;
            var epOrMvs = new EpisodeItemAndServersMovieItem(null, clickedItem, SelectedMovie);
            await _movieService.SavePlayedAsync(epOrMvs);
            Frame.Navigate(typeof(WebPlayer), epOrMvs);
        }
        #endregion

        void CalculateGrid()
        {
            var windowBounds = Window.Current.Bounds;
            double width = windowBounds.Width;
            double height = windowBounds.Height;

            if (height + 280 > width)
            {
                SelectedMovie.Columns = "1";
            }
            else if (width > (height * 2 - 200))
            {
                SelectedMovie.Columns = "3";
            }
            else if (width > (height * 2.3 - 200))
            {
                SelectedMovie.Columns = "4";
            }
            else
            {
                SelectedMovie.Columns = "2";
            }
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            CalculateGrid();
        }
    }
}
