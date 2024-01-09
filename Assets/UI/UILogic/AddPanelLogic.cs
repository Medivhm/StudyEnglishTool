using Managers;
using Model;
using System;
using System.Collections;
using Tools;
using UnityEngine;

namespace UILogic
{
    public class AddPanelLogic : MonoBehaviour
    {
        public void SaveWord(Word word)
        {
            WordManager.Instance.AddWord(word);
            WordManager.Instance.Save();
        }
    }
}