// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Microsoft.Knowzy.Common.Contracts.Helpers
{
    public interface IFileHelper
    {
        string ActualPath { get; }
        string ReadTextFile(string filePath);
        void WriteTextFile(string filePath, string content);
    }
}