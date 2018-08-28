using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
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
        private static IReadOnlyList<IReadOnlyList<Point>> Empty = new List<List<Point>>(0);
        
        private bool _showsHoveredPathDirections = false;
        private bool _shouldCheckMouse = false;
        private Point? _lastPointOver;
        private Point? _previousTileOver;
        private IReadOnlyList<IReadOnlyList<Point>> _availableMoves = Empty;
        private List<Transform2> _currentPathTransforms = new List<Transform2>(0);
        private readonly IVisual _highlight = new ColoredRectangle
        {
            Transform = new Transform2(TileData.RenderSize),
            Color = UiColors.MovementPathDirectionsPreview_Tile
        };

        public MovementPathDirectionsPreview()
        {
            Event.Subscribe(EventSubscription.Create<MovementOptionsAvailable>(OnMovementOptionsAvailable, this));
            Event.Subscribe(EventSubscription.Create<MovementConfirmed>(OnMovementConfirmed, this));
            Event.Subscribe(EventSubscription.Create<MovementFinished>(OnMovementFinished, this));
        }

        private void OnMovementOptionsAvailable(MovementOptionsAvailable e)
        {
            _availableMoves = e.AvailableMoves.ToList();
            _showsHoveredPathDirections = true;
            _shouldCheckMouse = GameWorld.CurrentCharacter.Team == Team.Friendly;
        }

        private void OnMovementConfirmed(MovementConfirmed e)
        {
            _shouldCheckMouse = false;
            _availableMoves = Empty;
            _lastPointOver = e.Path.First();
            _currentPathTransforms = new List<Transform2>(0);
        }

        private void OnMovementFinished(MovementFinished obj) => _currentPathTransforms = new List<Transform2>(0);

        private void InitDirections()
        {
            _previousTileOver = null;
            var path = _availableMoves.ToList().Find(x => x.Last() == _lastPointOver) ?? new List<Point>();
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
            if (_showsHoveredPathDirections)
                _currentPathTransforms.ToList().ForEach(x => _highlight.Draw(parentTransform + x));
        }
    }
}
