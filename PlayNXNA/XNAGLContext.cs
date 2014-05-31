using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;

namespace PlayNXNA
{
    /// <summary>
    /// Shouldn't need to be used - we don't support GL. Can be useful for testing.
    /// </summary>
    public class XNAGLContext : playn.core.gl.GLContext
    {

        public XNAGLContext() : base(PlayN.platform(), 1) { }

        public override void activeTexture(int i)
        {
            throw new NotImplementedException();
        }

        protected override void bindFramebufferImpl(int i1, int i2, int i3)
        {
            throw new NotImplementedException();
        }

        public override void bindTexture(int i)
        {
            throw new NotImplementedException();
        }

        public override void checkGLError(string str)
        {
            throw new NotImplementedException();
        }

        public override void clear(float f1, float f2, float f3, float f4)
        {
            throw new NotImplementedException();
        }

        public override playn.core.gl.GLBuffer.Float createFloatBuffer(int i)
        {
            throw new NotImplementedException();
        }

        protected override int createFramebufferImpl(int i)
        {
            throw new NotImplementedException();
        }

        public override playn.core.gl.GLProgram createProgram(string str1, string str2)
        {
            throw new NotImplementedException();
        }

        public override playn.core.gl.GLBuffer.Short createShortBuffer(int i)
        {
            throw new NotImplementedException();
        }

        public override int createTexture(int i1, int i2, bool b1, bool b2, bool b3)
        {
            throw new NotImplementedException();
        }

        public override int createTexture(bool b1, bool b2, bool b3)
        {
            throw new NotImplementedException();
        }

        protected override int defaultFrameBuffer()
        {
            throw new NotImplementedException();
        }

        public override void deleteFramebuffer(int i)
        {
            throw new NotImplementedException();
        }

        public override void destroyTexture(int i)
        {
            throw new NotImplementedException();
        }

        public override void endClipped()
        {
            throw new NotImplementedException();
        }

        public override void generateMipmap(int i)
        {
            throw new NotImplementedException();
        }

        public override bool getBoolean(int i)
        {
            throw new NotImplementedException();
        }

        public override float getFloat(int i)
        {
            throw new NotImplementedException();
        }

        public override int getInteger(int i)
        {
            throw new NotImplementedException();
        }

        public override string getString(int i)
        {
            throw new NotImplementedException();
        }

        protected override playn.core.gl.GLShader quadShader()
        {
            throw new NotImplementedException();
        }

        public override InternalTransform rootTransform()
        {
            throw new NotImplementedException();
        }

        public override void setTextureFilter(playn.core.gl.GLContext.Filter glcf1, playn.core.gl.GLContext.Filter glcf2)
        {
            throw new NotImplementedException();
        }

        public override void startClipped(int i1, int i2, int i3, int i4)
        {
            throw new NotImplementedException();
        }

        protected override playn.core.gl.GLShader trisShader()
        {
            throw new NotImplementedException();
        }
    }
}
