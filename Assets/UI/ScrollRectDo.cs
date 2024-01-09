using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 使用Init初始化，仅支持GridLayout，需把Word更换为元素内信息的载体
/// </summary>
public class ScrollRectDo : MonoBehaviour
{
    public GameObject Prefab;
    public Transform View;
    public Transform Content;

    public int amount;
    public int numPerLine;
    private List<Word> Items;                          // T是里面存放的item数据，非item本身
    private Action<GameObject, Word> Action;           // 如何通过T进行item初始化的方法

    private int allNum = 0;
    private float lastFloat = 0;
    private float offset = 0;
    private int firstIndex = 0;
    private int lastIndex = 0;
    private bool hasInit = false;
    private bool isBuzy = false;
    private Transform firstTrans;
    private Transform lastTrans;
    private RectOffset padding;
    private ScrollRect rect;
    private GridLayoutGroup gridLayout;
    private ContentSizeFitter sizeFitter;
    private Vector2 cellSize;
    private Vector2 spacing;
    private float scale;

    private float UpPoxY
    {
        get { return View.InverseTransformPoint(firstTrans.position).y; }
    }
    private float DownPoxY
    {
        get { return View.InverseTransformPoint(lastTrans.position).y; }
    }
    private float ViewHeight;
    private int ShowLine;


    public void Awake()
    {
        scale = Define.Constant.ScaleFactor;
        rect = GetComponent<ScrollRect>();
        gridLayout = Content.GetComponent<GridLayoutGroup>();
        sizeFitter = Content.GetComponent<ContentSizeFitter>();
        padding = gridLayout.padding;
        cellSize = gridLayout.cellSize;
        spacing = gridLayout.spacing;
        rect.onValueChanged.AddListener(OnScroll);
        ViewHeight = View.GetComponent<RectTransform>().rect.height;
        ShowLine = amount / numPerLine;
    }

    public void Init(List<Word> Items, Action<GameObject, Word> Action)
    {
        StartCoroutine(InitEx(Items, Action));
    }

    IEnumerator InitEx(List<Word> Items, Action<GameObject, Word> Action)
    {
        hasInit = false;

        this.Items = Items;
        this.Action = Action;
        allNum = Items.Count;
        gridLayout.enabled = true;
        sizeFitter.enabled = true;
        Clear();
        yield return null;

        var num = allNum < amount ? allNum : amount;
        for (int i = 0; i < num; i++)
        {
            var go = Instantiate(Prefab, Content);
            go.name = "Item_" + i.ToString();
            InitPrefab(go, Items[i]);
            yield return null;
            if (i == 0)
            {
                firstTrans = go.GetComponent<Transform>();
            }
            if (i == num - 1)
            {
                lastTrans = go.GetComponent<Transform>();
            }
        }
        if (num % numPerLine != 0)
        {
            for (int i = 0; i < (numPerLine - num % numPerLine); i++)
            {
                var go = Instantiate(Prefab, Content);
                go.name = "Item_" + (num + i + 1).ToString();
                go.SetActive(false);
                InitPrefab(go, Items[num + i + 1]);
            }
        }
        yield return null;

        firstIndex = 0;
        lastIndex = num - 1;
        yield return null;

        gridLayout.enabled = false;
        sizeFitter.enabled = false;
        yield return null;

        hasInit = true;
    }

    private void InitPrefab(GameObject go, Word data)
    {
        if (Action != null)
        {
            Action(go, data);
        }
    }

