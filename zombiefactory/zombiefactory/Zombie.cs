using System;
using Microsoft.Xna.Framework;

namespace zombiefactory
{
    public class Zombie : Enemy
    {
        #region constants
        public const string SPRITE_NAME = "Link";
        public const int SPRITE_FRAMES = 3;
        public const int SPRITE_LINES = 4;
        public const float DEPTH = 0.1f;
        public const float UPDATE_TIME = 1.0f / 10.0f;
        public const float ATTACK_DELAY = 0.5f; //seconds between attacks
        public const float MAX_SPEED = 75.0f;
        public const int MAX_HEALTH = 10;
        public const int MAX_DAMAGE = 10;
        #endregion constants

        #region properties
        int Damage { get; set; }
        float TimeSinceLastAttack { get; set; }
        #endregion properties

        public Zombie(ZombieGame game, Vector2 initPos)
            : base(game, initPos, SPRITE_NAME, SPRITE_FRAMES, SPRITE_LINES, DEPTH, UPDATE_TIME, MAX_HEALTH, MAX_DAMAGE, MAX_SPEED, ATTACK_DELAY)
        {
           
        }
    }
}
