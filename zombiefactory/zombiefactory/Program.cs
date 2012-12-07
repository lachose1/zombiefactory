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
            using (ZombieFactory game = new ZombieFactory())
            {
                game.Run();
            }
        }
    }
#endif
}

