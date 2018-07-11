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

namespace Microsoft.Graph.HOL
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Windows.Storage;
    using Microsoft.Graph.HOL.Utils;

    public class OneDriveHelper
    {
        public static async Task<List<DriveItem>> GetRecentItems()
        {
            throw new NotImplementedException();
        }

        public static async Task UploadItem(StorageFile storageFile)
        {
            throw new NotImplementedException();
        }

        public static async Task<Stream> DownloadFile(DriveItem driveItem)
        {
            throw new NotImplementedException();
        }

        public static async Task<Stream> ConvertContetPDF(DriveItem driveItem)
        {
            throw new NotImplementedException();
        }

        private static bool ValidateExtension(string filename)
        {
            string extension = "doc, docx, epub, eml, htm, html, md, msg, odp, ods, odt, pps, ppsx, ppt, pptx, rtf, tif, tiff, xls, xlsm, xlsx";
            return extension.Contains(filename.GetExtension());
        }

        public static async Task<List<DriveItem>> GetItems(int numberOfElements)
        {
            List<DriveItem> filesName = new List<DriveItem>();

            try
            {
                var graphClient = AuthenticationHelper.GetAuthenticatedClient();
                var onedrive = await graphClient.Me.Drive.Root.Children.Request().GetAsync();

                filesName = await GetNameFiles(graphClient, filesName, onedrive, numberOfElements);

                return filesName;
            }

            catch (Exception ex)
            {
                Debug.WriteLine("Could not create a graph client: " + ex.Message);
                throw;
            }
        }

        private static async Task<List<DriveItem>> GetNameFiles(GraphServiceClient graphClient, List<DriveItem> filesName, IDriveItemChildrenCollectionPage items, int numberOfElements)
        {

            foreach (var item in items)
            {
                if (item.File != null)
                {
                    filesName.Add(item);
                }
                else
                {
                    var driveItemInfo = graphClient.Me.Drive.Items[item.Id].Children.Request().GetAsync().Result;
                    await GetNameFiles(graphClient, filesName, driveItemInfo, numberOfElements);
                }

                if (filesName.Count == numberOfElements)
                {
                    break;
                }

            }

            return filesName;
        }
    }
}
