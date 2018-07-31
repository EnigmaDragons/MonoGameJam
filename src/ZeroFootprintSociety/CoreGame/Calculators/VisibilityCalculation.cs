using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using ZeroFootPrintSociety.Characters;

namespace ZeroFootPrintSociety.CoreGame.Calculators
{
    public class VisibilityCalculation
    {
        private readonly Character _character;

        public VisibilityCalculation(Character character)
        {
            _character = character;
        }

        public DictionaryWithDefault<Point, bool> Calculate()
        {
            DictionaryWithDefault<Point, bool> canSee = new DictionaryWithDefault<Point, bool>(false);
            new PointRadiusCalculation(_character.CurrentTile.Position, 10).Calculate()
                .Where(x => GameWorld.Map.Exists(x))
                .Where(x => new ShotCalculation(_character.CurrentTile, GameWorld.Map[x]).BestShot().BlockChance != 100)
                .ForEach(x => canSee[x] = true);
            return canSee;
        }
    }
}
