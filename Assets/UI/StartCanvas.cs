using Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    class StartCanvas : MonoBehaviour
    {
        public Text InfoText;
        public InputField FileOutInput;
        public InputField FileInInput;

        private int TimerId = -1;

        public void EnterOnClick()
        {
            ClearTimer();
            SceneManager.Instance.LoadScene("Main");
        }

        public void DeleteCacheOnClick()
        {
            FileTool.DeleteWordsFile();
            FileTool.GetWords();
            ClearTimer();
            InfoText.text = "<color=green>已清除缓存</color>";
            TimerId = TimerManager.Instance.CreateTimer(2f, () =>
            {
                InfoText.text = "提示面板";
            });
        }

        public void FileOutOnClick()
        {
            string path = FileOutInput.text;
            if(Directory.Exists(path))
            {
                FileTool.CopyFile(Define.FilePath.WordsPath, path + "/Words.txt");
                FileOutInput.text = String.Empty;
                InfoText.text = "<color=green>导出成功，导入时请保证文件名一致</color>";
                TimerId = TimerManager.Instance.CreateTimer(2f, () =>
                {
                    InfoText.text = "提示面板";
                });
            }
            else
            {
                ClearTimer();
                InfoText.text = "<color=red>不存在该导出路径，导出失败</color>";
                TimerId = TimerManager.Instance.CreateTimer(2f, () =>
                {
                    InfoText.text = "提示面板";
                });
            }
        }

        public void FileInOnClick()
        {
            string path = FileInInput.text;
            if (FileTool.HasFile(path))
            {
                FileTool.CopyFile(path, Define.FilePath.WordsPath);
                FileTool.GetWords();
                FileInInput.text = String.Empty;
                InfoText.text = "<color=green>导入成功</color>";
                TimerId = TimerManager.Instance.CreateTimer(2f, () =>
                {
                    InfoText.text = "提示面板";
                });
            }
            else
            {
                ClearTimer();
                InfoText.text = "<color=red>不存在该文件，导入失败</color>";
                TimerId = TimerManager.Instance.CreateTimer(2f, () =>
                {
                    InfoText.text = "提示面板";
                });
            }
        }

        public void ClearTimer()
        {
            if(TimerId != -1)
            {
                TimerManager.Instance.RemoveTimer(TimerId);
                TimerId = -1;
            }
        }
    }
}
