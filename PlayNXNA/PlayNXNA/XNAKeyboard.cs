using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using XKeys = Microsoft.Xna.Framework.Input.Keys;
using XXKeys = Microsoft.Xna.Framework.Input.Keyboard;

namespace PlayNXNA
{
    public class XNAKeyboard : Keyboard
    {
        private Keyboard.Listener listener;
        private XKeys[] pressedKeys;

        public void getText(Keyboard.TextType ktt, string str1, string str2, playn.core.util.Callback c)
        {
            throw new InvalidOperationException();
        }

        public bool hasHardwareKeyboard()
        {
            return true;
        }

        public void setListener(Keyboard.Listener kl)
        {
            listener = kl;   
        }

        public void update()
        {
            if (listener == null) return;

            double time = PlayN.platform().time();
            XKeys[] keys = XXKeys.GetState().GetPressedKeys();
            foreach (XKeys key in keys)
            {
                if (!pressedKeys.Contains(key))
                {
                    Key pnKey = GetPlayNKey(key);
                    if (pnKey == null) continue;
                    listener.onKeyDown(new Keyboard.Event.Impl(new Events.Flags.Impl(), time, pnKey));
                }
            }
            if (pressedKeys != null)
            {
                foreach (XKeys key in pressedKeys)
                {
                    if (!keys.Contains(key))
                    {
                        Key pnKey = GetPlayNKey(key);
                        if (pnKey != null)
                        {
                            listener.onKeyUp(new Keyboard.Event.Impl(new Events.Flags.Impl(), time, pnKey));
                        }
                    }
                }
            }
            pressedKeys = keys;
        }

