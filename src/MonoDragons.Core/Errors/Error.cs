using System;
using System.Threading.Tasks;

namespace MonoDragons.Core.Errors
{
    public static class Error
    {
        public static void Handle(Action action, Action<Exception> onError)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                onError(ex);
            }
        }

        public static async Task HandleAsync(Action action, Func<Exception, Task> onError)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                await onError(ex);
            }
        }
    }
}
