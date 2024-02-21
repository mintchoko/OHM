using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    private void Awake()
    {
        UIManager.Instance.ShowUI("TitleBG");
        UIManager.Instance.ShowUI("TitleUI", false);
    }
}
