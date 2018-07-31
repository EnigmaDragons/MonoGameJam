
namespace ZeroFootPrintSociety.UIEffects
{
    class FXString
    {
        public string Name { get; }
        public string Color { get; }

        /// <summary>
        /// If the FX has an offset of half tile to the right or it's centered
        /// </summary>
        public bool Offset { get; }

        public FXString(string raw)
        {
            Name = raw.Split('-')[0];
            Color = raw.IndexOf('-') > 0 ? raw.Split('-')[1] : "None";
            Offset = raw.StartsWith("o");
        }
    }
}
