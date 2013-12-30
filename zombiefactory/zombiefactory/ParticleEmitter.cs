using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace zombiefactory
{
    public class ParticleEmitter : Emitter
    {
        #region properties
        public LinkedList<Particle> ActiveParticles;
        #endregion properties

        public ParticleEmitter(ZombieGame game, int maxParticles, bool automaticSpawn, float timeBetweenSpawn, Vector2 position)
            : base(game, maxParticles, automaticSpawn, timeBetweenSpawn, position)
        {
            ActiveParticles = new LinkedList<Particle>();
        }

        public override void Update(GameTime gameTime)
        {
            while (ActiveParticles.Count > MaxItems)
                ActiveParticles.RemoveFirst();

            LinkedListNode<Particle> node = ActiveParticles.First;
            while (node != null)
            {
                node.Value.Update(gameTime);
                bool alive = node.Value.IsAlive;
                node = node.Next;

                if (node != null)
                {
                    if (!alive)
                        ActiveParticles.Remove(node.Previous.Value);
                }
                else
                {
                    if (!alive)
                        ActiveParticles.RemoveLast();
                }
            }

            TimeSinceLastSpawn += 1.0f / ZombieGame.FpsHandler.FpsValue;
        }

        public override void Draw(GameTime gameTime)
        {
            LinkedListNode<Particle> node = ActiveParticles.First;
            while (node != null)
            {
                node.Value.Draw(gameTime);
                node = node.Next;
            }
        }

        public bool addParticle(string fileName, Vector2 position, Vector2 direction, float life, float depth, float speed)
        {
            bool particleAdded = TimeSinceLastSpawn > TimeBetweenSpawn;

            if (particleAdded)
            {
                ActiveParticles.AddLast(new Particle(ZombieGame, fileName, position, direction, life, depth, speed));
                TimeSinceLastSpawn = 0.0f;
            }

            return particleAdded;
        }
    }
}
