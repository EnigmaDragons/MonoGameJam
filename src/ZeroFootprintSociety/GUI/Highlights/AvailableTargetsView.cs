using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Themes;


namespace ZeroFootPrintSociety.GUI
{
    public class AvailableTargetsView : IVisualAutomaton
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly List<IAutomaton> _automata = new List<IAutomaton>();

        public AvailableTargetsView()
        {
            Event.Subscribe(EventSubscription.Create<ShootSelected>(ShowOptions, this));
            Event.Subscribe(EventSubscription.Create<ActionCancelled>(e => ClearOptions(), this));
            Event.Subscribe(EventSubscription.Create<ActionConfirmed>(e => ClearOptions(), this));
        }

        private void ClearOptions()
        {
            _visuals.Clear();
            _automata.Clear();
        }

        private void ShowOptions(ShootSelected e)
        {
            e.AvailableTargets.ForEach(x =>
            {
                _visuals.Add(new ColoredRectangle
                {
                    Transform = x.Character.CurrentTile.Transform, 
                    Color = UIColors.AvailableTargetsView_Rectanges
                });
                var anim = new TileRotatingEdgesAnim(x.Character.CurrentTile.Position, UIColors.AvailableTargetsView_TileRotatingEdgesAnim);
                anim.Init();
                _visuals.Add(anim);
                _automata.Add(anim);
            });
        }

        public void Draw(Transform2 parentTransform)
        {
            _visuals.ToList().ForEach(x => x.Draw(parentTransform));
        }

        public void Update(TimeSpan delta)
        {
            _automata.ToList().ForEach(x => x.Update(delta));
        }
    }
}
