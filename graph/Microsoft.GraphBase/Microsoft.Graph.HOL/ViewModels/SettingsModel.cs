namespace Microsoft.Graph.HOL.ViewModels
{
    using System;

    [Serializable()]
    public class SettingsModel
    {
        public bool Notification { get; set; }

        public bool NotificationVibration { get; set; }

        public bool NotificationSound { get; set; }
    }
}
