using Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    class MainCanvas : MonoBehaviour
    {
        public GameObject[] Panels;

        public void BackToStartOnClick()
        {
            SceneManager.Instance.LoadScene("Start");
        }

        public void RandPanelOnClick()      // 0
        {
            ShowPanel(0);
        }

        public void AddWordOnClick()        // 1
        {
            ShowPanel(1);
        }

        public void WordsListOnClick()      // 2
        {
            ShowPanel(2);
        }

        public void TranslateOnClick()      // 3
        {
            ShowPanel(3);
        }

        private void ShowPanel(int idx)
        {
            HideAllPanels();
            Panels[idx].SetActive(true);
        }

        private void HideAllPanels()
        {
            foreach(GameObject panel in Panels)
            {
                panel.SetActive(false);
            }
        }
    }
}
