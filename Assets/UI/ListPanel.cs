using Managers;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;
using UILogic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ListPanel : MonoBehaviour
    {
        public ScrollRectDo ScrollRectDo; 

        private void Awake()
        {
            
        }

        private void OnEnable()
        {
            Init();
        }

        private void Init()
        {
            ScrollRectDo.Init(WordManager.Instance.words.Values.ToList<Word>(), InitPrefab);
        }

        private void InitPrefab(GameObject go, Word data)
        {
            go.transform.GetChild(0).gameObject.GetComponent<Text>().text = data.GetWord();
            StringBuilder sb = new StringBuilder();
            foreach (var meaning in data.GetMeanings())
            {
                sb.Append(meaning.ToString()).Append(';');
            }
            sb.Remove(sb.Length - 1, 1);
            go.transform.GetChild(1).gameObject.GetComponent<Text>().text = sb.ToString();
        }
    }
}