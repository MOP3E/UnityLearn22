using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TowerDefense
{
    internal class EventArgsTransformValue : EventArgs
    {
        public Transform Value;

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
