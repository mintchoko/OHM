using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BoardUI : MonoBehaviour
{
    [SerializeField]
    private GameObject ResultUI;
    [SerializeField]
    private Button StartButton;
    [SerializeField]
    private Text Turn_Text;
    [SerializeField]
    private TextMeshProUGUI Second_Text;
    [SerializeField]
    private GameObject Player_Knight1;
    [SerializeField]
    private GameObject Player_Knight2;
    [SerializeField]
    private GameObject Player_Knight3;
    [SerializeField]
    private GameObject Enemy_Knight1;
    [SerializeField]
    private GameObject Enemy_Knight2;
    [SerializeField]
    private GameObject Enemy_Knight3;
    private GameObject[] Enemys;
    [SerializeField]
    private GameObject Board;
    public GameObject[] slots;
    public bool isRunning = false;
    public float time = 0f;
    public int sec = 60;
    // Start is called before the first frame update
    void Start()
    {
        Enemys = new GameObject[] {Enemy_Knight1,Enemy_Knight2,Enemy_Knight3};
        InitializeSlots();
        arrange();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeTurn();
        ChangeLoc();
        EndBattle();
        if(isRunning){
            time += Time.deltaTime;
            if(time >= 60f){
                StopBattle();
            }
        }
    }
    public void ChangeSecondText(){
        sec--;
        Second_Text.text = "" + sec;
    }
    public void EndBattle(){
        if(BattleData.Instance.end){
            StopBattle();
        }
    }
    public void ChangeLoc()
    {
        for(int i = 0; i < slots.Length;i++){
            GameObject slot = slots[i];
            if(slot.transform.childCount > 1){
                Transform child = slot.transform.GetChild(1);
                string childTag = child.gameObject.tag;
                int x = i / 10;
                int y = i % 10;
                switch(childTag)
                {
                    case "Player_1":
                        if(BattleData.Instance.AllAlive[0]){
                            BattleData.Instance.Player_Knight1.loc[0] = x;
                            BattleData.Instance.Player_Knight1.loc[1] = y;
                            BattleData.Instance.AllLoc[0, 0] = x;
                            BattleData.Instance.AllLoc[0, 1] = y;
                        }
                        break;
                    case "Player_2":
                        if(BattleData.Instance.AllAlive[1]){
                            BattleData.Instance.Player_Knight2.loc[0] = x;
                            BattleData.Instance.Player_Knight2.loc[1] = y;
                            BattleData.Instance.AllLoc[1, 0] = x;
                            BattleData.Instance.AllLoc[1, 1] = y;
                        }
                        break;
                    case "Player_3":
                        if(BattleData.Instance.AllAlive[2]){
                            BattleData.Instance.Player_Knight3.loc[0] = x;
                            BattleData.Instance.Player_Knight3.loc[1] = y;
                            BattleData.Instance.AllLoc[2, 0] = x;
                            BattleData.Instance.AllLoc[2, 1] = y;
                        }
                        break;
                    case "Enemy_1":
                        if(BattleData.Instance.AllAlive[3]){
                            BattleData.Instance.Enemy_Knight1.loc[0] = x;
                            BattleData.Instance.Enemy_Knight1.loc[1] = y;
                            BattleData.Instance.AllLoc[3, 0] = x;
                            BattleData.Instance.AllLoc[3, 1] = y;
                        }
                        break;
                    case "Enemy_2":                       
                        if(BattleData.Instance.AllAlive[4]){
                            BattleData.Instance.Enemy_Knight2.loc[0] = x;
                            BattleData.Instance.Enemy_Knight2.loc[1] = y;
                            BattleData.Instance.AllLoc[4, 0] = x;
                            BattleData.Instance.AllLoc[4, 1] = y;
                        }
                        break;
                    case "Enemy_3":
                        if(BattleData.Instance.AllAlive[5]){
                            BattleData.Instance.Enemy_Knight3.loc[0] = x;
                            BattleData.Instance.Enemy_Knight3.loc[1] = y;
                            BattleData.Instance.AllLoc[5, 0] = x;
                            BattleData.Instance.AllLoc[5, 1] = y;
                        }
                        break;
                }
            }
        }
    }
    public void ChangeTurn(){
        int turns = BattleData.Instance.turn;
        string t = $"Turn {turns}";
        Turn_Text.text = t;
    }
    public void StartClick()
    {   
        StartButton.gameObject.SetActive(false);
        isRunning = true;
        InvokeRepeating("CallBattle", 0f, 1f);
        
    }
    public void CallBattle(){
        ChangeSecondText();
        Battle.battleAll();
        die();
        moving();
    }
    public void StopBattle(){
        CancelInvoke("CallBattle");
        isRunning = false;
        time = 0;
        Second_Text.text = "";
        sec = 60;
        ResultUI.SetActive(true);
    }

    public void InitializeSlots()
    {
        slots = new GameObject[Board.transform.childCount];
        for(int i = 0; i < Board.transform.childCount; i++){
            slots[i] = Board.transform.GetChild(i).gameObject;
        }
    }
    public void arrange()
    {
        Player_Knight1.SetActive(true);
        Player_Knight2.SetActive(true);
        Player_Knight3.SetActive(true);
        Enemy_Knight1.SetActive(true);
        Enemy_Knight2.SetActive(true);
        Enemy_Knight3.SetActive(true);

        Player_Knight1.transform.SetParent(slots[23].transform);
        Player_Knight1.transform.position = slots[23].transform.position;
        Player_Knight2.transform.SetParent(slots[21].transform);
        Player_Knight2.transform.position =slots[21].transform.position;
        Player_Knight3.transform.SetParent(slots[22].transform);
        Player_Knight3.transform.position = slots[22].transform.position;

        int numEnemy = Enemys.Length;

        for(int i = 0; i < numEnemy; i++){
            GameObject SelectEnemy = Enemys[i];
            int ran = Random.Range(0, slots.Length);
            GameObject slot = slots[ran];
            if(slot.transform.childCount > 1){
                i--;
                continue;
            }
            SelectEnemy.transform.SetParent(slot.transform);
            SelectEnemy.transform.position = slot.transform.position;
            int x = ran / 10;
            int y = ran % 10;
            switch (i)
            {
                case 0:
                    BattleData.Instance.Enemy_Knight1.loc[0] = x;
                    BattleData.Instance.Enemy_Knight1.loc[1] = y;
                    break;
                case 1:
                    BattleData.Instance.Enemy_Knight2.loc[0] = x;
                    BattleData.Instance.Enemy_Knight2.loc[1] = y;
                    break;
                case 2:
                    BattleData.Instance.Enemy_Knight3.loc[0] = x;
                    BattleData.Instance.Enemy_Knight3.loc[1] = y;
                    break;
            }
        }

        StartButton.gameObject.SetActive(true);
    }
    public void die(){
        for(int i = 0; i < 6; i++){
            if(BattleData.Instance.AllAlive[i]){
                continue;
            }
            switch(i){
                case 0:
                    BattleData.Instance.Player_Knight1.loc[0] = -1;
                    BattleData.Instance.Player_Knight1.loc[1] = -1;
                    BattleData.Instance.AllLoc[0, 0] = -1;
                    BattleData.Instance.AllLoc[0, 1] = -1;
                    Player_Knight1.SetActive(false);
                    break;
                case 1:
                    BattleData.Instance.Player_Knight2.loc[0] = -1;
                    BattleData.Instance.Player_Knight2.loc[1] = -1;
                    BattleData.Instance.AllLoc[1, 0] = -1;
                    BattleData.Instance.AllLoc[1, 1] = -1;
                    Player_Knight2.SetActive(false);
                    break;
                case 2:
                    BattleData.Instance.Player_Knight3.loc[0] = -1;
                    BattleData.Instance.Player_Knight3.loc[1] = -1;
                    BattleData.Instance.AllLoc[2, 0] = -1;
                    BattleData.Instance.AllLoc[2, 1] = -1;
                    Player_Knight3.SetActive(false);
                    break;
                case 3:
                    BattleData.Instance.Enemy_Knight1.loc[0] = -1;
                    BattleData.Instance.Enemy_Knight1.loc[1] = -1;
                    BattleData.Instance.AllLoc[3, 0] = -1;
                    BattleData.Instance.AllLoc[3, 1] = -1;
                    Enemy_Knight1.SetActive(false);
                    break;
                case 4:
                    BattleData.Instance.Enemy_Knight2.loc[0] = -1;
                    BattleData.Instance.Enemy_Knight2.loc[1] = -1;
                    BattleData.Instance.AllLoc[4, 0] = -1;
                    BattleData.Instance.AllLoc[4, 1] = -1;
                    Enemy_Knight2.SetActive(false);
                    break;
                case 5:
                    BattleData.Instance.Enemy_Knight3.loc[0] = -1;
                    BattleData.Instance.Enemy_Knight3.loc[1] = -1;
                    BattleData.Instance.AllLoc[5, 0] = -1;
                    BattleData.Instance.AllLoc[5, 1] = -1;
                    Enemy_Knight3.SetActive(false);
                    break;

            }
        }
    }
    public void moving(){
        for(int i = 0; i < 6; i++){
            if(!BattleData.Instance.AllAlive[i]){
                continue;
            }
            int s;
            switch(i){
                case 0:
                    s = BattleData.Instance.Player_Knight1.loc[0] * 10 + BattleData.Instance.Player_Knight1.loc[1];
                    Player_Knight1.transform.SetParent(slots[s].transform);
                    Player_Knight1.transform.position = slots[s].transform.position;
                    break;
                case 1:
                    s = BattleData.Instance.Player_Knight2.loc[0] * 10 + BattleData.Instance.Player_Knight2.loc[1];
                    Player_Knight2.transform.SetParent(slots[s].transform);
                    Player_Knight2.transform.position = slots[s].transform.position;
                    break;
                case 2:
                    s = BattleData.Instance.Player_Knight3.loc[0] * 10 + BattleData.Instance.Player_Knight3.loc[1];
                    Player_Knight3.transform.SetParent(slots[s].transform);
                    Player_Knight3.transform.position = slots[s].transform.position;
                    break;
                case 3:
                    s = BattleData.Instance.Enemy_Knight1.loc[0] * 10 + BattleData.Instance.Enemy_Knight1.loc[1];
                    Enemy_Knight1.transform.SetParent(slots[s].transform);
                    Enemy_Knight1.transform.position = slots[s].transform.position;
                    break;
                case 4:
                    s = BattleData.Instance.Enemy_Knight2.loc[0] * 10 + BattleData.Instance.Enemy_Knight2.loc[1];
                    Enemy_Knight2.transform.SetParent(slots[s].transform);
                    Enemy_Knight2.transform.position = slots[s].transform.position;
                    break;
                case 5:
                    s = BattleData.Instance.Enemy_Knight3.loc[0] * 10 + BattleData.Instance.Enemy_Knight3.loc[1];
                    Enemy_Knight3.transform.SetParent(slots[s].transform);
                    Enemy_Knight3.transform.position = slots[s].transform.position;
                    break;
            }
        }
    }
}
