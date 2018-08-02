using System;
using MonoDragons.Core.AudioSystem;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.ActionEvents;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.Soundtrack
{
    public class TheSoundGuy
    {
        private const float DefaultVolume = 0.5f;

        private bool _isFriendlyTurn = true;
        
        public TheSoundGuy()
        {
            Event.Subscribe<TurnBegun>(OnTurnBegun, this);
            Event.Subscribe<ActionCancelled>(OnActionCancelled, this);
            Event.Subscribe<ActionConfirmed>(OnActionConfirmed, this);
            Event.Subscribe<MovementConfirmed>(OnMovementConfirmed, this);
            Event.Subscribe<ShotFired>(OnShotFired, this);
            Event.Subscribe<ShotHit>(OnShotHit, this);
            Event.Subscribe<ShotBlocked>(OnShotBlocked, this);
            Event.Subscribe<ShotMissed>(OnShotMissed, this);
            Event.Subscribe<Moved>(OnMoved, this);
        }

        private void OnShotMissed(ShotMissed obj)
        {
            Sound.SoundEffect($"SFX/shot-miss-1.wav", 0.8f).Play();
        }
        
        private void OnShotBlocked(ShotBlocked obj)
        {
            Sound.SoundEffect($"SFX/shot-miss-1.wav", 0.8f).Play();
        }

        private void OnShotHit(ShotHit obj)
        {
            Sound.SoundEffect($"SFX/shot-hit-1.wav", DefaultVolume).Play();
        }

        private void OnMoved(Moved obj)
        {
            if (GameWorld.FriendlyPerception[obj.Position])
                Sound.SoundEffect($"SFX/step-hard-{Rng.Int(1, 6)}.wav", 0.5f).Play();
        }

        private void OnShotFired(ShotFired obj)
        {
            Sound.SoundEffect("SFX/rifle-1.wav", DefaultVolume).Play();
        }

        private void OnMovementConfirmed(MovementConfirmed obj)
        {
            if (GameWorld.IsFriendlyTurn)
                Sound.SoundEffect("SFX/button-press-1.wav", DefaultVolume).Play();
        }

        private void OnActionCancelled(ActionCancelled obj)
        {
            Sound.SoundEffect("SFX/cancel.wav", DefaultVolume).Play();
        }
        
        private void OnActionConfirmed(ActionConfirmed obj)
        {
            if (GameWorld.IsFriendlyTurn)
                Sound.SoundEffect("SFX/button-press-1.wav", DefaultVolume).Play();
        }

        private void OnTurnBegun(TurnBegun e)
        {
            var newTurnIsFriendly = GameWorld.IsFriendlyTurn;
            if (!_isFriendlyTurn && newTurnIsFriendly)
                Sound.SoundEffect("SFX/turn-start.wav", DefaultVolume).Play();
            _isFriendlyTurn = newTurnIsFriendly;
        }

        public void Update(TimeSpan delta)
        {
            throw new NotImplementedException();
        }
    }
}