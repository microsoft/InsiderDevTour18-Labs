// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using AutoMapper;
using Microsoft.Knowzy.Domain;
using Microsoft.Knowzy.Models.ViewModels;

namespace Microsoft.Knowzy.Models.Profiles
{
    public class ReceivingProfile : Profile
    {
        public ReceivingProfile()
        {
            CreateMap<Receiving, ReceivingsViewModel>()
                .IncludeBase<Order, OrdersViewModel>();

            CreateMap<Receiving, ReceivingViewModel>()
                .IncludeBase<Order, OrderViewModel>().ReverseMap();
        }
    }
}
