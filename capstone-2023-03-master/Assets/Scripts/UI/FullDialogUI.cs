using DataStructs;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class FullDialogUI : MonoBehaviour, IPointerDownHandler
{
    private bool isPrinting = false; // �߰��� ����

    private int dialogIndex;
    private int lineCount;
    private LineStruct currentLine;

    [SerializeField]
    private TMP_Text lineText;

    private Action CloseAction;

    private void OnEnable()
    {
        InputActions.keyActions.UI.Check.started += NextDialogByCheck;
    }

    private void OnDisable()
    {
        InputActions.keyActions.UI.Check.started -= NextDialogByCheck;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isPrinting) // ��� ������ ���� ���� ���� ��ȭ�� �Ѿ
            NextDialog();
    }

    public void NextDialogByCheck(InputAction.CallbackContext context)
    {
        if (!isPrinting) // ��� ������ ���� ���� ���� ��ȭ�� �Ѿ
            NextDialog();
    }

    public void Init(int index, Action CloseCallback = null)
    {
        dialogIndex = index;
        CloseAction = CloseCallback;
        lineCount = 0;
        NextDialog();
    }

    public void Init(string line, Action CloseCallback = null)
    {
        dialogIndex = -1;
        lineText.text = line;
        CloseAction = CloseCallback;
    }

    public void NextDialog()
    {
        if (dialogIndex == -1 || GameData.Instance.DialogDic[dialogIndex].Count == lineCount)
        {
            UIManager.Instance.HideUI("FullDialogUI");
            CloseAction?.Invoke();
            return;
        }

        currentLine = GameData.Instance.DialogDic[dialogIndex][lineCount];
        StartCoroutine(TypeSentence(currentLine.line));
        lineCount++;
    }

    //�ѱ��ھ� ���
    IEnumerator TypeSentence(string sentence)
    {
        isPrinting = true;
        lineText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            lineText.text += letter;
            yield return new WaitForSeconds(0.07f); // wait for the next frame
        }
        isPrinting = false; // ��� �Ϸ�
    }
}
