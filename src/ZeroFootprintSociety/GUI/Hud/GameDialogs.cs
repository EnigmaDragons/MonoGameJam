using System;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.GUI.Hud
{
    public class GameDialogs : IVisualAutomaton
    {
        private bool _displayingDialog = false;
        private GameDialog _currentDialog;
        public ClickUIBranch Branch = new ClickUIBranch("DialogMaster", 9);

        public GameDialogs()
        {
            Event.Subscribe<DialogStarted>(StartDialog, this);
        }

        public void Update(TimeSpan delta)
        {
            if (!_displayingDialog)
                return;
            _currentDialog.Update(delta);
            if (_currentDialog.IsDone)
            {
                _displayingDialog = false;
                Branch.Remove(_currentDialog.Branch);
            }
        }

        public void Draw(Transform2 parentTransform)
        {
            if (_displayingDialog)
                _currentDialog.Draw(parentTransform);
        }

        private void StartDialog(DialogStarted e)
        {
            _currentDialog = new GameDialog(e.Dialogs);
            Branch.Add(_currentDialog.Branch);
            _displayingDialog = true;
        } 
    }
}
