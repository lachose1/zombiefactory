using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace zombiefactory
{
    public class Player : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public enum Direction { Up, Right, Down, Left };
        public const string SPRITE_NAME = "Link";
        public const int SPRITE_FRAMES = 3;
        public const int SPRITE_LINES = 4;

        #region properties
        ZombieGame ZombieGame { get; set; }
        public Vector2 Position { get; private set; }
        AnimatedSprite Sprite { get; set; }
        #endregion properties

        public Player(ZombieGame game, Vector2 initPos)
            : base(game)
        {
            ZombieGame = game;
            Position = initPos;
            Sprite = new AnimatedSprite(ZombieGame, SPRITE_NAME, SPRITE_FRAMES, SPRITE_LINES, Position);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (ZombieGame.InputMgr.ControllerState.ThumbSticks.Left == Vector2.Zero)
            {
                Sprite.IsLooping = false;
            }
            else
            {
                Sprite.IsLooping = true;

                if (ZombieGame.InputMgr.ControllerState.ThumbSticks.Left.Y > 0)
                {
                    if (ZombieGame.InputMgr.ControllerState.ThumbSticks.Left.Y > Math.Abs(ZombieGame.InputMgr.ControllerState.ThumbSticks.Left.X))
                        Sprite.CurLine = (int)Direction.Up;
                    else if (ZombieGame.InputMgr.ControllerState.ThumbSticks.Left.X > 0)
                        Sprite.CurLine = (int)Direction.Right;
                    else
                        Sprite.CurLine = (int)Direction.Left;
                }
                else
                {
                    if (Math.Abs(ZombieGame.InputMgr.ControllerState.ThumbSticks.Left.Y) > Math.Abs(ZombieGame.InputMgr.ControllerState.ThumbSticks.Left.X))
                        Sprite.CurLine = (int)Direction.Down;
                    else if (ZombieGame.InputMgr.ControllerState.ThumbSticks.Left.X > 0)
                        Sprite.CurLine = (int)Direction.Right;
                    else
                        Sprite.CurLine = (int)Direction.Left;
                }
            }

            Sprite.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Sprite.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
