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
    public abstract class Emitter : Microsoft.Xna.Framework.DrawableGameComponent
    {
        #region properties
        protected ZombieGame ZombieGame { get; private set; }
        protected bool AutomaticSpawn { get; private set; }
        protected float TimeBetweenSpawn { get; private set; }
        protected float TimeSinceLastSpawn { get; set; }
        protected int MaxItems { get; set; }
        public Vector2 Position { get; set; }
        #endregion properties

        public Emitter(ZombieGame game, int maxItems, bool automaticSpawn, float timeBetweenSpawn, Vector2 position)
            : base(game)
        {
            ZombieGame = game;
            MaxItems = maxItems;
            AutomaticSpawn = automaticSpawn;
            TimeBetweenSpawn = timeBetweenSpawn;
            TimeSinceLastSpawn = TimeBetweenSpawn;
            Position = position;
        }

        public override void Update(GameTime gameTime)
        {
            TimeSinceLastSpawn += 1.0f / ZombieGame.FpsHandler.FpsValue;
            base.Update(gameTime);
        }
    }
}
