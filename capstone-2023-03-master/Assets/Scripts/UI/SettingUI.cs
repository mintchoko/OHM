
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{

    private bool isFullScreen;

    [SerializeField]
    public TMP_Text ScreenButtonText;
    [SerializeField]
    public TMP_Text bgmPercent;
    [SerializeField]
    public TMP_Text effectPercent;

    [SerializeField]
    public Button minusBgmButton;
    [SerializeField]
    public Button plusBgmButton;
    [SerializeField]
    public Button minusEffectButton;
    [SerializeField]
    public Button plusEffectButton;

    private void Awake()
    {
        // 현재 스크린 모드 가져오기
        isFullScreen = Screen.fullScreen;
        ScreenButtonText.text = isFullScreen ? "전체화면" : "창모드";
        UpdateButtons();
    }

    //풀스크린 여부에 따라 클릭시 화면 모드 전환하고 버튼 텍스트도 바꾸기.
    public void ScreenModeButtonClick()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
        ScreenButtonText.text = isFullScreen ? "전체화면" : "창모드";
    }

    //버튼 클릭시마다 호출. 볼륨 크기 숫자를 바꾸고, 버튼의 활성화 상태를 변경.
    public void UpdateButtons()
    {
        bgmPercent.text = $"{SettingData.Instance.Setting.bgm}%";
        effectPercent.text = $"{SettingData.Instance.Setting.effect}%";

        UpdateButtonState(minusBgmButton, SettingData.Instance.Setting.bgm > 0);
        UpdateButtonState(plusBgmButton, SettingData.Instance.Setting.bgm < 100);
        UpdateButtonState(minusEffectButton, SettingData.Instance.Setting.effect > 0);
        UpdateButtonState(plusEffectButton, SettingData.Instance.Setting.effect < 100);
    }

    //조건에 따라, 화살표 버튼을 비활성화/활성화
    private void UpdateButtonState(Button button, bool interactable) 
    {
        button.gameObject.SetActive(interactable);
    }

    //볼륨 10씩 마이너스
    public void MinusClick(string type)
    {
        if (type == "BGM")
        {
            SoundManager.Instance.UpdateVolume(Sound.Bgm, -10);
        }
        else if (type == "Effect")
        {
            SoundManager.Instance.UpdateVolume(Sound.Effect, -10);
        }
        UpdateButtons();
    }

    //볼륨 10씩 플러스
    public void PlusClick(string type)
    {
        if (type == "BGM")
        {
            SoundManager.Instance.UpdateVolume(Sound.Bgm, 10);
        }
        else if (type == "Effect")
        {
            SoundManager.Instance.UpdateVolume(Sound.Effect, 10);
        }
        UpdateButtons();
    }

    public void ExitButtonClick()
    {
        UIManager.Instance.HideUI("SettingUI");
    }
}
