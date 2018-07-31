using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeroFootPrintSociety.Characters.Creator
{
    public class Perk
    {
        public string Name { get; }
        public string Description { get; }
        public Action OnClick { get; }

        public Perk(string name, string description, Action onClick)
        {
            Name = name;
            Description = description;
            OnClick = onClick;
        }
    }
}
