using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace zombiefactory
{
    public class Tileset
    {
        public Texture2D TilesTexture { get; private set; }
        int Rows { get; set; }
        int Columns { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }
        Rectangle[,] Rectangles;

        public Tileset(Texture2D texture, int rows, int columns)
        {
            TilesTexture = texture;
            Rows = rows;
            Columns = columns;

            TileWidth = TilesTexture.Width / Rows;
            TileHeight = TilesTexture.Height / Columns;

            Rectangles = new Rectangle[Rows, Columns];

            for (int i = 0; i < Rows; ++i)
            {
                for (int j = 0; j < Columns; ++j)
                {
                    Rectangles[i, j] = new Rectangle(j * TileWidth, i * TileHeight, TileWidth, TileHeight);
                }
            }
        }

        public Rectangle getRectangle(int tileNumber)
        {
            return Rectangles[tileNumber / Rows, tileNumber % Rows];
        }
    }
}
