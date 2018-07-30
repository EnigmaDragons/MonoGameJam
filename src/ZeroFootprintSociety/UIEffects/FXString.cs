
namespace ZeroFootPrintSociety.UIEffects
{
    class FXString
    {
        public string Name { get; }
        public string Color { get; }

        public FXString(string raw)
        {
            Name = raw.Split('-')[0];
            Color = raw.IndexOf('-') > 0 ? raw.Split('-')[1] : "None";
        }
    }
}
