using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace zombiefactory
{
    public class Particle : Sprite
    {
        public Vector2 Direction { get; set; }
        public float Life { get; set; }
        public bool IsAlive { get; private set; }
        public float Speed { get; private set; }

        public Particle(ZombieGame game, string fileName, Vector2 position, Vector2 direction, float life, float depth, float speed)
            : base(game, fileName, position, depth)
        {
            Direction = direction;
            Life = life;
            Speed = speed;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            Position += Direction * Speed / ZombieGame.FpsHandler.FpsValue;
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
