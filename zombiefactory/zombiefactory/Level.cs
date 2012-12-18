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


namespace zombiefactory
{
    public class Level : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Tileset Tileset { get; set; }
        ZombieGame ZombieGame { get; set; }
        Vector2 Origin { get; set; }
        float Rotation { get; set; }
        float Scale { get; set; }
        Color Color { get; set; }
        SpriteEffects Effects { get; set; }
        float Depth { get; set; }

        //Idealement le nom et la taille du tileset vont tous aller dans le fichier lvl.dat ou wtv, et on va juste passer game et levelName
        public Level(ZombieGame game, string tilesetName, int tileSetRows, int tileSetColumns, string levelName)
            : base(game)
        {
            ZombieGame = game;
            Tileset = new Tileset(ZombieGame.TextureMgr.Find(tilesetName), tileSetRows, tileSetColumns);

            Rotation = 0.0f;
            Scale = 1.0f;
            Color = Color.White;
            Origin = Vector2.Zero;
            Effects = SpriteEffects.None;
            Depth = 0.0f;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            ZombieGame.SpriteBatch.Draw(Tileset.TilesTexture, new Rectangle(0, 0, Tileset.TileWidth, Tileset.TileHeight), Tileset.getRectangle(0, 0), Color, Rotation, Origin, Effects, Depth);

            base.Draw(gameTime);
        }
    }
}
