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
    
    public class EnnemySpawner : DrawableGameComponent
    {

        ZombieGame ZombieGame { get; set; }
        int MaxEnemies { get; set; }
        bool AutomaticSpawn { get; set; }
        float SpawnInterval { get; set; }
        public LinkedList<Enemy> ActiveEnnemies { get; set; }
        
        public EnnemySpawner(ZombieGame game, int maxEnemies, bool automaticSpawn, float spawnInterval)
            : base(game)
        {
            ZombieGame = game;
            MaxEnemies = maxEnemies;
            AutomaticSpawn = automaticSpawn;
            SpawnInterval = spawnInterval;
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

            base.Draw(gameTime);
        }
    }
}
