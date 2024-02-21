using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item2UI : MonoBehaviour
{
    private KeyCode[] keyCodes = {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
    };

    public GameObject Slot1;
    public GameObject Slot2;
    public GameObject Slot3;
    public GameObject Slot4;
    public GameObject[] Slots;

    void Start()
    {
        Slots = new GameObject[4];

        Slots[0] = Slot1;
        Slots[1] = Slot2;
        Slots[2] = Slot3;
        Slots[3] = Slot4;
    }

    // Update is called once per frame
    void Update()
    {
        ResetOutline();
        SelectSlot(GameManager.SelectedItem);

        for(int i = 0; i <= 3;i++){
            if(Input.GetKeyDown(keyCodes[i])){
                if(GameManager.Turn){ //2P
                    if(!GameManager.UsedItem2[i]){
                        GameManager.SelectedItem = i;
                        Debug.Log(GameManager.SelectedItem);
                    }
                }
            }
            if(GameManager.Turn && GameManager.UsedItem2[i]){
                DeleteImage(i);
            }
            
        }
    }

    void SelectSlot(int slotNum){
        Outline l = Slots[slotNum].GetComponent<Outline>();
        Vector2 v = new Vector2(5.0f,5.0f);
        l.effectDistance = v;
    }

    void ResetOutline(){
        for(int i = 0; i < Slots.Length; i++){
            Outline l = Slots[i].GetComponent<Outline>();
            Vector2 v = new Vector2(1.0f,1.0f);
            l.effectDistance = v;
        }
    }

    void DeleteImage(int i){
        Transform s = Slots[i].transform.Find("Image");
        s.gameObject.SetActive(false);
    }
}
