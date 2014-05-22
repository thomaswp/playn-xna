using System;
using Microsoft.Xna.Framework.Content;

namespace PlayNXNA
{
    /// <summary>
    /// Provide a content manager that can be used in multiple threads,
    /// but only tries to load one asset at a time. It will block the
    /// other threads trying to load assets until the current asset
    /// load is completed.
    /// 
    /// From: http://konaju.com/?p=27
    /// </summary>
    public class ThreadSafeContentManager : ContentManager
    {
        static object loadLock = new object();

        public ThreadSafeContentManager(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        public ThreadSafeContentManager(IServiceProvider serviceProvider, string rootDirectory)
            : base(serviceProvider, rootDirectory)
        {
        }

        public override T Load<T>(string assetName)
        {
            lock (loadLock)
            {
                return base.Load<T>(assetName);
            }
        }
    }
}
