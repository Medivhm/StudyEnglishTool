using System;
using System.Collections.Generic;

namespace OpenapiDemo
{
    static class TranslanteDemo
    {
        // 您的应用ID
        private static string APP_KEY = "480ea59c9fd30037";
        // 您的应用密钥
        private static string APP_SECRET = "ZQ8lP9G7AHOpXxVutre0zMtix6l8CsPs";

        public static string Translate(string q, string from, string to, string vocabId)
        {
            // 添加请求参数
            Dictionary<String, String[]> paramsMap = createRequestParams(q, from, to, vocabId);
            // 添加鉴权相关参数
            AuthV3Util.addAuthParams(APP_KEY, APP_SECRET, paramsMap);
            Dictionary<String, String[]> header = new Dictionary<string, string[]>() { { "Content-Type", new String[] { "application/x-www-form-urlencoded" } } };
            // 请求api服务
            byte[] result = HttpUtil.doPost("https://openapi.youdao.com/api", header, paramsMap, "application/json");
            // 打印返回结果
            if (result != null)
            {
                string resStr = System.Text.Encoding.UTF8.GetString(result);
                Console.WriteLine(resStr);
                return resStr;
            }
            return "翻译失败，请检查";
        }

        private static Dictionary<String, String[]> createRequestParams(string q, string from, string to, string vocabId)
        {
            // note: 将下列变量替换为需要请求的参数
            //string q = "待翻译文本";
            //string from = "源语言语种";
            //string to = "目标语言语种";
            //string vocabId = "您的用户词表ID";

            return new Dictionary<string, string[]>() {
                { "q", new string[]{q}},
                {"from", new string[]{from}},
                {"to", new string[]{to}},
                {"vocabId", new string[]{vocabId}}
            };
        }
    }
}