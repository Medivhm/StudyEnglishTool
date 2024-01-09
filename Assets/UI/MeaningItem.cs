using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeaningItem : MonoBehaviour
{
    public Button delBtn;
    public InputField POS;
    public InputField meaning;

    private void Awake()
    {
        delBtn.onClick.AddListener(() =>
        {
            GameObject.Destroy(this.gameObject);
        });
    }

    public string GetPOS()
    {
        return POS.text;
    }

    public string GetMeaning()
    {
        return meaning.text;
    }
}
