using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using PlayNMouse = playn.core.Mouse;
using XMouse = Microsoft.Xna.Framework.Input.Mouse;
using playn.core;

namespace PlayNXNA
{
    public class XNAMouse : MouseImpl
    {
        private bool leftDown, rightDown;
        private float lastX, lastY;
        private int scrollValue;

        public XNAMouse()
        {
            MouseState state = XMouse.GetState();
            float x = state.X, y = state.Y;
        }

        public void update(bool active)
        {
            XNAPointer pointer = (XNAPointer)PlayN.platform().pointer();

            double time = PlayN.currentTime();
            MouseState state = XMouse.GetState();
            float x = state.X, y = state.Y;
            if (state.LeftButton == ButtonState.Pressed && !leftDown && active)
            {
                onMouseDown(new PlayNMouse.ButtonEvent.Impl(new Events.Flags.Impl(), time, x, y, 0));
                pointer.onMouseDown(time, x, y);
                leftDown = true;
            }
            else if (state.LeftButton == ButtonState.Released && leftDown)
            {
                onMouseUp(new PlayNMouse.ButtonEvent.Impl(new Events.Flags.Impl(), time, x, y, 0));
                pointer.onMouseUp(time, x, y);
                leftDown = false;
            }
            else if (state.RightButton == ButtonState.Pressed && !rightDown && active)
            {
                onMouseDown(new PlayNMouse.ButtonEvent.Impl(new Events.Flags.Impl(), time, x, y, 0));
                pointer.onMouseDown(time, x, y);
                rightDown = true;
            }
            else if (state.RightButton == ButtonState.Released && rightDown)
            {
                onMouseUp(new PlayNMouse.ButtonEvent.Impl(new Events.Flags.Impl(), time, x, y, 0));
                pointer.onMouseUp(time, x, y);
                rightDown = false;
            }

            if (lastX != x || lastY != y)
            {
                onMouseMove(new PlayNMouse.MotionEvent.Impl(new Events.Flags.Impl(), time, x, y, x - lastX, y - lastY));
                pointer.onMouseMove(time, x, y);
                lastX = x;
                lastY = y;
            }

            if (state.ScrollWheelValue != scrollValue && active)
            {
                onMouseWheelScroll(new PlayNMouse.WheelEvent.Impl(new Events.Flags.Impl(), time, x, y, Math.Sign(state.ScrollWheelValue - scrollValue)));
                scrollValue = state.ScrollWheelValue;
            }
        }
    }
}
