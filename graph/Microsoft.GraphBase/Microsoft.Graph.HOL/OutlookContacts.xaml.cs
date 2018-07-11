// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
namespace Microsoft.Graph.HOL
{
    using Microsoft.Graph.HOL.Utils;
    using Microsoft.Graph.HOL.ViewModels;
    using System;
    using System.Collections.ObjectModel;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using System.Linq;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OutlookContacts : Page
    {
        private ObservableCollection<ContactsModel> _items = new ObservableCollection<ContactsModel>();

        public ObservableCollection<ContactsModel> Items
        {
            get { return this._items; }
        }

        public OutlookContacts()
        {
            this.InitializeComponent();
            Progress.IsActive = true;
        }

        private async void Page_Loading(FrameworkElement sender, object args)
        {
            try
            {
                var contacts = await OutlookHeñper.GetContacts();
                foreach (var contact in contacts.Where(x => x.EmailAddresses.Count() > 0))
                {
                    Items.Add(new ContactsModel()
                    {
                        ContactInfo = $"{contact.DisplayName} - {contact.EmailAddresses.FirstOrDefault().Address}"
                    });
                }
            }
            catch (Exception ex)
            {
                InfoText.Text = $"OOPS! An error ocurred: {ex.GetMessage()}";
            }
            finally
            {
                Progress.IsActive = false;
            }
        }
    }
}
