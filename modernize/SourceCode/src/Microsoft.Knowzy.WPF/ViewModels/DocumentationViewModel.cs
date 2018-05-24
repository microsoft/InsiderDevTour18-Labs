using Caliburn.Micro;

namespace Microsoft.Knowzy.WPF.ViewModels
{
    public class DocumentationViewModel : Screen
    {
        private const string _url = "https://www.youtube.com/microsoft";

        public string URL => _url;
    }
}
