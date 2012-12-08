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
    public class AnimatedSprite : Microsoft.Xna.Framework.DrawableGameComponent
    {
        #region properties
        ZombieGame ZombieGame { get; set; }
        Texture2D SpriteSheet { get; set; }
        public Vector2 Position { get; private set; }
        public Vector2 Origin { get; private set; }
        public float Rotation { get; private set; }
        public float Scale { get; private set; }
        public Color Color { get; private set; }
        Rectangle[,] Rectangles;
        public int Frames { get; private set; }
        public int Lines { get; private set; }
        public bool IsLooping { get; private set; }
        public int CurLine { get; private set; }
        public int CurFrame { get; private set; }
        public SpriteEffects Effects { get; private set; }
        public float Depth { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        #endregion properties

        public AnimatedSprite(ZombieGame game, string fileName, int frames, int lines, Vector2 position)
            : base(game)
        {
            ZombieGame = game;
            SpriteSheet = game.TextureMgr.Find(fileName);
            Frames = frames;
            Lines = lines;
            Position = position;

            CurLine = 0;
            CurFrame = 0;
            IsLooping = true;

            Rotation = 0.0f;
            Scale = 1.0f;
            Color = Color.White;
            Origin = Vector2.Zero;
            Effects = SpriteEffects.None;
            Depth = 0.0f;

            Width = SpriteSheet.Width / Frames;
            Height = SpriteSheet.Height / Lines;

            Rectangles = new Rectangle[Lines, Frames]; //Like a matrix, the first index is the row (line), the second the column (frame)

            for (int i = 0; i < Lines; ++i)
            {
                for (int j = 0; j < Frames; ++j)
                {
                    Rectangles[i, j] = new Rectangle(j * Width, i * Height, Width, Height);
                }
            }
        }

        public override void Initialize()
        {
            if (IsLooping)
            {
                CurFrame++;
                CurFrame %= Frames;
            }
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            ZombieGame.spriteBatch.Draw(SpriteSheet, Rectangles[CurLine, CurFrame], new Rectangle((int)Position.X, (int)Position.Y, Width, Height), Color, Rotation, Origin, Effects, Depth);

            base.Draw(gameTime);
        }
    }
}
