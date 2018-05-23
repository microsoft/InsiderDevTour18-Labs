// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
namespace Microsoft.Graph.HOL
{
    using Microsoft.Graph.HOL.Utils;
    using Microsoft.Graph.HOL.ViewModels;
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using Windows.Storage;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ContentFileOneDrive : Page
    {

        private ObservableCollection<OneDriveFiles> _items = new ObservableCollection<OneDriveFiles>();

        public ObservableCollection<OneDriveFiles> Items
        {
            get { return this._items; }
        }

        public ContentFileOneDrive()
        {
            this.InitializeComponent();
            Progress.IsActive = true;
        }      

        private async void Page_Loading(FrameworkElement sender, object args)
        {
            try
            {
                var recentFiles = await OneDriveHelper.GetItems(10);
                foreach (var recentFile in recentFiles)
                {
                    Items.Add(new OneDriveFiles()
                    {
                        Name = recentFile.Name,
                        IconPath = recentFile.Name.GetIcon(),
                        ItemInDrive = recentFile

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

        private async void itemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Progress.IsActive = true;
            itemListView.IsEnabled = false;

            try
            {
                var item = (OneDriveFiles)itemListView.SelectedItem;
                InfoText.Text = $"Downloading File: {item.Name} in pdf";                
                var stream = await OneDriveHelper.ConvertContetPDF(item.ItemInDrive);

                StorageFolder folder = Windows.Storage.KnownFolders.PicturesLibrary;
                StorageFile storageFile = await folder.CreateFileAsync($"{item.Name.GetFileName()}.pdf");
                var storageFileStream = await storageFile.OpenAsync(FileAccessMode.ReadWrite);

                await stream.CopyToAsync(storageFileStream.AsStreamForWrite());
                await stream.FlushAsync();
                stream.Dispose();
                storageFileStream.Dispose();                

                InfoText.Text = $"Download File: {item.Name.GetFileName()}.pdf complete. See the Pictures folder";
            }
            catch (Exception ex)
            {
                InfoText.Text = $"OOPS! An error ocurred: {ex.GetMessage()}";
            }
            finally
            {
                itemListView.IsEnabled = true;
                Progress.IsActive = false;
            }
        }
    }
}
