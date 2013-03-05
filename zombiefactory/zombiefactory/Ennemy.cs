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
        public const float DEPTH = 0.1f;
        #endregion constants

        #region properties
        public float MaxSpeed { get; protected set; }
        public bool IsMoving { get; protected set; }
        #endregion properties

        public Ennemy(ZombieGame game, Vector2 initPos, float maxSpeed)
            : base(game, SPRITE_NAME, SPRITE_FRAMES, SPRITE_LINES, initPos, DEPTH)
        {
            MaxSpeed = maxSpeed;
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
            //Run AI code for direction
            float PlayerX = ZombieGame.Player.Sprite.Position.X;
            float PlayerY = ZombieGame.Player.Sprite.Position.Y;

            if (PlayerY > Sprite.Position.Y)
            {
                if (PlayerY > Math.Abs(PlayerX))
                    Sprite.CurLine = (int)Direction.Up;
                if (PlayerX < Sprite.Position.X)
                    Sprite.CurLine = (int)Direction.Left;
                else
                    Sprite.CurLine = (int)Direction.Right;
            }
            else
            {
                if (PlayerX > Math.Abs(PlayerY))
                    Sprite.CurLine = (int)Direction.Up;
                if (PlayerX < Sprite.Position.X)
                    Sprite.CurLine = (int)Direction.Left;
                else
                    Sprite.CurLine = (int)Direction.Right;
            }

        }

        protected override void MoveSprite()
        {
            //Run AI code for movement
            float x = Sprite.Position.X;
            float y = Sprite.Position.Y;
            int Direction = Sprite.CurLine;

            float speedX = ZombieGame.InputMgr.ControllerState.ThumbSticks.Left.X * MaxSpeed;
            float speedY = ZombieGame.InputMgr.ControllerState.ThumbSticks.Left.Y * MaxSpeed;

            Speed = new Vector2(speedX, speedY);

            x += Speed.X / ZombieGame.FpsHandler.FpsValue;
            y -= Speed.Y / ZombieGame.FpsHandler.FpsValue;
        }

        protected override bool IsCollision(float x, float y)
        {
            return false;
        }
    }
}
