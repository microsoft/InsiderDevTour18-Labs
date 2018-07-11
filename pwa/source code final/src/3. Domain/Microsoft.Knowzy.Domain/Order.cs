// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Knowzy.Domain
{
    public abstract class Order
    {
        public string Id { get; set; }
        public DateTime? TimeStamp { get; set; }
        public string CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Tracking { get; set; }
        public string Type { get; set; }
        public int PostalCarrierId { get; set; }
        public virtual PostalCarrier PostalCarrier { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime? StatusUpdated { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
        public decimal Total => OrderLines.Sum(orderLine => orderLine.Price);
    }
}
