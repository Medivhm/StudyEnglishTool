using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

namespace Tools
{
    // 解析状态机
    class AnalyzeWords
    {
        private static Dictionary<string, Word> words;
        private static Word word;
        private static Meaning meaning;
        private static int pointer;
        private static string content;

        public static Dictionary<string, Word> Analyze(string c)
        {
            content = c;
            words = new Dictionary<string, Word>();
            pointer = 0;
            Start();
            Dictionary<string, Word> res = words;
            NullAll();
            return res;
        }

        public static void Start()
        {
            if (InRange())
            {
                if (content[pointer] != '<')
                {
                    // 出错了
                    DebugTool.Error("[ERROR --- 001] 单词本解析错误");
                    return;
                }
                pointer++;
                GetWordStart();
            }
        }

        public static void GetWordStart()
        {
            word = new Word();
            StringBuilder sb = new StringBuilder();
            while (InRange())
            {
                if (content[pointer] == '<')
                {
                    word.SetWord(sb.ToString());
                    GetMeaning();
                    return;
                }
                sb.Append(content[pointer]);
                pointer++;
            }
        }

        public static void GetMeaning()
        {
            if (content[pointer] == '<')
            {
                pointer++;
            }
            else if(content[pointer] == '>')
            {
                GetWordOver();
                GetWordStart();
            }
            else
            {
                // 错误
                DebugTool.Error("[ERROR --- 002] 单词本解析错误");
                return;
            }
            meaning = new Meaning();
            StringBuilder sb = new StringBuilder();
            while (InRange())
            {
                if (content[pointer] == '>')
                {
                    SetMeaningStr(meaning, sb.ToString());
                    word.SetMeaning(meaning);
                    pointer++;
                    GetMeaning();
                    return;
                }
                sb.Append(content[pointer]);
                pointer++;
            }
        }

        public static void GetWordOver()
        {
            if (InRange())
            {
                if (content[pointer] == '>')
                {
                    pointer++;
                    words.Add(word.GetWord(), word);
                    pointer++;
                    GetWordStart();
                }
            }
        }

        public static void GetBlank()
        {

        }

        private static bool InRange()
        {
            if(pointer < content.Length)
            {
                return true;
            }
            return false;
        }

        private static void SetMeaningStr(Meaning meaning, string meaningStr)
        {
            string[] strs = meaningStr.Split('|');
            meaning.SetProps(strs[0], strs[1]);
        }

        private static void NullAll()
        {
            words = null;
            meaning = null;
            word = null;
            pointer = 0;
            content = null;
        }
    }
}
