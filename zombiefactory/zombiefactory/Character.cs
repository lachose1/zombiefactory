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
        public int MaxHealth {get; private set; }
        private int health;
        public int Health
        { 
            get
            {
                return health;
            }
            private set
            {
                if (value > MaxHealth)
                    health = MaxHealth;
                else if (value <= 0)
                {
                    health = 0;
                    IsAlive = false;
                }
                else
                    health = value;
            }
        }
        public bool IsAlive { get; private set; }
        #endregion properties

        public Character(ZombieGame game, string spriteName, int spriteFrames, int spriteLines, Vector2 initPos, float depth, float updateTime, int maxHealth)
            : base(game)
        {
            ZombieGame = game;
            Sprite = new AnimatedSprite(ZombieGame, spriteName, spriteFrames, spriteLines, initPos, depth, updateTime);
            MaxHealth = maxHealth;
            Health = MaxHealth;
            IsAlive = true;
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

        protected bool IsTerrainCollision(float x, float y)
        {
            int[] tileType = new int[4]; // Tiles a 4 points, un sur chaque cote du sprite
            tileType[(int)Direction.Up] = ZombieGame.Level.TileType[(int)(y / ZombieGame.Level.Tileset.TileHeight),
                (int)((x + Sprite.FrameWidth / 2) / ZombieGame.Level.Tileset.TileWidth)]; //Evidemment ne fonctionne pas en x < 0 ou y < 0
            tileType[(int)Direction.Right] = ZombieGame.Level.TileType[(int)((y + Sprite.FrameHeight / 2) / ZombieGame.Level.Tileset.TileHeight),
                (int)((x + Sprite.FrameWidth) / ZombieGame.Level.Tileset.TileWidth)];
            tileType[(int)Direction.Down] = ZombieGame.Level.TileType[(int)((y + Sprite.FrameHeight) / ZombieGame.Level.Tileset.TileHeight),
                (int)((x + Sprite.FrameWidth / 2) / ZombieGame.Level.Tileset.TileWidth)];
            tileType[(int)Direction.Left] = ZombieGame.Level.TileType[(int)((y + Sprite.FrameHeight / 2) / ZombieGame.Level.Tileset.TileHeight),
                    (int)(x / ZombieGame.Level.Tileset.TileWidth)];

            for (int i = 0; i < (int)Direction.NbDirections; ++i)
            {
                if (tileType[i] == 82 || tileType[i] == 146 || tileType[i] == 147 || tileType[i] == 162 || tileType[i] == 163)
                    return true;
            }

            return false;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }

        public void RestoreHealth(int amount)
        {
            Health += amount;
        }
    }
}
