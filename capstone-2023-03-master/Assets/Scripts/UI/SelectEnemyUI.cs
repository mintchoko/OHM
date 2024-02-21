using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class SelectEnemyUI : MonoBehaviour
{
    [SerializeField]
    private Button Enemy3Click;
    [SerializeField]
    private Button Enemy2Click;
    [SerializeField]
    private Button Enemy1Click;

    TaskCompletionSource<bool> taskCompletionSource;


    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            if (!EnemyData.Instance.Isalive[i])
            {
                switch (i)
                {
                    case 0:
                        Enemy1Click.gameObject.SetActive(false);
                        break;
                    case 1:
                        Enemy2Click.gameObject.SetActive(false);
                        break;
                    case 2:
                        Enemy3Click.gameObject.SetActive(false);
                        break;
                }
            }
        }
    }

    public void init(TaskCompletionSource<bool> task)
    {
        taskCompletionSource = task;
    }

    public void Enemy3Clicked()
    {
        BattleData.Instance.SelectedEnemy = 2;
        taskCompletionSource.SetResult(true);
        UIManager.Instance.HideUI("SelectEnemyUI");
    }

    public void Enemy2Clicked()
    {
        BattleData.Instance.SelectedEnemy = 1;
        taskCompletionSource.SetResult(true);
        UIManager.Instance.HideUI("SelectEnemyUI");
    }

    public void Enemy1Clicked()
    {
        BattleData.Instance.SelectedEnemy = 0;
        taskCompletionSource.SetResult(true);
        UIManager.Instance.HideUI("SelectEnemyUI");
    }

}
