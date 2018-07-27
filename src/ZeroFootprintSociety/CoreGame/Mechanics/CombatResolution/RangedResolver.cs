using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroFootPrintSociety.Characters;

namespace ZeroFootPrintSociety.CoreGame.Mechanics.CombatResolution
{
    /// <summary>
    /// When a ranged attack occurs:
    /// The acting character resolves their shot(s) and damage first.
    /// 
    /// If the defending character is alive and has a ranged weapon equipped, he fires back and resolve his shot and damage.
    /// 
    /// Chance to hit per bullet: (Character ACC + Weapon ACC - COVER - Defender AGI)%
    /// Damage: Weapon DMG
    ///
    /// TODO: Fire events for each stuff happening.
    /// </summary>
    public static class RangedResolver
    {
        public static void ResolveOneShot(Character attackerCharacter, Character defenderCharacter)
        {
            int ChanceToHit = RangedResolver.ChanceToHit(attackerCharacter, defenderCharacter);
        }

        private static int ChanceToHit(Character attackerCharacter, Character defenderCharacter)
        {
            int chanceToHit = 0;

            //attackerCharacter.Stats.AccuracyPercent

            return chanceToHit;
        }
    }
}
