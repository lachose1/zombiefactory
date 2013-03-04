using System;
using Microsoft.Xna.Framework;

namespace zombiefactory
{
    class Ennemy : Character
    {
        #region constants
        public const string SPRITE_NAME = "Link";
        public const int SPRITE_FRAMES = 3;
        public const int SPRITE_LINES = 4;
        public const float MAX_SPEED = 150.0f;
        public const float DEPTH = 0.1f;
        #endregion constants

        public Ennemy(ZombieGame game, Vector2 initPos)
            : base(game, SPRITE_NAME, SPRITE_FRAMES, SPRITE_LINES, initPos, DEPTH)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            SetSpriteDirection();
            MoveSprite();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        private void SetSpriteDirection()
        {

        }

        protected override void MoveSprite()
        {

        }

        protected override bool IsCollision(float x, float y)
        {
            return false;
        }
    }
}
