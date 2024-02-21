using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//���� â���� ����Ű�� UI ����
public class InGameUI : MonoBehaviour
{

    private void OnEnable()
    {
        UIManager.Instance.UIChange += ChangeUIControll;
        InputActions.keyActions.Player.Deck.started += OnDeckStarted;
        InputActions.keyActions.Player.Pause.started += OnPauseStarted;
        InputActions.keyActions.Player.MiniMap.started += OnMiniMapStarted;
    }

    private void OnDisable()
    {
        UIManager.Instance.UIChange -= ChangeUIControll;
        InputActions.keyActions.Player.Deck.started -= OnDeckStarted;
        InputActions.keyActions.Player.Pause.started -= OnPauseStarted;
        InputActions.keyActions.Player.MiniMap.started -= OnMiniMapStarted;
    }

    private void ChangeUIControll(GameObject currentUI)
    {
        if (currentUI?.name == "StatUI") //HUD ��������, �� �÷��̾� ���� �߿��� UI ����Ű ��Ȱ��ȭ
        {
            InputActions.keyActions.UI.Disable();
            InputActions.keyActions.Player.Enable();
        }
        else //�� �̿��� UI�� �������� �÷��̾� ���� ��Ȱ��ȭ
        {
            InputActions.keyActions.Player.Disable();
            InputActions.keyActions.UI.Enable();
        }
    }

    //I��ư���� �κ��丮 UI ����
    public void OnDeckStarted(InputAction.CallbackContext context)
    {
        LibraryUI libraryUI = UIManager.Instance.ShowUI("LibraryUI").GetComponent<LibraryUI>();
        libraryUI.Init(LibraryMode.Deck);
    }

    //ESC��ư���� �Ͻ����� UI ����
    public void OnPauseStarted(InputAction.CallbackContext context)
    {
        UIManager.Instance.ShowUI("TitleBG");
        UIManager.Instance.ShowUI("PauseUI", false);
    }

    //Tab���� �̴ϸ� UI ����
    public void OnMiniMapStarted(InputAction.CallbackContext context)
    {
        UIManager.Instance.ShowUI("MiniMapUI");
    }

}
