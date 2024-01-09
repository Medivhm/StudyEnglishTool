using OpenapiDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Tools
{
    class TranslateTool
    {
        public enum Language
        {
            SimpleChinese           = 0,
            TraditionalChinese      = 1,
            English                 = 2,
            Japanese                = 3,
        }

        private static string[] LangCode =
        {
            "zh-CHS",         // 中简
            "zh-CHT",         // 中繁
            "en",             // 英语
            "ja",             // 日语
        };

        private class TransResult
        {
            public string[] returnPhrase;               // 短语
            public string query;                        // 问题
            public string errorCode;                    // 错误码
            public string l;                            // 返回语言
            public string tSpeakUrl;                    // 应该是声音的url
            public string[] translation;                // 翻译结果
            public bool isWord;                         // 是否是word
        }

        /// <summary>
        /// 翻译
        /// </summary>
        /// <param name="untreated">源文本</param>
        /// <param name="from">源文本语言</param>
        /// <param name="to">译文语言</param>
        /// <param name="outId">使用指定的词典</param>
        /// <returns>译文或错误信息</returns>
        public static string Translate(string untreated, Language from, Language to, string outId)
        {
            var jsonStr = TranslanteDemo.Translate(untreated, LangCode[((int)from)], LangCode[((int)to)], outId);
            TransResult res = JsonUtility.FromJson<TransResult>(jsonStr);
            if (res == null)
            {
                return "翻译失败，请检查";
            }
            else if (res.errorCode != "0")
            {
                return "翻译失败，错误代码 " + res.errorCode;
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                foreach (string s in res.translation)
                {
                    sb.AppendLine(s);
                }
                return sb.ToString();
            }
        }
    }
}
