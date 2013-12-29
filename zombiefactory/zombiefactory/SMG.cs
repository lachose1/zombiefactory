using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;


namespace zombiefactory
{
    public class SMG : Gun
    {

        const string SMG_NAME = "SMG";
        const float SMG_FIRE_RATE = 0.07f;
        const float SMG_BULLET_SPEED = 600.0f;
        public const int SMG_DAMAGE = 2;

        #region properties
        #endregion properties

        public SMG(ZombieGame game, Vector2 initPos)
            : base(game, initPos, SMG_NAME, SMG_DAMAGE, -1, -1, 12, SMG_FIRE_RATE, SMG_BULLET_SPEED)
        {

        }

        protected override void Shoot(GameTime gameTime)
        {
            if (IsShooting)
            {
                if (Emitters[0].addParticle("Bullet", Sprite.Position, new Vector2((float)Math.Cos(Sprite.Rotation), (float)Math.Sin(Sprite.Rotation)), 200.0f, 0.0f, ComputeBulletSpeed()))
                {
                    GunShotSound.Play();
                }
            }

            foreach (ParticleEmitter emitter in Emitters)
            {
                emitter.Position = Sprite.Position;
                emitter.Update(gameTime);
            }
        }
    }
}
