// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Microsoft.Graph.HOL.Console
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class OneDriveHelper
    {
        public static List<string> GetItems(int numberOfElements)
        {
            throw new NotImplementedException();
        }

        private static List<string> GetNameFiles(GraphServiceClient graphClient,  List<string> filesName, IDriveItemChildrenCollectionPage items, int numberOfElements)
        {

            foreach (var item in items)
            {                
                if (item.File != null)
                {
                    filesName.Add(item.Name);                    
                }
                else
                {
                    var driveItemInfo = graphClient.Me.Drive.Items[item.Id].Children.Request().GetAsync().Result;
                    GetNameFiles(graphClient, filesName, driveItemInfo, numberOfElements);
                }

                if(filesName.Count == numberOfElements)
                {
                    break;
                }
                
            }

            return filesName;
        }
    }

}

