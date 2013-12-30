using System;
using Microsoft.Xna.Framework;

namespace zombiefactory
{
    public abstract class Enemy : Character
    {

        #region properties
        int Damage { get; set; }
        float AttackDelay { get; set; }
        protected float TimeSinceLastAttack { get; set; }
        #endregion properties

        public Enemy(ZombieGame game, Vector2 initPos, string spriteName, int spriteFrames, int spriteLines, float depth, float updateTime, int maxHealth, int damage, float maxSpeed, float attackDelay)
            : base(game, spriteName, spriteFrames, spriteLines, initPos, depth, updateTime, maxHealth, maxSpeed)
        {
            Damage = damage;
            Sprite.IsLooping = true;
            DamageSound = ZombieGame.SfxMgr.Find(spriteName + "Damage");
            AttackDelay = attackDelay;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            TimeSinceLastAttack += 1.0f / ZombieGame.FpsHandler.FpsValue;

            SetSpriteDirection();
            MoveSprite();

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        protected void SetSpriteDirection()
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

            if (!IsCollision(x, y))
                Sprite.Position = new Vector2(x, y);
        }

        protected override bool IsCollision(float x, float y)
        {
            if (IsTerrainCollision(x, y))
                return true;

            if (IsPlayerCollision(x, y))
            {
                if (TimeSinceLastAttack > AttackDelay)
                {
                    ZombieGame.Player.TakeDamage(Damage);
                    TimeSinceLastAttack = 0.0f;
                }
                return true;
            }

            if (IsEnemyCollision(x, y))
                return true;

            return false;
        }

        protected override bool IsEnemyCollision(float x, float y)
        {
            Rectangle futureEnemyRect = new Rectangle((int)x, (int)y, Sprite.FrameWidth, Sprite.FrameHeight);

            foreach (Enemy enemy in ZombieGame.EnemySpawner.ActiveEnemies)
            {
                if (this != enemy)
                {
                    Rectangle enemyRect = new Rectangle((int)enemy.Sprite.Position.X, (int)enemy.Sprite.Position.Y,
                        enemy.Sprite.FrameWidth, enemy.Sprite.FrameHeight);

                    if (futureEnemyRect.Intersects(enemyRect))
                        return true;
                }
            }

            return false;
        }

        private bool IsPlayerCollision(float x, float y)
        {
            Rectangle futureEnemyRect = new Rectangle((int)x, (int)y, Sprite.FrameWidth, Sprite.FrameHeight);
            Rectangle playerRect = new Rectangle((int)ZombieGame.Player.Sprite.Position.X, (int)ZombieGame.Player.Sprite.Position.Y,
                    ZombieGame.Player.Sprite.FrameWidth, ZombieGame.Player.Sprite.FrameHeight);

            return futureEnemyRect.Intersects(playerRect);
        }
    }
}
