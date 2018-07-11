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
