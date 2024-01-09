using Managers;
using Model;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UILogic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RandPanel : MonoBehaviour
    {
        public Transform Content;
        public GameObject TextPrefab;
        public InputField InputField;
        public Text resultText;
        public GameObject ButtonRand;

        private RandPanelLogic Logic;

        public void Awake()
        {
            Logic = this.GetComponent<RandPanelLogic>();
        }

        private void OnEnable()
        {
            Logic.ResetRand();
            Word word = Logic.Rand();
            if(word != null )
            {
                StartCoroutine(ResetContent(word.GetMeanings()));
            }
        }

        public IEnumerator ResetContent(List<Meaning> meanings)
        {
            ClearContent();
            for(int i = 0; i < meanings.Count; i++)
            {
                Meaning meaning = meanings[i];
                GameObject textGo = GameObject.Instantiate(TextPrefab, Content);
                Text text = textGo.GetComponent<Text>();
                text.text = meaning.ToString();
                yield return null;
            }
            ClearInputText();
            InputField.ActivateInputField();
            LayoutRebuilder.ForceRebuildLayoutImmediate(Content.gameObject.GetComponent<RectTransform>());
        }

        public void ClearContent()
        {
            int num = Content.childCount;
            for(int i = 0; i < num; i++)
            {
                Transform go = Content.GetChild(i);
                Destroy(go.gameObject);
            }
        }

        public string GetInputText()
        {
            return InputField.text;
        }

        public void ClearInputText()
        {
            InputField.text = string.Empty;
        }

        public void RandOnClick()
        {
            ButtonRand.SetActive(false);
            JudgeResult();

            TimerManager.Instance.CreateTimer("ButtonRand", 1, () =>
            {
                ButtonRand.SetActive(true);
                Word word = Logic.Rand();
                if (word == null)
                {
                    ShowOver();
                    return;
                }
                StartCoroutine(ResetContent(word.GetMeanings()));
            });
        }

        public bool JudgeResult()
        {
            string currWord = Logic.GetCurrentWord().GetWord();
            if (this.GetInputText() == currWord)
            {
                ShowCorrect(currWord);
                return true;
            }
            ShowError(currWord);
            return false;
        }

        public void ShowCorrect(string currWord)
        {
            resultText.text = "<color=green>" + currWord + "</color>";
        }

        public void ShowError(string currWord)
        {
            resultText.text = "<color=red>" + currWord + "</color>";
        }

        public void ShowOver()
        {
            resultText.text = "<color=green>已经完整过一遍了</color>";
        }
    }
}