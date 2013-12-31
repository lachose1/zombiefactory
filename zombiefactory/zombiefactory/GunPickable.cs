using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace zombiefactory
{
    public class GunPickable : Pickable
    {

        #region constants
        public const float DEPTH = 0.1f;
        public const float UPDATE_TIME = 1.0f / 7.5f;
        #endregion constants

        #region properties
        string GunName { get; set; }
        #endregion properties

        public GunPickable(ZombieGame game, Vector2 spawningLocation, string gunName)
            : base(game, gunName, 1, 1, spawningLocation, DEPTH, UPDATE_TIME)
        {
            ZombieGame = game;
            GunName = gunName;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void OnPickUp()
        {
            ZombieGame.Player.PickUpGun(GunName);
        }
    }
}
