using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using Microsoft.Xna.Framework.Graphics;

namespace PlayNXNA
{
    public class XNAStaticImage : XNAImage
    {

        public XNAStaticImage(Texture2D texture, playn.core.gl.Scale scale)
        {
            this.texture = texture;
            this._scale = scale;
        }
        public override void addCallback(playn.core.util.Callback callback)
        {
            callback.onSuccess(this);
        }
    }
}
