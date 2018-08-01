using System;
using System.Diagnostics;

namespace MonoDragons.Core.Development
{
    public static class Perf
    {
        private static bool _isEnabled = true;

        public static void Disable()
        {
            _isEnabled = false;
        }

        public static void Time(string name, Action action, int minLoggingMillis = 8)
        {
            Time(name, () => { action(); return true; }, minLoggingMillis);
        }

        public static T Time<T>(string name, Func<T> getResult, int minLoggingMillis = 8)
        {
            if (!_isEnabled)
                return getResult();

            var start = DateTime.Now;
            var result = getResult();
            var duration = DateTime.Now - start;
            if (duration.TotalMilliseconds >= minLoggingMillis)
                Debug.WriteLine($"{name} - {duration.TotalMilliseconds}ms");
            return result;
        }
    }
}