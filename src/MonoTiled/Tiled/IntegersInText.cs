using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TiledExample.Tiled
{
    public class IntegersInText
    {
        private readonly string _text;

        public IntegersInText(string text)
        {
            _text = text;
        }

        public IEnumerable<int> Get()
        {
            return Regex.Split(_text, @"\D+")
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => int.Parse(x));
        }
    }
}
