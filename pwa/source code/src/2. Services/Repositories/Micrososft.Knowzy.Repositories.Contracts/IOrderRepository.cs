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

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Knowzy.Domain;
using Microsoft.Knowzy.Models.ViewModels;

namespace Micrososft.Knowzy.Repositories.Contracts
{
    public interface IOrderRepository
    {
        Task<IEnumerable<ShippingsViewModel>> GetShippings();
        Task<IEnumerable<ReceivingsViewModel>> GetReceivings();
        Task<ShippingViewModel> GetShipping(string orderId);
        Task<ReceivingViewModel> GetReceiving(string orderId);
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<PostalCarrier>> GetPostalCarriers();
        Task<IEnumerable<Customer>> GetCustomers();
        Task<int> GetShippingCount();
        Task<int> GetReceivingCount();
        Task<int> GetProductCount();
        Task AddShipping(Shipping shipping);
        Task UpdateShipping(Shipping shipping);
        Task AddReceiving(Receiving receiving);
        Task UpdateReceiving(Receiving receiving);
    }
}
