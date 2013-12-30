using System;
using Microsoft.Xna.Framework;

namespace zombiefactory
{
    public class Leaper : Enemy
    {
        #region constants
        public const string SPRITE_NAME = "Leaper";
        public const int SPRITE_FRAMES = 3;
        public const int SPRITE_LINES = 4;
        public const float DEPTH = 0.1f;
        public const float UPDATE_TIME = 1.0f / 10.0f;
        public const float ATTACK_DELAY = 0.5f; //seconds between attacks
        public const float MAX_SPEED = 20.0f;
        public const int MAX_HEALTH = 10;
        public const int MAX_DAMAGE = 10;
        public const float JUMP_DELAY = 2.0f; //seconds between jumps
        public const float JUMP_SPEED = 110.0f;
        public const float JUMP_TIME = 2.0f; //seconds of jump
        #endregion constants

        #region properties
        float TimeSinceLastJump { get; set; }
        float TimeInAir { get; set; }
        bool IsJumping { get; set; }
        Vector2 CurrentPlayerPosition { get; set; }
        #endregion properties

        public Leaper(ZombieGame game, Vector2 initPos)
            : base(game, initPos, SPRITE_NAME, SPRITE_FRAMES, SPRITE_LINES, DEPTH, UPDATE_TIME, MAX_HEALTH, MAX_DAMAGE, MAX_SPEED, ATTACK_DELAY)
        {
            IsJumping = false;
        }

        public override void Update(GameTime gameTime)
        {
            TimeSinceLastAttack += 1.0f / ZombieGame.FpsHandler.FpsValue;
            TimeSinceLastJump += 1.0f / ZombieGame.FpsHandler.FpsValue;
            
            SetSpriteDirection();
            MoveSprite();

            base.Update(gameTime);
        }

        protected override void MoveSprite()
        {
            float x = Sprite.Position.X;
            float y = Sprite.Position.Y;

            bool testCollisionFirst = IsCollision(x, y);

            if (IsJumping)
            {
                if (!testCollisionFirst)
                {
                    JumpAttack(CurrentPlayerPosition);
                    return;
                }
                else
                {
                    IsJumping = false;
                    TimeInAir = 0.0f;
                }
            }

            if (TimeSinceLastJump > JUMP_DELAY)
            {
                CurrentPlayerPosition = new Vector2(ZombieGame.Player.Sprite.Position.X - x,
                ZombieGame.Player.Sprite.Position.Y - y);
                JumpAttack(CurrentPlayerPosition);
                TimeSinceLastJump = 0.0f;
                return;
            }

            Vector2 distance = new Vector2(ZombieGame.Player.Sprite.Position.X - x,
                ZombieGame.Player.Sprite.Position.Y - y);
            distance.Normalize();

            Speed = distance * MaxSpeed;

            x += Speed.X / ZombieGame.FpsHandler.FpsValue;
            y += Speed.Y / ZombieGame.FpsHandler.FpsValue;

            if (!testCollisionFirst)
                Sprite.Position = new Vector2(x, y);
        }

        private void JumpAttack(Vector2 playerPosition)
        {
            float x = Sprite.Position.X;
            float y = Sprite.Position.Y;

            playerPosition.Normalize();

            Speed = playerPosition * JUMP_SPEED;

            x += Speed.X / ZombieGame.FpsHandler.FpsValue;
            y += Speed.Y / ZombieGame.FpsHandler.FpsValue;

            if (!IsCollision(x, y))
            {
                Sprite.Position = new Vector2(x, y);
            }

            TimeInAir += 1.0f / ZombieGame.FpsHandler.FpsValue;

            if (TimeInAir > JUMP_TIME)
            {
                IsJumping = false;
                TimeInAir = 0.0f;
            }
            else
                IsJumping = true;
        }
    }
}
