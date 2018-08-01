using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.Themes;

namespace ZeroFootPrintSociety.GUI
{
    public class ChoiceUI : IVisual
    {
        private readonly Label _title;
        private readonly List<OptionUI> _options;
        private readonly VisualClickableUIElement _backButton;
        public ClickUIBranch Branch { get; }

        public ChoiceUI(string title, params OptionUI[] options) : this(title, () => {}, false, options) {}

        public ChoiceUI(string title, Action backAction, params OptionUI[] options) : this(title, backAction, true, options) {}

        private ChoiceUI(string title, Action backAction, bool backButtonActive, params OptionUI[] options)
        {
            _title = new Label { Text = title, TextColor = Color.White, Transform = new Transform2(new Size2(1600, 100)) };
            _options = options.ToList();
            _backButton = new TextButton(new Rectangle(700, 800, 200, 35), backAction, "Back", UIColors.Buttons_Default, UIColors.Buttons_Hover, UIColors.Buttons_Press, () => backButtonActive);
            Branch = new ClickUIBranch("Choice", 2);
            Branch.Add(_backButton);
            _options.ForEach(x => Branch.Add(x));
        }

        public void Draw(Transform2 parentTransform)
        {
            _title.Draw(parentTransform);
            _options.ForEach(x => x.Draw(parentTransform));
            _backButton.Draw(parentTransform);
        }
    }
}
