using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSymbol : RoomSymbol
{
    public override void TalkStart()
    {
        UIManager.Instance.ShowUI("DialogUI")
            .GetComponent<DialogUI>()
            .Init(index, Fight); //��ȭ�� ������ ���� UI ����

    }

    public void Fight()
    {
        GameObject Room = transform.parent.gameObject;
        SoundManager.Instance.Play("Sounds/BattleBgm", Sound.Bgm);
        UIManager.Instance.ShowUI("BackGroundUI");
        UIManager.Instance.ShowUI("BattleUI", false).GetComponent<BattleUI>().Init(index, Room.name, StageManager.Instance.Stage);
        BattleData.Instance.LoadData();
    }

    //���� �� ��ȭ
    public void AfterFight()
    {
        //���� ���������� �׳� ����
        if (StageManager.Instance.Theme == Define.ThemeType.Final)
        {
            UIManager.Instance.ShowUI("DialogUI")
                .GetComponent<DialogUI>()
                .Init(index + Define.BOSS_AFTER_INDEX, TalkEnd);
        }
        else
        {
            UIManager.Instance.ShowUI("DialogUI")
              .GetComponent<DialogUI>()
              .Init(index + Define.BOSS_AFTER_INDEX, SelectOpen); //��ȭ�� ������ ���� ���� �˾� ���� 
        }
    }

    //���� ���� �˾� ����
    public void SelectOpen()
    {
        UIManager.Instance.ShowUI("SelectUI")
            .GetComponent<SelectUI>()
            .Init(
                "�������� ũ��� ���� ���Ǹ� �Ͻðڽ��ϱ�?",
                AfterNego, //�� ���� �� ���� ���� ȣ��
                FightEnd //�ƴϿ� ���� �� ���� ���� ȣ��
            );
    }

    public void FightEnd() //���� ������ ���� ���Ǹ� ���� ���� �� ȣ��
    {

        //������ ... ��ŭ ����
        PlayerData.Instance.Money += GameData.Instance.RewardDic[StageManager.Instance.Stage + Define.BOSS_INDEX].money; 
        PlayerData.Instance.Viewers += GameData.Instance.RewardDic[StageManager.Instance.Stage + Define.BOSS_INDEX].viewers;



        //���� ī�� UI ���� ��, TalkEnd ȣ��
        CardSelectUI cardSelectUI = UIManager.Instance.ShowUI("CardSelectUI").GetComponent<CardSelectUI>();
        cardSelectUI.SetCloseCallback(TalkEnd);
        cardSelectUI.BossBattleReward();
    }

    //���� ���� ���� �� �ļ� ��ȭ
    public void AfterNego()
    {
        UIManager.Instance.ShowUI("DialogUI")
            .GetComponent<DialogUI>()
            .Init(index + Define.BOSS_NEGO_INDEX, NegotiateEnd); //��ȭ�� ������ ���� ���� ȹ��
    }


    public void NegotiateEnd() //���� �� ȣ��
    {
        //������ ... ��ŭ ����
        PlayerData.Instance.Money += GameData.Instance.RewardDic[StageManager.Instance.Stage + Define.BOSS_INDEX].money / 2;
        PlayerData.Instance.Viewers += GameData.Instance.RewardDic[StageManager.Instance.Stage + Define.BOSS_INDEX].viewers / 2;

        //���� ī�� UI ���� ��, TalkEnd ȣ��
        CardSelectUI cardSelectUI = UIManager.Instance.ShowUI("CardSelectUI").GetComponent<CardSelectUI>();
        cardSelectUI.SetCloseCallback(TalkEnd);
        cardSelectUI.BossNegoReward();
    }

    public override void TalkEnd()
    {
        base.TalkEnd();
        if(PlayerData.Instance.CheckLevelUp())
        {
            UIManager.Instance.ShowUI("CardSelectUI")
                .GetComponent<CardSelectUI>()
                .LevelUpReward();
        }
        StageManager.Instance.LevelClear();
    }
}
