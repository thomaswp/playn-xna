using System;

namespace TuxBlocks
{
#if WINDOWS || XBOX
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (TuxBlocksGameXNA game = new TuxBlocksGameXNA())
            {
                game.Run();
            }
        }
    }
#endif
}

