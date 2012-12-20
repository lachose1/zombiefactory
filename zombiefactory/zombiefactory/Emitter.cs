using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace zombiefactory
{
    public class Emitter
    {
        ZombieGame ZombieGame { get; set; }
        Particle ParticleToSpawn { get; set; }
        public int MaxParticles { get; set; }
        public bool AutomaticSpawn { get; set; }
        float TimeBetweenSpawn { get; set; }
        float TimeSinceLastSpawn { get; set; }
        LinkedList<Particle> ActiveParticles;
        Random Noise { get; set; }

        public Emitter(ZombieGame game, int maxParticles, bool automaticSpawn, float timeBetweenSpawn, Particle particle)
        {
            ZombieGame = game;
            ParticleToSpawn = particle;
            TimeBetweenSpawn = timeBetweenSpawn;
            AutomaticSpawn = automaticSpawn;
            TimeSinceLastSpawn = 0.0f;
            ActiveParticles = new LinkedList<Particle>();
        }

        public void Update(GameTime gameTime, float dt)
        {
            LinkedListNode<Particle> node = ActiveParticles.First;
            while (node != null)
            {
                node.Value.Update(gameTime);
                if (!node.Value.IsAlive)
                {
                    node = node.Next;
                    if (node == null)
                    {
                        ActiveParticles.RemoveLast();
                    }
                    else
                    {
                        ActiveParticles.Remove(node.Previous);
                    }
                }
            }
            //if (AutomaticSpawn)
            //    addParticle(ParticleToSpawn);

            TimeSinceLastSpawn += 1.0f / ZombieGame.FpsHandler.FpsValue;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            LinkedListNode<Particle> node = ActiveParticles.First;
            while (node != null)
            {
                node.Value.Draw(gameTime);
                node = node.Next;
            }
        }

        public void addParticle(string fileName, Vector2 position, Vector2 direction, float life, float depth, float speed)
        {
            if (!(TimeSinceLastSpawn < TimeBetweenSpawn))
            {
                ActiveParticles.AddLast(new Particle(ZombieGame, fileName, position, direction, life, depth, speed));
                ZombieGame.Components.Add(ActiveParticles.Last.Value);
                TimeSinceLastSpawn = 0.0f;
            }
        }
    }
}
