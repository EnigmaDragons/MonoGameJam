using System;

namespace MonoDragons.Core
{
    public sealed class NotInitializedException : Exception
    {
        public NotInitializedException(string elementName)
            : base ($"{elementName} was not initialized") { }
    }
}
