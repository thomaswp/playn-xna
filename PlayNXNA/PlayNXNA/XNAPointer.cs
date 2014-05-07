using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;

namespace PlayNXNA
{
    class XNAPointer : PointerImpl
    {
        private bool mouseDown;

        public void onMouseDown(double time, float x, float y)
        {
            onPointerStart(new Pointer.Event.Impl(new Events.Flags.Impl(), time, x, y, false), false);
            mouseDown = true;
        }

        public void onMouseUp(double time, float x, float y)
        {
            onPointerEnd(new Pointer.Event.Impl(new Events.Flags.Impl(), time, x, y, false), false);
            mouseDown = false;
        }

        public void onMouseMove(double time, float x, float y)
        {
            if (mouseDown)
            {
                onPointerDrag(new Pointer.Event.Impl(new Events.Flags.Impl(), time, x, y, false), false);
            }
        }
    }
}
