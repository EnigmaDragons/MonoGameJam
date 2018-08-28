using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.PhysicsEngine;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Themes;
using ZeroFootPrintSociety.Tiles;

using ZeroFootPrintSociety.CoreGame;

namespace ZeroFootPrintSociety.GUI
{
    class MovementPathHighlights : IVisualAutomaton
    {
        private List<IVisual> _visuals = new List<IVisual>();
        private List<IAutomaton> _automata = new List<IAutomaton>();

        public MovementPathHighlights()
        {
            Event.Subscribe(EventSubscription.Create<MovementConfirmed>(OnMovementConfirmed, this));
            Event.Subscribe(EventSubscription.Create<MovementFinished>(_ => OnMovementFinished(), this));
            Event.Subscribe(EventSubscription.Create<TurnEnded>(_ => OnMovementFinished(), this));
        }

        private void OnMovementConfirmed(MovementConfirmed e)
        {
            var destination = e.Path.Last();
            if (GameWorld.CurrentCharacter.Team != Team.Friendly && !GameWorld.FriendlyPerception.ContainsKey(destination))
                return;

            var visuals = new List<IVisual>();
            var automata = new List<IAutomaton>();
            visuals.Add(new UiImage
            {
                Image = "Effects/Cover_Gray",
                Transform = GameWorld.Map.TileToWorldTransform(destination).WithSize(TileData.RenderSize),
                Tint = UiColors.AvailableMovesView_Rectangles
            });
            var anim = new TileRotatingEdgesAnim(destination, Color.FromNonPremultiplied(110, 170, 255, 255)).Initialized();
            visuals.Add(anim);
            automata.Add(anim);
            _visuals = visuals;
            _automata = automata;
        }

        private void OnMovementFinished()
        {
            _visuals = new List<IVisual>();
            _automata = new List<IAutomaton>();
        }

        public void Update(TimeSpan delta)
        {
            _automata.ToList().ForEach(x => x.Update(delta));
        }

        public void Draw(Transform2 parentTransform)
        {
            _visuals.ToList().ForEach(x => x.Draw(parentTransform));
        }
    }
}
