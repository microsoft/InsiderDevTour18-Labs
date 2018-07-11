// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Microsoft.Graph.HOL.Helpers
{
    using Microsoft.Graph.HOL.ViewModels;
    using Newtonsoft.Json;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;

    public class DataSyncHelper
    {
        public static async Task SaveSettingsInOneDrive(SettingsModel settingsModel)
        {
            throw new NotImplementedException();
        }

        public static async Task<SettingsModel> GetSettingsInOneDrive()
        {
            return new SettingsModel();
        }

        private static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        private static string DeserializeFromStream(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
