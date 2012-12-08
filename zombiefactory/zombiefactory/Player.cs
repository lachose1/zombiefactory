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
        public enum Direction { Up, Right, Left, Down };
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
