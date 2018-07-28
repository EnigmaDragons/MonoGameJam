using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using ZeroFootPrintSociety.CoreGame.ActionEvents;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.Characters.Ui
{
    class DamageNumbersView : IVisualAutomaton
    {
        private static readonly TimeSpan DisplayDuration = TimeSpan.FromMilliseconds(1800);
        private readonly Character _owner;
        private List<Tuple<TimeSpan, string>> _numbers = new List<Tuple<TimeSpan, string>>();
        private bool _isDisplayingDamage;

        public DamageNumbersView(Character owner)
        {
            Event.Subscribe<ShotHit>(OnShotHit, this);
            Event.Subscribe<ShotMissed>(OnShotMissed, this);
            _owner = owner;
        }

        private void OnShotHit(ShotHit e)
        {
            if (e.Target.Equals(_owner))
                Add(e.DamageAmount.ToString());
        }

        private void OnShotMissed(ShotMissed e)
        {
            if (e.Target.Equals(_owner))
                Add("Miss");
        }

        private void Add(string text)
        {
            _numbers.Add(new Tuple<TimeSpan, string>(
                DisplayDuration,
                text));
            _isDisplayingDamage = true;
        }

        public void Draw(Transform2 parentTransform)
        {
            _numbers.ToList().ForEach(
                x => UI.DrawText(x.Item2, parentTransform.Location + new Vector2(5, -34), Color.White));
        }

        public void Update(TimeSpan delta)
        {
            if (_isDisplayingDamage && _numbers.Count == 0)
            {
                _isDisplayingDamage = false;
                Event.Publish(new ActionResolved());
            }

            var numbers = _numbers.ToList();
            _numbers.Clear();
            _numbers.AddRange(numbers
                .Select(x => new Tuple<TimeSpan, string>(x.Item1 - delta, x.Item2))
                .Where(i => i.Item1 > TimeSpan.Zero));            
        }
    }
}
