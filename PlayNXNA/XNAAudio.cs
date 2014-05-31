using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using Microsoft.Xna.Framework.Audio;

namespace PlayNXNA
{
    public class XNAAudio : AudioImpl
    {
        public XNAAudio(XNAPlatform platform) : base(platform) { }

        public XNASound createSound(SoundEffect effect)
        {
            XNASound sound = new XNASound();
            dispatchLoaded(sound, effect);
            return sound;
        }
    }
}
