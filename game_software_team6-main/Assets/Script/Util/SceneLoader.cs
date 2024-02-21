using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : Singleton<SceneLoader>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    //�� ������ û���ϰ�, �ε�â �θ��� �Լ�
    public void LoadScene(string sceneName)
    {
        UIManager.Instance.ShowUI("LoadingUI")
            .GetComponent<LoadingUI>()
            .Init(sceneName);
        UIManager.Instance.Clear();
        SoundManager.Instance.Clear();
        
    }
}
