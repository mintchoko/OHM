using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    //�̱������� ������ ���� ������Ʈ + ������Ʈ ����
    //Singleton<T>�� ��ӹ��� ������ T ������Ʈ���� �ڽ��� ����Ű�� ������ Instance�� ����

    private static T instance;
    public static T Instance { get { Init(); return instance; } private set { instance = value; } }

    private static bool isDestroyed = false;


    protected virtual void Awake()
    {
        Init();
    }

    //����Ƽ�� OnDisable(������Ʈ�� ��Ȱ��ȭ) �Լ��� Destroy(������Ʈ�� �ı�)�ɶ��� ȣ��ȴ�.
    //�� �ڵ� ��, OnDisable �Լ����� �̱��� �ν��Ͻ��� �����Ϸ��� �� ��, ������Ʈ�� �ı��� ��Ȳ�̶�� �̱��� ������Ʈ�� ���� �����ع�����...
    //�׷��ٰ� �ڵ����� ���� �����ϴ� �۾��� ���ָ� ������.
    //�׷��� �̱��� ������Ʈ�� �ı��ǰ� ���� �����ϴ� ���� static bool�� ����� ��������� �ʵ��� ��.
    private void OnDestroy()
    {
        isDestroyed = true;
    }

    protected static void Init()
    {
        if (isDestroyed == true) return;

        if (instance == null)
        {
            GameObject go = GameObject.Find(typeof(T).Name);

            if (go == null)
            {
                go = new GameObject(typeof(T).Name);
                go.AddComponent<T>();
            }

            instance = go.GetComponent<T>();
        }
    }
}
