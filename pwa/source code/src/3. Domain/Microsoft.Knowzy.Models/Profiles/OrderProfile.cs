// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Linq;
using AutoMapper;
using Microsoft.Knowzy.Domain;
using Microsoft.Knowzy.Models.ViewModels;

namespace Microsoft.Knowzy.Models.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrdersViewModel>();

            CreateMap<Order, OrderViewModel>()
                .ForMember(orderViewModel => orderViewModel.PostalCarrierName,
                    options => options.ResolveUsing(order => order.PostalCarrier.Name))
                .ForMember(orderViewModel => orderViewModel.PostalCarrierId,
                    options => options.ResolveUsing(order => order.PostalCarrier.Id))
                .ForMember(orderViewModel => orderViewModel.OrderLines,
                    options => options.MapFrom(order => order.OrderLines.OrderBy(orderLine => orderLine.Id)));
        }
    }
}
