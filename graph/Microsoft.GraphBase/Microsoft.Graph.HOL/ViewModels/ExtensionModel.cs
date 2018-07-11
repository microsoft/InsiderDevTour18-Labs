// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Microsoft.Graph.HOL.ViewModels
{
    using System.Collections.Generic;

    public class ExtensionModel
    {
        public string Display { get; set; }

        // The properties of an entity that display in the UI.
        public Dictionary<string, object> Properties;

        public ExtensionModel()
        {
            Properties = new Dictionary<string, object>();
        }
    }
}