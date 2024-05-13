using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataStruct;

public class BattleData : Singleton<BattleData>
{
    public Knight1 Player_Knight1 = new Knight1();
    public Knight2 Player_Knight2 = new Knight2();
    public Knight3 Player_Knight3 = new Knight3();
    public Knight1 Enemy_Knight1 = new Knight1();
    public Knight2 Enemy_Knight2 = new Knight2();
    public Knight3 Enemy_Knight3 = new Knight3();
    public bool[] PlayerAlive = new bool[3];
    public bool[] EnemyAlive = new bool[3];
    public bool[] AllAlive = new bool[6];
    public int[,] AllLoc = new int[6,2];
    public int turn;
    // Start is called before the first frame update
    public bool result; //winner
    public bool end; //battle end?
    
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

        Player_Knight1.CurrentHp = Player_Knight1.MaxHP;
        Player_Knight2.CurrentHp = Player_Knight2.MaxHP;
        Player_Knight3.CurrentHp = Player_Knight3.MaxHP;
        Enemy_Knight1.CurrentHp = Enemy_Knight1.MaxHP;
        Enemy_Knight2.CurrentHp = Enemy_Knight2.MaxHP;
        Enemy_Knight3.CurrentHp = Enemy_Knight3.MaxHP;
        
        for(int i = 0; i < PlayerAlive.Length; i++){
            PlayerAlive[i] = true;
            EnemyAlive[i] = true;
        }
        for(int i = 0; i < AllAlive.Length; i++){
            AllAlive[i] = true;
        }
        for(int i = 0; i < AllLoc.GetLength(0);i++){
            for(int j = 0; j < AllLoc.GetLength(1);j++){
                AllLoc[i, j] = -1;
            }
        }
        result = false;
        end = false;

        turn = 1;
    }

    // Update is called once per frame
    public void Reset()
    {
        Player_Knight1 = new Knight1();
        Player_Knight2 = new Knight2();
        Player_Knight3 = new Knight3();
        Enemy_Knight1 = new Knight1();
        Enemy_Knight2 = new Knight2();
        Enemy_Knight3 = new Knight3();

        Player_Knight1.CurrentHp = Player_Knight1.MaxHP;
        Player_Knight2.CurrentHp = Player_Knight2.MaxHP;
        Player_Knight3.CurrentHp = Player_Knight3.MaxHP;
        Enemy_Knight1.CurrentHp = Enemy_Knight1.MaxHP;
        Enemy_Knight2.CurrentHp = Enemy_Knight2.MaxHP;
        Enemy_Knight3.CurrentHp = Enemy_Knight3.MaxHP;

        for(int i = 0; i < PlayerAlive.Length; i++){
            PlayerAlive[i] = true;
            EnemyAlive[i] = true;
        }

        for(int i = 0; i < AllAlive.Length; i++){
            AllAlive[i] = true;
        }
        for(int i = 0; i < AllLoc.GetLength(0);i++){
            for(int j = 0; j < AllLoc.GetLength(1);j++){
                AllLoc[i, j] = -1;
            }
        }
        result = false;
        end = false;
    }
}
