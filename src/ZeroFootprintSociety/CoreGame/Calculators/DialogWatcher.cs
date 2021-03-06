﻿using System.Collections.Generic;
using MonoDragons.Core;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.Characters.Prefabs;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.GUI.Hud;

namespace ZeroFootPrintSociety.CoreGame.Calculators
{
    public class DialogWatcher
    {
        private readonly Dictionary<string, List<Dialogue>> _convos = new Dictionary<string, List<Dialogue>>
        {
            {
                "dark alley",
                new List<Dialogue>
                {
                    new Dialogue { CharacterImage = MainChar.Bust, Message = "The ZantoCorp building is up ahead. Getting past these ZantoCorp bakebrains should be easy." },
                    new Dialogue { CharacterImage = MainChar.Bust, Message = "We've gotta get past the Elite Guard. " +
                                                                           "Then we should be able to use the codes Wilson sent us to access the building." },
                    new Dialogue { CharacterImage = Sidechick.Bust, Message = "I wouldn't wanna be on shift as a ZantoCorp guard tonight. Let's go!" },
                    new Dialogue { CharacterImage = MainChar.Bust, Message = "Thanks for coming with me on this. We both know it's not gonna be a walk in the park." },
                    new Dialogue { CharacterImage = Sidechick.Bust, Message = "Are you kidding? They don't stand a chance!" },
                } 
            },
            {
                "corp",
                new List<Dialogue>
                {
                    new Dialogue { CharacterImage = MainChar.Bust, Message = "The Nanite Control Central is hiding in the back, we are going need to be quiet if we're gonna get through." },
                    new Dialogue { CharacterImage = Sidechick.Bust, Message = "You know me -- silent as a bomb!" },
                }
            },
            {
                "boss",
                new List<Dialogue>
                {
                    new Dialogue { CharacterImage = CorpSec3.Bust, Message = "Punks like you don't know what's good for the world! You think the rules don't apply to you." },
                    new Dialogue { CharacterImage = Sidechick.Bust, Message = "You don't care about what's good for the world!" },
                    new Dialogue { CharacterImage = MainChar.Bust, Message = "All you care about is enslaving it!" },
                    new Dialogue { CharacterImage = CorpSec3.Bust, Message = "I don't normally get to enjoy my job, but every once in awhile some " +
                                                                           "problem appears that's particularly enjoyable to take care of." },
                    new Dialogue { CharacterImage = Sidechick.Bust, Message = "Most people don't enjoy pain. That's all you're gonna get!" },
                    new Dialogue { CharacterImage = MainChar.Bust, Message = "*Chugs an energy drink*" },
                }
            },
        };

        private readonly List<string> _completedDialogs = new List<string>();

        private List<Dialogue> _dialogToShowAtEndOfMovement = new List<Dialogue>();
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
                if (character.CurrentTile.Dialog == "boss")
                    GameWorld.Friendlies.ForEach(x => x.State.RemainingHealth = x.Stats.HP);
            }
        }

        private void OnMovementFinished()
        {
            if (_shouldShowDialogOnMovementResolved)
            {
                EventQueue.Instance.Add(new DialogueStarted { Dialogs = _dialogToShowAtEndOfMovement });
                _shouldShowDialogOnMovementResolved = false;
            }
        }
    }
}
