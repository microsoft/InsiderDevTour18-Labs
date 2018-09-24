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

using ContosoIT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.AI.MachineLearning;
using Windows.ApplicationModel;
using Windows.Graphics.Imaging;
using Windows.Media;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Visibility = Windows.UI.Xaml.Visibility;

namespace ContosoIT.Pages
{
    public sealed partial class DevicesPage : Page
    {
        private ContosoIT.ContosoITModel model;

        public DevicesPage()
        {
            InitializeComponent();
        }

        public List<string> Suggestions { get; set; }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await InitializeModel();

            var selectedFile = (DetectionDataParametersModel)e.Parameter;
            await BeginDetection(selectedFile);
        }

        private async Task BeginDetection(DetectionDataParametersModel detectionDataParameters)
        {
            if (detectionDataParameters == null)
            {
                return;
            }

            UpdateProgressRingAndResultsVisibility(true);

            // Your code goes here
            var videoFrame = await ImageToVideoframe(detectionDataParameters.SelectedFile);
            ContosoITInput modelInput = new ContosoITInput() { data = ImageFeatureValue.CreateFromVideoFrame(videoFrame)};
            ContosoITOutput modelResult = await model.EvaluateAsync(modelInput);
            var classLabel = modelResult.classLabel.GetAsVectorView().FirstOrDefault();

            await ShowResults(detectionDataParameters.SelectedFile, classLabel);
        }

        private async Task InitializeModel()
        {
            string modelPath = @"ms-appx:///Assets/ContosoIT.onnx";
            StorageFile modelFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(modelPath));
            model = await ContosoIT.ContosoITModel.CreateFromStreamAsync(modelFile);
        }

        private async Task<VideoFrame> ImageToVideoframe(StorageFile imageFile)
        {            
            using (IRandomAccessStream imageStream = await imageFile.OpenAsync(FileAccessMode.Read))
            {
                // Create the decoder from the stream
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(imageStream);

                // Get the SoftwareBitmap representation of the file
                SoftwareBitmap softwareBitmap = await decoder.GetSoftwareBitmapAsync();

                VideoFrame imageFrame = VideoFrame.CreateWithSoftwareBitmap(softwareBitmap);

                return imageFrame;
            }
        }

        // Show ProgressRing and hide results grid
        // true -> shows progress ring and hides results grid
        // false -> hides progress ring and shows results grid
        private void UpdateProgressRingAndResultsVisibility(bool show)
        {
            Progress.IsActive = show;
            MainGrid.Visibility = show ? Visibility.Collapsed : Visibility.Visible;
            NoDeviceFound.Visibility = Visibility.Collapsed;
        }

        private async Task ShowResults(IStorageFile file, string label)
        {
            DetectedLabel.Text = label == DetectionConstants.SurfaceStudioTag ? 
                DetectionConstants.SurfaceStudio : DetectionConstants.SurfacePro;
            Suggestions = label == DetectionConstants.SurfaceStudioTag ?
                DetectionConstants.SurfaceStudioSuggestions : DetectionConstants.SurfaceProSuggestions;
            SuggestionsListView.ItemsSource = Suggestions;

            using (var stream = await file.OpenAsync(FileAccessMode.Read))
            {
                var bitmapImage = new BitmapImage();
                await bitmapImage.SetSourceAsync(stream);
                DetectedImage.Source = bitmapImage;
            }

            UpdateProgressRingAndResultsVisibility(false);
        }

        private static async Task<VideoFrame> ConvertFileToVideoFrameAsync(IStorageFile file)
        {
            SoftwareBitmap softwareBitmap;
            using (var stream = await file.OpenAsync(FileAccessMode.Read))
            {
                var decoder = await BitmapDecoder.CreateAsync(stream);
                softwareBitmap = await decoder.GetSoftwareBitmapAsync();
                softwareBitmap = SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore);
            }

            return VideoFrame.CreateWithSoftwareBitmap(softwareBitmap);
        }
    }
}