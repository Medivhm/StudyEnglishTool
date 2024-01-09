using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Define;
using Managers;

namespace Tools
{
    class FileTool
    {
        public static bool HasFile(string p)
        {
            string path = p;
            if (File.Exists(path))
            {
                return true;
            }
            return false;
        }

        public static void CopyFile(string from, string to)
        {
            File.Copy(from, to, true);
        }

        public static void DeleteWordsFile()
        {
            string path = FilePath.WordsPath;

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static void SetWords()
        {
            string path = FilePath.WordsPath;

            using (StreamWriter sw = File.CreateText(path))
            {
                StringBuilder sb = new StringBuilder();
                foreach (var word in WordManager.Instance.words.Values)
                {
                    sb.Clear();
                    sb.Append("<").Append(word.GetWord());
                    foreach(var meaning in word.GetMeanings())
                    {
                        sb.Append("<").Append(meaning.GetPartOfSpeech()).Append("|").Append(meaning.GetMeaning()).Append(">");
                    }
                    sb.Append(">");
                    sw.Write(sb);

                    DebugTool.Log("Save Word :" + sb.ToString());
                }
            }
        }

        public static void GetWords()
        {
            string path = FilePath.WordsPath;
            if (!File.Exists(path))
            {
                File.Create(path);
                WordManager.Instance.words = new Dictionary<string, Model.Word>();
                return;
            }

            string content = File.ReadAllText(path);
            WordManager.Instance.words = AnalyzeWords.Analyze(content);
        }
    }
}
