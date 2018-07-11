// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.Knowzy.WPF.ViewModels;
using System.Windows;

namespace Microsoft.Knowzy.WPF.Views
{
    /// <summary>
    /// Interaction logic for DocumentationView.xaml
    /// </summary>
    public partial class DocumentationView : Window
    {
        public DocumentationView()
        {
            InitializeComponent();

            DataContextChanged += DocumentationView_DataContextChanged;
        }

        private void DocumentationView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null && e.NewValue is DocumentationViewModel viewModel)
            {
                webBrowser.Navigate(viewModel.URL);
            }
        }
    }
}
