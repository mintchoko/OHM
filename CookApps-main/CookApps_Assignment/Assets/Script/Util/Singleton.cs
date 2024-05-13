using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;
    public static T Instance { get { Init(); return instance; } private set { instance = value; } }

    private static bool isDestroyed = false;


    protected virtual void Awake()
    {
        Init();
    }

    private void OnDestroy()
    {
        isDestroyed = true;
    }

    protected static void Init()
    {
        if(isDestroyed == true) return;

        if (instance == null)
        {
            GameObject go = GameObject.Find(typeof(T).Name);

            if(go == null)
            { 
                go = new GameObject(typeof(T).Name);
                go.AddComponent<T>();
            }

            instance = go.GetComponent<T>();
        }
    }
}
