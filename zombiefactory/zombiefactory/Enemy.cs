using System;
using Microsoft.Xna.Framework;

namespace zombiefactory
{
    public class Enemy : Character
    {
        #region constants
        public const string SPRITE_NAME = "Link";
        public const int SPRITE_FRAMES = 3;
        public const int SPRITE_LINES = 4;
        public const float DEPTH = 0.1f;
        public const float UPDATE_TIME = 1.0f / 10.0f;
        #endregion constants

        #region properties
        int Damage { get; set; }
        public bool IsMoving { get; protected set; } //Useless? Delete?
        #endregion properties

        public Enemy(ZombieGame game, Vector2 initPos, float maxSpeed, int maxHealth, int damage)
            : base(game, SPRITE_NAME, SPRITE_FRAMES, SPRITE_LINES, initPos, DEPTH, UPDATE_TIME, maxHealth, maxSpeed)
        {
            Damage = damage;
            Sprite.IsLooping = true;
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
            //TODO: Run AI code for direction, according to enemy type
            //Currently simply looks at the player
            float PlayerX = ZombieGame.Player.Sprite.Position.X;
            float PlayerY = ZombieGame.Player.Sprite.Position.Y;

            if (PlayerY > Sprite.Position.Y)
            {
                if ((PlayerY - Sprite.Position.Y) > Math.Abs(PlayerX - Sprite.Position.X))
                    Sprite.CurLine = (int)Direction.Down;
                else if (PlayerX > Sprite.Position.X)
                    Sprite.CurLine = (int)Direction.Right;
                else
                    Sprite.CurLine = (int)Direction.Left;
            }
            else
            {
                if (Math.Abs(PlayerY - Sprite.Position.Y) > Math.Abs(PlayerX - Sprite.Position.X))
                    Sprite.CurLine = (int)Direction.Up;
                else if (PlayerX > Sprite.Position.X)
                    Sprite.CurLine = (int)Direction.Right;
                else
                    Sprite.CurLine = (int)Direction.Left;
            }

        }

        protected override void MoveSprite()
        {
            //TODO: Run AI code for movement, according to enemy type
            //Currently simply follows the player
            float x = Sprite.Position.X;
            float y = Sprite.Position.Y;

            Vector2 distance = new Vector2(ZombieGame.Player.Sprite.Position.X - x,
                ZombieGame.Player.Sprite.Position.Y - y);
            distance.Normalize();

            Speed = distance * MaxSpeed;

            x += Speed.X / ZombieGame.FpsHandler.FpsValue;
            y += Speed.Y / ZombieGame.FpsHandler.FpsValue;

            if(!IsCollision(x, y))
                Sprite.Position = new Vector2(x, y);
        }

        protected override bool IsCollision(float x, float y)
        {
            if (IsTerrainCollision(x, y))
                return true;

            if (IsPlayerCollision(x, y))
                return true;

            return false;
        }

        private bool IsPlayerCollision(float x, float y)
        {
            Rectangle futureEnemyRect = new Rectangle((int)x, (int)y, Sprite.FrameWidth, Sprite.FrameHeight);
            Rectangle playerRect = new Rectangle((int)ZombieGame.Player.Sprite.Position.X, (int)ZombieGame.Player.Sprite.Position.Y,
                    ZombieGame.Player.Sprite.FrameWidth, ZombieGame.Player.Sprite.FrameHeight);

            if (futureEnemyRect.Intersects(playerRect))
            {
                ZombieGame.Player.TakeDamage(Damage);
                return true;
            }

            return false;
        }
    }
}
