// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

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
    public sealed partial class ActivityGraph : Page
    {
        public ActivityGraph()
        {
            this.InitializeComponent();
        }

        private async void Button_Activity_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Progress.IsActive = true;
                await UserExtensionHelper.CreateActivity();
                InfoText.Text = $"Activity Created see Timeline";
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
