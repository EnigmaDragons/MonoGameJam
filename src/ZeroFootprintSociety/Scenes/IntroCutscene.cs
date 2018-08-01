using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Animations;
using MonoDragons.Core.AudioSystem;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Inputs;
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
        private readonly IAnimation _fadeOut;
        private int _index;
        private readonly List<string> _texts = new List<string>
        {
            "\n 2174 - New Atlantica City \n \n" +
            
            "After the catastrophic collapse of the East Euro-Asiatic Alliance in the \n" +
            "late 21st century, large corporations capitalized on the power vacuum and \n" +
            "used their vast wealth to wrest control from regional governments.",
            
            "\n ZantoCorp used bribery, propaganda, and the fears of populace to outmaneuver \n" +
            "their competitors and become the ersatz rulers of most of the Eastern European \n" +
            "region. Presently, they directly employ 56% of the workforce, making ZantoCorp \n" +
            "the largest labor monopoly the world has ever seen.",
            
            "\n Using city drones, integrated camera feeds, and data-sniffing, they maintain \n" +
            "extensive dossiers on all Euro-Asiatic workers, and have nearly 100% complete \n" +
            "records on all New Atlantica City residents. Most people live in relatively \n" +
            "harmony with their corporate overlords. The have never been wealthier or \n" +
            "better fed before.", 
            
            "Not everyone is happy with the comprehensive monitoring and the brutal legal \n" +
            "consequences for even minor offenses. \n \n" +
            
            "Weldon Zemke is the leader of a small group of dissidents whose primary goal \n" +
            "is to live in the New Atlantica City as ghosts. They use sophisticated \n" +
            "electronic countermeasures to remain unseen by cameras, and to leave no lasting \n" +
            "data trail. They are the Zero-Footprint Society.",
        };
        
        public IntroCutscene(string nextScene)
        {
            _nextScene = nextScene;
            _chatBox = new ChatBox(_texts[_index++], 0.7.VW(), GuiFonts.BodySpriteFont, 40, 40) { Position = new Vector2(0.15.VW(), 0.34.VH())};
            _fadeOut = new ScreenFade { Duration = TimeSpan.FromSeconds(1.5), FromAlpha = 0, ToAlpha = 255};
        }

        public override void Init()
        {
            Sound.Music("trapped-and-stuck").Play();
            Input.On(Control.Start, Advance);
            AddClickable(new ScreenClickable(Advance));
            Add(_chatBox);
            Add(_fadeOut);
        }

        private void Advance()
        {
            if (_index == _texts.Count && _chatBox.IsMessageCompletelyDisplayed())
                _fadeOut.Start(() => Scene.NavigateTo(_nextScene));
            else if (_chatBox.IsMessageCompletelyDisplayed())
                _chatBox.ShowMessage(_texts[_index++]);
            else
                _chatBox.CompletelyDisplayMessage();
        }
    }
}
