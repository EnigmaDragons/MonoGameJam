using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;
using System;
using System.Collections.Generic;

namespace ZeroFootPrintSociety.Credits
{
    public abstract class JamScene : IScene
    {
        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly List<IAutomaton> _automata = new List<IAutomaton>();

        private ClickUI _clickUi;

        protected abstract void OnInit();
        protected abstract void DrawBackground();
        protected abstract void DrawForeground();

        public void Init()
        {
            _clickUi = new ClickUI();
            _automata.Add(_clickUi);
            OnInit();
        }

        public void Update(TimeSpan delta)
        {
            _automata.ForEach(x => x.Update(delta));
            OnUpdate(delta);
        }

        protected virtual void OnUpdate(TimeSpan delta)
        {
        }

        public void Draw()
        {
            DrawBackground();
            _visuals.ForEach(x => x.Draw(Transform2.Zero));
            DrawForeground();
        }

        protected void AddVisual(IVisual v)
        {
            _visuals.Add(v);
        }

        protected void AddUi(ClickableUIElement e)
        {
            _clickUi.Add(e);
        }

        protected void Add(VisualClickableUIElement e)
        {
            _clickUi.Add(e);
            _visuals.Add(e);
        }

        protected void Add(IVisualAutomaton v)
        {
            _automata.Add(v);
            _visuals.Add(v);
        }

        protected void Add(IAutomaton a)
        {
            _automata.Add(a);
        }

        protected void Add(IVisual v)
        {
            _visuals.Add(v);
        }

        public void Dispose()
        {
            _clickUi.Dispose();
        }
    }
}
