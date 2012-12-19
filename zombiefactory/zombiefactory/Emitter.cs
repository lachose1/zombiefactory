using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace zombiefactory
{
    class Emitter
    {
        ZombieGame ZombieGame { get; set; }
        public int MaxParticles { get; set; }
        public bool AutomaticSpawn { get; set; }
        float NextSpawnCountdown { get; set; }
        float TimeSinceLastSpawn { get; set; }
        LinkedList<Particle> ActiveParticles;
        Random Noise { get; set; }

        public Emitter(ZombieGame game, string fileName, int frames, 
            int lines, Vector2 position, Vector2 direction, float life,
            int maxParticles, bool automaticSpawn)
        {
            ActiveParticles = new LinkedList<Particle>();
        }
    }
}
