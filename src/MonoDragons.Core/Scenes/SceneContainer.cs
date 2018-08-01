using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using System;
using System.Collections.Generic;
using MonoDragons.Core.Development;

namespace MonoDragons.Core.Scenes
{
    public abstract class SceneContainer : IVisualAutomaton
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly List<IAutomaton> _automata = new List<IAutomaton>();
        private readonly List<object> _actors = new List<object>();
        private readonly bool _useAbsolutePosition;
        private bool _isInitialized;

        protected virtual Func<Transform2> GetOffset { get; set; }

        protected void Add(IVisual visual) =>  OnlyDuringInit(() => _visuals.Add(visual));
        protected void Add(IAutomaton automaton) => OnlyDuringInit(() => _automata.Add(automaton));
        protected void Add(object actor) => OnlyDuringInit(() => _actors.Add(actor));
        
        public SceneContainer()
            : this(false) { }

        public SceneContainer(bool useAbsolutePosition)
        {
            _useAbsolutePosition = useAbsolutePosition;
            GetOffset = () => Transform2.Zero;
        }

        protected void Add(IVisualAutomaton obj)
        {
            OnlyDuringInit(() =>
            {
                Add((IVisual) obj);
                Add((IAutomaton) obj);
            });
        }
        
        public virtual void Draw(Transform2 parentTransform)
        {
            var t = _useAbsolutePosition ? Transform2.Zero : parentTransform + GetOffset();
            _visuals.ForEach(x =>
            {
                Perf.Time($"Drew {x.GetType().Name}", ()  => x.Draw(t), 20);
            });
        }

        public virtual void Update(TimeSpan delta)
        {
            _isInitialized = true;
            _automata.ForEach(x => x.Update(delta));
        }

        private void OnlyDuringInit(Action action)
        {
            if (_isInitialized)
                throw new InvalidOperationException("May not Add new elements to the Scene after Initialization");
            action();
        }
    }
}
