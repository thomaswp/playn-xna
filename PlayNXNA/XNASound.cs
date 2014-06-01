using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using Microsoft.Xna.Framework.Audio;

namespace PlayNXNA
{
    public class XNASound : AbstractSound
    {
        private SoundEffect effect;
        private SoundEffectInstance instance;

        public override void onLoaded(object impl)
        {
            effect = (SoundEffect) impl;
            instance = effect.CreateInstance();
            base.onLoaded(impl);
        }

        protected override bool playImpl()
        {
            SoundEffectInstance nInstance = effect.CreateInstance();
            nInstance.Volume = instance.Volume;
            instance.Dispose();
            instance = nInstance;
            instance.Play();
            return true;
        }

        protected override void releaseImpl()
        {
            instance.Dispose();
        }

        protected override void setLoopingImpl(bool b)
        {
            instance.IsLooped = b;
        }

        protected override void setVolumeImpl(float f)
        {
            Console.WriteLine(effect.Name + ": " +  f);
            instance.Volume = f;
        }

        protected override void stopImpl()
        {
            instance.Stop();
        }
    }
}
