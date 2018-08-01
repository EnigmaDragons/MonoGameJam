using MonoDragons.Core.AudioSystem;
using MonoDragons.Core.Common;
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
        
    }
}