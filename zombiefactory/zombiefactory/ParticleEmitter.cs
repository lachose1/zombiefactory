using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace zombiefactory
{
    public class ParticleEmitter : DrawableGameComponent
    {
        ZombieGame ZombieGame { get; set; }
        public int MaxParticles { get; set; }
        public bool AutomaticSpawn { get; set; }
        float TimeBetweenSpawn { get; set; }
        float TimeSinceLastSpawn { get; set; }
        public LinkedList<Particle> ActiveParticles;
        Random Noise { get; set; }

        public ParticleEmitter(ZombieGame game, int maxParticles, bool automaticSpawn, float timeBetweenSpawn)
            :base(game)
        {
            ZombieGame = game;
            TimeBetweenSpawn = timeBetweenSpawn;
            AutomaticSpawn = automaticSpawn;
            TimeSinceLastSpawn = 0.0f;
            ActiveParticles = new LinkedList<Particle>();
        }

        public override void Update(GameTime gameTime)
        {
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
            //if (AutomaticSpawn)
            //    addParticle(ParticleToSpawn);

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
