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
