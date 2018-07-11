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