    public void OnScroll(Vector2 vec2)
    {
        if (allNum < amount)
        {
            // 数量不够，没必要操作
            return;
        }
        if (hasInit)
        {
            //拖动快了会导致offset无限接近0，正负号出现错误？
            offset = vec2.y - lastFloat;
            lastFloat = vec2.y;
            //Debug.LogWarning(lastIndex + "             " + firstIndex + "            " + allNum + "           " + ShowLine + "         "+ ShowLine * (cellSize.y + spacing.y));
            //Debug.LogWarning(offset);
            //Debug.LogWarning((offset > 0).ToString() + "            " + (UpPoxY + cellSize.y / 4 < 0).ToString() + "            " + (firstIndex != 0).ToString());
            //Debug.LogWarning((offset < 0).ToString() + "            " + (DownPoxY - cellSize.y / 4 > -ViewHeight).ToString() + "            " + (lastIndex != allNum).ToString() );
            if (UpPoxY + cellSize.y / 4 < 0 && firstIndex != 0)
            {
                //Debug.LogWarning("往上看");
                if (!isBuzy)
                {
                    isBuzy = true;
                    UpContent();
                    firstIndex = firstIndex - numPerLine;
                    if ((lastIndex + 1) % numPerLine == 0)
                    {
                        lastIndex -= numPerLine;
                    }
                    else
                    {
                        lastIndex -= (lastIndex + 1) % numPerLine;
                    }
                    firstTrans = Content.GetChild(0);
                    lastTrans = Content.GetChild(Content.childCount - 1);
                    //Debug.LogWarning(firstTrans + "-------------" + lastTrans + " now lastTrans is " + lastTrans.name);
                    isBuzy = false;
                }
            }
            else if (DownPoxY - cellSize.y / 4 > -ViewHeight && lastIndex != allNum - 1)
            {
                //Debug.LogWarning("往下看" + DownPoxY + "           " + cellSize.y/4 + "               " + -ViewHeight);
                if (!isBuzy)
                {
                    isBuzy = true;
                    DownContent();
                    firstIndex += numPerLine;
                    lastIndex = (lastIndex + numPerLine) > allNum - 1 ? allNum - 1 : (lastIndex + numPerLine);
                    firstTrans = Content.GetChild(0);
                    lastTrans = Content.GetChild(Content.childCount - 1);
                    //Debug.LogWarning(firstIndex + "-------------" + lastIndex + " now lastTrans is " + lastTrans.name);
                    isBuzy = false;
                }
            }
        }
    }

    public void Clear()
    {
        for (int i = 0; i < Content.childCount; i++)
        {
            GameObject.Destroy(Content.GetChild(i).gameObject);
        }
    }

    public void UpContent()
    {
        var rectTrans = Content.GetComponent<RectTransform>();
        rectTrans.sizeDelta = new Vector2(rectTrans.rect.width, rectTrans.rect.height - cellSize.y - spacing.y);
        GameObject[] goes = new GameObject[numPerLine];
        for (int i = 0; i < numPerLine; i++)
        {
            goes[i] = Content.GetChild(amount - numPerLine + i).gameObject;
            InitPrefab(goes[i], Items[firstIndex - numPerLine + i]);
            goes[i].SetActive(true);
        }
        for (int i = numPerLine - 1; i >= 0; i--)
        {
            var pos = goes[i].transform.position;
            goes[i].transform.position = new Vector3(pos.x, pos.y + ShowLine * (cellSize.y + spacing.y) * scale, pos.z);
            goes[i].transform.SetAsFirstSibling();
        }
    }

    public void DownContent()
    {
        var rectTrans = Content.GetComponent<RectTransform>();
        rectTrans.sizeDelta = new Vector2(rectTrans.rect.width, rectTrans.rect.height + cellSize.y + spacing.y);
        GameObject[] goes = new GameObject[numPerLine];
        for (int i = 0; i < numPerLine; i++)
        {
            goes[i] = Content.GetChild(i).gameObject;
            if (lastIndex + i + 1 < allNum)
            {
                InitPrefab(goes[i], Items[lastIndex + i + 1]);
            }
            else
            {
                goes[i].SetActive(false);
            }
        }
        for (int i = 0; i < numPerLine; i++)
        {
            var pos = goes[i].transform.position;
            goes[i].transform.position = new Vector3(pos.x, pos.y - ShowLine * (cellSize.y + spacing.y) * scale, pos.z);
            goes[i].transform.SetAsLastSibling();
        }
    }
}
