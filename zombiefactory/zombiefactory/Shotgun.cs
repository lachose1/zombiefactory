using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;


namespace zombiefactory
{
    public class Shotgun : Gun
    {

        const string NAME = "Pistol"; // !!! Change to Shotgun once we have the sprite and sound files
        const float FIRE_RATE = 0.5f;
        const float BULLET_SPEED = 500.0f;
        const float EMITTERS_OFFSET = 10;
        public const int DAMAGE = 10;

        public Shotgun(ZombieGame game, Vector2 initPos)
            : base(game, initPos, NAME, DAMAGE, -1, -1, 12, FIRE_RATE, BULLET_SPEED)
        {
            Emitters.Add(new ParticleEmitter(game, 100, false, FIRE_RATE, new Vector2(Sprite.Position.X - EMITTERS_OFFSET, Sprite.Position.Y)));
            Emitters.Add(new ParticleEmitter(game, 100, false, FIRE_RATE, new Vector2(Sprite.Position.X + EMITTERS_OFFSET, Sprite.Position.Y)));
        }

        protected override void MoveEmitters()
        {
            float xOffset = (float)(EMITTERS_OFFSET * Math.Sin(Sprite.Rotation));
            float yOffset = (float)(EMITTERS_OFFSET * Math.Cos(Sprite.Rotation));

            Emitters[0].Position = new Vector2(Sprite.Position.X - xOffset, Sprite.Position.Y + yOffset);
            Emitters[1].Position = new Vector2(Sprite.Position.X + xOffset, Sprite.Position.Y - yOffset);
        }
    }
}
