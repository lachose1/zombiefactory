using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;


namespace zombiefactory
{
    public class Pistol : Gun
    {

        const string PISTOL_NAME = "Pistol";
        const float PISTOL_FIRE_RATE = 0.25f;
        const float PISTOL_BULLET_SPEED = 500.0f;
        public const int PISTOL_DAMAGE = 10;

        public Pistol(ZombieGame game, Vector2 initPos)
            : base(game, initPos, PISTOL_NAME, PISTOL_DAMAGE, -1, -1, 12, PISTOL_FIRE_RATE, PISTOL_BULLET_SPEED)
        {
            Emitters.Add(new ParticleEmitter(game, 100, false, PISTOL_FIRE_RATE, Sprite.Position));
        }

        protected override void MoveEmitters()
        {
            Emitters[0].Position = Sprite.Position;
        }
    }
}
