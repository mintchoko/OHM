using System;
using UnityEngine;

public class RoomSymbol : MonoBehaviour
{
    protected int index; //기본적으로 띄울 대화문과, 심볼의 동작 등을 결정하는 인덱스
    protected Define.EventType type;

    public void Init(int index, Define.EventType type)
    {
        this.index = index;
        this.type = type;
    }

    //플레이어가 이벤트 심볼에 말을 걸었을 때
    public virtual void TalkStart()
    {
        //대화창 UI를 열고 UI가 닫혔을 때, 이 심볼의 TalkEnd 함수가 수행됨 (콜백)
        UIManager.Instance.ShowUI("DialogUI")
            .GetComponent<DialogUI>()
            .Init(index, TalkEnd);
    }

    //이벤트 심볼의 대화 이벤트가 끝났을 때 (보통 대화창을 닫았을 때) 호출됨
    public virtual void TalkEnd()
    {
        AssetLoader.Instance.Destroy(gameObject);
    }
}
