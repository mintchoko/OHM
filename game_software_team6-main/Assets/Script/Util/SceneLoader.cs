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

    //씬 데이터 청소하고, 로딩창 부르는 함수
    public void LoadScene(string sceneName)
    {
        UIManager.Instance.ShowUI("LoadingUI")
            .GetComponent<LoadingUI>()
            .Init(sceneName);
        UIManager.Instance.Clear();
        SoundManager.Instance.Clear();
        
    }
}
