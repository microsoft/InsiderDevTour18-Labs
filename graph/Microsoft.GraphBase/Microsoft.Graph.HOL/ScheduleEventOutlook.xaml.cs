// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
namespace Microsoft.Graph.HOL
{
    using Microsoft.Graph.HOL.Utils;
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ScheduleEventOutlook : Page
    {
        public ScheduleEventOutlook()
        {
            this.InitializeComponent();
        }

        private async void Button_Appointment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Progress.IsActive = true;
                var startDate = new DateTime(this.startDate.Date.Year, this.startDate.Date.Month, this.startDate.Date.Day, this.starthour.Time.Hours, this.starthour.Time.Minutes, this.starthour.Time.Seconds);
                var endDate = new DateTime(this.endDate.Date.Year, this.endDate.Date.Month, this.endDate.Date.Day, this.endhour.Time.Hours, this.endhour.Time.Minutes, this.endhour.Time.Seconds);
                await OutlookHeñper.SetAppointment(this.txtSubject.Text, startDate, endDate);

                InfoText.Text = "Event scheduled correctly";
            }
            catch(Exception ex)
            {
                InfoText.Text = $"OOPS! An error ocurred: {ex.GetMessage()}";
            }    
            finally
            {
                this.Progress.IsActive = false;
            }
        }
    }
}
