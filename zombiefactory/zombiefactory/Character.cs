using System;
using Microsoft.Xna.Framework;

namespace zombiefactory
{
    public abstract class Character : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public enum Direction { Up, Right, Down, Left, NbDirections };

        #region properties
        protected ZombieGame ZombieGame { get; set; }
        public AnimatedSprite Sprite { get; protected set; }
        public Vector2 Speed { get; protected set; }
        #endregion properties

        public Character(ZombieGame game, string spriteName, int spriteFrames, int spriteLines, Vector2 initPos, float depth, float updateTime)
            : base(game)
        {
            ZombieGame = game;
            Sprite = new AnimatedSprite(ZombieGame, spriteName, spriteFrames, spriteLines, initPos, depth, updateTime);
        }

        public override void Initialize()
        {
            Speed = Vector2.Zero;

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            Sprite.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Sprite.Draw(gameTime);

            base.Draw(gameTime);
        }

        protected abstract void MoveSprite();

        protected abstract bool IsCollision(float x, float y);
    }
}
