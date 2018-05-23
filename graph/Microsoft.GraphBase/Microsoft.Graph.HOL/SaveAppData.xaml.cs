// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
namespace Microsoft.Graph.HOL
{
    using Microsoft.Graph.HOL.Helpers;
    using Microsoft.Graph.HOL.Utils;
    using Microsoft.Graph.HOL.ViewModels;
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SaveAppData : Page
    {
        public SaveAppData()
        {
            this.InitializeComponent();
        }

        private async void Page_Loading(FrameworkElement sender, object args)
        {
            try
            {
                Progress.IsActive = true;

                var settingModel = await DataSyncHelper.GetSettingsInOneDrive();
                this.toggleNot.IsOn = settingModel.Notification;
                this.ToggleNotVibration.IsOn = settingModel.NotificationVibration;
                this.ToggleNotSound.IsOn = settingModel.NotificationSound;

                InfoText.Text = "Settings Loaded from OneDrive";

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

        private async void Button_SaveSettings_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                Progress.IsActive = true;
                var settingModel = new SettingsModel()
                {
                    Notification = this.toggleNot.IsOn,
                    NotificationVibration = this.ToggleNotVibration.IsOn,
                    NotificationSound = this.ToggleNotSound.IsOn                    
                };

                await DataSyncHelper.SaveSettingsInOneDrive(settingModel);
                InfoText.Text = "Settings Saved in OneDrive";
                
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
