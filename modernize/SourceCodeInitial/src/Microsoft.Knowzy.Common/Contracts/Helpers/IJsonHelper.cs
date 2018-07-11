// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Microsoft.Knowzy.Common.Contracts.Helpers
{
    public interface IJsonHelper
    {
        T Deserialize<T>(string serializedObject);
        string Serialize<T>(T objectToSerialize);
    }
}