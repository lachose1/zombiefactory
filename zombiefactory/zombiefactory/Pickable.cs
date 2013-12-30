using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace zombiefactory
{
    public abstract class Pickable : Microsoft.Xna.Framework.DrawableGameComponent
    {
        #region properties
        protected ZombieGame ZombieGame { get; set; }
        public AnimatedSprite Sprite { get; protected set; }
        public AnimatedSprite HoveringSprite { get; protected set; }
        public SoundEffect PickupSound { get; set; }
        public Vector2 SpawningLocation { get; set; }
        public int HoveringTextType;
        public bool IsPickedUp { get; set; }
        #endregion properties

        public Pickable(ZombieGame game, string spriteName, int spriteFrames, int spriteLines, Vector2 spawningLocation, float depth, float updateTime, int hoveringTextType)
            : base(game)
        {
            ZombieGame = game;
            IsPickedUp = false;
            HoveringTextType = hoveringTextType;
            SpawningLocation = spawningLocation;
            Sprite = new AnimatedSprite(ZombieGame, spriteName, spriteFrames, spriteLines, spawningLocation, depth, updateTime);
            HoveringSprite = new AnimatedSprite(ZombieGame, "Pickme", 1, 1, new Vector2(spawningLocation.X - 24.0f, spawningLocation.Y - 20.0f), depth, updateTime);
            //Ajout le pickupsound
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            Sprite.Update(gameTime);
            HoveringSprite.Update(gameTime);

            float newY = (float)(5.0 * Math.Sin(0.005 * gameTime.TotalGameTime.Milliseconds) + SpawningLocation.Y - 20.0);

            HoveringSprite.Position = new Vector2(HoveringSprite.Position.X, newY);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Sprite.Draw(gameTime);
            HoveringSprite.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
