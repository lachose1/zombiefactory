using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;


namespace zombiefactory
{
    public class SMG : Gun
    {

        const string NAME = "SMG";
        const float FIRE_RATE = 0.07f;
        const float BULLET_SPEED = 600.0f;
        const float RELOAD_TIME = 1.0f;
        public const int DAMAGE = 2;

        public SMG(ZombieGame game, Vector2 initPos)
            : base(game, initPos, NAME, DAMAGE, 1000, 320, 32, FIRE_RATE, BULLET_SPEED, false, RELOAD_TIME)
        {
            Emitters.Add(new ParticleEmitter(game, 50, false, FIRE_RATE, Sprite.Position));
        }

        protected override void MoveEmitters()
        {
            Emitters[0].Position = Sprite.Position;
        }
    }
}
