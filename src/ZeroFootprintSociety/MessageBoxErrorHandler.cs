using MonoDragons.Core.Errors;
using System;
using System.Windows.Forms;

namespace ZeroFootPrintSociety
{
    class MessageBoxErrorHandler : IErrorHandler
    {
        public void ResolveError(Exception ex)
        {

            var inner = ex;
            while (inner.InnerException != null)
                inner = inner.InnerException;

            MessageBox.Show(inner.Message + Environment.NewLine + inner.StackTrace);
            Environment.Exit(1);
        }
    }
}
