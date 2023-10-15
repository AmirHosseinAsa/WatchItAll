using System;
using WatchItAll.UI.Pages;
using Windows.System.Profile;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace WatchItAll.UI
{
    public sealed partial class MainWindow : Page
    {
        public MainWindow()
        {
            this.InitializeComponent();
            bool isXbox = AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Xbox";
            if (!isXbox)
                ApplicationView.GetForCurrentView().ExitFullScreenMode();

            RootNavigation.SelectedItem = navHome; // Set the initial selected item
            RootNavigation.ItemInvoked += RootNavigation_ItemInvoked;

            // Load the main page content
            ContentFrame.NavigationFailed += ContentFrame_NavigationFailed;
            ContentFrame.Navigated += ContentFrame_Navigated;
            ContentFrame.Navigate(typeof(HomePage));
        }

        private void RootNavigation_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var selectedItem = (NavigationViewItem)args.InvokedItemContainer;
            var pageType = Type.GetType($"WatchItAll.UI.Pages.{selectedItem.Tag}");
            ContentFrame.Navigate(pageType);
        }

        private async void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            // Handle navigation failure
        }

        private async void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            // Handle navigation success
        }
    }
}
