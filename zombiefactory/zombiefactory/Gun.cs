using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

// TODO: We need to give bullets a max lifespan or something so
// they get destroyed eventually when going out of the screen

namespace zombiefactory
{
    public abstract class Gun : Microsoft.Xna.Framework.DrawableGameComponent
    {

        const float STICK_THRESHOLD = 0.04f;

        #region properties
        ZombieGame ZombieGame { get; set; }
        protected Sprite Sprite { get; set; }
        string GunName { get; set; }
        int Damage { get; set; }
        int MaxAmmo { get; set; }
        int Ammo { get; set; }
        int ClipSize { get; set; }
        float FireRate { get; set; }
        float BulletSpeed { get; set; }
        protected bool IsShooting { get; set; }
        protected List<ParticleEmitter> Emitters; //This is a list because of the possibility of designing a gun shooting many bullets at once (e.g. shotgun)
        public SoundEffect GunShotSound { get; set; }
        #endregion properties

        public Gun(ZombieGame game, Vector2 initPos, string gunName, int damage, int maxAmmo, int ammo, int clipSize, float fireRate, float bulletSpeed)
            : base(game)
        {
            ZombieGame = game;

            Sprite = new Sprite(ZombieGame, gunName, initPos, 0.0f);
            Sprite.Origin = new Vector2(0, Sprite.Height / 2);
            Sprite.Rotation = 3 * MathHelper.PiOver2;

            Emitters = new List<ParticleEmitter>();
            Emitters.Add(new ParticleEmitter(ZombieGame, 100, false, fireRate, Sprite.Position));

            IsShooting = false;
            GunShotSound = ZombieGame.SfxMgr.Find(gunName + "Shot");

            GunName = gunName;
            Damage = damage;
            MaxAmmo = maxAmmo;
            Ammo = ammo;
            ClipSize = clipSize;
            FireRate = fireRate;
            BulletSpeed = bulletSpeed;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            SetSpriteDirection();
            MoveSprite();
            CheckCollision();
            Shoot(gameTime);
            
            base.Update(gameTime);
        }

        protected abstract void Shoot(GameTime gameTime);

        public override void Draw(GameTime gameTime)
        {
            Sprite.Draw(gameTime); // Draw the gun
            foreach (ParticleEmitter e in Emitters) // Draw the bullets
                e.Draw(gameTime);

            base.Draw(gameTime);
        }

        private void SetSpriteDirection()
        {
            IsShooting = (Math.Abs(ZombieGame.InputMgr.ControllerState.ThumbSticks.Right.X) > STICK_THRESHOLD || Math.Abs(ZombieGame.InputMgr.ControllerState.ThumbSticks.Right.Y) > STICK_THRESHOLD);

            if (IsShooting)
                Sprite.Rotation = -(float)Math.Atan2((double)ZombieGame.InputMgr.ControllerState.ThumbSticks.Right.Y, (double)ZombieGame.InputMgr.ControllerState.ThumbSticks.Right.X);
            else
            {
                Vector2 directionStick = ZombieGame.InputMgr.ControllerState.ThumbSticks.Left;
                if (directionStick != Vector2.Zero)
                {
                    if (directionStick.Y > 0)
                    {
                        if (directionStick.Y > Math.Abs(directionStick.X))
                            Sprite.Rotation = 3 * MathHelper.PiOver2;
                        else if (directionStick.X > 0)
                            Sprite.Rotation = 0.0f;
                        else
                            Sprite.Rotation = MathHelper.Pi;
                    }
                    else
                    {
                        if (Math.Abs(directionStick.Y) > Math.Abs(directionStick.X))
                            Sprite.Rotation = MathHelper.PiOver2;
                        else if (directionStick.X > 0)
                            Sprite.Rotation = 0.0f;
                        else
                            Sprite.Rotation = MathHelper.Pi;
                    }
                }
            }

        }

        private void MoveSprite()
        {
            Vector2 PlayerPosition = ZombieGame.Player.Sprite.Position;
            float x = PlayerPosition.X + ZombieGame.Player.Sprite.FrameWidth / 2 + Sprite.Width / 2;
            float y = PlayerPosition.Y + ZombieGame.Player.Sprite.FrameHeight / 2;

            Sprite.Position = new Vector2(x, y);
        }

        protected float ComputeBulletSpeed()
        {
            float bulletSpeedX = (float)Math.Cos(Sprite.Rotation) * BulletSpeed;
            float bulletSpeedY = (float)Math.Sin(Sprite.Rotation) * BulletSpeed;
            bulletSpeedX += ZombieGame.Player.Speed.X;
            bulletSpeedY -= ZombieGame.Player.Speed.Y;

            Vector2 bulletSpeed = new Vector2(bulletSpeedX, bulletSpeedY);

            return bulletSpeed.Length();
        }

        private void CheckCollision()
        {
            foreach (ParticleEmitter e in Emitters)
            {
                foreach (Particle particle in e.ActiveParticles)
                {
                    Rectangle particleRectangle = new Rectangle((int)particle.Position.X, (int)particle.Position.Y, particle.Width, particle.Height);
                    foreach (Enemy enemy in ZombieGame.EnemySpawner.ActiveEnemies)
                    {
                        Rectangle enemyRect = new Rectangle((int)enemy.Sprite.Position.X, (int)enemy.Sprite.Position.Y,
                            enemy.Sprite.FrameWidth, enemy.Sprite.FrameHeight);

                        if (particleRectangle.Intersects(enemyRect))
                        {
                            particle.IsAlive = false;
                            enemy.TakeDamage(Damage);
                        }
                    }
                }
            }
        }
    }
}
