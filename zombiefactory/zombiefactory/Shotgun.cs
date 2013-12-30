using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;


namespace zombiefactory
{
    public class Shotgun : Gun
    {

        const string NAME = "Shotgun";
        const float FIRE_RATE = 0.55f;
        const float BULLET_SPEED = 500.0f;
        const float EMITTERS_OFFSET = 2;
        const float RELOAD_TIME = 1.0f;
        public const int DAMAGE = 20;

        #region properties
        public SoundEffect ShotgunPump { get; set; }
        #endregion properties

        public Shotgun(ZombieGame game, Vector2 initPos)
            : base(game, initPos, NAME, DAMAGE, 96, 96, 12, FIRE_RATE, BULLET_SPEED, false, RELOAD_TIME)
        {
            Emitters.Add(new ParticleEmitter(game, 100, false, FIRE_RATE, new Vector2(Sprite.Position.X - EMITTERS_OFFSET, Sprite.Position.Y)));
            Emitters.Add(new ParticleEmitter(game, 100, false, FIRE_RATE, new Vector2(Sprite.Position.X + EMITTERS_OFFSET, Sprite.Position.Y)));
            ShotgunPump = game.SfxMgr.Find("ShotgunPump");
        }

        protected override void MoveEmitters()
        {
            float xOffset = (float)(EMITTERS_OFFSET * Math.Sin(Sprite.Rotation));
            float yOffset = (float)(EMITTERS_OFFSET * Math.Cos(Sprite.Rotation));

            Emitters[0].Position = new Vector2(Sprite.Position.X - xOffset, Sprite.Position.Y + yOffset);
            Emitters[1].Position = new Vector2(Sprite.Position.X + xOffset, Sprite.Position.Y - yOffset);
        }

        protected override void Shoot(GameTime gameTime)
        {
            int firstEmitter = 0;
            if (IsShooting && !IsReloading && !IsAmmoEmpty)
            {
                foreach (ParticleEmitter emitter in Emitters)
                {
                    if (emitter.addParticle("Bullet", emitter.Position, new Vector2((float)Math.Cos(Sprite.Rotation), (float)Math.Sin(Sprite.Rotation)), 200.0f, 0.0f, ComputeBulletSpeed()))
                    {
                        if (firstEmitter == 0)
                        {
                            GunShotSound.Play();
                            ShotgunPump.Play();
                        }
                        --ClipAmmo;
                        firstEmitter++;
                    }
                }
            }
        }
    }
}
