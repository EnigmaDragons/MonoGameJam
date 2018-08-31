using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoDragons.Core;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Memory;
using MonoDragons.Core.Scenes;
using ZeroFootPrintSociety.CoreGame;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.GUI
{
    class TileRotatingEdgesAnim : SceneContainer, IInitializable
    {
        private MustInit<SpriteAnimation> _anim = new MustInit<SpriteAnimation>($"{nameof(TileRotatingEdgesAnim)}.{nameof(_anim)}");
        private Color _color;

        public TileRotatingEdgesAnim(Point tile, Color color)
        {
            base.GetOffset = () => GameWorld.Map.TileToWorldTransform(tile);
            _color = color;
        }

        public void Init()
        {
            const float scale = 1.9f;
            const float duration = 0.12f;
            if (!_anim.IsInitialized)
                _anim.Init(new SpriteAnimation(
                    new SpriteAnimationFrame(Resources.Load<Texture2D>($"Effects/RotatingHighlight/2.png"), scale, duration) { Tint = _color },
                    new SpriteAnimationFrame(Resources.Load<Texture2D>($"Effects/RotatingHighlight/3.png"), scale, duration) { Tint = _color },
                    new SpriteAnimationFrame(Resources.Load<Texture2D>($"Effects/RotatingHighlight/4.png"), scale, duration) { Tint = _color },
                    new SpriteAnimationFrame(Resources.Load<Texture2D>($"Effects/RotatingHighlight/5.png"), scale, duration) { Tint = _color },
                    new SpriteAnimationFrame(Resources.Load<Texture2D>($"Effects/RotatingHighlight/6.png"), scale, duration) { Tint = _color },
                    new SpriteAnimationFrame(Resources.Load<Texture2D>($"Effects/RotatingHighlight/7.png"), scale, duration) { Tint = _color },
                    new SpriteAnimationFrame(Resources.Load<Texture2D>($"Effects/RotatingHighlight/8.png"), scale, duration) { Tint = _color }));
            Add(_anim.Get());
        }
    }
}
