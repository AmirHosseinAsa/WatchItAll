using WatchItAll.DataLayer.Entities;
using System;
using System.Numerics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace WatchItAll.UI.UserControls
{
    public sealed partial class UcMovieCard : Page
    {

        public static readonly DependencyProperty ValueProperty =
          DependencyProperty.Register("Value", typeof(MovieItem), typeof(UcMovieCard), new PropertyMetadata(null));

        public MovieItem Value
        {
            get { return (MovieItem)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public UcMovieCard()
        {
            this.InitializeComponent();
        }
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            DataContext = Value;
        }
        public void UcCard_Loaded(object sender, RoutedEventArgs e)
        {
            var ucMovieCard = sender as StackPanel;
            if (ucMovieCard != null)
            {
                var compositor = ElementCompositionPreview.GetElementVisual(ucMovieCard).Compositor;

                // Opacity animation
                var opacityAnimation = compositor.CreateScalarKeyFrameAnimation();
                opacityAnimation.InsertKeyFrame(0, 0);
                opacityAnimation.InsertKeyFrame(1, 1);
                opacityAnimation.Duration = TimeSpan.FromMilliseconds(1500);

                var visual = ElementCompositionPreview.GetElementVisual(ucMovieCard);
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
    }
}
