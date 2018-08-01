using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Themes;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.GUI
{
    public class MovementPathDirectionsPreview : MutableSceneContainer
    {
        private bool _showsHoveredPathDirections = false;
        private bool _checksMouse = false;
        private Point? _lastPointOver;
        private Point? _previousTileOver;
        private List<List<Point>> _availableMoves;

        public MovementPathDirectionsPreview()
        {
            Event.Subscribe(EventSubscription.Create<MovementOptionsAvailable>(OnMovementOptionsAvailable, this));
            Event.Subscribe(EventSubscription.Create<MovementConfirmed>(OnMovementConfirmed, this));
            Event.Subscribe(EventSubscription.Create<MovementFinished>(OnMovementFinished, this));
            Event.Subscribe(EventSubscription.Create<Moved>(OnChangesTile, this));
        }

        private void OnMovementOptionsAvailable(MovementOptionsAvailable e)
        {
            _availableMoves = e.AvailableMoves;
            _showsHoveredPathDirections = true;
            _checksMouse = GameWorld.CurrentCharacter.Team == Team.Friendly;
        }

        private void OnMovementConfirmed(MovementConfirmed e)
        {
            _checksMouse = false;
            _availableMoves = null;
            _lastPointOver = e.Path.First();
        }

        private void OnChangesTile(Moved e)
        {
            if (_showsHoveredPathDirections)
            {
                Pop();
                _previousTileOver = _lastPointOver;
                _lastPointOver = e.Position;
                if (IsEmpty())
                    _showsHoveredPathDirections = false;
            }
        }

        private void OnMovementFinished(MovementFinished obj)
            => Clear();

        private void InitDirections()
        {
            _previousTileOver = null;
            List<Point> moveList = _availableMoves.Find(x => x.Last() == _lastPointOver);
            if (moveList != null)
            {
                Clear();
                for (int i = 0; i < moveList.Count(); i++)
                {
                    Add(new ColoredRectangle
                    {
                        Transform = GameWorld.Map.TileToWorldTransform(moveList[i]).WithSize(TileData.RenderSize),
                        Color = UIColors.MovementPathDirectionsPreview_Tile
                    });
                }
            }
        }
        
        public override void Update(TimeSpan delta)
        {
            if (_showsHoveredPathDirections && CurrentGame.TheGame.IsActive)
            {
                if (_checksMouse && _lastPointOver != GameWorld.HoveredTile)
                {
                    _lastPointOver = GameWorld.HoveredTile;
                    InitDirections();
                }
                else if (_availableMoves != null && GameWorld.CurrentCharacter.Team != Team.Friendly)
                {
                    InitDirections();
                }

                base.Update(delta);
            }
        }

        public override void Draw(Transform2 parentTransform)
        {
            if (_showsHoveredPathDirections)
            {
                base.Draw(parentTransform);
            }
        }
    }
}
