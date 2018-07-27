using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame.Mechanics.Events
{
    public class OverwatchEvent : CombatEvent
    {
        public Character FoundCharacter { get; }

        public OverwatchEvent(Character foundCharacter)
        {
            FoundCharacter = foundCharacter;
            // TODO: Add more props to event.
        }
    }
}
