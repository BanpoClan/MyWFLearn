using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCreek.Cache.Factory
{
    public class Cache
    {
        public static Interface.ICache CreateInstance()
        {
            return new MyCreek.Cache.InProc.Cache();
        }
    }
}
