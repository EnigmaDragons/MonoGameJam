using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoDragons.Core.Errors
{
    public interface IErrorHandler
    {
        Task ResolveError(Exception ex);
        Task ResolveError(Game game, Exception ex);
    }
}
