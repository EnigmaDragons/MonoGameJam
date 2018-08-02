using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Animations;
using MonoDragons.Core.AudioSystem;
using MonoDragons.Core.Inputs;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Scenes;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.GUI;
using Control = MonoDragons.Core.Inputs.Control;

namespace ZeroFootPrintSociety.Scenes
{
    class IntroCutscene : ClickUiScene
    {
        private readonly string _nextScene;

        private readonly ChatBox _chatBox;
        private int _index;
        private readonly UiImage _bg;
        private readonly List<string> _texts = new List<string>
        {
            "\n 2174 - New Atlantica City \n \n" +
            
            "After the catastrophic collapse of the East Euro-Asiatic Alliance in the \n" +
            "late 21st century, large corporations capitalized on the power vacuum and \n" +
            "used their vast wealth to wrest control from regional governments.",
            
            "\n ZantoCorp used bribery, propaganda, and the fears of the populace to outmaneuver \n" +
            "their competitors and become the ersatz rulers of most of the Eastern European \n" +
            "region. Presently, they directly employ 56% of the workforce, giving ZantoCorp \n" +
            "the largest labor monopoly the world has ever seen.",
            
            "\n Using city drones, integrated camera feeds, and data-sniffing, they maintain \n" +
            "extensive dossiers on all Euro-Asiatic workers, and have nearly 100% complete \n" +
            "records on all New Atlantica City residents. Most people live in relative \n" +
            "harmony with their corporate overlords. The have never been wealthier or \n" +
            "better fed before.", 
            
            "Not everyone is happy with the comprehensive monitoring and the brutal legal \n" +
            "consequences for even minor offenses. \n \n" +
            
            "Weldon Zemke is the leader of a small group of dissidents whose primary goal \n" +
            "is to live in the New Atlantica City as ghosts. They use sophisticated \n" +
            "electronic countermeasures to remain unseen by cameras, and to leave no lasting \n" +
            "data trail. They are the Zero-Footprint Society.",
            
            "After months of their best efforts, one of ZantoCorp's top agents managed to track \n" +
            "Zemke down. An agent injected Zemke with tracking Nanites, which will attach \n" +
            "themselves to all of his cells, and leave him unable to avoid digital detection any longer. \n \n" +
            
            "Once the Nanites embed themselves into his cells and replicate, anyone he comes into \n" +
            "contact with will be permanently under the watchful eye of ZantoCorp. The entire \n" +
            "Zero-Footprint Society is in grave peril!",
            
            "Cassia Lanthe, a close associate of Zemke, works with some of her contacts from the \n" +
            "Zero-Footprint Society in devising a plan to disable the Nanites before they are \n" +
            "embedded. Preliminary research indicates that this model is one of the newest \n" +
            "prototypes, developed locally at ZantoCorp HQ in New Atlantica City. \n \n" +
            
            "Zemke and Lanthe set out to infiltrate the building, in hopes of reaching the Nanite \n" +
            "control center, and disabling the Nanites before it is too late. Moving causes the Nanites \n" +
            "to circulate faster, so Zemke is on a very tight clock, where every step counts."
        };
        
        public IntroCutscene(string nextScene)
        {
            _nextScene = nextScene;
            _chatBox = new ChatBox(_texts[_index++], 0.7.VW(), GuiFonts.BodySpriteFont, 40, 40) { Position = new Vector2(0.15.VW(), 0.34.VH())};
            _bg = new UiImage
            {
                Image = "UI/intro-bg",
                Transform = new Transform2(new Size2(1.0.VW(), 1.0.VH())),
                Tint = 90.Alpha()
            };
        }

        public override void Init()
        {
            Sound.Music("intro").Play();
            Input.On(Control.Start, Advance);
            Input.On(Control.Select, () => Scene.NavigateTo(_nextScene));
            Add(_bg);
            AddClickable(new ScreenClickable(Advance));
            Add(_chatBox);
            Add(new ScreenFade { Duration = TimeSpan.FromSeconds(1.5), FromAlpha = 255, ToAlpha = 0}.Started());
        }

        private void Advance()
        {
            if (_index == _texts.Count && _chatBox.IsMessageCompletelyDisplayed())
                Scene.NavigateTo(_nextScene);
            else if (_chatBox.IsMessageCompletelyDisplayed())
                _chatBox.ShowMessage(_texts[_index++]);
            else
                _chatBox.CompletelyDisplayMessage();
            if (_index == 5)
                _bg.Image = "UI/nanites";
            if (_index == 6)
                _bg.Image = "UI/blueprints";
        }
    }
}
