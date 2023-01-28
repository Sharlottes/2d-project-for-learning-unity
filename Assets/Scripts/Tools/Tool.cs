using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Tools
{
    public interface ITool
    {
        public void Init();
        public void Active();
    }

}
