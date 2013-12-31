using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace zombiefactory
{
    public class ParticleEmitter : Emitter
    {
        #region properties
        public List<Particle> ActiveParticles;
        #endregion properties

        public ParticleEmitter(ZombieGame game, int maxParticles, bool automaticSpawn, float timeBetweenSpawn, Vector2 position)
            : base(game, maxParticles, automaticSpawn, timeBetweenSpawn, position)
        {
            ActiveParticles = new List<Particle>();
        }

        public override void Update(GameTime gameTime)
        {
            while (ActiveParticles.Count > MaxItems)
                ActiveParticles.RemoveAt(0);

            foreach (Particle p in ActiveParticles)
                p.Update(gameTime);

            ActiveParticles.RemoveAll(p => !p.IsAlive);

            TimeSinceLastSpawn += 1.0f / ZombieGame.FpsHandler.FpsValue;
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Particle p in ActiveParticles)
                p.Draw(gameTime);
        }

        public bool addParticle(string fileName, Vector2 position, Vector2 direction, float life, float depth, float speed)
        {
            bool particleAdded = TimeSinceLastSpawn > TimeBetweenSpawn;

            if (particleAdded)
            {
                ActiveParticles.Add(new Particle(ZombieGame, fileName, position, direction, life, depth, speed));
                TimeSinceLastSpawn = 0.0f;
            }

            return particleAdded;
        }
    }
}
