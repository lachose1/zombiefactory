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
        public bool IsPickedUp { get; set; }
        #endregion properties

        public Pickable(ZombieGame game, string spriteName, int spriteFrames, int spriteLines, Vector2 spawningLocation, float depth, float updateTime)
            : base(game)
        {
            ZombieGame = game;
            IsPickedUp = false;
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
            if (IsPickedUp)
                return;

            Sprite.Update(gameTime);
            HoveringSprite.Update(gameTime);

            if (IsPlayerCollision(Sprite.Position.X, Sprite.Position.Y))
                PickedUp();

            HoveringSprite.Position = new Vector2(HoveringSprite.Position.X,
                (float)(5.0 * Math.Sin(0.005 * gameTime.TotalGameTime.TotalMilliseconds) + SpawningLocation.Y - 20.0));

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (IsPickedUp)
                return;

            Sprite.Draw(gameTime);
            HoveringSprite.Draw(gameTime);

            base.Draw(gameTime);
        }

        private void PickedUp()
        {
            IsPickedUp = true;
            OnPickUp();
        }

        protected abstract void OnPickUp();

        protected bool IsPlayerCollision(float x, float y)
        {
            Rectangle PickableRect = new Rectangle((int)x, (int)y, Sprite.FrameWidth, Sprite.FrameHeight);

            Rectangle PlayerRect = new Rectangle((int)ZombieGame.Player.Sprite.Position.X, (int)ZombieGame.Player.Sprite.Position.Y,
                ZombieGame.Player.Sprite.FrameWidth, ZombieGame.Player.Sprite.FrameHeight);

            return PickableRect.Intersects(PlayerRect);
        }
    }
}
