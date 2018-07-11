// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq;

namespace Microsoft.Knowzy.Repositories.Core
{
    public static class OrderRepositoryHelper
    {
        public static string GenerateString(int size)
        {
            var random = new Random();
            var alphabet = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var chars = Enumerable.Range(0, size)
                .Select(x => alphabet[random.Next(0, alphabet.Length)]);
            return new string(chars.ToArray());
        }
    }
}
