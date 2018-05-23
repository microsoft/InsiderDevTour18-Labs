namespace Microsoft.Graph.HOL.Utils
{
    using System;

    public static class ExceptionExtension
    {
        public static string GetMessage(this Exception ex)
        {
            return ex.InnerException != null ? ex.InnerException.Message : ex.Message;
        }
    }
}
