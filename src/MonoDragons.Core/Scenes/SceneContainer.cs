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
        private readonly List<object> _actors = new List<object>();
        private readonly bool _useAbsolutePosition;

        protected virtual Func<Transform2> GetOffset { get; set; }

        protected void Add(IVisual visual) =>  _visuals.Add(visual);
        protected void Add(IAutomaton automaton) => _automata.Add(automaton);
        protected void Add(object actor) => _actors.Add(actor);
        
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
#if DEBUG
            _visuals.ForEach(x =>
            {
                try { x.Draw(t); }
                catch (Exception e)
                {
                    throw new Exception($"Error: Drawing {x.GetType()}", e);
                }
            });
#else
            _visuals.ForEach(x => x.Draw(t));
#endif
        }

        public virtual void Update(TimeSpan delta)
        {
#if DEBUG
            _automata.ForEach(x =>
            {
                try { x.Update(delta); }
                catch (Exception e)
                {
                    throw new Exception($"Error: Updating {x.GetType()}", e);
                }
            });
#else
            _automata.ForEach(x => x.Update(delta));
#endif
        }
    }
}
