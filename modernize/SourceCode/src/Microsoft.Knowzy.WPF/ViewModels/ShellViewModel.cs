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

using System.Collections.Generic;
using Caliburn.Micro;
using Microsoft.Knowzy.WPF.Messages;

namespace Microsoft.Knowzy.WPF.ViewModels
{
    public class ShellViewModel : Conductor<Screen>, IHandle<EditItemMessage>, IHandle<UpdateLanesMessage>, IHandle<OpenAboutMessage>, IHandle<OpenLoginMessage>,
        IHandle<OpenDocumentationMessage>
    {
        private readonly MainViewModel _mainViewModel;
        private readonly ListProductsViewModel _listProductsViewModel;
        private readonly KanbanViewModel _kanbanViewModel;
        private readonly EditItemViewModel _editItemViewModel;
        private readonly LoginViewModel _loginViewModel;
        private readonly AboutViewModel _aboutViewModel;
        private readonly DocumentationViewModel _documentationViewModel;
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _windowManager;

        public ShellViewModel(MainViewModel mainViewModel, ListProductsViewModel listProductsViewModel, KanbanViewModel kanbanViewModel, 
            EditItemViewModel editItemViewModel, LoginViewModel loginViewModel, AboutViewModel aboutViewModel, DocumentationViewModel documentationViewModel,
            IEventAggregator eventAggregator, IWindowManager windowManager)
        {
            _mainViewModel = mainViewModel;
            _listProductsViewModel = listProductsViewModel;
            _kanbanViewModel = kanbanViewModel;
            _editItemViewModel = editItemViewModel;
            _loginViewModel = loginViewModel;
            _aboutViewModel = aboutViewModel;
            _documentationViewModel = documentationViewModel;
            _eventAggregator = eventAggregator;
            _windowManager = windowManager;
        }

        protected override void OnViewAttached(object view, object context)
        {
            DisplayName = Localization.Resources.Title_Shell;
            _eventAggregator.Subscribe(this);
            _mainViewModel.ScreenList = new List<Screen>{ _listProductsViewModel, _kanbanViewModel}; 
            ActivateItem(_mainViewModel);
            base.OnViewAttached(view, context);
        }

        protected override void OnDeactivate(bool close)
        {
            _eventAggregator.Unsubscribe(this);
            base.OnDeactivate(close);
        }

        public void Handle(EditItemMessage message)
        {
            _editItemViewModel.Item = message.Item;
            _windowManager.ShowDialog(_editItemViewModel);
        }

        public void Handle(UpdateLanesMessage message)
        {
            _kanbanViewModel.InitializeLanes();
            _mainViewModel.Save();
        }

        public void Handle(OpenAboutMessage message)
        {
            _windowManager.ShowDialog(_aboutViewModel);
        }

        public void Handle(OpenLoginMessage message)
        {
            _windowManager.ShowDialog(_loginViewModel);
        }

        public void Handle(OpenDocumentationMessage message)
        {
            _windowManager.ShowDialog(_documentationViewModel);
        }
    }
}
