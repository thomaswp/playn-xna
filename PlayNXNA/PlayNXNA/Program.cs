using System;

namespace PlayNTest
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (HelloGameXNA game = new HelloGameXNA())
            {
                game.Run();
            }
        }
    }
#endif
}

