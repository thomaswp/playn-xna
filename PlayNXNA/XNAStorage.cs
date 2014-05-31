using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;

namespace PlayNXNA
{
    public class XNAStorage : Storage
    {
        public string getItem(string str)
        {
            return null;
        }

        public bool isPersisted()
        {
            return false;
        }

        public java.lang.Iterable keys()
        {
            return new java.util.ArrayList();
        }

        public void removeItem(string str)
        {
            
        }

        public void setItem(string key, string value)
        {
            
        }

        public Storage.Batch startBatch()
        {
            return new XNABatch();
        }

        public class XNABatch : Storage.Batch
        {

            public void commit()
            {
                
            }

            public void removeItem(string str)
            {
                
            }

            public void setItem(string str1, string str2)
            {
                
            }
        }
    }
}
