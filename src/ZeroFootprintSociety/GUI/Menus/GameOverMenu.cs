using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;
using System;
using System.Collections.Generic;
using MonoDragons.Core.Animations;
using MonoDragons.Core.AudioSystem;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.GUI
{
    class GameOverMenu : IVisualAutomaton
    {
        private const int _menuWidth = 300;
        private const int _menuHeight = 600;
        private readonly int _menuX = 0.5f.VW() - (_menuWidth / 2);
        private readonly int _menuY = 0.3f.VH();

        private readonly List<IVisual> _visuals = new List<IVisual>();
        private readonly ClickUIBranch _interceptLayer = new ClickUIBranch("MenuBack", 11);
        private readonly ClickUIBranch _branch = new ClickUIBranch("Menu", 12);
        private readonly UiColoredRectangle _black = new UiColoredRectangle{ Transform = new Transform2(new Size2(5000, 5000)), Color = Color.Transparent };
        private readonly ClickUI _clickUI;
        
        private bool _isGameOver;
        private readonly IAnimation _fade;

        public GameOverMenu(ClickUI clickUi)
        {
            _clickUI = clickUi;
            var ctx = new Buttons.MenuContext { X = _menuX, Y = _menuY, Width = _menuWidth, FirstButtonYOffset = 50 };

            var mainMenuButton = Buttons.Text(ctx, 7, "Return to Main Menu", 
                () =>
                {
                    AudioPlayer.Instance.StopAll();
                    Scene.NavigateTo("MainMenu");
                }, () => true);

            _visuals.Add(_black);
            _fade = new ScreenFade
            {
                ToAlpha = 255, 
                FromAlpha = 0, 
                Duration = TimeSpan.FromSeconds(2)
            };
            _visuals.Add(_fade);
            _visuals.Add(new UiImage
            {
                Image = "UI/game-over.png",
                Transform = new Transform2(new Vector2(0.5.VW() - 329, 0.3.VH()), new Size2(639, 150))
            });

            _visuals.Add(mainMenuButton);
            _interceptLayer.Add(new ScreenClickable(() => { }));
            _branch.Add(mainMenuButton);
            Event.Subscribe(EventSubscription.Create<GameOver>(e => Enable(), this));
        }

        private void Enable()
        {
            _isGameOver = true;
            _clickUI.Add(_interceptLayer);
            _clickUI.Add(_branch);
            _fade.Start(() => _black.Color = Color.Black);
            AudioPlayer.Instance.StopAll();
            Sound.SoundEffect("SFX/death-swell.mp3").Play();
        }

        public void Draw(Transform2 parentTransform)
        {
            if (!_isGameOver)
                return;

            _visuals.ForEach(x => x.Draw(parentTransform));
        }

        public void Update(TimeSpan delta)
        {
            _fade.Update(delta);
        }
    }
}
