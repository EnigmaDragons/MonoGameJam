using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace ZeroFootPrintSociety.GUI.Menus
{
    public sealed class FileSelectionMenu : IVisualAutomaton
    {
        private const int _menuWidth = 300;
        private const int _menuHeight = 600;
        private readonly int _menuX = 0.5f.VW() - (_menuWidth / 2);
        private readonly int _menuY = 0.3f.VH();

        private ClickUI _clickUI;

        private bool _fileInUse;

        public void Draw(Transform2 parentTransform)
        {
            throw new NotImplementedException();
        }

        public void Update(TimeSpan delta)
        {
            throw new NotImplementedException();
        }
    }
}
