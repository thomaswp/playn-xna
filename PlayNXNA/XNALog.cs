using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;

namespace PlayNXNA
{
    public class XNALog : LogImpl
    {
        protected override void logImpl(Log.Level ll, string str, Exception t)
        {
            Console.WriteLine(str);
            if (t != null) Console.WriteLine(t);
        }
    }
}
