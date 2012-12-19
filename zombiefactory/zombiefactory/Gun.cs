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
    public class Gun : Microsoft.Xna.Framework.DrawableGameComponent
    {

        #region properties
        ZombieGame ZombieGame { get; set; }
        AnimatedSprite Sprite { get; set; }
        float Direction { get; set; }
        int Damage { get; set; }
        int Ammo { get; set; }
        bool Shooting { get; set; }
        #endregion properties

        public Gun(ZombieGame game, Vector2 initPos)
            : base(game)
        {
            ZombieGame = game;
            Sprite = new AnimatedSprite(ZombieGame, "Pistol", 1, 1, initPos);
            Sprite.Origin = new Vector2(0, Sprite.Height / 2);
            Shooting = false;
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
            //Vector2 PlayerPosition = Player.GetPosition();
            //float x = PlayerPosition.X + Player.Sprite.Width / 2;
            //float y = PlayerPosition.Y + Player.Sprite.Height / 2;

            //Sprite.Origin = new Vector2(x, y);
            if (Math.Abs(ZombieGame.InputMgr.ControllerState.ThumbSticks.Right.X) > 0.04f || Math.Abs(ZombieGame.InputMgr.ControllerState.ThumbSticks.Right.Y) > 0.04f)
            {
                Sprite.Rotation = -(float)Math.Atan2((double)ZombieGame.InputMgr.ControllerState.ThumbSticks.Right.Y, (double)ZombieGame.InputMgr.ControllerState.ThumbSticks.Right.X);
                Shooting = true;
            }
            else
                Shooting = false;
        }

        private void MoveSprite()
        {
            Vector2 PlayerPosition = ZombieGame.Player.GetPosition();
            float x = PlayerPosition.X + ZombieGame.Player.Sprite.Width / 2 + Sprite.Width / 2;
            float y = PlayerPosition.Y + ZombieGame.Player.Sprite.Height / 2;

            Sprite.Position = new Vector2(x, y);
        }

        public Vector2 GetPosition()
        {
            return Sprite.Position;
        }
    }
}
