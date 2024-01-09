using Managers;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Tools;
using UILogic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AddPanel : MonoBehaviour
    {
        public GameObject ItemPrefab;
        public Transform Content;
        public InputField wordInput;

        private AddPanelLogic Logic;

        private void Awake()
        {
            Logic = this.GetComponent<AddPanelLogic>();
        }

        private void OnEnable()
        {
            Init();
        }

        private void Init()
        {
            wordInput.text = String.Empty;
            RemoveAllMeaning();
            CreateItem();
        }

        public void AddLineOnClick()
        {
            CreateItem();
        }

        public void AddWordOnClick()
        {
            if (CheckLegal() == false)
            {
                DebugTool.Error("[ERROR --- 006] AddPanel 请检查是否有空未填");
                return;
            }

            Word word = new Word();
            word.SetWord(wordInput.text);
            for(int i = 0; i < Content.childCount; i++)
            {
                Meaning meaning = new Meaning();
                MeaningItem item = Content.GetChild(i).gameObject.GetComponent<MeaningItem>();
                meaning.SetProps(item.GetPOS(), item.GetMeaning());
                word.SetMeaning(meaning);
            }

            Logic.SaveWord(word);
            ClearInput();
        }

        private void ClearInput()
        {
            Init();
        }

        private void RemoveAllMeaning()
        {
            if (Content.childCount > 0)
            {
                for (int i = 0; i < Content.childCount; i++)
                {
                    GameObject.Destroy(Content.GetChild(i).gameObject);
                }
            }
        }

        private void CreateItem()
        {
            GameObject item = GameObject.Instantiate(ItemPrefab, Content);
        }

        private bool CheckLegal()
        {
            if (wordInput.text == "")
                return false;
            if (Content.childCount == 0)
                return false;
            return true;
        }
    }
}