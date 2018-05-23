// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
namespace Microsoft.Graph.HOL
{
    using Microsoft.Graph.HOL.Utils;
    using Microsoft.Graph.HOL.ViewModels;
    using System;
    using System.Collections.ObjectModel;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RecentOneDriveFiles : Page
    {
        private ObservableCollection<OneDriveFiles> _items = new ObservableCollection<OneDriveFiles>();

        public ObservableCollection<OneDriveFiles> Items
        {
            get { return this._items; }
        }

        public RecentOneDriveFiles()
        {
            this.InitializeComponent();
            Progress.IsActive = true;
        }

        private async void Page_Loading(FrameworkElement sender, object args)
        {
            try
            {
                var recentFiles = await OneDriveHelper.GetRecentItems();
                foreach (var recentFile in recentFiles)
                {
                    Items.Add(new OneDriveFiles()
                    {
                        Name = recentFile.Name,
                        IconPath = recentFile.Name.GetIcon()

                    });
                }
            }
            catch (Exception ex)
            {
                InfoText.Text = $"OOPS! An error ocurred: {ex.GetMessage()}";
            }
            finally
            {
                Progress.IsActive = false;
            }
        }
    }
}
