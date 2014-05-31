using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;

namespace PlayNXNA
{
    public class XNATouch : Touch
    {
        private Touch.Listener _listener;

        public void cancelLayerTouches(Layer l) { }

        public bool hasTouch()
        {
            return false;
        }

        public bool isEnabled()
        {
            return false;
        }

        public Touch.Listener listener()
        {
            return _listener;
        }

        public void setEnabled(bool b) { }

        public void setListener(Touch.Listener tl)
        {
            _listener = tl;
        }
    }
}
