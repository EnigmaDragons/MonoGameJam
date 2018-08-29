using System.Collections.Generic;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame.Mechanics.Resolution
{
    public sealed class GainXpOnEnemyDeath
    {
        private static readonly Dictionary<int, int> RawXpByLevelDifference = new Dictionary<int, int>
        {
            {-12, 140},
            {-11, 130},
            {-10, 120},
            {-09, 110},
            {-08, 100},
            {-07, 90},
            {-06, 80},
            {-05, 70},
            {-04, 60},
            {-03, 50},
            {-02, 40},
            {-01, 30},
            {00, 20},
            {01, 17},
            {02, 14},
            {03, 11},
            {04, 08},
            {05, 05},
            {06, 03},
            {07, 01},
            {08, 01},
            {09, 01},
            {10, 01},
            {11, 01},
            {12, 01},
        };
        
        public GainXpOnEnemyDeath() => Event.Subscribe<CharacterDeceased>(OnCharacterDeceased, this);

        private void OnCharacterDeceased(CharacterDeceased e)
        {
            if (e.Killer.IsFriendly)
                e.Killer.Notify(new XpGained { Character = e.Killer, XpAmount = RawXpByLevelDifference[KillerLevelDifference(e)]});
        }

        private int KillerLevelDifference(CharacterDeceased e) => e.Killer.Level - e.Victim.Level;
    }
}