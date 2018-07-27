
using System;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.Characters
{
    public abstract class Character: IVisualAutomaton
    {
        protected CharacterBody Body { get; }
        public CharacterData Stats { get; }

        public GameTile CurrentTile
        {
            get => Body.CurrentTile;
            set
            {
                Body.CurrentTile = value;
                value.OnCharacterSteps(this);
            }
        }

        public Character(CharacterBody body, CharacterData data)
        {
            Body = body;
            Stats = data;
        }

        public void Init()
        {
            Body.Init();
        }

        public void Draw(Transform2 parentTransform)
        {
            Body.Draw(parentTransform);
        }

        public void Update(TimeSpan delta)
        {
            Body.Update(delta);
        }
    }
}
