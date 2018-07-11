// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using FluentValidation;

namespace Microsoft.Knowzy.Models.ViewModels.Validators
{
    public class OrderViewModelValidator : AbstractValidator<OrderViewModel>
    {
        public OrderViewModelValidator()
        {            
            RuleFor(order => order.Address).NotEmpty().WithMessage("Address cannot be empty");
            RuleFor(order => order.CompanyName).NotEmpty().WithMessage("Company Name cannot be empty");
            RuleFor(order => order.Email).EmailAddress().WithMessage("Introduce a valid email").NotEmpty().WithMessage("Email cannot be empty");
            RuleFor(order => order.ContactPerson).NotEmpty().WithMessage("Contact Person cannot be empty");
            RuleFor(order => order.PhoneNumber).NotEmpty().WithMessage("Phone Number cannot be empty");
            RuleFor(order => order.PostalCarrierId).NotEmpty().WithMessage("Postal Carrier cannot be empty");
        }
    }
}
