using System;

namespace MonoDragons.Core.Errors
{
    public interface IErrorHandler
    {
        void ResolveError(Exception ex);
    }
}
