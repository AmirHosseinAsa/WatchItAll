using System;
using System.Linq;
using System.Numerics;
using WatchItAll.Core.Interfaces;
using WatchItAll.Core.Services;
using WatchItAll.DataLayer.Entities;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace WatchItAll.UI.Pages
{
    public sealed partial class BookmarksPage : Page
    {
        IMovieService _movieService = new MovieService();
        public BookmarksPage()
        {
            this.InitializeComponent();
            MovieGrid.Visibility = Visibility.Collapsed;
            LoadingIndicator.Visibility = Visibility.Visible;
            LoadMovieTitlesAsync();
        }
        private async void LoadMovieTitlesAsync()
        {
            var BookmarkedMVS = await _movieService.LoadMVSAsync("bookmarks_mvs.json");
            try
            {
                if (BookmarkedMVS != null && BookmarkedMVS.Any())
                {
                    BookmarkedMVS.Reverse();
                    BookmarkedMovieList.ItemsSource = BookmarkedMVS;
                }
            }
            catch (System.Exception)
            {
            }
            LoadingIndicator.Visibility = Visibility.Collapsed;
            MovieGrid.Visibility = Visibility.Visible;
        }

        #region Animation
        public void MovieGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var MovieCardGrid = sender as Grid;
            if (MovieCardGrid != null)
            {
                var compositor = ElementCompositionPreview.GetElementVisual(MovieCardGrid).Compositor;

                // Opacity animation
                var opacityAnimation = compositor.CreateScalarKeyFrameAnimation();
                opacityAnimation.InsertKeyFrame(0, 0);
                opacityAnimation.InsertKeyFrame(1, 1);
                opacityAnimation.Duration = TimeSpan.FromMilliseconds(1500);

                var visual = ElementCompositionPreview.GetElementVisual(MovieCardGrid);
                visual.Opacity = 0;
                visual.StartAnimation("Opacity", opacityAnimation);

                // Scale animation
                var scaleAnimation = compositor.CreateVector3KeyFrameAnimation();
                scaleAnimation.InsertKeyFrame(0, new Vector3(0.8f, 0.8f, 0));
                scaleAnimation.InsertKeyFrame(1, new Vector3(1, 1, 0));
                scaleAnimation.Duration = TimeSpan.FromMilliseconds(1500);

                visual.Scale = new Vector3(0.8f, 0.8f, 0);
                visual.StartAnimation("Scale", scaleAnimation);

                // Translation animation
                var translationAnimation = compositor.CreateVector3KeyFrameAnimation();
                translationAnimation.InsertKeyFrame(0, new Vector3(0, 50, 0));
                translationAnimation.InsertKeyFrame(1, new Vector3(0, 0, 0));
                translationAnimation.Duration = TimeSpan.FromMilliseconds(1500);

                visual.Offset = new Vector3(0, 50, 0);
                visual.StartAnimation("Offset", translationAnimation);
            }
        }

        #endregion

        #region  Navigation

        private void MovieList_Clicked(object sender, ItemClickEventArgs e)
        {
            MovieItem selectedMovie= (MovieItem)e.ClickedItem;
            Frame.Navigate(typeof(DetailPage), selectedMovie);
        }
        #endregion
    }
}
