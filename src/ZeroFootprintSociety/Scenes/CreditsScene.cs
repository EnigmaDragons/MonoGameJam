using MonoDragons.Core.Animations;
using MonoDragons.Core.AudioSystem;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;
using System.Collections.Generic;
using ZeroFootPrintSociety.Credits;

namespace ZeroFootPrintSociety.Scenes
{
    public sealed class CreditsScene : JamScene
    {
        private readonly Queue<IAnimation> _animations = new Queue<IAnimation>();
        private TimerTask _timer;
        private bool _animationsAreFinished;

        protected override void OnInit()
        {
            Sound.Music("fast-forward", 1.0f).Play();

            AddUi(new ScreenClickable(() => Scene.NavigateTo("MainMenu")));

            AddAnimation(new TitleCreditSegment());

            AddAnimation(new GameDesignerCredit());
            AddAnimation(new LeadArtistCredit());
            AddAnimation(new LeadProgrammerCredit());
            AddAnimation(new CharacterArtistCredit());
            AddAnimation(new ProjectManagerCredit());
            AddAnimation(new EnvironmentArtCredit());
            AddAnimation(new ProgrammerCredit());
            AddAnimation(new UiDesignerCredit());
            AddAnimation(new LevelDesignerCredit());
            AddAnimation(new WriterCredit());
            AddAnimation(new AlgorithmOptimizer());
            AddAnimation(new WriterCredit());
            AddAnimation(new ComposerCredit());

            _timer = new TimerTask(StartNext, 600, recurring: false);
            Add(_timer);
        }

        private void AddAnimation(IAnimation anim)
        {
            _animations.Enqueue(anim);
            Add(anim);
        }

        private void QueueNext()
        {
            _timer.Reset();
        }

        private void StartNext()
        {
            if (_animations.Count > 0)
                _animations.Dequeue().Start(QueueNext);
            else
                _animationsAreFinished = true;
        }

        protected override void DrawBackground()
        {
            if (_animationsAreFinished)
                UI.FillScreen("Images/Logo/oilsplash");
        }

        protected override void DrawForeground()
        {
        }
    }
}
