using System;

namespace TestGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (TestGameXNA game = new TestGameXNA())
            {
                game.Run();
            }
        }
    }
#endif
}

