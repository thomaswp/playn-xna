using System;

namespace Showcase
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (ShowcaseGameXNA game = new ShowcaseGameXNA())
            {
                game.Run();
            }
        }
    }
#endif
}

