using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace zombiefactory
{
    public class Emitter
    {
        ZombieGame ZombieGame { get; set; }
        public int MaxParticles { get; set; }
        public bool AutomaticSpawn { get; set; }
        float NextSpawnCountdown { get; set; }
        float TimeSinceLastSpawn { get; set; }
        LinkedList<Particle> ActiveParticles;
        Random Noise { get; set; }

        public Emitter(ZombieGame game, int maxParticles, bool automaticSpawn)
        {
            ZombieGame = game;
            ActiveParticles = new LinkedList<Particle>();
        }

        public void Update(GameTime gameTime, float dt)
        {
            LinkedListNode<Particle> node = ActiveParticles.First;
            while (node != null)
            {
                bool isAlive = node.Value.Update(gameTime, dt);
                node = node.Next;
                if (!isAlive)
                {
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

        public void addParticle(string fileName, int frames, int lines, Vector2 position, Vector2 direction, float life)
        {
            ActiveParticles.AddLast(new Particle(ZombieGame, fileName, frames, lines, position, direction, life));
            ZombieGame.Components.Add(ActiveParticles.Last.Value);
        }
    }
}
