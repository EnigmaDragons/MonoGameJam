using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroFootPrintSociety.Characters.Teams;
using ZeroFootPrintSociety.CoreGame;

namespace ZeroFootPrintSociety.Characters
{
    /// <summary>
    /// Instance managing the `Character` object's team.
    /// </summary>
    public class CharacterTeam
    {
        public Team Team { get; set; }

        public CharacterTeam(Team team)
        {
            Team = Team;
            Team.CanShootAt();
        }


    }
}
