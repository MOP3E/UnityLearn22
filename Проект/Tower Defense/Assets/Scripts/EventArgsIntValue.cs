using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    internal class EventArgsIntValue : EventArgs
    {
        public int Value = 0;

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
