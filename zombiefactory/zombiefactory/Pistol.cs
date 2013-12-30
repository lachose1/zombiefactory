using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;


namespace zombiefactory
{
    public class Pistol : Gun
    {

        const string NAME = "Pistol";
        const float FIRE_RATE = 0.25f;
        const float BULLET_SPEED = 500.0f;
        const float RELOAD_TIME = 1.0f;
        public const int DAMAGE = 10;

        public Pistol(ZombieGame game, Vector2 initPos)
            : base(game, initPos, NAME, DAMAGE, -1, -1, 12, FIRE_RATE, BULLET_SPEED, true, RELOAD_TIME)
        {
            Emitters.Add(new ParticleEmitter(game, 50, false, FIRE_RATE, Sprite.Position));
        }

        protected override void MoveEmitters()
        {
            Emitters[0].Position = Sprite.Position;
        }
    }
}
