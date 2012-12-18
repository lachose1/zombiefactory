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
        public const float MAX_SPEED = 150.0f;

        #region properties
        ZombieGame ZombieGame { get; set; }
        AnimatedSprite Sprite { get; set; }
        #endregion properties

        public Player(ZombieGame game, Vector2 initPos)
            : base(game)
        {
            ZombieGame = game;
            Sprite = new AnimatedSprite(ZombieGame, SPRITE_NAME, SPRITE_FRAMES, SPRITE_LINES, initPos);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            SetSpriteDirection();
            MoveSprite();

            Sprite.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Sprite.Draw(gameTime);

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

        private void MoveSprite()
        {
            float x = Sprite.Position.X;
            float y = Sprite.Position.Y;

            x += ZombieGame.InputMgr.ControllerState.ThumbSticks.Left.X * MAX_SPEED / ZombieGame.FpsHandler.FpsValue;
            y -= ZombieGame.InputMgr.ControllerState.ThumbSticks.Left.Y * MAX_SPEED / ZombieGame.FpsHandler.FpsValue;

            Sprite.Position = new Vector2(x, y);
        }

        public Vector2 GetPosition()
        {
            return Sprite.Position;
        }
    }
}
