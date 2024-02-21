using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectUI : MonoBehaviour
{
    [SerializeField]
    TMP_Text message;
    [SerializeField]
    Action YesAction;
    [SerializeField]
    Action NoAction;

    public void Init(string text, Action YesCallback, Action NoCallback)
    {
        message.text = text;
        YesAction = YesCallback;
        NoAction = NoCallback;
    }

    public void YesClick()
    {
        UIManager.Instance.HideUI("SelectUI");
        YesAction?.Invoke();
    }

    public void NoClick()
    {
        UIManager.Instance.HideUI("SelectUI");
        NoAction?.Invoke();
    }

}
