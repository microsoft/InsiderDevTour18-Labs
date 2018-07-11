// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Microsoft.Graph.HOL.Utils
{
    using System;

    public static class ExceptionExtension
    {
        public static string GetMessage(this Exception ex)
        {
            return ex.InnerException != null ? ex.InnerException.Message : ex.Message;
        }
    }
}
