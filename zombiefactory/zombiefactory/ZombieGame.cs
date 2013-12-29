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
        private FpsDisplay FpsDisplayer { get; set; } //Does not follow the camera yet
        public Player Player { get; private set; }
        public Level Level { get; private set; }
        public Camera Camera { get; private set; }
        //public List<Enemy> Enemies { get; private set; }
        public EnemySpawner EnemySpawner { get; set; }
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

            Player = new Player(this, new Vector2(100.0f, 100.0f));
            Level = new Level(this, "testlvl");
            Camera = new Camera(this);
            //Enemies = new List<Enemy>();
            //Enemies.Add(new Enemy(this, new Vector2(200.0f, 300.0f), 75.0f, 100, 10));
            EnemySpawner = new EnemySpawner(this, 100, true, 0.5f, new Vector2(200.0f, 300.0f));

            FpsDisplayer = new FpsDisplay(this, "Arial14");
            FpsDisplayer.Enabled = false; //We don't want the FPS to be shown as default

            Components.Add(FpsHandler);
            Components.Add(InputMgr);
            Components.Add(FpsDisplayer);
            Components.Add(Level);
            Components.Add(Player);
            Components.Add(EnemySpawner);
            Components.Add(Camera);

            base.Initialize();
        }

        private void LoadAssets()
        {
            FontMgr.Add("Fonts/Arial14");
            TextureMgr.Add("Sprites/Link");
            TextureMgr.Add("Sprites/character-4directions");
            TextureMgr.Add("Sprites/Pistol");
            TextureMgr.Add("Sprites/SMG");
            TextureMgr.Add("Sprites/Bullet");
            TextureMgr.Add("Tilesets/alttp_tiles");
            TextureMgr.Add("Sprites/Monolith");

            SfxMgr.Add("Sounds/PistolShot");
            SfxMgr.Add("Sounds/SMGShot");
            SfxMgr.Add("Sounds/DefaultDamage");
            SfxMgr.Add("Sounds/LinkDamage");
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
            if (InputMgr.ControllerState.Buttons.Back == ButtonState.Pressed || !Player.IsAlive) //Back quits game for now
                this.Exit();

            if (InputMgr.IsNewKey(Keys.F)) //F toggles FPS displayer
                FpsDisplayer.Enabled = !FpsDisplayer.Enabled;

            //Enemies.RemoveAll(enemy => !enemy.IsAlive);

            //foreach (Enemy enemy in Enemies)
                //enemy.Update(gameTime);

            base.Update(gameTime);
        }

        protected override bool BeginDraw()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp,
                DepthStencilState.Default, RasterizerState.CullNone, null, Camera.Transform);

            return base.BeginDraw();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //foreach (Enemy enemy in Enemies)
                //enemy.Draw(gameTime);

            base.Draw(gameTime);
        }

        protected override void EndDraw()
        {
            SpriteBatch.End();

            base.EndDraw();
        }
    }
}
