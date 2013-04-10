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
    
    public class EnemySpawner : Microsoft.Xna.Framework.DrawableGameComponent
    {

        ZombieGame ZombieGame { get; set; }
        int MaxEnemies { get; set; }
        bool AutomaticSpawn { get; set; }
        float TimeBetweenSpawn { get; set; }
        float TimeSinceLastSpawn { get; set; }
        public LinkedList<Enemy> ActiveEnemies { get; set; }
        Vector2 Position { get; set; }
        
        public EnemySpawner(ZombieGame game, int maxEnemies, bool automaticSpawn, float timeBetweenSpawn, Vector2 position)
            : base(game)
        {
            ZombieGame = game;
            MaxEnemies = maxEnemies;
            AutomaticSpawn = automaticSpawn;
            TimeBetweenSpawn = timeBetweenSpawn;
            TimeSinceLastSpawn = TimeBetweenSpawn;
            Position = position;
            ActiveEnemies = new LinkedList<Enemy>();
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            LinkedListNode<Enemy> node = ActiveEnemies.First;
            while (node != null)
            {
                node.Value.Update(gameTime);
                bool alive = node.Value.IsAlive;
                node = node.Next;

                if (node != null)
                {
                    if (!alive)
                        ActiveEnemies.Remove(node.Previous.Value);
                }
                else
                {
                    if (!alive)
                        ActiveEnemies.RemoveLast();
                }
            }
            if (AutomaticSpawn)
                addEnemy(75.0f, 10, 10);

            TimeSinceLastSpawn += 1.0f / ZombieGame.FpsHandler.FpsValue;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            LinkedListNode<Enemy> node = ActiveEnemies.First;
            while (node != null)
            {
                node.Value.Draw(gameTime);
                node = node.Next;
            }
        }

        public bool addEnemy(float maxSpeed, int maxHealth, int damage)
        {
            bool enemyAdded = TimeSinceLastSpawn >= TimeBetweenSpawn;

            if(enemyAdded)
            {
                ActiveEnemies.AddLast(new Enemy(ZombieGame, Position, maxSpeed, maxHealth, damage));
                TimeSinceLastSpawn = 0.0f;
            }

            return enemyAdded;
        }
    }
}
