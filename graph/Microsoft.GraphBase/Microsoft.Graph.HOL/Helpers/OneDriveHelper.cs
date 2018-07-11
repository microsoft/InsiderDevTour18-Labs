// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

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
