using System.Collections.Generic;

namespace ContosoIT.Models
{
    public static class DetectionConstants
    {
        public const string OnnxModelFileName = "Surface.onnx";

        public const string SurfaceProTag = "surface-pro";

        public const string SurfaceStudioTag = "surface-studio";

        public const string SurfacePro = "Surface Pro";

        public const string SurfaceStudio = "Surface Studio";

        public static List<string> SurfaceProSuggestions = new List<string>
        {
            "Cracked screen and physical damage",
            "Surface Pro update history",
            "Download a recovery image for your Surface",
            "Creating and using a USB recovery drive",
            "Install Surface and Windows updates",
            "Trouble installing Surface updates?",
            "Surface documents",
            "FAQ: Protecting your data if you send your Surface in for service",
            "How to get service for Surface",
            "How to prepare your Surface for service",
            "Set up your Surface after service",
            "Download drivers and firmware for Surface"
        };

        public static List<string> SurfaceStudioSuggestions = new List<string>
        {
            "Surface Studio update history",
            "Download a recovery image for your Surface",
            "Creating and using a USB recovery drive",
            "Install Surface and Windows updates",
            "Trouble installing Surface updates?",
            "Surface documents",
            "FAQ: Protecting your data if you send your Surface in for service",
            "How to get service for Surface",
            "How to prepare your Surface for service",
            "Set up your Surface after service",
            "Download drivers and firmware for Surface",
            "Install Wireless Drivers on Surface"
        };
    }
}
