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
using Microsoft.Knowzy.Common.Contracts.Helpers;
using Microsoft.Knowzy.WPF.ViewModels;
using Microsoft.Knowzy.WPF.ViewModels.Models;
using System.Diagnostics;
using System.Linq;

namespace Microsoft.Knowzy.WPF.Views
{
    public partial class MainView
    {
        public MainView()
        {
            DataContextChanged += MainView_DataContextChanged;

            InitializeComponent();
        }

        private void MainView_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null || !(e.NewValue is MainViewModel))
            {
                return;
            }

            var viewModel = e.NewValue as MainViewModel;
            viewModel.PropertyChanged += (_, args) =>
            {
                // TODO
            };
        }
    }
}
