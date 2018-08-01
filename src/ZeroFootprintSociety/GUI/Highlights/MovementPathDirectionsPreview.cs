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
    public class MovementPathDirectionsPreview : IVisualAutomaton
    {
        private bool _showsHoveredPathDirections = false;
        private bool _shouldCheckMouse = false;
        private Point? _lastPointOver;
        private Point? _previousTileOver;
        private List<List<Point>> _availableMoves = new List<List<Point>>(0);
        private List<Transform2> _currentPathTransforms = new List<Transform2>(0);
        private readonly IVisual _highlight = new ColoredRectangle
        {
            Transform = new Transform2(TileData.RenderSize),
            Color = UIColors.MovementPathDirectionsPreview_Tile
        };

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
            _shouldCheckMouse = GameWorld.CurrentCharacter.Team == Team.Friendly;
        }

        private void OnMovementConfirmed(MovementConfirmed e)
        {
            _shouldCheckMouse = false;
            _availableMoves = null;
            _lastPointOver = e.Path.First();
        }

        private void OnChangesTile(Moved e)
        {
            if (_showsHoveredPathDirections)
            {
                _currentPathTransforms = _currentPathTransforms.Skip(1).ToList();
                _previousTileOver = _lastPointOver;
                _lastPointOver = e.Position;
                if (!_currentPathTransforms.Any())
                    _showsHoveredPathDirections = false;
            }
        }

        private void OnMovementFinished(MovementFinished obj) => _currentPathTransforms = new List<Transform2>(0);

        private void InitDirections()
        {
            _previousTileOver = null;
            var path = _availableMoves.Find(x => x.Last() == _lastPointOver) ?? new List<Point>();
            _currentPathTransforms = path.Select(x => GameWorld.Map.TileToWorldTransform(x)).ToList();
        }
        
        public void Update(TimeSpan delta)
        {
            if (_showsHoveredPathDirections && CurrentGame.TheGame.IsActive)
            {
                if (_shouldCheckMouse && _lastPointOver != GameWorld.HoveredTile)
                {
                    _lastPointOver = GameWorld.HoveredTile;
                    InitDirections();
                }
                else if (_availableMoves != null && GameWorld.CurrentCharacter.Team != Team.Friendly)
                {
                    InitDirections();
                }
            }
        }

        public void Draw(Transform2 parentTransform)
        {
            if (_showsHoveredPathDirections && _currentPathTransforms.Any())
                _currentPathTransforms.ToList().ForEach(x => _highlight.Draw(parentTransform + x));
        }
    }
}
