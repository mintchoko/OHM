using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private Image childImage;
    [SerializeField] 
    Transform DragParent;
    public Transform StartParent;
    public Vector3 DefaultPos;
    public static GameObject Player;

    // Start is called before the first frame update
    public void Start()
    {
        
    }

    // Update is called once per frame

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        Player = gameObject;
        StartParent = transform.parent;
        transform.SetParent(DragParent);
        DefaultPos = this.transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
        this.transform.position = mousePos;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        Player = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if(transform.parent == DragParent){
            this.transform.position = DefaultPos;
            this.transform.SetParent(StartParent);
        }
    }
}