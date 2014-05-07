using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;

namespace PlayNXNA
{
    public class XNANet : NetImpl
    {
        public XNANet(XNAPlatform platform) : base(platform) { }

        public override Net.WebSocket createWebSocket(string url, Net.WebSocket.Listener listener)
        {
            return base.createWebSocket(url, listener);
        }

        protected override void execute(BuilderImpl req, playn.core.util.Callback callback)
        {
            // TODO
        }
    }
}
