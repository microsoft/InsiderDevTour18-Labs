// ******************************************************************

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
using Microsoft.Knowzy.Authentication;
using Microsoft.Knowzy.Common.Contracts;
using Microsoft.Knowzy.WPF.Messages;
using Microsoft.Knowzy.WPF.ViewModels.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Microsoft.Knowzy.WPF.ViewModels
{
    public class MainViewModel : Screen
    {
        private const int TabListView = 0;
        private const int TabGridView = 1;
        private readonly IDataProvider _dataProvider;
        private readonly IEventAggregator _eventAggregator;
        private readonly IAuthenticationService _authenticationService;


        public MainViewModel(IDataProvider dataProvider, IEventAggregator eventAggregator, IAuthenticationService authenticationService)
        {
            _dataProvider = dataProvider;
            _eventAggregator = eventAggregator;
            _authenticationService = authenticationService;
        }
        
        private int _selectedIndexTab;

        public int SelectedIndexTab
        {
            get => _selectedIndexTab;
            set
            {
                _selectedIndexTab = value;
                NotifyOfPropertyChange(() => SelectedIndexTab);
            }
        }

        public List<Screen> ScreenList { get; set; }

        public ObservableCollection<ItemViewModel> DevelopmentItems { get; set; } = new ObservableCollection<ItemViewModel>();

        public string LoggedUser
        {
            get
            {
                NotifyOfPropertyChange(() => HasLoggedUser);
                return _authenticationService.UserLogged;
            }
        }

        public bool HasLoggedUser => !string.IsNullOrWhiteSpace(_authenticationService.UserLogged);

        private bool _showAdaptiveCard;

        public bool ShowAdaptiveCard
        {
            get => _showAdaptiveCard;
            set
            {
                _showAdaptiveCard = value;
                NotifyOfPropertyChange(() => ShowAdaptiveCard);
            }
        }

        protected override void OnViewAttached(object view, object context)
        {
            foreach (var item in _dataProvider.GetData())
            {
                DevelopmentItems.Add(new ItemViewModel(item, _eventAggregator));
            }
            
            base.OnViewAttached(view, context);
        }

        public void ShowListView()
        {
            if (SelectedIndexTab == TabListView) return;
            SelectedIndexTab --;
        }

        public void ShowGridView()
        {
            if (SelectedIndexTab == TabGridView) return;
            SelectedIndexTab ++;
        }

        public void NewItem()
        {
            var item = new ItemViewModel(_eventAggregator);
            _eventAggregator.PublishOnUIThread(new EditItemMessage(item));

            if (item.Id == null) return;
            DevelopmentItems.Add(item);

            // This prop. is just used to fire a visibility change in the UI, it should be improved in a real scenario
            ShowAdaptiveCard = true;
        }

        public void Login()
        {
            _eventAggregator.PublishOnUIThread(new OpenLoginMessage());
        }

        public void Logout()
        {
            _authenticationService.Logout();
            NotifyOfPropertyChange(() => LoggedUser);
        }

        public void About()
        {
            _eventAggregator.PublishOnUIThread(new OpenAboutMessage());
        }

        public void Documentation()
        {
            _eventAggregator.PublishOnUIThread(new OpenDocumentationMessage());
        }

        public void Exit()
        {
            Application.Current.Shutdown();
        }

        public void Save()
        {
            var products = DevelopmentItems?.Select(item => item.Product).ToArray();
            _dataProvider.SetData(products);
        }

        public void UpdateNotes(string notes)
        {
            var lastItem = DevelopmentItems.LastOrDefault();

            if (lastItem != null)
            {
                lastItem.Notes = notes;
            }
        }
    }
}
