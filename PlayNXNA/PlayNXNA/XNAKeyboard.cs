using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;

namespace PlayNTest
{
    public class XNAKeyboard : Keyboard
    {
        public void getText(Keyboard.TextType ktt, string str1, string str2, playn.core.util.Callback c)
        {
            // TODO
        }

        public bool hasHardwareKeyboard()
        {
            return true;
        }

        public void setListener(Keyboard.Listener kl)
        {
            // TODO
        }
    }
}
