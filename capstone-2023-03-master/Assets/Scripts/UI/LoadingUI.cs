using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    private string nextScene = null;

    [SerializeField]
    public Image progressBar; //�ε� ������

    public void Init(string sceneName)
    {
        nextScene = sceneName;
        StartCoroutine(LoadProcess());
    }

    IEnumerator LoadProcess()
    {
        yield return null; 

        // �񵿱� �ε� ����
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextScene);

        // �ε��� �Ϸ�� ������ ��ٸ�
        while (!asyncLoad.isDone)
        {
            // ������� ����ϰ� progressBar �̹����� fillAmount �Ӽ��� �Ҵ�
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            progressBar.fillAmount = progress;
            yield return null;
        }

    }
}
