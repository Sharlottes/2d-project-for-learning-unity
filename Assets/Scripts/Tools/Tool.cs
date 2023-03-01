using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Tools
{
    public abstract class Tool : MonoBehaviour
    {
        public abstract void Init();
        public abstract void Active();
    }
}
