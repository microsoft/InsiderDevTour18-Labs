// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace Microsoft.Graph.HOL.ViewModels
{
    public class OneDriveFiles
    {
        public string Name { get; set; }

        public string IconPath { get; set; }

        public DriveItem ItemInDrive { get; set; }
    }
}
