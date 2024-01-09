using Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;
using UnityEngine;

class Loading : MonoBehaviour
{

    void Start()
    {
        DebugTool.Log("Enter Start");
        DebugTool.Log(Define.FilePath.WordsPath);

        //string str = TranslateTool.Translate("I don't know", TranslateTool.Language.English, TranslateTool.Language.SimpleChinese, null);
        //DebugTool.Log("翻译结果:  " + str);
    }
}