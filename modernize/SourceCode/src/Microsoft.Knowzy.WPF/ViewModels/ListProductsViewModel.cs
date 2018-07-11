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

// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.

// ******************************************************************

using Caliburn.Micro;
using Microsoft.Knowzy.WPF.ViewModels.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace Microsoft.Knowzy.WPF.ViewModels
{
    public sealed class ListProductsViewModel : Screen
    {
        private readonly MainViewModel _mainViewModel;

        public ListProductsViewModel(MainViewModel mainViewModel, IEventAggregator eventAggregator)
        {
            _mainViewModel = mainViewModel;
            DisplayName = Localization.Resources.ListView_Tab;
        }

        public ObservableCollection<ItemViewModel> DevelopmentItems => _mainViewModel.DevelopmentItems;           

        public void SortProducts(object sortField, bool isSortAscending)
        {
            var field = sortField as string;
            if (string.IsNullOrEmpty(field)) return;

            var view = CollectionViewSource.GetDefaultView(DevelopmentItems);

            var sortDirection = isSortAscending ? ListSortDirection.Ascending : ListSortDirection.Descending;
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription(field, sortDirection));
        }
    }
}
