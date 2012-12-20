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
        public Player Player { get; private set; }
        public Gun Gun { get; private set; }
        public Level Level { get; private set; }
        #endregion properties

        public ZombieGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Graphics.SynchronizeWithVerticalRetrace = true;
            Graphics.PreferredBackBufferWidth = 800;
            Graphics.PreferredBackBufferHeight = 600;
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

            Player = new Player(this, new Vector2(100.0f, 100.0f));
            Gun = new Gun(this, new Vector2(100.0f, 100.0f));
            Level = new Level(this, "testlvl");

            Components.Add(FpsHandler);
            Components.Add(InputMgr);
            Components.Add(FpsDisplayer);
            Components.Add(Level);
            Components.Add(Player);
            Components.Add(Gun);

            base.Initialize();
        }

        private void LoadAssets()
        {
            FontMgr.Add("Fonts/Arial14");
            TextureMgr.Add("Sprites/Link");
            TextureMgr.Add("Sprites/Pistol");
            TextureMgr.Add("Sprites/Bullet");
            TextureMgr.Add("Tilesets/alttp_tiles");
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
            
            //EmitterTest.addParticle("Pistol", 1, 1, new Vector2(200.0f, 200.0f), new Vector2(300.0f, 300.0f), 200.0f);

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override bool BeginDraw()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            return base.BeginDraw();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }

        protected override void EndDraw()
        {
            SpriteBatch.End();

            base.EndDraw();
        }
    }
}
