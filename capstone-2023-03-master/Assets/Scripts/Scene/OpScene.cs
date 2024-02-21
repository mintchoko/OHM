using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        SoundManager.Instance.Play("OpBgm", Sound.Effect);
        UIManager.Instance.ShowUI("FullDialogUI").GetComponent<FullDialogUI>().Init(
            6000,
            () =>
            {
                SceneLoader.Instance.LoadScene("GameScene");
            }
        );
    }
}
