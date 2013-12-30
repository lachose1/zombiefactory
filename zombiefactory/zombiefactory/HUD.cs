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
    public class HUD : Microsoft.Xna.Framework.DrawableGameComponent
    {
        ZombieGame ZombieGame { get; set; }
        string GunName { get; set; }
        string ClipAmmo { get; set; }
        string Ammo { get; set; }
        string FontName { get; set; }
        SpriteFont FontDisplay { get; set; }
        Vector2 ScreenSize { get; set; }

        public HUD(ZombieGame game, string fontName)
            : base(game)
        {
            ZombieGame = game;
            FontName = fontName;
        }

        public override void Initialize()
        {
            GunName = "No Gun";
            ClipAmmo = "0";
            Ammo = "0";

            ScreenSize = new Vector2(ZombieGame.GraphicsDevice.DisplayMode.Width,
                ZombieGame.GraphicsDevice.DisplayMode.Height);

            base.Initialize();

            LoadContent();
        }

        protected override void LoadContent()
        {
            FontDisplay = ZombieGame.FontMgr.Find(FontName);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            GunName = ZombieGame.Player.Gun.GunName;
            if (ZombieGame.Player.Gun.InfiniteAmmo)
            {
                ClipAmmo = "Inf";
                Ammo = "Inf";
            }
            else
            {
                ClipAmmo = ZombieGame.Player.Gun.ClipAmmo.ToString();
                Ammo = ZombieGame.Player.Gun.Ammo.ToString();
            }


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            ZombieGame.SpriteBatch.DrawString(FontDisplay, GunName, new Vector2(100, 100), Color.Black, 0,
                Vector2.Zero, 1.0f, SpriteEffects.None, 0);
            ZombieGame.SpriteBatch.DrawString(FontDisplay, ClipAmmo, new Vector2(100, 120), Color.Black, 0,
                Vector2.Zero, 1.0f, SpriteEffects.None, 0);
            ZombieGame.SpriteBatch.DrawString(FontDisplay, Ammo, new Vector2(100, 140), Color.Black, 0,
                Vector2.Zero, 1.0f, SpriteEffects.None, 0);

            base.Draw(gameTime);
        }
    }
}
