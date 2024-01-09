using Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;
using UnityEngine;

namespace UI
{
    class StartCanvas : MonoBehaviour
    {
        public void EnterOnClick()
        {
            SceneManager.Instance.LoadScene("Main");
        }

        public void DeleteCacheOnClick()
        {
            FileTool.DeleteWordsFile();
        }
    }
}
