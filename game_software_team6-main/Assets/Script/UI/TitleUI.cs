using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUI : BaseUI
{
    public void SingleButtonClick()
    {
        Debug.Log("시작");
        SceneLoader.Instance.LoadScene("SampleScene");
    }

    public void MultiButtonClick()
    {
        UIManager.Instance.ShowUI("ConfirmUI", false)
            .GetComponent<ConfirmUI>()
            .Init(
                "준비중입니다.",
                () => { UIManager.Instance.HideUI("ConfirmUI"); }
            );
    }

    public void SettingButtonClick()
    {
        UIManager.Instance.ShowUI("SettingUI");
    }

    public void ExitButtonClick()
    {
        UIManager.Instance.ShowUI("SelectUI", false)
            .GetComponent<SelectUI>()
            .Init(
                "정말로 종료하시겠습니까?",
                () => { Application.Quit(); },
                () => { UIManager.Instance.HideUI("SelectUI"); }
            );
    }
}
