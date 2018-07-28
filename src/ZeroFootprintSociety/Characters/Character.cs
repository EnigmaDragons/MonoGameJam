
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.Characters
{
    public abstract class Character: IVisualAutomaton
    {
        protected CharacterBody Body { get; }
        public CharacterData Stats { get; }
        public CharacterGear Gear { get; }

        public GameTile CurrentTile
        {
            get => Body.CurrentTile;
            set
            {
                Body.CurrentTile = value;
                Body.CurrentTile.OnCharacterSteps(this);
            }
        }

        public Character(CharacterBody body, CharacterData data, CharacterGear gear)
        {
            Body = body;
            Stats = data;
            Gear = gear;
        }

        public void Move(List<Point> points) => Body.Move(points);

        public void Init(GameTile tile)
        {
            Body.Init(tile);
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
