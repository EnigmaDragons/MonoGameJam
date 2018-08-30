namespace MonoDragons.Core.Common
{
    public static class StringExtensions
    {
        public static string PadLeft(this object obj, int width, char padChar)
            => obj.ToString().PadLeft(width, padChar);
    }
}