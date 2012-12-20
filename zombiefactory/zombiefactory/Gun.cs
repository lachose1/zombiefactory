using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace zombiefactory
{
    public class Gun : Microsoft.Xna.Framework.DrawableGameComponent
    {

        const float STICK_THRESHOLD = 0.04f;
        //Guns' fire rates and bullet speeds
        const float PISTOL_FIRE_RATE = 0.25f;
        const float PISTOL_BULLET_SPEED = 750.0f;

        #region properties
        ZombieGame ZombieGame { get; set; }
        Sprite Sprite { get; set; }
        int Damage { get; set; }
        int Ammo { get; set; }
        bool IsShooting { get; set; }
        List<Emitter> Emitters;
        #endregion properties

        public Gun(ZombieGame game, Vector2 initPos)
            : base(game)
        {
            ZombieGame = game;
            Sprite = new Sprite(ZombieGame, "Pistol", initPos, 0.0f);
            Sprite.Origin = new Vector2(0, Sprite.Height / 2);
            Sprite.Rotation = 3 * MathHelper.PiOver2;
            Emitters = new List<Emitter>();
            Emitters.Add(new Emitter(ZombieGame, 100, false, PISTOL_FIRE_RATE));
            IsShooting = false;
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

            if (IsShooting)
                Emitters[0].addParticle("Bullet", Sprite.Position, new Vector2((float)Math.Cos(Sprite.Rotation), (float)Math.Sin(Sprite.Rotation)),
                    200.0f, 0.0f, ComputeBulletSpeed());

            foreach (Emitter emitter in Emitters)
                emitter.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Sprite.Draw(gameTime);
            Emitters[0].Draw(gameTime);

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

        private float ComputeBulletSpeed()
        {
            float bulletSpeedX = (float)Math.Cos(Sprite.Rotation) * PISTOL_BULLET_SPEED;
            float bulletSpeedY = (float)Math.Sin(Sprite.Rotation) * PISTOL_BULLET_SPEED;
            bulletSpeedX += ZombieGame.Player.Speed.X;
            bulletSpeedY -= ZombieGame.Player.Speed.Y;

            Vector2 bulletSpeed = new Vector2(bulletSpeedX, bulletSpeedY);

            return bulletSpeed.Length();
        }

        private void CheckCollision()
        {
            Rectangle monolithRectangle = new Rectangle((int)ZombieGame.Monolith.Position.X, (int)ZombieGame.Monolith.Position.Y,
                ZombieGame.Monolith.Width, ZombieGame.Monolith.Height);

            foreach (Particle particle in Emitters[0].ActiveParticles)
            {
                Rectangle particleRectangle = new Rectangle((int)particle.Position.X, (int)particle.Position.Y, particle.Width, particle.Height);
                if (particleRectangle.Intersects(monolithRectangle))
                    particle.IsAlive = false;
            }
        }
    }
}
