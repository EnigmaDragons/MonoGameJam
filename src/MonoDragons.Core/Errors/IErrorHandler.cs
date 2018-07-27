using System;
using System.Threading.Tasks;

namespace MonoDragons.Core.Errors
{
    public interface IErrorHandler
    {
        Task ResolveError(Exception ex);
    }
}
