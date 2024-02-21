using TMPro;
using UnityEngine;


public class TitleUI : BaseUI
{
    public void StartButtonClick()
    {
        SceneLoader.Instance.LoadScene("OpScene");
    }

    public void LibraryButtonClick()
    {
        UIManager.Instance.ShowUI("LibraryUI")
            .GetComponent<LibraryUI>()
            .Init(LibraryMode.Library);
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
                "������ �����Ͻðڽ��ϱ�?",
                () => { Application.Quit(); },
                () => { UIManager.Instance.HideUI("SelectUI"); }
            );
    }
}
