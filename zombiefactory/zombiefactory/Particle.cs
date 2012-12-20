using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace zombiefactory
{
    public class Particle : AnimatedSprite
    {
        public Vector2 Direction { get; set; }
        public float Life { get; set; }
        public bool IsAlive { get; private set; }

        public Particle(ZombieGame game, string fileName, int frames, int lines, Vector2 position, Vector2 direction, float life, float depth)
            : base(game, fileName, frames, lines, position, depth)
        {
            Direction = direction;
            Life = life;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            Position += Direction / ZombieGame.FpsHandler.FpsValue;
            Life -= 1 / ZombieGame.FpsHandler.FpsValue;

            if (Life > 0)
            {
                IsAlive = false;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
