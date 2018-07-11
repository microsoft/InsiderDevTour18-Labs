// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Microsoft.Graph.HOL.Utils
{
    public static class FileExtension
    {
        public static string GetExtension(this string fileName)
        {
            var pos = fileName.LastIndexOf(".");
            if (pos == 0)
            {
                return fileName;
            }

            return fileName.Substring(pos + 1);
        }

        public static string GetFileName(this string fileName)
        {
            var pos = fileName.LastIndexOf(".");
            if (pos == 0)
            {
                return fileName;
            }

            return fileName.Substring(0, pos - 1);
        }

        public static string GetIcon(this string fileName)
        {
            var extension = fileName.GetExtension();
            switch (extension)
            {
                case "doc":
                case "docx":
                    return @"Icons\word.png";
                case "xls":
                case "xlsx":
                    return @"Icons\excel.png";
                case "ppt":
                case "pptx":
                    return @"Icons\pptx.png";
                case "pdf":
                    return @"Icons\pdf.jpg";
                case "jpg":
                case "jpeg":
                    return @"Icons\jpeg.png";
                case "vsdx":
                    return @"Icons\visio.png";
                case "png":
                    return @"Icons\visio.jpg";
                default:
                    return @"Icons\file.jpg";
            }
        }
    }
}
