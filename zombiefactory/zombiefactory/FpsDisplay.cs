using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace zombiefactory
{
    public class FpsDisplay : Microsoft.Xna.Framework.DrawableGameComponent
    {
        ZombieGame ZombieGame { get; set; }
        Vector2 BottomRightPosition { get; set; }
        Vector2 StringPosition { get; set; }
        string StringFps { get; set; }
        Vector2 Dimension { get; set; }
        SpriteFont FontDisplay { get; set; }
        float FpsValue { get; set; }
        string FontName { get; set; }
        float BottomMargin { get; set; }
        float RightMargin { get; set; }

        public FpsDisplay(ZombieGame game, string fontName)
            : base(game)
        {
            ZombieGame = game;
            FontName = fontName;
        }

        public override void Initialize()
        {
            BottomMargin = 0;
            RightMargin = 10;

            BottomRightPosition = new Vector2(ZombieGame.Window.ClientBounds.Width - RightMargin,
                                            ZombieGame.Window.ClientBounds.Height - BottomMargin);
            FpsValue = -1;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            FontDisplay = ZombieGame.FontMgr.Find(FontName);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (ZombieGame.FpsHandler.FpsValue != FpsValue)
            {
                StringFps = ZombieGame.FpsHandler.FpsValue.ToString("0");
                Dimension = FontDisplay.MeasureString(StringFps);
                StringPosition = BottomRightPosition - Dimension;
                FpsValue = ZombieGame.FpsHandler.FpsValue;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            ZombieGame.SpriteBatch.DrawString(FontDisplay, StringFps, StringPosition, Color.Black, 0, 
                Vector2.Zero, 1.0f, SpriteEffects.None, 0);

            base.Draw(gameTime);
        }
    }
}
