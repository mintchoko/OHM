using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestSymbol : RoomSymbol
{
    public override void TalkStart()
    {
        base.TalkStart();
    }

    public override void TalkEnd()
    {
        base.TalkEnd();
        PlayerData.Instance.CurrentHp = (int)Mathf.Min(
            PlayerData.Instance.CurrentHp + (PlayerData.Instance.MaxHp * 0.25f),
            PlayerData.Instance.MaxHp
        ); //HP 회복: 현재 HP에서 MaxHp의 2.5할 회복
    }
}
