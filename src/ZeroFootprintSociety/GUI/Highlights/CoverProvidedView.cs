using System;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.GUI
{
    public class CoverProvidedView : IVisualAutomaton
    {
        private bool _showsHoveredTileCoverToo = false;
        private bool _checksMouse = false;
        
        private Point? _lastPointOver;
        private Point? _previousTileOver;

        public CoverProvidedView()
        {
            Event.Subscribe<MovementOptionsAvailable>(OnMovementOptionsAvailable, this);
            Event.Subscribe<MovementConfirmed>(OnMovementConfirmed, this);
            Event.Subscribe<MovementFinished>(OnMovementFinished, this);
        }

        private void OnMovementOptionsAvailable(MovementOptionsAvailable obj)
        {
            _checksMouse = true;
            _showsHoveredTileCoverToo = false;
        }

        private void OnMovementConfirmed(MovementConfirmed obj)
        {
            _checksMouse = false;
        }
        private void OnMovementFinished(MovementFinished obj)
        {
            
        }

        public void Update(TimeSpan delta)
        {
            if (CurrentGame.TheGame.IsActive)
            {
                if (_checksMouse && _lastPointOver != GameWorld.HoveredTile)
                {
                    _lastPointOver = GameWorld.HoveredTile;
                    
                }
            }
            Point hoveredTile = GameWorld.Map.TileToWorldPosition(GameWorld.HoveredTile);
            
        }

        public void Draw(Transform2 parentTransform)
        {
            
        }
    }
}
