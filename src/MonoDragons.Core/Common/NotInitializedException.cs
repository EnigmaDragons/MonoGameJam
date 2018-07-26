using System;

namespace MonoDragons.Core.Common
{
    public sealed class NotInitializedException : Exception
    {
        public NotInitializedException(string elementName)
            : base ($"{elementName} was not initialized") { }
    }
}
