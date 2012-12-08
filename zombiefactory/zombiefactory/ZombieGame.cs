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
    public class ZombieGame : Microsoft.Xna.Framework.Game
    {
        const float FPS_INTERVAL = 1.0f;

        #region properties
        public GraphicsDeviceManager Graphics;
        public SpriteBatch SpriteBatch;
        public InputManager InputMgr { get; private set; }
        public ResourceManager<Texture2D> TextureMgr { get; private set; }
        public ResourceManager<SpriteFont> FontMgr { get; private set; }
        public ResourceManager<Song> MusicMgr { get; private set; }
        public ResourceManager<SoundEffect> SfxMgr { get; private set; }
        public FpsCounter FpsHandler { get; private set; }
        private FpsDisplay FpsDisplayer { get; set; }
        #endregion properties

        public ZombieGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Graphics.SynchronizeWithVerticalRetrace = true;
            IsFixedTimeStep = true;
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            //Resource Managers
            FontMgr = new ResourceManager<SpriteFont>(this);
            TextureMgr = new ResourceManager<Texture2D>(this);
            MusicMgr = new ResourceManager<Song>(this);
            SfxMgr = new ResourceManager<SoundEffect>(this);

            LoadAssets();

            FpsHandler = new FpsCounter(this, FPS_INTERVAL);
            InputMgr = new InputManager(this);
            FpsDisplayer = new FpsDisplay(this, "Arial14");

            Components.Add(FpsHandler);
            Components.Add(InputMgr);
            Components.Add(FpsDisplayer);
            //Test Sprite
            Components.Add(new AnimatedSprite(this, "Link", 3, 4, new Vector2(100.0f, 100.0f)));

            base.Initialize();
        }

        private void LoadAssets()
        {
            FontMgr.Add("Fonts/Arial14");
            TextureMgr.Add("Sprites/Link");
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (InputMgr.ControllerState.Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override bool BeginDraw()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            SpriteBatch.Begin();

            return base.BeginDraw();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Juste un test, dans la vraie vie c'est de l'hostie de marde parce que ca fait Find() a chaque frame
            SpriteBatch.DrawString(FontMgr.Find("Arial14"), "Zombie Factory", new Vector2(0, 0), Color.Black);

            base.Draw(gameTime);
        }

        protected override void EndDraw()
        {
            SpriteBatch.End();

            base.EndDraw();
        }
    }
}
