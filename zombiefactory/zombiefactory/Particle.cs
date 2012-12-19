using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace zombiefactory
{
    class Particle : AnimatedSprite
    {
        public Vector2 Direction { get; set; }
        public float Life { get; set; }

        public Particle(ZombieGame game, string fileName, int frames, int lines, Vector2 position, Vector2 direction, float life)
            : base(game, fileName, frames, lines, position)
        {
            Direction = direction;
            Life = life;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public bool Update(GameTime gameTime, float dt)
        {
            this.Position += this.Direction * dt;
            this.Life -= dt;

            base.Update(gameTime);

            if (this.Life > 0)
            {
                return true;
            }
            return false;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
