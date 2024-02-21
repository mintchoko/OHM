using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EventSymbol : RoomSymbol
{
    public override void TalkStart()
    {

        if(index == 307) //���� ȹ�� �ε����� ������
        {
            if(PlayerData.Instance.HasPartner[StageManager.Instance.Stage - 1] == false) //���� ���������� ���� �� �ִ� ���Ḧ ȹ�� ���� ��쿡�� ī�� ȹ��
            {
                index = 307 + StageManager.Instance.Stage - 1;
            }
            else
            {
                index = Random.Range(301, 307); //�ƴ� ��� �������� �̺�Ʈ �ٽ� �̱�
            }    
        }

        base.TalkStart();
    }

    public override void TalkEnd()
    {
        base.TalkEnd();

        switch (index)
        {
            case 300: //ī�� ȹ�� �̺�Ʈ ��
                UIManager.Instance.ShowUI("CardSelectUI").GetComponent<CardSelectUI>().EventReward();
                break;
            case 301: //��û�� ���� �̺�Ʈ ��

                int takeViewers; //���� ��û��
                int giveMoney; //�� ��

                takeViewers = GameData.Instance.RewardDic[StageManager.Instance.Stage].viewers * 2;
                giveMoney = GameData.Instance.RewardDic[StageManager.Instance.Stage].money; //���� �� ���������� ����ŭ ������

                if(giveMoney <= PlayerData.Instance.Money) //�� �Ⱥ����ϸ� ����â ���� ��ȯ ����
                {
                    UIManager.Instance.ShowUI("SelectUI", false).GetComponent<SelectUI>().Init(
                        $"���� {giveMoney}��ŭ �ְ� ��û�ڸ� {takeViewers}��ŭ �޾ƿ��ðڽ��ϱ�?",
                        () => {
                            PlayerData.Instance.Viewers += takeViewers;
                            PlayerData.Instance.Money -= giveMoney;
                            if (PlayerData.Instance.CheckLevelUp()) //������ üũ
                            {
                                UIManager.Instance.ShowUI("CardSelectUI")
                                    .GetComponent<CardSelectUI>()
                                    .LevelUpReward();
                            } 
                        }, //�� �����ϸ� �� ����, ��û�� ����
                        null //�ƴϿ� �����ϸ� �׾�ŷ Ƣ�������
                    );
                }

                break;
            case 302: //��û�� �Ǹ� �̺�Ʈ ��

                int giveViewers;
                int takeMoney;

                giveViewers = GameData.Instance.RewardDic[StageManager.Instance.Stage].viewers;
                takeMoney = GameData.Instance.RewardDic[StageManager.Instance.Stage].money * 2;

                if (giveViewers <= PlayerData.Instance.Viewers) //��û�� �Ⱥ����ϸ� ����â ���� ��ȯ ����
                {
                    UIManager.Instance.ShowUI("SelectUI", false).GetComponent<SelectUI>().Init(
                        $"��û�ڸ� {giveViewers}��ŭ �ְ� ���� {takeMoney}��ŭ �޾ƿ��ðڽ��ϱ�?",
                        () => {
                            PlayerData.Instance.Viewers -= giveViewers;
                            PlayerData.Instance.Money += takeMoney;
                        }, //�� �����ϸ� ��û�� ����, �� ���
                        null
                    );
                }
                break;
            case 303: //ī�� ���� �̺�Ʈ ��
                UIManager.Instance.ShowUI("LibraryUI").GetComponent<LibraryUI>().Init(LibraryMode.EventDiscard);
                break;
            case 304: //���ݴ��� ��
                
                int minusHp = (int)(PlayerData.Instance.MaxHp * 0.2f); //HP ����: ���� HP���� MaxHp�� 2�� ����
                PlayerData.Instance.CurrentHp -= minusHp;

                if(PlayerData.Instance.CurrentHp <= 0)
                {
                    UIManager.Instance.ShowUI("GameOverUI"); //ü�� 0�̸� ���� ����
                }
                else
                {
                    UIManager.Instance.ShowUI("ConfirmUI").GetComponent<ConfirmUI>().Init($"���ݴ��� ü����\n {minusHp} ��ŭ �����ߴ�!"); //�ƴϸ� ü�� �پ��ٰ� �˸�
                }

                break;
            case 305: //ü�� ���� ��

                int plusHp = (int)Mathf.Min(
                    PlayerData.Instance.MaxHp * 0.2f,
                    PlayerData.Instance.MaxHp - PlayerData.Instance.CurrentHp 
                ); //HP ����: ���� HP���� MaxHp�� 2�� ȸ��
                
                PlayerData.Instance.CurrentHp += plusHp;
                UIManager.Instance.ShowUI("ConfirmUI").GetComponent<ConfirmUI>().Init($"��û�ڵ�� ��� �� ���п�\n ü���� {plusHp} ��ŭ ȸ���ߴ�!");
                break;
            case 306: //�� ���� ��

                int deltaMoney;
                float random = Random.Range(0f, 1f);

                if (random < 0.63f)
                {
                    deltaMoney = 100;
                }
                else if (random < 0.95f)
                {
                    deltaMoney = 150;
                }
                else
                {
                    deltaMoney = 300;
                }

                PlayerData.Instance.Money += deltaMoney; //�� ����
                UIManager.Instance.ShowUI("ConfirmUI").GetComponent<ConfirmUI>().Init($"��û�ڵ���\n ���� {deltaMoney}����ŭ �����ߴ�!"); 
                break;
            case 307: //�̺�Ʈ ���� 1
            case 308: //�̺�Ʈ ���� 2
            case 309: //�̺�Ʈ ���� 3
                UIManager.Instance.ShowUI("CardSelectUI").GetComponent<CardSelectUI>().PartnerReward();
                break;
        }
    }
}
