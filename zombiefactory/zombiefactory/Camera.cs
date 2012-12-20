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
    public class Camera : Microsoft.Xna.Framework.GameComponent
    {
        public Matrix Transform { get; private set; }
        ZombieGame ZombieGame { get; set; }
        Viewport View { get; set; }
        Vector2 Centre { get; set; }
        Vector2 PlayerSize { get; set; }
        Vector2 ScreenSize { get; set; }

        public Camera(ZombieGame game)
            : base(game)
        {
            ZombieGame = game;
            View = game.GraphicsDevice.Viewport;
            PlayerSize = new Vector2(game.Player.Sprite.FrameWidth, game.Player.Sprite.FrameHeight);
            ScreenSize = new Vector2(game.Graphics.PreferredBackBufferWidth, game.Graphics.PreferredBackBufferHeight);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            Centre = new Vector2(ZombieGame.Player.Sprite.Position.X + PlayerSize.X / 2 - ScreenSize.X / 2,
                ZombieGame.Player.Sprite.Position.Y + PlayerSize.Y / 2 - ScreenSize.Y / 2);
            Transform = Matrix.CreateScale(new Vector3(1, 1, 0)) *
                Matrix.CreateTranslation(new Vector3(-Centre.X, -Centre.Y, 0));

            base.Update(gameTime);
        }
    }
}
