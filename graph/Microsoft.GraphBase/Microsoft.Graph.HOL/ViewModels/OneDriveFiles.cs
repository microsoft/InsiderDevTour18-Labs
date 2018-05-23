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
