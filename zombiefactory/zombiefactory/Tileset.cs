using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace zombiefactory
{
    public class Tileset
    {
        public Texture2D TilesTexture { get; private set; }
        int Rows { get; set; }
        int Columns { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        Rectangle[,] Rectangles;

        public Tileset(Texture2D texture, int rows, int columns)
        {
            TilesTexture = texture;
            Rows = rows;
            Columns = columns;

            Width = TilesTexture.Width / Rows;
            Height = TilesTexture.Height / Columns;

            Rectangles = new Rectangle[Rows, Columns];

            for (int i = 0; i < Rows; ++i)
            {
                for (int j = 0; j < Columns; ++j)
                {
                    Rectangles[i, j] = new Rectangle(j * Width, i * Height, Width, Height);
                }
            }
        }

        public Rectangle getRectangle(int row, int col)
        {
            return Rectangles[row, col];
        }
    }
}
