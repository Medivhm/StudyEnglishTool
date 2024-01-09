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
    class SceneManager : MonoSingleton<SceneManager>
    {
        private string currScene;

        protected override void OnAwake()
        {
            currScene = "Start";
        }

        public void LoadScene(string name)
        {
            AsyncOperation async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(name);
            async.allowSceneActivation = true;
            async.completed += (async) =>
            {
                this.currScene = name;
                Debug.Log("Now Enter " + currScene);
            };
        }

    }
}
