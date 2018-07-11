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

using AdaptiveCards;
using AdaptiveCards.Rendering;
using AdaptiveCards.Rendering.Wpf;
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
        private readonly AdaptiveCardRenderer _renderer;
        private readonly AdaptiveCard _card;

        private AdaptiveTextBlock _cardTitleTextBlock;
        private RenderedAdaptiveCard _renderedCard;

        public MainView()
        {
            DataContextChanged += MainView_DataContextChanged;

            InitializeComponent();

            string json;

            try
            {
                var fileHelper = IoC.Get<IFileHelper>();
                json = fileHelper.ReadTextFile(fileHelper.ActualPath + "Assets/WindowsNotificationHostConfig.json");
            }
            catch
            {
                json = null;
            }

            if (json == null)
            {
                _renderer = new AdaptiveCardRenderer();
            }
            else
            {
                var hostConfig = AdaptiveHostConfig.FromJson(json);
                _renderer = new AdaptiveCardRenderer(hostConfig);
            }

            _card = CreateCard();
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
                if (args.PropertyName == nameof(viewModel.ShowAdaptiveCard) && viewModel.ShowAdaptiveCard)
                {
                    var lastItem = viewModel.DevelopmentItems.LastOrDefault();

                    if (lastItem == null)
                    {
                        return;
                    }

                    UpdateAdaptiveCard(lastItem);
                }
            };
        }

        private void UpdateAdaptiveCard(ItemViewModel item)
        {
            _cardTitleTextBlock.Text = $"{item.Name}, expected to start at " +
                $"{item.DevelopmentStartDate.ToShortDateString()} and completed at " +
                $"{item.ExpectedCompletionDate.ToShortDateString()}.";

            if (_renderedCard != null)
            {
                adaptiveCardContainer.Children.Clear();
                _renderedCard.OnAction -= RenderedCard_OnAction;
                _renderedCard = null;
            }

            try
            {
                _renderedCard = _renderer.RenderCard(_card);
                _renderedCard.OnAction += RenderedCard_OnAction;
                adaptiveCardContainer.Children.Add(_renderedCard.FrameworkElement);
            }
            catch (AdaptiveException exception)
            {
                Debug.WriteLine(exception);
            }
        }

        private AdaptiveCard CreateCard()
        {
            _cardTitleTextBlock = new AdaptiveTextBlock { Wrap = true };

            var card = new AdaptiveCard
            {
                Body =
                {
                    new AdaptiveTextBlock("You added a new product")
                    {
                        Size = AdaptiveTextSize.Medium,
                        Weight = AdaptiveTextWeight.Bolder
                    },
                    _cardTitleTextBlock
                },
                Actions =
                {
                    new AdaptiveSubmitAction { Id = "Ok", Title = "OK" },
                    new AdaptiveShowCardAction
                    {
                        Title = "Add some notes",
                        Card = new AdaptiveCard
                        {
                            Body =
                            {
                                new AdaptiveTextInput
                                {
                                    Id = "Notes",
                                    IsMultiline = true,
                                    Placeholder = "Type here"
                                }
                            },
                            Actions = { new AdaptiveSubmitAction { Title = "Save" } }
                        }
                    }
                }
            };

            return card;
        }

        private void RenderedCard_OnAction(RenderedAdaptiveCard sender, AdaptiveActionEventArgs e)
        {
            if (e.Action is AdaptiveSubmitAction submitAction)
            {
                var viewModel = DataContext as MainViewModel;

                if (submitAction.Id == "Ok")
                {
                    viewModel.ShowAdaptiveCard = false;
                    return;
                }

                var inputs = sender.UserInputs.AsDictionary();
                var notes = inputs["Notes"];
                viewModel.UpdateNotes(notes);
                viewModel.ShowAdaptiveCard = false;

                sender.OnAction -= RenderedCard_OnAction;
            }
        }
    }
}
