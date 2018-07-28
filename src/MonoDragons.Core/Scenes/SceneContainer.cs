using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using System;
using System.Collections.Generic;

namespace MonoDragons.Core.Scenes
{
    public abstract class SceneContainer : IVisualAutomaton
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly List<IAutomaton> _automata = new List<IAutomaton>();
        private readonly bool _useAbsolutePosition;

        protected virtual Func<Transform2> GetOffset { get; set; }

        protected void Add(IVisual visual) =>  _visuals.Add(visual);
        protected void Add(IAutomaton automaton) => _automata.Add(automaton);
        
        public SceneContainer()
            : this(false) { }

        public SceneContainer(bool useAbsolutePosition)
        {
            _useAbsolutePosition = useAbsolutePosition;
            GetOffset = () => Transform2.Zero;
        }

        protected void Add(IVisualAutomaton obj)
        {
            Add((IVisual)obj);
            Add((IAutomaton)obj);
        }
        
        public virtual void Draw(Transform2 parentTransform)
        {
            var t = _useAbsolutePosition ? Transform2.Zero : parentTransform + GetOffset();
            _visuals.ForEach(x => x.Draw(t));
        }

        public virtual void Update(TimeSpan delta)
        {
            _automata.ForEach(x => x.Update(delta));
        }
    }
}
