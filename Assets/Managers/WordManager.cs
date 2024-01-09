using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace Managers
{
    class WordManager : MonoSingleton<WordManager>
    {
        //public List<Word> words = new List<Word>();
        public Dictionary<string, Word> words = new Dictionary<string, Word>();

        protected override void OnAwake()
        {
            // Test Data
            //List<Meaning> meanings = new List<Meaning>();
            //var meaning = new Meaning();
            //meaning.SetProps("adj.", "相等的；能胜任的");
            //meanings.Add(meaning);
            //meaning = new Meaning();
            //meaning.SetProps("v.", "比得上");
            //meanings.Add(meaning);

            //Word word = new Word();
            //word.SetWord("equal");
            //word.SetMeanings(meanings);

            //words.Add(word);
            //FileTool.SetWords();
            // Test


            // 初始化数据
            FileTool.GetWords();
        }

        public void AddWord(Word word)
        {
            if(!words.TryGetValue(word.GetWord(), out _))
            {
                words.Add(word.GetWord(), word);
            }
            else
            {
                DebugTool.Error("[ERROR --- 005] WordManager 已存在该单词" + word.GetWord());
            }
        }

        public void DelWord(string word)
        {
            if (words.TryGetValue(word, out _))
            {
                words.Remove(word);
            }
        }

        public void Save()
        {
            FileTool.SetWords();
        }
    }
}
