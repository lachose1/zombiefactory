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
        Player Player { get; set; }
        float Direction { get; set; }
        int Damage { get; set; }
        int Ammo { get; set; }
        #endregion properties

        public Gun(ZombieGame game, Player player, Vector2 initPos)
            : base(game)
        {
            ZombieGame = game;
            Player = player;
            Sprite = new AnimatedSprite(ZombieGame, "Pistol", 1, 1, initPos);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            //SetSpriteDirection();
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
            
        }

        private void MoveSprite()
        {
            Vector2 PlayerPosition = Player.GetPosition();
            float x = PlayerPosition.X;
            float y = PlayerPosition.Y;

            Sprite.Position = new Vector2(x, y);
        }

        public Vector2 GetPosition()
        {
            return Sprite.Position;
        }
    }
}
