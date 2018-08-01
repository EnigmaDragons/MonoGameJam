using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.Memory;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Themes;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.GUI
{
    public class CoverProvidedView : IVisualAutomaton
    {
        private bool _showsHoveredTileCoverToo = false;
        private bool _checksMouse = false;
        private bool _isShown = false;

        private Dictionary<Point, IEnumerable<GameTile>> _pointsWithAdjacentCoverTiles;

        private GameTile[] _THEM_YKNOW;

        private Dictionary<Cover, UiImage> _adjactentShields = new Dictionary<Cover, UiImage>
        {
            { Cover.Light, new UiImage { Tint = 100.Alpha(), Image = Cover.Light.GetVisual(), Transform = new Transform2(new Rectangle(0, 0, TileData.RenderWidth, TileData.RenderHeight))} },
            { Cover.Medium, new UiImage { Tint = 100.Alpha(), Image = Cover.Medium.GetVisual(), Transform = new Transform2(new Rectangle(0, 0, TileData.RenderWidth, TileData.RenderHeight))} },
            { Cover.Heavy, new UiImage { Tint = 100.Alpha(), Image = Cover.Heavy.GetVisual(), Transform = new Transform2(new Rectangle(0, 0, TileData.RenderWidth, TileData.RenderHeight))} },
        };

        private IEnumerable<KeyValuePair<Cover, Transform2>> TRANSFORMS_FOR_COVERS;
        private int _countShown = 0;

        private Label _label = new Label { TextColor = UIColors.AvailableTargetsUI_CoverPercentText };

        private Point _lastTileHovered;

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
            _lastTileHovered = GameWorld.HoveredTile;

            _pointsWithAdjacentCoverTiles = obj.AvailableMoves
                .Select(x => GameWorld.Map[x.Last()])
                .Where(tile => tile.Cover == Cover.None)
                .ToDictionary(
                    tile => tile.Position,
                    tile => GetDirections(tile.Position)
                        .Select(y => GameWorld.Map[y])
                        .Where((subTile, i) => subTile.Cover != Cover.None)
                ).Where(kv => kv.Value.Any()).ToDictionary(x => x.Key, x => x.Value);
        }

        private static List<Point> GetDirections(Point point) => new List<Point> 
        {
            new Point(point.X - 1, point.Y),
            new Point(point.X + 1, point.Y),
            new Point(point.X, point.Y - 1),
            new Point(point.X, point.Y + 1)
        };

        private void OnMovementConfirmed(MovementConfirmed obj)
        {
            _checksMouse = false;
            _isShown = false;
        }
        private void OnMovementFinished(MovementFinished obj)
        {
        }

        public void Update(TimeSpan delta)
        {
            
            //if (CurrentGame.TheGame.IsActive)
            if (true)
            {
                if (_checksMouse)
                {
                    if (_lastTileHovered != GameWorld.HoveredTile)
                    {
                        _lastTileHovered = GameWorld.HoveredTile;
                        _isShown = _pointsWithAdjacentCoverTiles.ContainsKey(_lastTileHovered);

                        if (_isShown)
                        {
                            TRANSFORMS_FOR_COVERS = new List<KeyValuePair<Cover, Transform2>>();
                            _pointsWithAdjacentCoverTiles[_lastTileHovered].ForEachIndex((gametile, i) =>
                            {
                                TRANSFORMS_FOR_COVERS += _pointsWithAdjacentCoverTiles
                                    .Where(x => x.Key == gametile.Position).Select(x => x.Value.Select(y => y).ToDictionary(y => y.Cover, y => y.Transform));
                                
                            });

                            _label.Transform = GameWorld.Map[_lastTileHovered].Transform;
                        }
                    }
                }
            }
        }

        public void Draw(Transform2 parentTransform)
        {
            if (_isShown && GameWorld.Map[GameWorld.HoveredTile].Cover == Cover.None)
            {
                //_adjactentShields[i].Draw(parentTransform);
                TRANSFORMS_FOR_COVERS.ForEach(keyValuePair =>
                {
                    _adjactentShields[keyValuePair.Key].Transform = keyValuePair.Value;
                    _adjactentShields[keyValuePair.Key].Draw(parentTransform);
                });
                _label.Draw(parentTransform);
            }
        }
    }
}
