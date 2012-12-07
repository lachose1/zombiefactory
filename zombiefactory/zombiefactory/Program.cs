using System;

namespace zombiefactory
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (ZombieGame game = new ZombieGame())
            {
                game.Run();
            }
        }
    }
#endif
}

