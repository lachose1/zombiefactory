using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace zombiefactory
{
    public class ShotgunPickable : Pickable
    {

        #region constants
        public const string SPRITE_NAME = "Shotgun";
        public const int SPRITE_FRAMES = 1;
        public const int SPRITE_LINES = 1;
        public const float DEPTH = 0.1f;
        public const float UPDATE_TIME = 1.0f / 7.5f;
        #endregion constants

        #region properties
        #endregion properties

        public ShotgunPickable(ZombieGame game, Vector2 spawningLocation)
            : base(game, SPRITE_NAME, SPRITE_FRAMES, SPRITE_LINES, spawningLocation, DEPTH, UPDATE_TIME, 0)
        {
            ZombieGame = game;
        }

        public override void Initialize()
        {
            base.Initialize();
        }
    }
}
