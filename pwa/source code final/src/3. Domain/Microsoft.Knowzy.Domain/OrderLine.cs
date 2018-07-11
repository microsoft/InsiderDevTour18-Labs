// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Microsoft.Knowzy.Domain
{
    public class OrderLine
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public virtual Order Order { get; set; }
        public string ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
