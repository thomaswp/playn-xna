using System;

namespace CuteGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (CuteGameXNA game = new CuteGameXNA())
            {
                game.Run();
            }
        }
    }
#endif
}

