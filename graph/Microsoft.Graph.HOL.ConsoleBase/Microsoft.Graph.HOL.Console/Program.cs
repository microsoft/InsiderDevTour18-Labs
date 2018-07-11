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

namespace Microsoft.Graph.HOL.Console
{
    using System;

    class Program
    {
        private static GraphServiceClient graphClient;
        private static int numberOfelements;

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Welcome to the C# Console Graph Sample!\n");
                Console.WriteLine("In this example we see:!\n");
                Console.WriteLine("***************\n");
                Console.WriteLine("1-Authenticate user via Graph API\n");
                Console.WriteLine("2-Get items from OneDrive via Graph API\n");
                Console.WriteLine("***************\n");

                Console.WriteLine("Authenticate user\n");                

                graphClient = AuthenticationHelper.GetAuthenticatedClient();
                var user = graphClient.Me.Request().GetAsync().Result;

                Console.WriteLine("Hello, " + user.DisplayName + ". Would you like to see your OneDrive items?[Y] Yes or [N] No\n");
                var select = Console.ReadKey();
                Console.WriteLine("\n");

                if (select.Key.Equals(ConsoleKey.Y))
                {
                    GetNumberOfElementsToShow();

                    Console.WriteLine("Download OneDrive Items.\n");
                    ShowOneDriveItem();

                    Console.WriteLine("End Download items from OneDrive.\n");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error detail: {(ex.InnerException != null ? ex.InnerException.Message : ex.Message)}");
                Console.ResetColor();
            }
            finally
            {
                Console.WriteLine("Press any key to finish\n");
                Console.ReadKey();
            }
        }

        private static void ShowOneDriveItem()
        {
            var filesName = OneDriveHelper.GetItems(numberOfelements);

            foreach (var fileName in filesName)
            {
                Console.WriteLine($"File Name: {fileName}");
            }
        }

        private static void GetNumberOfElementsToShow()
        {
            bool correctNumber = false;

            while (!correctNumber)
            {
                Console.WriteLine("How many documents from OneDrive want to show? (Enter a number please)\n");
                var number = Console.ReadLine();

                try
                {
                    numberOfelements = Convert.ToInt32(number);
                    correctNumber = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Enter a valid number please\n");
                }
            }
        }
    }
}
