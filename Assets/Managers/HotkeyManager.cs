using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnityEngine;

namespace Managers
{
    class HotkeyManager : MonoSingleton<HotkeyManager>
    {
        Dictionary<string, KeyCode> hotkeyPairs = new Dictionary<string, KeyCode>();

        protected override void OnAwake()
        {

        }
    }
}
