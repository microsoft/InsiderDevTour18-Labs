// MIT License
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in all
//  copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//  SOFTWARE

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
namespace Microsoft.Graph.HOL
{
    using Microsoft.Graph.HOL.Utils;
    using System;
    using Windows.Storage.Pickers;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UploadFileToOneDrive : Page
    {
        public UploadFileToOneDrive()
        {
            this.InitializeComponent();
        }

        private async void Button_Upload_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                var openPicker = new FileOpenPicker();
                openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                openPicker.FileTypeFilter.Add(".txt");
                openPicker.FileTypeFilter.Add(".jpg");
                openPicker.FileTypeFilter.Add(".jpeg");
                openPicker.FileTypeFilter.Add(".png");
                openPicker.FileTypeFilter.Add(".pdf");

                var file = await openPicker.PickSingleFileAsync();
                var basicProperty = await file.GetBasicPropertiesAsync();
                if (basicProperty.Size > 2000000)
                {
                    InfoText.Text = "The file can not exceed 2 MB";
                    return;
                }
                Progress.IsActive = true;
                uploadBtn.IsEnabled = false;
                await OneDriveHelper.UploadItem(file);
                InfoText.Text = "The files is your OneDrive in Hol/Graph/ folder";
            }
            catch (Exception ex)
            {
                InfoText.Text = $"OOPS! An error ocurred: {ex.GetMessage()}";
            }
            finally
            {
                Progress.IsActive = false;
                uploadBtn.IsEnabled = true;
            }
        }
    }
}
