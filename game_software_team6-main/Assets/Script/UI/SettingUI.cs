
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
        // ���� ��ũ�� ��� ��������
        isFullScreen = Screen.fullScreen;
        ScreenButtonText.text = isFullScreen ? "전체화면" : "창모드";
        UpdateButtons();
    }

    //Ǯ��ũ�� ���ο� ���� Ŭ���� ȭ�� ��� ��ȯ�ϰ� ��ư �ؽ�Ʈ�� �ٲٱ�.
    public void ScreenModeButtonClick()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
        ScreenButtonText.text = isFullScreen ? "전체화면" : "창모드";
    }

    //��ư Ŭ���ø��� ȣ��. ���� ũ�� ���ڸ� �ٲٰ�, ��ư�� Ȱ��ȭ ���¸� ����.
    public void UpdateButtons()
    {
        bgmPercent.text = $"{SettingData.Instance.Setting.bgm}%";
        effectPercent.text = $"{SettingData.Instance.Setting.effect}%";

        UpdateButtonState(minusBgmButton, SettingData.Instance.Setting.bgm > 0);
        UpdateButtonState(plusBgmButton, SettingData.Instance.Setting.bgm < 100);
        UpdateButtonState(minusEffectButton, SettingData.Instance.Setting.effect > 0);
        UpdateButtonState(plusEffectButton, SettingData.Instance.Setting.effect < 100);
    }

    //���ǿ� ����, ȭ��ǥ ��ư�� ��Ȱ��ȭ/Ȱ��ȭ
    private void UpdateButtonState(Button button, bool interactable) 
    {
        button.gameObject.SetActive(interactable);
    }

    //���� 10�� ���̳ʽ�
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

    //���� 10�� �÷���
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
