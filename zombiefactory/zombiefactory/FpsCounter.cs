using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace zombiefactory
{
    public class FpsCounter : Microsoft.Xna.Framework.GameComponent
    {
        public float FpsValue { get; private set; }
        float Interval { get; set; }
        float ElapsedTime { get; set; }
        int Frames { get; set; }
        Game ZombieGame { get; set; }

        public FpsCounter(Game game, float interval)
            : base(game)
        {
            ZombieGame = game;
            Interval = interval;
        }

        public override void Initialize()
        {
            FpsValue = 6000;
            ElapsedTime = 0;
            Frames = 0;
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            float elapsedTime;

            elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;


            ++Frames;
            ElapsedTime += elapsedTime;

            if (ElapsedTime > Interval)
            {
                FpsValue = Frames / ElapsedTime;
                Frames = 0;
                ElapsedTime -= Interval;
            }

            base.Update(gameTime);
        }
    }
}

