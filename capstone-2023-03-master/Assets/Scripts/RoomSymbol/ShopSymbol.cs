using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSymbol : RoomSymbol
{
    public override void TalkStart()
    {
        base.TalkStart();
    }

    public override void TalkEnd()
    {
        UIManager.Instance.ShowUI("ShopTitleUI");
    }
}
