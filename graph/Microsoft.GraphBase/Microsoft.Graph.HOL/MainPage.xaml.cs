// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Microsoft.Graph.HOL
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Windows.Storage;
    using Windows.System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static ApplicationDataContainer _settings = ApplicationData.Current.RoamingSettings;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            // set the initial SelectedItem 
            foreach (NavigationViewItemBase item in NavView.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "LogIn")
                {
                    NavView.SelectedItem = item;
                    break;
                }
            }

            ContentFrame.Navigated += On_Navigated;

            // add keyboard accelerators for backwards navigation
            KeyboardAccelerator GoBack = new KeyboardAccelerator();
            GoBack.Key = VirtualKey.GoBack;
            GoBack.Invoked += BackInvoked;
            KeyboardAccelerator AltLeft = new KeyboardAccelerator();
            AltLeft.Key = VirtualKey.Left;
            AltLeft.Invoked += BackInvoked;
            this.KeyboardAccelerators.Add(GoBack);
            this.KeyboardAccelerators.Add(AltLeft);
            // ALT routes here
            AltLeft.Modifiers = VirtualKeyModifiers.Menu;

        }

        private async void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            // find NavigationViewItem with Content that equals InvokedItem
            var item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);
            await NavView_Navigate(item as NavigationViewItem);
        }

        private async Task NavView_Navigate(NavigationViewItem item)
        {
            try
            {
                UserExtensionHelper.PickupWhereYouLeft(item.Tag.ToString());
                switch (item.Tag)
                {
                    case "LogIn":
                        await LoginUser();
                        break;
                    case "LogOut":
                        AuthenticationHelper.SignOut();
                        login.Visibility = Visibility.Visible;
                        logout.Visibility = Visibility.Collapsed;
                        RecentFiles.Visibility = Visibility.Collapsed;
                        UploadFile.Visibility = Visibility.Collapsed;
                        DownloadFile.Visibility = Visibility.Collapsed;
                        ContentFile.Visibility = Visibility.Collapsed;
                        OutlookContacts.Visibility = Visibility.Collapsed;
                        ScheduleEvent.Visibility = Visibility.Collapsed;
                        UserExtension.Visibility = Visibility.Collapsed;
                        SaveAppData.Visibility = Visibility.Collapsed;
                        Activity.Visibility = Visibility.Collapsed;
                        break;
                    case "RecentFiles":
                        ContentFrame.Navigate(typeof(RecentOneDriveFiles));
                        break;
                    case "UploadFile":
                        ContentFrame.Navigate(typeof(UploadFileToOneDrive));
                        break;
                    case "DownloadFile":
                        ContentFrame.Navigate(typeof(DownloadOneDriveFile));
                        break;
                    case "ContentFile":
                        ContentFrame.Navigate(typeof(ContentFileOneDrive));
                        break;
                    case "OutlookContacts":
                        ContentFrame.Navigate(typeof(OutlookContacts));
                        break;
                    case "ScheduleEvent":
                        ContentFrame.Navigate(typeof(ScheduleEventOutlook));
                        break;
                    case "UserExtension":
                        ContentFrame.Navigate(typeof(UserExtension));
                        break;
                    case "SaveAppData":
                        ContentFrame.Navigate(typeof(SaveAppData));
                        break;
                    case "Activity":
                        ContentFrame.Navigate(typeof(ActivityGraph));
                        break;
                }
            }
            catch (Exception ex)
            {
                NavView.Header = $"And error ocurred: {ex}";
            }
        }

        private async Task LoginUser()
        {
            try
            {
                var graphClient = AuthenticationHelper.GetAuthenticatedClient();
                if (graphClient != null)
                {
                    var user = await graphClient.Me.Request().GetAsync();
                    NavView.Header = $"HI! {user.DisplayName}, Welcome to Microsoft Graph HOL";
                    login.Visibility = Visibility.Collapsed;
                    logout.Visibility = Visibility.Visible;
                    RecentFiles.Visibility = Visibility.Visible;
                    UploadFile.Visibility = Visibility.Visible;
                    DownloadFile.Visibility = Visibility.Visible;
                    ContentFile.Visibility = Visibility.Visible;
                    OutlookContacts.Visibility = Visibility.Visible;
                    ScheduleEvent.Visibility = Visibility.Visible;
                    UserExtension.Visibility = Visibility.Visible;
                    SaveAppData.Visibility = Visibility.Visible;
                    Activity.Visibility = Visibility.Visible;
                }
            }
            catch (NotImplementedException ex)
            {
                NavView.Header = $"Register you app and add credential in App.xaml";                
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            On_BackRequested();
        }

        private void BackInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            On_BackRequested();
            args.Handled = true;
        }

        private bool On_BackRequested()
        {
            bool navigated = false;

            // don't go back if the nav pane is overlayed
            if (NavView.IsPaneOpen && (NavView.DisplayMode == NavigationViewDisplayMode.Compact || NavView.DisplayMode == NavigationViewDisplayMode.Minimal))
            {
                return false;
            }
            else
            {
                if (ContentFrame.CanGoBack)
                {
                    ContentFrame.GoBack();
                    navigated = true;
                }
            }
            return navigated;
        }

        private void On_Navigated(object sender, NavigationEventArgs e)
        {
            NavView.IsBackEnabled = ContentFrame.CanGoBack;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var parameter = (string)e.Parameter;

            if(!string.IsNullOrEmpty(parameter))
            {
                await LoginUser();

                switch (parameter)
                {                   
                    case "RecentFiles":
                        ContentFrame.Navigate(typeof(RecentOneDriveFiles));
                        break;
                    case "UploadFile":
                        ContentFrame.Navigate(typeof(UploadFileToOneDrive));
                        break;
                    case "DownloadFile":
                        ContentFrame.Navigate(typeof(DownloadOneDriveFile));
                        break;
                    case "ContentFile":
                        ContentFrame.Navigate(typeof(ContentFileOneDrive));
                        break;
                    case "OutlookContacts":
                        ContentFrame.Navigate(typeof(OutlookContacts));
                        break;
                    case "ScheduleEvent":
                        ContentFrame.Navigate(typeof(ScheduleEventOutlook));
                        break;
                    case "UserExtension":
                        ContentFrame.Navigate(typeof(UserExtension));
                        break;
                    case "SaveAppData":
                        ContentFrame.Navigate(typeof(SaveAppData));
                        break;
                    case "Activity":
                        ContentFrame.Navigate(typeof(ActivityGraph));
                        break;
                }
            }
        }
    }
}
