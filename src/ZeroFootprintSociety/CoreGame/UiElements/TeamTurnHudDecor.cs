using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.UserInterface;

namespace ZeroFootPrintSociety.CoreGame.UiElements
{
    class TeamTurnHudDecor : IVisual
    {
        private static readonly Color EnemyColor = Color.FromNonPremultiplied(255, 0, 0, 120);
        private static readonly Color FriendlyColor = Color.FromNonPremultiplied(196, 233, 246, 176);

        public void Draw(Transform2 parentTransform)
        {
            var color = GameWorld.IsEnemyTurn ? EnemyColor : FriendlyColor;
            var text = GameWorld.IsEnemyTurn ? "Enemy Turn" : "Player Turn";

            UI.DrawTextCentered(text, new Rectangle(0.0.VW(), 0.034.VH(), 1.0.VW(), 22), color, "Fonts/18");
            UI.Draw("UI/border-top.png", new Transform2(new Vector2(0.06.VW(), 0.015.VH()), new Size2(0.86.VW(), 22)), color);
            UI.Draw("UI/border-side.png", new Transform2(new Vector2(0.008.VW(), 0.04.VH()), new Size2(22, 0.8.VH())), color);
            UI.DrawWithSpriteEffects("UI/border-side.png", new Transform2(new Vector2(0.978.VW(), 0.04.VH()), new Size2(22, 0.8.VH())), color, SpriteEffects.FlipHorizontally);
        }
    }
}
