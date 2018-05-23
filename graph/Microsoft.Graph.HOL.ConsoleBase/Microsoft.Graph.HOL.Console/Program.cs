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
