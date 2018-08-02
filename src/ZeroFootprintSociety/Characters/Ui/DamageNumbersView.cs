using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using ZeroFootPrintSociety.CoreGame.ActionEvents;
using ZeroFootPrintSociety.GUI;

namespace ZeroFootPrintSociety.Characters.GUI
{
    class DamageNumbersView : IVisualAutomaton
    {
        private static readonly TimeSpan DisplayDuration = TimeSpan.FromMilliseconds(1800);
        private static readonly TimeSpan DelayDuration = TimeSpan.FromMilliseconds(300);
        private readonly Character _owner;
        private readonly List<Tuple<TimeSpan, string>> _numbers = new List<Tuple<TimeSpan, string>>();
        private readonly List<Tuple<TimeSpan, string>> _delayed = new List<Tuple<TimeSpan, string>>();
        private bool _isDisplayingDamage;

        public DamageNumbersView(Character owner)
        {
            Event.Subscribe<ShotHit>(OnShotHit, this);
            Event.Subscribe<ShotMissed>(OnShotMissed, this);
            Event.Subscribe<ShotBlocked>(OnShotBlocked, this);
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

        private void OnShotBlocked(ShotBlocked e)
        {
            if (e.Target.Equals(_owner))
                Add("Block");
        }

        private void Add(string text)
        {
            _delayed.Add(new Tuple<TimeSpan, string>(
                DelayDuration,
                text));
            _isDisplayingDamage = true;
        }

        public void Draw(Transform2 parentTransform)
        {
            var numbers = _numbers.ToList();
            for (var i = 0; i < numbers.Count; i++)
                UI.DrawText(numbers[i].Item2, parentTransform.Location + new Vector2(5, -34 - (i * 24)), Color.White);
        }

        public void Update(TimeSpan delta)
        {
            if (_isDisplayingDamage && _numbers.Count == 0 && _delayed.Count == 0)
                _isDisplayingDamage = false;
            
            var updated =  _delayed.ToList()
                .Select(x => new Tuple<TimeSpan, string>(x.Item1 - delta, x.Item2)).ToList();
            _delayed.Clear();
            _delayed.AddRange(updated.Where(x => x.Item1 > TimeSpan.Zero));
            
            var numbers = _numbers.ToList();
            _numbers.Clear();
            _numbers.AddRange(numbers
                .Select(x => new Tuple<TimeSpan, string>(x.Item1 - delta, x.Item2))
                .Where(i => i.Item1 > TimeSpan.Zero));
            _numbers.AddRange(updated
                .Where(x => x.Item1 <= TimeSpan.Zero)
                .Select(i => new Tuple<TimeSpan, string>(DisplayDuration, i.Item2)));
        }
    }
}
