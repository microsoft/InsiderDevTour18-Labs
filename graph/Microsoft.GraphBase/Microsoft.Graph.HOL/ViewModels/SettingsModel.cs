// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

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
