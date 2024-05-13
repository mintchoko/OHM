using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{
    GameObject Character()
    {
        if(transform.childCount > 1){
            return transform.GetChild(1).gameObject;
        }
        else{
            return null;
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(Character() == null){
            Draggable.Player.transform.SetParent(transform);
            Draggable.Player.transform.position = transform.position;
        }
    }
    
}
