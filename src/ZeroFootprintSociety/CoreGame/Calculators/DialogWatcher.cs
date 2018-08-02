using System.Collections.Generic;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.Characters.Prefabs;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.GUI.Hud;

namespace ZeroFootPrintSociety.CoreGame.Calculators
{
    public class DialogWatcher
    {
        private readonly Dictionary<string, List<Dialog>> _convos = new Dictionary<string, List<Dialog>>
        {
            {
                "dark alley",
                new List<Dialog>
                {
                    new Dialog { CharacterImage = MainChar.Bust, Message = "The ZantoCorp building is up ahead. Getting past these ZantoCorp bakebrains should be easy." },
                    new Dialog { CharacterImage = MainChar.Bust, Message = "We've gotta get past the Elite Guard. " +
                                                                           "Then we should be able to use the codes Wilson sent us to access the building." },
                    new Dialog { CharacterImage = Sidechick.Bust, Message = "I wouldn't wanna be on shift as a ZantoCorp guard tonight. Let's go!" },
                } 
            },
            {
                "corp",
                new List<Dialog>
                {
                    new Dialog { CharacterImage = MainChar.Bust, Message = "The Nanite Control Central is hiding in the back, we are going need to be quiet if we're gonna get through." },
                    new Dialog { CharacterImage = Sidechick.Bust, Message = "You know me -- silent as a bomb!" },
                }
            },
        };

        private readonly List<string> _completedDialogs = new List<string>();

        private List<Dialog> _dialogToShowAtEndOfMovement = new List<Dialog>();
        private bool _shouldShowDialogOnMovementResolved = false;

        public DialogWatcher()
        {
            Event.Subscribe<Moved>(e => OnMove(e.Character), this);
            Event.Subscribe<MovementFinished>(_ => OnMovementFinished(), this);
        }

        private void OnMove(Character character)
        {
            if (character.IsFriendly && _convos.ContainsKey(character.CurrentTile.Dialog) && !_completedDialogs.Contains(character.CurrentTile.Dialog))
            {
                _shouldShowDialogOnMovementResolved = true;
                _dialogToShowAtEndOfMovement = _convos[character.CurrentTile.Dialog];
                _completedDialogs.Add(character.CurrentTile.Dialog);
            }
        }

        private void OnMovementFinished()
        {
            if (_shouldShowDialogOnMovementResolved)
            {
                Event.Publish(new DialogStarted { Dialogs = _dialogToShowAtEndOfMovement });
                _shouldShowDialogOnMovementResolved = false;
            }
        }
    }
}
