using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonoDragons.Core.Scenes
{
    public abstract class MutableSceneContainer : IVisualAutomaton
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly List<IAutomaton> _automata = new List<IAutomaton>();
        private readonly bool _useAbsolutePosition;

        protected virtual Func<Transform2> GetOffset { get; set; }

        protected void Add(IVisual visual) => _visuals.Add(visual);
        protected void Add(IAutomaton automaton) => _automata.Add(automaton);

        public MutableSceneContainer()
            : this(false) { }

        public MutableSceneContainer(bool useAbsolutePosition)
        {
            _useAbsolutePosition = useAbsolutePosition;
            GetOffset = () => Transform2.Zero;
        }

        protected void Clear()
        {
            _visuals.Clear();
            _automata.Clear();
        }

        protected void Add(IVisualAutomaton obj)
        {
            Add((IVisual)obj);
            Add((IAutomaton)obj);
        }

        public virtual void Draw(Transform2 parentTransform)
        {
            var t = _useAbsolutePosition ? Transform2.Zero : parentTransform + GetOffset();
            _visuals.ToList().ForEach(x => x.Draw(t));
        }

        public virtual void Update(TimeSpan delta)
        {
            _automata.ToList().ForEach(x => x.Update(delta));
        }
    }
}
