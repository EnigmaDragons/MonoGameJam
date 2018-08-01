using MonoDragons.Core.Animations;
using MonoDragons.Core.AudioSystem;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;
using System.Collections.Generic;
using MonoDragons.Core.Inputs;
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
            Sound.Music("credits", 1.0f).Play();
            Input.On(Control.Start, () => Scene.NavigateTo("MainMenu"));
            Input.On(Control.Select, () => Scene.NavigateTo("MainMenu"));
            AddUi(new ScreenClickable(() => Scene.NavigateTo("MainMenu")));

            AddAnimation(new TitleCreditSegment());
            
            // Leads
            AddAnimation(new LeadGameDesignerCredit());
            AddAnimation(new LeadProgrammerCredit());
            AddAnimation(new LeadArtistCredit());
            
            // Gameplay Design
            AddAnimation(new GameplayProgrammerCredit());
            AddAnimation(new LevelDesigner1Credit());
            AddAnimation(new LevelDesigner2Credit());
            
            // Front-End
            AddAnimation(new EnvironmentArtCredit());
            AddAnimation(new UiDesignerCredit());
            AddAnimation(new CharacterArtistCredit());
            AddAnimation(new AudioCredit());
            
            // Back-End
            AddAnimation(new ProgrammerCredit());
            AddAnimation(new AlgorithmOptimizer());
            
            // Project
            AddAnimation(new ProjectManagerCredit());
            AddAnimation(new Tester1Credit());

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
