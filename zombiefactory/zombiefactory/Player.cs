using System;
using Microsoft.Xna.Framework;

namespace zombiefactory
{
    public class Player : Character
    {
        #region constants
        public const string SPRITE_NAME = "Link";
        public const int SPRITE_FRAMES = 3;
        public const int SPRITE_LINES = 4;
        public const float MAX_SPEED = 150.0f;
        public const float DEPTH = 0.1f;
        public const float UPDATE_TIME = 1.0f / 10.0f;
        #endregion constants

        #region properties
        Gun Gun { get; set; }
        #endregion properties

        public Player(ZombieGame game, Vector2 initPos)
            : base(game, SPRITE_NAME, SPRITE_FRAMES, SPRITE_LINES, initPos, DEPTH, UPDATE_TIME)
        {
            Gun = new Gun(game, initPos);
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
            float speedX = ZombieGame.InputMgr.ControllerState.ThumbSticks.Left.X * MAX_SPEED;
            float speedY = ZombieGame.InputMgr.ControllerState.ThumbSticks.Left.Y * MAX_SPEED;

            Speed = new Vector2(speedX, speedY);

            x += Speed.X / ZombieGame.FpsHandler.FpsValue;
            y -= Speed.Y / ZombieGame.FpsHandler.FpsValue;

            if(!IsCollision(x, y))
                Sprite.Position = new Vector2(x, y);
        }

        protected override bool IsCollision(float x, float y)
        {
            Rectangle futurePos = new Rectangle((int)x, (int)y, Sprite.FrameWidth, Sprite.FrameHeight);
            //Rectangle monolithRectangle = new Rectangle((int)ZombieGame.Monolith.Position.X, (int)ZombieGame.Monolith.Position.Y,
            //    ZombieGame.Monolith.Width, ZombieGame.Monolith.Height);

            int[] tileType = new int[4]; // Tiles a 4 points, un sur chaque cote du sprite
            tileType[(int)Direction.Up] = ZombieGame.Level.TileType[(int)(y / ZombieGame.Level.Tileset.TileHeight),
                (int)((x + Sprite.FrameWidth / 2) / ZombieGame.Level.Tileset.TileWidth)]; //Evidemment ne fonctionne pas si le player va en x < 0 ou y < 0
            tileType[(int)Direction.Right] = ZombieGame.Level.TileType[(int)((y + Sprite.FrameHeight / 2) / ZombieGame.Level.Tileset.TileHeight),
                (int)((x + Sprite.FrameWidth) / ZombieGame.Level.Tileset.TileWidth)];
            tileType[(int)Direction.Down] = ZombieGame.Level.TileType[(int)((y + Sprite.FrameHeight) / ZombieGame.Level.Tileset.TileHeight),
                (int)((x + Sprite.FrameWidth / 2) / ZombieGame.Level.Tileset.TileWidth)];
            tileType[(int)Direction.Left] = ZombieGame.Level.TileType[(int)((y + Sprite.FrameHeight / 2) / ZombieGame.Level.Tileset.TileHeight),
                    (int)(x / ZombieGame.Level.Tileset.TileWidth)];

            //It's important that the terrain collision is done before the enemy collision, because it is much quicker than enemy collision, so we can avoid wasted cycles
            for (int i = 0; i < (int)Direction.NbDirections; ++i)
            {
                if (tileType[i] == 82 || tileType[i] == 146 || tileType[i] == 147 || tileType[i] == 162 || tileType[i] == 163)
                    return true;
            }

            //if (futurePos.Intersects(monolithRectangle)) //Eventually this will loop and test for every enemy's rectangle
            //    return true;


            return false;
        }
    }
}
