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
