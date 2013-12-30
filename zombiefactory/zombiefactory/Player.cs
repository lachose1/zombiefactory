using System;
using Microsoft.Xna.Framework;

namespace zombiefactory
{
    public class Player : Character
    {
        #region constants
        public const string SPRITE_NAME = "character-4directions";
        public const int SPRITE_FRAMES = 3;
        public const int SPRITE_LINES = 4;
        public const float MAX_SPEED = 150.0f;
        public const float DEPTH = 0.1f;
        public const float UPDATE_TIME = 1.0f / 7.5f;
        public const int MAX_HEALTH = 100;
        #endregion constants

        #region properties
        //Pistol Gun { get; set; }
        Shotgun Gun { get; set; }
        // SMG Gun { get; set; }
        #endregion properties

        public Player(ZombieGame game, Vector2 initPos)
            : base(game, SPRITE_NAME, SPRITE_FRAMES, SPRITE_LINES, initPos, DEPTH, UPDATE_TIME, MAX_HEALTH, MAX_SPEED)
        {
            //Gun = new Pistol(game, initPos);
            Gun = new Shotgun(game, initPos);
            // Gun = new SMG(game, initPos);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            SetSpriteDirection();
            MoveSprite();

            Gun.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Gun.Draw(gameTime);

            base.Draw(gameTime);
        }

        private void SetSpriteDirection()
        {
            Vector2 directionStick; //Stick currently used to set sprite direction

            if (ZombieGame.InputMgr.ControllerState.ThumbSticks.Left == Vector2.Zero) //If sprite is moving loop anim
                Sprite.IsLooping = false;
            else
                Sprite.IsLooping = true;

            if (ZombieGame.InputMgr.ControllerState.ThumbSticks.Right != Vector2.Zero) //As soon as aim (right stick) is used, sprite takes that direction, else the direction of the movement
                directionStick = ZombieGame.InputMgr.ControllerState.ThumbSticks.Right;
            else
                directionStick = ZombieGame.InputMgr.ControllerState.ThumbSticks.Left;

            if (directionStick != Vector2.Zero)
            {
                if (directionStick.Y > 0)
                {
                    if (directionStick.Y > Math.Abs(directionStick.X))
                        Sprite.CurLine = (int)Direction.Up;
                    else if (directionStick.X > 0)
                        Sprite.CurLine = (int)Direction.Right;
                    else
                        Sprite.CurLine = (int)Direction.Left;
                }
                else
                {
                    if (Math.Abs(directionStick.Y) > Math.Abs(directionStick.X))
                        Sprite.CurLine = (int)Direction.Down;
                    else if (directionStick.X > 0)
                        Sprite.CurLine = (int)Direction.Right;
                    else
                        Sprite.CurLine = (int)Direction.Left;
                }
            }
        }

        protected override void MoveSprite()
        {
            float x = Sprite.Position.X;
            float y = Sprite.Position.Y;
            float speedX = ZombieGame.InputMgr.ControllerState.ThumbSticks.Left.X * MaxSpeed;
            float speedY = ZombieGame.InputMgr.ControllerState.ThumbSticks.Left.Y * MaxSpeed;

            Speed = new Vector2(speedX, speedY);

            x += Speed.X / ZombieGame.FpsHandler.FpsValue;
            y -= Speed.Y / ZombieGame.FpsHandler.FpsValue;

            if(!IsCollision(x, y))
                Sprite.Position = new Vector2(x, y);
        }

        protected override bool IsCollision(float x, float y)
        {
            //It's important that the terrain collision is done before the enemy collision, because it is much quicker than enemy collision, so we can avoid wasted cycles
            if (IsTerrainCollision(x, y))
                return true;

            if (IsEnemyCollision(x, y)) //Eventually this will loop and test for every enemy's rectangle
                return true;

            return false;
        }

        protected override bool IsEnemyCollision(float x, float y)
        {
            Rectangle futurePlayerRect = new Rectangle((int)x, (int)y, Sprite.FrameWidth, Sprite.FrameHeight);

            foreach (Enemy enemy in ZombieGame.EnemySpawner.ActiveEnemies)
            {
                Rectangle enemyRect = new Rectangle((int)enemy.Sprite.Position.X, (int)enemy.Sprite.Position.Y,
                    enemy.Sprite.FrameWidth, enemy.Sprite.FrameHeight);

                if (futurePlayerRect.Intersects(enemyRect))
                    return true;
            }

            return false;
        }
    }
}
