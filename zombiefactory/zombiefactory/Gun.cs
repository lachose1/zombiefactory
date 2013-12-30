using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

// TODO: We need to give bullets a max lifespan or something so
// they get destroyed eventually when going out of the screen
// Probably should use Emitter's MaxItems property to do that.

namespace zombiefactory
{
    public abstract class Gun : Microsoft.Xna.Framework.DrawableGameComponent
    {

        const float STICK_THRESHOLD = 0.04f;

        #region properties
        protected ZombieGame ZombieGame { get; set; }
        protected Sprite Sprite { get; set; }
        public string GunName { get; private set; }
        public bool InfiniteAmmo { get; private set; }
        int Damage { get; set; }
        int MaxAmmo { get; set; } // Max ammo that can be carried for this gun

        int ammo;
        public int Ammo // Total ammo carried for this gun, minus what's in the current clip
        {
            get { return ammo; }
            protected set
            {
                if (value < 0)
                    ammo = 0;
                else if (value > MaxAmmo)
                    ammo = MaxAmmo;
                else
                    ammo = value;
            }
        }

        int clipAmmo;
        public int ClipAmmo // Ammo in the current clip
        {
            get { return clipAmmo; }
            protected set
            {
                if (value < 0)
                    clipAmmo = 0;
                else if (value > ClipSize)
                    clipAmmo = ClipSize;
                else
                    clipAmmo = value;

                if (clipAmmo == 0)
                {
                    IsReloading = true;
                    TimerReloading = 0;
                }
            }
        }

        int ClipSize { get; set; } // Max ammo in a clip
        float FireRate { get; set; }
        float BulletSpeed { get; set; }
        protected bool IsShooting { get; set; }
        protected bool IsReloading { get; set; }
        protected List<ParticleEmitter> Emitters;
        public SoundEffect GunShotSound { get; set; }
        float ReloadingTime { get; set; }
        float TimerReloading { get; set; }
        public bool IsAmmoEmpty { get { return !InfiniteAmmo && Ammo == 0 && ClipAmmo == 0; } }
        #endregion properties

        public Gun(ZombieGame game, Vector2 initPos, string gunName, int damage, int maxAmmo, int ammo,
            int clipSize, float fireRate, float bulletSpeed, bool infiniteAmmo, float reloadingTime)
            : base(game)
        {
            ZombieGame = game;
            GunName = gunName;
            Damage = damage;
            InfiniteAmmo = infiniteAmmo;
            ReloadingTime = reloadingTime;

            MaxAmmo = maxAmmo;
            Ammo = ammo;
            ClipSize = clipSize;

            if (!InfiniteAmmo)
            {
                if (Ammo >= ClipSize)
                    ClipAmmo = ClipSize;
                else
                    ClipAmmo = Ammo;

                Ammo -= ClipAmmo;
            }
            else
                ClipAmmo = ClipSize;

            FireRate = fireRate;
            BulletSpeed = bulletSpeed;

            Sprite = new Sprite(ZombieGame, GunName, initPos, 0.0f);
            Sprite.Origin = new Vector2(0, Sprite.Height / 2);
            Sprite.Rotation = 3 * MathHelper.PiOver2;

            Emitters = new List<ParticleEmitter>();

            IsShooting = false;
            IsReloading = false;
            GunShotSound = ZombieGame.SfxMgr.Find(GunName + "Shot");
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if(IsReloading)
                TimerReloading += 1.0f / ZombieGame.FpsHandler.FpsValue;

            if (TimerReloading >= ReloadingTime)
                Reload();

            SetSpriteDirection();
            MoveSprite();
            MoveEmitters();
            CheckCollision();
            Shoot(gameTime);
            
            base.Update(gameTime);
        }

        private void Reload()
        {
            IsReloading = false;
            TimerReloading = 0;

            if (Ammo >= ClipSize || InfiniteAmmo)
                ClipAmmo = ClipSize;
            else
                ClipAmmo = Ammo;

            Ammo -= ClipAmmo;
        }

        protected virtual void Shoot(GameTime gameTime)
        {
            if (IsShooting && !IsReloading && ! IsAmmoEmpty)
            {
                foreach (ParticleEmitter emitter in Emitters)
                {
                    if (emitter.addParticle("Bullet", emitter.Position, new Vector2((float)Math.Cos(Sprite.Rotation), (float)Math.Sin(Sprite.Rotation)), 200.0f, 0.0f, ComputeBulletSpeed()))
                    {
                        GunShotSound.Play();
                        --ClipAmmo;
                    }
                }
            }

            foreach (ParticleEmitter emitter in Emitters)
                emitter.Update(gameTime);
        }

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

        protected abstract void MoveEmitters();

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
