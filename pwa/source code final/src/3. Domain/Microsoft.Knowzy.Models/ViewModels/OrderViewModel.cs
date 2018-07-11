// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Knowzy.Domain;
using Microsoft.Knowzy.Models.ViewModels.Validators;

namespace Microsoft.Knowzy.Models.ViewModels
{
    public class OrderViewModel : IValidatableObject
    {
        [Display(Name = "Order Number:")]
        public string Id { get; set; }
        [Display(Name = "Company name:")]
        public string CompanyName { get; set; }
        [Display(Name = "Address:")]
        public string Address { get; set; }
        [Display(Name = "Contact Person:")]
        public string ContactPerson { get; set; }
        [Display(Name = "Contact email:")]
        public string Email { get; set; }
        [Display(Name = "Phone number:")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Tracking:")]
        public string Tracking { get; set; }
        [Display(Name = "Postal Carrier:")]
        public string PostalCarrierName { get; set; }
        public int PostalCarrierId { get; set; }
        [Display(Name = "Status:")]
        public OrderStatus Status { get; set; }
        public string CustomerId { get; set; }

        public List<OrderLineViewModel> OrderLines { get; set; }


        public int MaxAvailableItems { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new OrderViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
