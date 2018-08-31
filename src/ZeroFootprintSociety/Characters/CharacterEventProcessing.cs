using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.ActionEvents;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.GUI;

namespace ZeroFootPrintSociety.Characters
{
    public sealed class CharacterEventProcessing
    {
        public CharacterEventProcessing()
        {
            Event.Subscribe<OverwatchTilesAvailable>(UpdateOverwatch, this);
            Event.Subscribe<AttackAnimationsFinished>(CheckForDeath, this);
            Event.Subscribe<TilesSeen>(OnTilesSeen, this);
            Event.Subscribe<TilesPerceived>(OnTilesPerceived, this);
            Event.Subscribe<MovementConfirmed>(OnMovementConfirmed, this);
            Event.Subscribe<MoveResolved>(ContinueMoving, this);
            Event.Subscribe<ShotHit>(OnShotHit, this);
            Event.Subscribe<ShotFired>(OnShotFired, this);
            Event.Subscribe<TurnBegun>(OnTurnBegun, this);
        }

        private void OnTurnBegun(TurnBegun e) => e.Character.Notify(e);
        private void ContinueMoving(MoveResolved e) => e.Character.Notify(e);
        private void OnTilesPerceived(TilesPerceived e) => e.Character.Notify(e);
        private void OnTilesSeen(TilesSeen e) => e.Character.Notify(e);
        private void OnShotHit(ShotHit e) => e.Target.Notify(e);
        private void OnShotFired(ShotFired e) => e.Attacker.Notify(e);
        private void CheckForDeath(AttackAnimationsFinished e) => e.Target.Notify(e);
        private void UpdateOverwatch(OverwatchTilesAvailable e) => GameWorld.CurrentCharacter.Notify(e);
        private void OnMovementConfirmed(MovementConfirmed e) => GameWorld.CurrentCharacter.Notify(e);
    }
}