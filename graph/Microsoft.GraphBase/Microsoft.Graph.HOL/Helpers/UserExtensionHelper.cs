// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Microsoft.Graph.HOL
{
    using Microsoft.Graph.HOL.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Windows.ApplicationModel;
    using Windows.ApplicationModel.UserActivities;
    using Windows.UI.Shell;

    public static class UserExtensionHelper
    {
        public static string option = string.Empty;

        public static async Task SetExtension(string extensionName, Dictionary<string, object> data)
        {
            throw new NotImplementedException();
        }

        public static async Task<List<ExtensionModel>> GetOpenExtensionsForMe()
        {
            try
            {
                var graphClient = AuthenticationHelper.GetAuthenticatedClient();

                var result = await graphClient.Me.Extensions.Request().GetAsync();

                return result.CurrentPage.Select(r => new ExtensionModel()
                {
                    Display = r.Id,
                    Properties = (Dictionary<string, object>)r.AdditionalData
                }).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error to get sextension in graph: " + ex.Message);
                throw;
            }
        }

        public static async Task DeleteOpenExtensionForMe(string extensionName)
        {
            try
            {
                var graphClient = AuthenticationHelper.GetAuthenticatedClient();
                await graphClient.Me.Extensions[extensionName].Request().DeleteAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error to delete extension in graph: " + ex.Message);
                throw;
            }
        }

        public static async Task CreateActivity()
        {
            throw new NotImplementedException();
        }

        public static async void PickupWhereYouLeft(string pickUpOption)
        {
        }
    }
}
