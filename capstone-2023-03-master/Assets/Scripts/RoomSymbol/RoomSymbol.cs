using System;
using UnityEngine;

public class RoomSymbol : MonoBehaviour
{
    protected int index; //�⺻������ ��� ��ȭ����, �ɺ��� ���� ���� �����ϴ� �ε���
    protected Define.EventType type;

    public void Init(int index, Define.EventType type)
    {
        this.index = index;
        this.type = type;
    }

    //�÷��̾ �̺�Ʈ �ɺ��� ���� �ɾ��� ��
    public virtual void TalkStart()
    {
        //��ȭâ UI�� ���� UI�� ������ ��, �� �ɺ��� TalkEnd �Լ��� ����� (�ݹ�)
        UIManager.Instance.ShowUI("DialogUI")
            .GetComponent<DialogUI>()
            .Init(index, TalkEnd);
    }

    //�̺�Ʈ �ɺ��� ��ȭ �̺�Ʈ�� ������ �� (���� ��ȭâ�� �ݾ��� ��) ȣ���
    public virtual void TalkEnd()
    {
        AssetLoader.Instance.Destroy(gameObject);
    }
}
