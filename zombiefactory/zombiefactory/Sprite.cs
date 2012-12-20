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
    public class Sprite : Microsoft.Xna.Framework.DrawableGameComponent
    {
        #region properties
        public ZombieGame ZombieGame { get; set; }
        public Texture2D SpriteSheet { get; private set; }
        public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; }
        public float Rotation { get; set; }
        public float Scale { get; private set; }
        public Color Color { get; private set; }
        public SpriteEffects Effects { get; private set; }
        public float Depth { get; private set; }
        #endregion properties

        public Sprite(ZombieGame game, string fileName, Vector2 position, float depth)
            : base(game)
        {
            ZombieGame = game;
            SpriteSheet = ZombieGame.TextureMgr.Find(fileName);
            Position = position;
            Rotation = 0.0f;
            Scale = 2.0f;
            Color = Color.White;
            Origin = Vector2.Zero;
            Effects = SpriteEffects.None;
            Depth = depth;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            ZombieGame.SpriteBatch.Draw(SpriteSheet, new Vector2(Position.X, Position.Y),
                new Rectangle((int)Position.X, (int)Position.Y, SpriteSheet.Width, SpriteSheet.Height), Color, Rotation, Origin, Scale, Effects, Depth);

            base.Draw(gameTime);
        }
    }
}
