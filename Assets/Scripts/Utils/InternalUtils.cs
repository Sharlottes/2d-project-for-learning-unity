using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Utils
{
    public static class InternalUtils
    {
        public delegate void RepeatDelegator(int index);
        public static void Repeat(int length, RepeatDelegator callback)
        {
            for (int i = 0; i < length; i++) callback(i);
        }
    }
}
