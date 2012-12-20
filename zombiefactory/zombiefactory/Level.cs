using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


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
        int Width { get; set; }
        int Height { get; set; }
        int[,] TileType { get; set; }

        //Idealement le nom et la taille du tileset vont tous aller dans le fichier lvl.dat ou wtv, et on va juste passer game et levelName
        public Level(ZombieGame game, string levelName)
            : base(game)
        {
            StreamReader levelData = new StreamReader("Content/Levels/" + levelName + ".dat");
            string tilesetName = levelData.ReadLine();
            int tilesetRows = int.Parse(levelData.ReadLine());
            int tilesetColumns = int.Parse(levelData.ReadLine());
            Width = int.Parse(levelData.ReadLine());
            Height = int.Parse(levelData.ReadLine());

            TileType = new int[Width, Height];

            for (int i = 0; i < Width; ++i)
            {
                string[] numbers = levelData.ReadLine().Split(' ');

                for (int j = 0; j < Height; ++j)
                {
                    TileType[i, j] = int.Parse(numbers[j]);
                }
            }

            ZombieGame = game;
            Tileset = new Tileset(ZombieGame.TextureMgr.Find(tilesetName), tilesetRows, tilesetColumns);
        }

        public override void Initialize()
        {
            Rotation = 0.0f;
            Scale = 1.0f;
            Color = Color.White;
            Origin = Vector2.Zero;
            Effects = SpriteEffects.None;
            Depth = 1.0f;

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            for (int i = 0; i < Width; ++i)
            {
                for (int j = 0; j < Height; ++j)
                {
                    ZombieGame.SpriteBatch.Draw(Tileset.TilesTexture, new Vector2((int)(j * Tileset.TileWidth * Scale), (int)(i * Tileset.TileHeight * Scale)),
                        Tileset.getRectangle(TileType[i, j]), Color, Rotation, Origin, Scale, Effects, Depth);
                }
            }

            base.Draw(gameTime);
        }
    }
}
