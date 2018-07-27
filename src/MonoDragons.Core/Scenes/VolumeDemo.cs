using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core.AudioSystem;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace MonoDragons.Core.Scenes
{
    public class VolumeDemo : IScene
    {
        private readonly Sound _sound1 = Sound.SoundEffect("Content/VolumeDemo/NewObjective.mp3");
        private readonly Sound _ambience = Sound.Ambient("Content/VolumeDemo/Waves.mp3");
        private readonly Sound _music1 = Sound.MusicRawPath("Content/VolumeDemo/HackerSpaceship.mp3");
        private readonly Sound _music2 = Sound.MusicRawPath("Content/VolumeDemo/CloningRoom.mp3");

        private readonly List<IVisual> _visuals = new List<IVisual>();
        private ClickUI _clickUi;

        public void Init()
        {
            _clickUi = new ClickUI();
            Add(CreateButton(new Vector2(50, 50), () => _sound1.Play(), "Play Sound 1"));
            Add(CreateButton(new Vector2(50, 150), () => _sound1.Pause(), "Pause Sound 1"));
            Add(CreateButton(new Vector2(50, 250), () => Sound.SoundEffect("Content/VolumeDemo/Scanning.mp3").Play(), "Create Sound 2"));
            Add(CreateButton(new Vector2(50, 350), () => MasterVolume.Instance.SoundEffectVolume -= 0.1f, "Lower Volume"));
            Add(CreateButton(new Vector2(50, 450), () => MasterVolume.Instance.SoundEffectVolume += 0.1f, "Raise Volume"));

            Add(CreateButton(new Vector2(450, 50), () => _ambience.Play(), "Play Ambience"));
            Add(CreateButton(new Vector2(450, 150), () => _ambience.Pause(), "Pause Ambience"));
            Add(CreateButton(new Vector2(450, 250), () => _ambience.Volume -= 0.1f, "Lower Ambience Volume"));
            Add(CreateButton(new Vector2(450, 350), () => _ambience.Volume += 0.1f, "Raise Ambience Volume"));
            Add(CreateButton(new Vector2(450, 450), () => MasterVolume.Instance.AmbienceVolume -= 0.1f, "Lower Volume"));
            Add(CreateButton(new Vector2(450, 550), () => MasterVolume.Instance.AmbienceVolume += 0.1f, "Raise Volume"));

            Add(CreateButton(new Vector2(850, 50), () => _music1.Play(), "Play Music 1"));
            Add(CreateButton(new Vector2(850, 150), () => _music1.Pause(), "Pause Music 1"));
            Add(CreateButton(new Vector2(850, 250), () => _music1.Reset(), "Reset Music 1"));
            Add(CreateButton(new Vector2(850, 350), () => _music2.Play(), "Play Music 2"));
            Add(CreateButton(new Vector2(850, 450), () => MasterVolume.Instance.MusicVolume -= 0.1f, "Lower Volume"));
            Add(CreateButton(new Vector2(850, 550), () => MasterVolume.Instance.MusicVolume += 0.1f, "Raise Volume"));
        }

        public void Update(TimeSpan delta)
        {
            _clickUi.Update(delta);
        }

        public void Draw()
        {
            _visuals.ForEach(x => x.Draw(Transform2.Zero));
        }

        public void Dispose() {}

        protected void Add(VisualClickableUIElement e)
        {
            _clickUi.Add(e);
            _visuals.Add(e);
        }

        private ImageTextButton CreateButton(Vector2 position, Action onClick, string text)
        {
            return new ImageTextButton(new Rectangle(position.ToPoint(), new Point(360, 75)), onClick, text,
                "VolumeDemo/PixelButton", "VolumeDemo/PixelButton-Hover", "VolumeDemo/PixelButton-Press");
        }
    }
}
