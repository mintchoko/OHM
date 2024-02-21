using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


//��ȭ�� UI ��ȯ
//UI ������ UI���� �̰ɷ� �θ��� �ȵ�
public class UIManager : Singleton<UIManager>
{
    //��ȯ�� ��� UI�� ��Ʈ�� ���ӿ�����Ʈ
    public GameObject UIRoot
    {
        get
        {
            GameObject root = GameObject.Find("UIRoot");
            if (root == null)
            {
                root = new GameObject();
                root.name = "UIRoot";
            }
            return root;
        }
    }

    //�˾� UI���� �������� ����
    private Stack<GameObject> UIStack = new Stack<GameObject>();

    //UI ���� �� �߻��� �̺�Ʈ
    public Action<GameObject> UIChange;


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }


    //�� ��ü UI�� �ε��ؼ� ȭ�鿡 ���� �Լ�
    //������ UI�� Ȱ��ȭ�Ǹ� ����ȭ/���ĺ��� ������ ���� �Ʒ��� �� UI�� ��Ȱ��ȭ�ϴ� �� �⺻ ����. (������ ���� ����)
    public GameObject ShowUI(string name, bool hidePreviousPanel = true)
    {
        GameObject ui = AssetLoader.Instance.Instantiate($"Prefabs/UI/{name}", UIRoot.transform);

        Canvas canvas = ui.GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.sortingOrder = UIStack.Count + 1; // ���ο� UI�� sortOrder�� ������ ũ�⿡ 1�� ���� ������ �����մϴ�.
        }

        if (UIStack.Count > 0 && hidePreviousPanel)
        {
            UIStack.Peek().SetActive(false);
        }
        UIStack.Push(ui);
        UIChange?.Invoke(ui);
        return ui;
    }


    //UI ���ÿ��� �� ���� �ִ� Ư�� UI�� ����, ���� UI�� ����ó�� �Ǿ������� �ٽ� ������
    public void HideUI(string name)
    {
        if (UIStack.Count > 0 && UIStack.Peek().name == name)
        {
            GameObject ui = UIStack.Pop();

            AssetLoader.Instance.Destroy(ui);

            if (UIStack.Count > 0)
            {
                UIStack.Peek().SetActive(true);
                UIChange?.Invoke(UIStack.Peek());
            }
            else
            {
                UIChange?.Invoke(null);
            }
        }
    }

    public void Clear()
    {
        UIStack.Clear();
    }
}
