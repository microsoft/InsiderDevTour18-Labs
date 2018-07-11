// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using ContosoIT.Models;
using System;
using Windows.Foundation;
using Windows.Media.Capture;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ContosoIT.Pages
{
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private async void OnAttachImageClick(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };

            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            var file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                var parameters = new DetectionDataParametersModel { SelectedFile = file };
                Frame.Navigate(typeof(DevicesPage), parameters);
            }
        }

        private async void OnTakePhotoClick(object sender, RoutedEventArgs e)
        {
            var capture = new CameraCaptureUI();
            capture.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            capture.PhotoSettings.CroppedSizeInPixels = new Size(500, 500);

            var photo = await capture.CaptureFileAsync(CameraCaptureUIMode.Photo);
            if (photo != null)
            {
                var parameters = new DetectionDataParametersModel { SelectedFile = photo };
                Frame.Navigate(typeof(DevicesPage), parameters);
            }
        }
    }
}
