using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace RaceXNA
{
    public class InputManager : Microsoft.Xna.Framework.GameComponent
    {
        private Game RaceGame;
        public GamePadState ControllerState { get; private set; }
        public GamePadState PreviousControllerState { get; private set; }
        private Keys[] PreviousKeys { get; set; }
        private Keys[] CurrentKeys { get; set; }
        private KeyboardState KbState { get; set; }

        public InputManager(Game game)
            : base(game)
        {
            RaceGame = game;
        }

        public override void Initialize()
        {
            PreviousKeys = new Keys[0];
            CurrentKeys = new Keys[0];
            ControllerState = GamePad.GetState(PlayerIndex.One);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            PreviousKeys = CurrentKeys;
            KbState = Keyboard.GetState();
            CurrentKeys = KbState.GetPressedKeys();
            PreviousControllerState = ControllerState;
            ControllerState = GamePad.GetState(PlayerIndex.One);

            base.Update(gameTime);
        }

        public bool IsKbActive
        {
            get { return CurrentKeys.Length > 0; }
        }

        public bool IsNewKey(Keys key)
        {
            int keyCount = PreviousKeys.Length;
            bool newKey = IsKeyDown(key);
            int i = 0;

            while (i < keyCount && newKey)
            {
                newKey = PreviousKeys[i] != key;
                ++i;
            }

            return newKey;
        }

        public bool IsKeyDown(Keys key)
        {
            return KbState.IsKeyDown(key);
        }
    }
}