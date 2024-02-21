using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public void ResumeClick()
    {
        UIManager.Instance.HideUI(name);
        UIManager.Instance.HideUI("TitleBG");
    }

    public void SettingClick()
    {
        UIManager.Instance.ShowUI("SettingUI");
    }

    public void TitleClick()
    {
        SceneLoader.Instance.LoadScene("TitleScene");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

}
