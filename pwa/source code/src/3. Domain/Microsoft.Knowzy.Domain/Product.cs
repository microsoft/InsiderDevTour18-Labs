// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Microsoft.Knowzy.Domain
{
    public class Product
    {
        public string Id { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string QuantityInStock { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}