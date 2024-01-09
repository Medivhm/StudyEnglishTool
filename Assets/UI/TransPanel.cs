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
    public class TransPanel : MonoBehaviour
    {
        private TransPanelLogic Logic;
        private Text ModeText;
        private string[] modeStr =
        {
            "英->中",
            "中->英",
        };
        private int mode;
        public int Mode
        {
            get { return mode; }
            set
            {
                mode = value;
                ModeText.text = modeStr[mode];
            }

        }


        public InputField Source;
        public Text Result;
        public Button ModeBtn;


        private void Awake()
        {
            Logic = this.GetComponent<TransPanelLogic>();
            ModeText = ModeBtn.transform.GetChild(0).GetComponent<Text>();
            Mode = 0;
        }

        private void OnEnable()
        {
            
        }

        public void ModeOnClick()
        {
            Mode = 1 - Mode;
        }

        public void TransOnClick()
        {
            if(CheckLegal() ==  false)
            {
                DebugTool.Error("[ERROR --- 007] TransPanel 未输入文本");
                return;
            }

            string source = Source.text;
            string result;
            if(mode == 0)
            {
                result = TranslateTool.Translate(source, TranslateTool.Language.English, TranslateTool.Language.SimpleChinese, null);
            }
            else if(mode == 1)
            {
                result = TranslateTool.Translate(source, TranslateTool.Language.SimpleChinese, TranslateTool.Language.English, null);
            }
            else
            {
                result = "出错了";
                DebugTool.Error("[ERROR --- 008] TransPanel 未知错误");
            }
            Result.text = result;
        }

        public bool CheckLegal()
        {
            if(Source.text == String.Empty)
            {
                return false;
            }
            return true;
        }
    }
}