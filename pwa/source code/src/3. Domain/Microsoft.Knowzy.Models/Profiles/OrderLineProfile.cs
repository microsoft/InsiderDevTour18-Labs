// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;
using Microsoft.Knowzy.Domain;
using Microsoft.Knowzy.Models.ViewModels;

namespace Microsoft.Knowzy.Models.Profiles
{
    public class OrderLineProfile : Profile
    {
        public OrderLineProfile()
        {
            CreateMap<OrderLine, OrderLineViewModel>()
                .ForMember(orderLineViewModel => orderLineViewModel.ProductImage,
                    options => options.ResolveUsing(orderLine => orderLine.Product.Image))
                .ForMember(orderLineViewModel => orderLineViewModel.ProductPrice,
                    options => options.ResolveUsing(orderLine => orderLine.Product.Price)); ;

            CreateMap<OrderLineViewModel, OrderLine>()
                .ForMember(orderLine => orderLine.Price,
                    options => options.ResolveUsing(
                        orderLineViewModel => orderLineViewModel.Quantity * orderLineViewModel.ProductPrice));
        }
    }
}
