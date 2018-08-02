using MonoDragons.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using ZeroFootPrintSociety.Characters.Prefabs;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.Characters
{
    class CharacterSpawning
    {
        public List<Character> GetCharacters(GameMap map)
        {
            var chars = map.Tiles.Where(x => !x.SpawnCharacter.Equals("None"))
                .Select(t =>
                {
                    if (t.SpawnCharacter.Equals("main", StringComparison.InvariantCultureIgnoreCase))
                        return new MainChar(GameWorld.MainCharClass).Initialized(t);
                    if (t.SpawnCharacter.Equals("sidechick", StringComparison.InvariantCultureIgnoreCase))
                        return new Sidechick().Initialized(t);
                    if (t.SpawnCharacter.Equals("corpsec1", StringComparison.InvariantCultureIgnoreCase))
                        return new CorpSec1().Initialized(t);
                    if (t.SpawnCharacter.Equals("corpsec2", StringComparison.InvariantCultureIgnoreCase))
                        return new CorpSec2(t.MustKill).Initialized(t);
                    if (t.SpawnCharacter.Equals("corpsec3", StringComparison.InvariantCultureIgnoreCase))
                        return new CorpSec3(t.MustKill).Initialized(t);
                    Logger.WriteLine($"Unknown SpawnCharacter '{t.SpawnCharacter}'");
                    return new CorpSec1().Initialized(t);
                }).ToList();

            return chars;
        }
    }
}
