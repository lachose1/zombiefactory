using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace zombiefactory
{
    
    public class EnemySpawner : Emitter
    {

        #region properties
        public LinkedList<Enemy> ActiveEnemies { get; set; }
        #endregion properties

        public EnemySpawner(ZombieGame game, int maxEnemies, bool automaticSpawn, float timeBetweenSpawn, Vector2 position)
            : base(game, maxEnemies, automaticSpawn, timeBetweenSpawn, position)
        {
            ActiveEnemies = new LinkedList<Enemy>();
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
                addEnemy("Zombie");

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

        public bool addEnemy(string Name)
        {
            bool enemyAdded = TimeSinceLastSpawn >= TimeBetweenSpawn;

            if (enemyAdded)
            {
                ActiveEnemies.AddLast(new Zombie(ZombieGame, Position));
                TimeSinceLastSpawn = 0.0f;
            }

            return enemyAdded;
        }
    }
}
