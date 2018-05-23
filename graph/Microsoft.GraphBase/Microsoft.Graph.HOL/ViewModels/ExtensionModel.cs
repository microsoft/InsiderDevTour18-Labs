namespace Microsoft.Graph.HOL.ViewModels
{
    using System.Collections.Generic;

    public class ExtensionModel
    {
        public string Display { get; set; }

        // The properties of an entity that display in the UI.
        public Dictionary<string, object> Properties;

        public ExtensionModel()
        {
            Properties = new Dictionary<string, object>();
        }
    }
}