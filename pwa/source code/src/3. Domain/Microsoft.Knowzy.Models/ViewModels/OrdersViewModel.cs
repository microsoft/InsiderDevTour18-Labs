// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations;
using Microsoft.Knowzy.Domain;

namespace Microsoft.Knowzy.Models.ViewModels
{
    public class OrdersViewModel
    {
        [Display(Name = "Order No.")]
        public string Id { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        public string Tracking { get; set; }
        public OrderStatus Status { get; set; }
    }
}
