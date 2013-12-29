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

        #region properties
        #endregion properties

        public Pistol(ZombieGame game, Vector2 initPos)
            : base(game, initPos, PISTOL_NAME, PISTOL_DAMAGE, -1, -1, 12, PISTOL_FIRE_RATE, PISTOL_BULLET_SPEED)
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