        public static Key GetPlayNKey(XKeys key)
        {
            switch (key)
            {
                case XKeys.Escape: return Key.ESCAPE;
                case XKeys.NumPad1: return Key.K1;
                case XKeys.NumPad2: return Key.K2;
                case XKeys.NumPad3: return Key.K3;
                case XKeys.NumPad4: return Key.K4;
                case XKeys.NumPad5: return Key.K5;
                case XKeys.NumPad6: return Key.K6;
                case XKeys.NumPad7: return Key.K7;
                case XKeys.NumPad8: return Key.K8;
                case XKeys.NumPad9: return Key.K9;
                case XKeys.NumPad0: return Key.K0;
                case XKeys.OemMinus: return Key.MINUS;
                //case XKeys.EQUALS: return Key.EQUALS;
                //case XKeys.BACK: return Key.BACK;
                case XKeys.Tab: return Key.TAB;
                case XKeys.Q: return Key.Q;
                case XKeys.W: return Key.W;
                case XKeys.E: return Key.E;
                case XKeys.R: return Key.R;
                case XKeys.T: return Key.T;
                case XKeys.Y: return Key.Y;
                case XKeys.U: return Key.U;
                case XKeys.I: return Key.I;
                case XKeys.O: return Key.O;
                case XKeys.P: return Key.P;
                case XKeys.OemOpenBrackets: return Key.LEFT_BRACKET;
                case XKeys.OemCloseBrackets: return Key.RIGHT_BRACKET;
                case XKeys.Enter: return Key.ENTER;
                case XKeys.LeftControl: return Key.CONTROL;
                case XKeys.A: return Key.A;
                case XKeys.S: return Key.S;
                case XKeys.D: return Key.D;
                case XKeys.F: return Key.F;
                case XKeys.G: return Key.G;
                case XKeys.H: return Key.H;
                case XKeys.J: return Key.J;
                case XKeys.K: return Key.K;
                case XKeys.L: return Key.L;
                case XKeys.OemSemicolon: return Key.SEMICOLON;
                case XKeys.OemQuotes: return Key.QUOTE; //TODO
                //case XKeys.: return Key.BACKQUOTE;
                case XKeys.LeftShift: return Key.SHIFT; // PlayN doesn't know left v. right
                case XKeys.OemBackslash: return Key.BACKSLASH;
                case XKeys.Z: return Key.Z;
                case XKeys.X: return Key.X;
                case XKeys.C: return Key.C;
                case XKeys.V: return Key.V;
                case XKeys.B: return Key.B;
                case XKeys.N: return Key.N;
                case XKeys.M: return Key.M;
                case XKeys.OemComma: return Key.COMMA;
                case XKeys.OemPeriod: return Key.PERIOD;
                //case XKeys.qu: return Key.SLASH;
                case XKeys.RightShift: return Key.SHIFT; // PlayN doesn't know left v. right
                //case XKeys.: return Key.MULTIPLY;
                //case XKeys.LMENU: return Key.ALT; // PlayN doesn't know left v. right
                case XKeys.Space: return Key.SPACE;
                case XKeys.CapsLock: return Key.CAPS_LOCK;
                case XKeys.F1: return Key.F1;
                case XKeys.F2: return Key.F2;
                case XKeys.F3: return Key.F3;
                case XKeys.F4: return Key.F4;
                case XKeys.F5: return Key.F5;
                case XKeys.F6: return Key.F6;
                case XKeys.F7: return Key.F7;
                case XKeys.F8: return Key.F8;
                case XKeys.F9: return Key.F9;
                case XKeys.F10: return Key.F10;
                case XKeys.NumLock: return Key.NP_NUM_LOCK;
                case XKeys.Scroll: return Key.SCROLL_LOCK;
                case XKeys.Subtract: return Key.NP_SUBTRACT;
                case XKeys.Add: return Key.NP_ADD;
                //case XKeys.NumPad7: return Key.NP7;
                //case XKeys.NumPad8: return Key.NP8;
                //case XKeys.NumPad9: return Key.NP9;
                //case XKeys.NUMPAD4: return Key.NP4;
                //case XKeys.NUMPAD5: return Key.NP5;
                //case XKeys.NUMPAD6: return Key.NP6;
                //case XKeys.NUMPAD1: return Key.NP1;
                //case XKeys.NUMPAD2: return Key.NP2;
                //case XKeys.NUMPAD3: return Key.NP3;
                //case XKeys.NUMPAD0: return Key.NP0;
                case XKeys.Decimal: return Key.NP_DECIMAL;
                case XKeys.F11: return Key.F11;
                case XKeys.F12: return Key.F12;
                //case XKeys.F13          : return Key.F13;
                //case XKeys.F14          : return Key.F14;
                //case XKeys.F15          : return Key.F15;
                //case XKeys.F16          : return Key.F16;
                //case XKeys.F17          : return Key.F17;
                //case XKeys.F18          : return Key.F18;
                //case XKeys.KANA         : return Key.
                //case XKeys.F19          : return Key.F19;
                //case XKeys.CONVERT      : return Key.
                //case XKeys.NOCONVERT    : return Key.
                //case XKeys.YEN          : return Key.
                //case XKeys.NUMPADEQUALS : return Key.
                //case XKeys.CIRCUMFLEX: return Key.CIRCUMFLEX;
                //case XKeys.: return Key.AT;
                //case XKeys.COLON: return Key.COLON;
                //case XKeys.unsersc: return Key.UNDERSCORE;
                //case XKeys.KANJI        : return Key.
                //case XKeys.STOP         : return Key.
                //case XKeys.AX           : return Key.
                //case XKeys.UNLABELED    : return Key.
                //case XKeys.NUMPADENTER  : return Key.
                case XKeys.RightControl: return Key.CONTROL; // PlayN doesn't know left v. right
                //case XKeys.SECTION      : return Key.
                //case XKeys.NUMPADCOMMA  : return Key.
                //case XKeys.DIVIDE       :
                //case XKeys.SYSRQ: return Key.SYSRQ;
                case XKeys.LeftAlt: return Key.ALT; // PlayN doesn't know left v. right
                //case XKeys.: return Key.FUNCTION;
                //case XKeys.: return Key.PAUSE;
                case XKeys.Home: return Key.HOME;
                case XKeys.Up: return Key.UP;
                case XKeys.PageUp: return Key.PAGE_UP;
                case XKeys.Left: return Key.LEFT;
                case XKeys.Right: return Key.RIGHT;
                case XKeys.End: return Key.END;
                case XKeys.Down: return Key.DOWN;
                case XKeys.PageDown: return Key.PAGE_DOWN;
                case XKeys.Insert: return Key.INSERT;
                case XKeys.Delete: return Key.DELETE;
                case XKeys.OemClear: return Key.CLEAR;
                //case XKeys.LMETA: return Key.META; // PlayN doesn't know left v. right
                case XKeys.LeftWindows         : return Key.WINDOWS; // Duplicate with LMETA
                //case XKeys.RMETA: return Key.META; // PlayN doesn't know left v. right
                //case XKeys.RWIN         : return Key.WINDOWS; // Duplicate with RMETA
                //case XKeys.APPS         : return Key.
                //case XKeys.POWER: return Key.POWER;
                //case XKeys.SLEEP        : return Key.
            }

            return null;
        }
    }
}
