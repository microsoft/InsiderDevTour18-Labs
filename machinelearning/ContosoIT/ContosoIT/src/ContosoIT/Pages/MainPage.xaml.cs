using System;
using System.Globalization;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ContosoIT.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            MainNav.SelectionChanged += OnMainNavSelectionChanged;

            void OnMainNavSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
            {
                if (args.SelectedItem is NavigationViewItem navViewItem)
                {
                    var pageName = $"ContosoIT.Pages.{navViewItem.Tag}";
                    var pageType = Type.GetType(pageName);

                    if (pageType != null)
                    {
                        ContentFrame.Navigate(pageType);
                    }
                }
            }

            ContentFrame.Navigated += OnContentFrameNavigated;

            void OnContentFrameNavigated(object sender, NavigationEventArgs args)
            {
                if (args.SourcePageType == typeof(DevicesPage) && args.Parameter != null)
                {
                    var navItem = MainNav.MenuItems.OfType<NavigationViewItem>()
                        .First(x => x.Tag as string == nameof(DevicesPage));
                    if (MainNav.SelectedItem != navItem)
                    {
                        MainNav.SelectedItem = navItem;
                    }

                    MainNav.IsPaneOpen = false;
                }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            MainNav.SelectedItem = MainNav.MenuItems.FirstOrDefault();
        }

        private void OnBackgroundImageOpened(object sender, RoutedEventArgs e) =>
            BackgroundImage.Visibility = Visibility.Visible;            

        private string GetMenuItemName(object selectedItem)
        {
            if (selectedItem is NavigationViewItem navViewItem)
            {
                string content;

                if (navViewItem.Content is StackPanel panel)
                {
                    content = ((TextBlock)panel.Children.First()).Text;
                }
                else
                {
                    content = navViewItem.Content.ToString();
                }

                return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(content.ToLower());
            }

            return string.Empty;
        }
    }
}
