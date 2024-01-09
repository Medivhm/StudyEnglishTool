using Managers;
using Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tools;
using UnityEngine;

namespace UILogic
{
    public class RandPanelLogic : MonoBehaviour
    {
        private Word currWord;
        private List<Word> words;
        int[] hasRanded;              // 如果已经随到了，就置1，反之置0
        int wordsNum;
        int noRandNum;                // 剩余还没被随到的单词数量
        
        public Word GetCurrentWord()
        {
            return currWord;
        }

        public Word Rand()
        {
            if (hasRanded == null)
            {
                ResetRand();
            }

            if (IsOver())
            {
                return null;
            }

            int rand = Random.Range(0, wordsNum);
            if (noRandNum > 0)
            {
                // 如果已经随到过了
                while (hasRanded[rand] == 1)
                {
                    rand = (rand + 1) % wordsNum;
                }
            }

            // 取到合适的随机数了
            hasRanded[rand] = 1;
            noRandNum--;
            currWord = words[rand];
            return currWord;
        }

        public void ResetRand()
        {
            words = WordManager.Instance.words.Values.ToList();
            wordsNum = words.Count;
            hasRanded = new int[wordsNum];
            for (int i = 0; i < wordsNum; i++)
            {
                hasRanded[i] = 0;
            }
            noRandNum = wordsNum;
            currWord = null;
        }

        private bool IsOver()
        {
            if(noRandNum <= 0)
            {
                return true;
            }
            return false;
        }
    }
}