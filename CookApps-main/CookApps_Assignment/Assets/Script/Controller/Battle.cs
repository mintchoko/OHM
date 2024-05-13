using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour
{
    public static void battleAll(){
        bool at;
        for(int i = 0; i < 6; i++){
            at = false;
            if(i != 2 && i != 5){
                for(int dx = -1; dx <= 1; dx++){
                    for(int dy = -1; dy <= 1; dy++){
                        if(dx == 0 && dy == 0){
                            continue;
                        }
                        int nX = BattleData.Instance.AllLoc[i, 0] + dx;
                        int nY = BattleData.Instance.AllLoc[i, 1] + dy;
                        
                        if(nX >= 0 && nX < 5 && nY >= 0 && nY < 10){
                            if(i < 3){
                                for(int j = 3; j < 6; j++){
                                    if(BattleData.Instance.AllLoc[j, 0] == nX && BattleData.Instance.AllLoc[j, 1] == nY){
                                        at = true;
                                        attack(i, j, nX, nY);
                                        break;
                                    }
                                }
                            }
                            else{
                                for(int j = 0; j < 3; j++){
                                    
                                    if(BattleData.Instance.AllLoc[j, 0] == nX && BattleData.Instance.AllLoc[j, 1] == nY){
                                        at = true;
                                        attack(i, j, nX, nY);
                                        break;
                                    }
                                }
                            }
                        }

                        if (at){
                            break;
                        }
                    }
                    if(at){
                        break;
                    }
                }
            }
            else{
                int range = 3;
                for(int dx = -range; dx <= range; dx++){
                    for(int dy = -range; dy <= range; dy++){
                        if(dx == 0 && dy == 0){
                            continue;
                        }
                        if (Mathf.Abs(dx) + Mathf.Abs(dy) <= range){
                            int nX = BattleData.Instance.AllLoc[i, 0] + dx;
                            int nY = BattleData.Instance.AllLoc[i, 1] + dy;
                            if(nX >= 0 && nX < 5 && nY >= 0 && nY < 10){
                                if(i == 2){
                                    for(int j = 3; j < 6; j++){
                                        if(BattleData.Instance.AllLoc[j, 0] == nX && BattleData.Instance.AllLoc[j, 1] == nY){
                                            at = true;
                                            attack(i, j, nX, nY);
                                            break;
                                        }
                                    }
                                }
                                else{
                                    for(int j = 0; j < 3; j++){
                                    
                                        if(BattleData.Instance.AllLoc[j, 0] == nX && BattleData.Instance.AllLoc[j, 1] == nY){
                                            at = true;
                                            attack(i, j, nX, nY);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        

                        
                        if(at){
                            break;
                        }
                    }
                    if(at){
                        break;
                    }
                }
            }

            if(!at){
                move(i);
            }
        }
        CheckAlive();

    }

    static void attack(int i, int enemy, int x, int y){
        Debug.Log(i + "attack " + enemy);
        int d;
        switch(i){
            case 0:
                d = BattleData.Instance.Player_Knight1.damage;
                if(enemy == 3){
                    BattleData.Instance.Enemy_Knight1.CurrentHp -= d;
                }
                else if(enemy == 4){
                    BattleData.Instance.Enemy_Knight2.CurrentHp -= d;
                }
                else{
                    BattleData.Instance.Enemy_Knight3.CurrentHp -= d;
                }
                break;

            case 1:
                d = BattleData.Instance.Player_Knight2.damage;
                for(int dx = -1; dx <= 1; dx++){
                    for(int dy = -1; dy <= 1; dy++){
                        if(dx == 0 && dy == 0){
                            continue;
                        }
                        int nX = BattleData.Instance.AllLoc[1, 0] + dx;
                        int nY = BattleData.Instance.AllLoc[1, 1] + dy;
                        
                        if(nX >= 0 && nX < 5 && nY >= 0 && nY < 10){
                            if(BattleData.Instance.AllLoc[3, 0] == nX && BattleData.Instance.AllLoc[3, 1] == nY){
                                BattleData.Instance.Enemy_Knight1.CurrentHp -= d;
                            }
                            if(BattleData.Instance.AllLoc[4, 0] == nX && BattleData.Instance.AllLoc[4, 1] == nY){
                                BattleData.Instance.Enemy_Knight2.CurrentHp -= d;
                            }
                            if(BattleData.Instance.AllLoc[5, 0] == nX && BattleData.Instance.AllLoc[5, 1] == nY){
                                BattleData.Instance.Enemy_Knight3.CurrentHp -= d;
                            }
                        }
                    }
                }
                break;

            case 2:
                d = BattleData.Instance.Player_Knight3.damage;
                if(enemy == 3){
                    BattleData.Instance.Enemy_Knight1.CurrentHp -= d;
                }
                else if(enemy == 4){
                    BattleData.Instance.Enemy_Knight2.CurrentHp -= d;
                }
                else{
                    BattleData.Instance.Enemy_Knight3.CurrentHp -= d;
                }
                break;

            case 3:
                d = BattleData.Instance.Enemy_Knight1.damage;
                if(enemy == 0){
                    BattleData.Instance.Player_Knight1.CurrentHp -= d;
                }
                else if(enemy == 1){
                    BattleData.Instance.Player_Knight2.CurrentHp -= d;
                }
                else{
                    BattleData.Instance.Player_Knight3.CurrentHp -= d;
                }
                break;

            case 4:
                d = BattleData.Instance.Enemy_Knight2.damage;
                for(int dx = -1; dx <= 1; dx++){
                    for(int dy = -1; dy <= 1; dy++){
                        if(dx == 0 && dy == 0){
                            continue;
                        }
                        int nX = BattleData.Instance.AllLoc[4, 0] + dx;
                        int nY = BattleData.Instance.AllLoc[4, 1] + dy;
                        
                        if(nX >= 0 && nX < 5 && nY >= 0 && nY < 10){
                            if(BattleData.Instance.AllLoc[0, 0] == nX && BattleData.Instance.AllLoc[0, 1] == nY){
                                BattleData.Instance.Player_Knight1.CurrentHp -= d;
                            }
                            if(BattleData.Instance.AllLoc[1, 0] == nX && BattleData.Instance.AllLoc[1, 1] == nY){
                                BattleData.Instance.Player_Knight2.CurrentHp -= d;
                            }
                            if(BattleData.Instance.AllLoc[2, 0] == nX && BattleData.Instance.AllLoc[2, 1] == nY){
                                BattleData.Instance.Player_Knight3.CurrentHp -= d;
                            }
                        }
                    }
                }
                break;

            case 5:
                d = BattleData.Instance.Enemy_Knight3.damage;
                if(enemy == 0){
                    BattleData.Instance.Player_Knight1.CurrentHp -= d;
                }
                else if(enemy == 1){
                    BattleData.Instance.Player_Knight2.CurrentHp -= d;
                }
                else{
                    BattleData.Instance.Player_Knight3.CurrentHp -= d;
                }
                break;
        }
    }
    static void move(int p){
        Debug.Log("move " + p);
        int target = -1;
        int x, y;
        int min = 100;
        switch(p){
            case 0:
                x = BattleData.Instance.AllLoc[0, 0];
                y = BattleData.Instance.AllLoc[0, 1];
                for(int i = 3; i < 6; i++){
                    int tx = BattleData.Instance.AllLoc[i, 0];
                    int ty = BattleData.Instance.AllLoc[i, 1];
                    if(tx == -1){
                        continue;
                    }
                    if (Mathf.Abs(x - tx) + Mathf.Abs(x - y) <= min){
                        min = Mathf.Abs(x - tx) + Mathf.Abs(x - y);
                        target = i;
                    }
                }

                if(BattleData.Instance.AllLoc[target, 0] > x){
                    if(!((BattleData.Instance.AllLoc[1,0] == x + 1 && BattleData.Instance.AllLoc[1,1] == y) || (BattleData.Instance.AllLoc[2,0] == x + 1 && BattleData.Instance.AllLoc[2,1] == y))){
                        BattleData.Instance.AllLoc[0, 0] += 1;
                        BattleData.Instance.Player_Knight1.loc[0] += 1;
                    }
                    
                }
                else if(BattleData.Instance.AllLoc[target, 0] < x){
                    if(!((BattleData.Instance.AllLoc[1,0] == x - 1 && BattleData.Instance.AllLoc[1,1] == y) || (BattleData.Instance.AllLoc[2,0] == x - 1 && BattleData.Instance.AllLoc[2,1] == y))){
                        BattleData.Instance.AllLoc[0, 0] -= 1;
                        BattleData.Instance.Player_Knight1.loc[0] -= 1;
                    }
                    
                }
                else{
                    if(BattleData.Instance.AllLoc[target, 1] > y){
                        if(!((BattleData.Instance.AllLoc[1,0] == x && BattleData.Instance.AllLoc[1,1] == y + 1) || (BattleData.Instance.AllLoc[2,0] == x && BattleData.Instance.AllLoc[2,1] == y + 1))){
                            BattleData.Instance.AllLoc[0, 1] += 1;
                            BattleData.Instance.Player_Knight1.loc[1] += 1;
                        }
                    }
                    else{
                        if(!((BattleData.Instance.AllLoc[1,0] == x && BattleData.Instance.AllLoc[1,1] == y - 1) || (BattleData.Instance.AllLoc[2,0] == x && BattleData.Instance.AllLoc[2,1] == y - 1))){
                            BattleData.Instance.AllLoc[0, 1] -= 1;
                            BattleData.Instance.Player_Knight1.loc[1] -= 1;
                        }
                        
                    }
                }
                break;
            case 1:
                x = BattleData.Instance.AllLoc[1, 0];
                y = BattleData.Instance.AllLoc[1, 1];
                for(int i = 3; i < 6; i++){
                    int tx = BattleData.Instance.AllLoc[i, 0];
                    int ty = BattleData.Instance.AllLoc[i, 1];
                    if(tx == -1){
                        continue;
                    }
                    if (Mathf.Abs(x - tx) + Mathf.Abs(x - y) <= min){
                        min = Mathf.Abs(x - tx) + Mathf.Abs(x - y);
                        target = i;
                    }
                }
                if(BattleData.Instance.AllLoc[target, 0] > x){
                    if(!((BattleData.Instance.AllLoc[0,0] == x + 1 && BattleData.Instance.AllLoc[0,1]== y) || (BattleData.Instance.AllLoc[2,0] == x + 1 && BattleData.Instance.AllLoc[2,1] == y))){
                        BattleData.Instance.AllLoc[1, 0] += 1;
                        BattleData.Instance.Player_Knight2.loc[0] += 1;
                    }
                    
                }
                else if(BattleData.Instance.AllLoc[target, 0] < x){
                    if(!((BattleData.Instance.AllLoc[0,0]== x - 1 && BattleData.Instance.AllLoc[0,1] == y) || (BattleData.Instance.AllLoc[2,0] == x - 1 && BattleData.Instance.AllLoc[2,1] == y))){
                        BattleData.Instance.AllLoc[1, 0] -= 1;
                        BattleData.Instance.Player_Knight2.loc[0] -= 1;
                    }
                    
                }
                else{
                    if(BattleData.Instance.AllLoc[target, 1] > y){
                        if(!((BattleData.Instance.AllLoc[0,0] == x && BattleData.Instance.AllLoc[0,1] == y + 1) || (BattleData.Instance.AllLoc[2,0] == x && BattleData.Instance.AllLoc[2,1] == y + 1))){
                            BattleData.Instance.AllLoc[1, 1] += 1;
                            BattleData.Instance.Player_Knight2.loc[1] += 1;
                        }
                    }
                    else{
                        if(!((BattleData.Instance.AllLoc[0,0] == x && BattleData.Instance.AllLoc[0,1] == y - 1) || (BattleData.Instance.AllLoc[2,0] == x && BattleData.Instance.AllLoc[2,1] == y - 1))){
                            BattleData.Instance.AllLoc[1, 1] -= 1;
                            BattleData.Instance.Player_Knight2.loc[1] -= 1;
                        }
                        
                    }
                }
                break;
            case 2:
                x = BattleData.Instance.AllLoc[2, 0];
                y = BattleData.Instance.AllLoc[2, 1];
                for(int i = 3; i < 6; i++){
                    int tx = BattleData.Instance.AllLoc[i, 0];
                    int ty = BattleData.Instance.AllLoc[i, 1];
                    if(tx == -1){
                        continue;
                    }
                    if (Mathf.Abs(x - tx) + Mathf.Abs(x - y) <= min){
                        min = Mathf.Abs(x - tx) + Mathf.Abs(x - y);
                        target = i;
                    }
                }
                if(BattleData.Instance.AllLoc[target, 0] > x){
                    if(!((BattleData.Instance.AllLoc[0,0] == x + 1 && BattleData.Instance.AllLoc[0,1] == y) || (BattleData.Instance.AllLoc[1,0]== x + 1 && BattleData.Instance.AllLoc[1,1] == y))){
                        BattleData.Instance.AllLoc[2, 0] += 1;
                        BattleData.Instance.Player_Knight3.loc[0] += 1;
                    }
                    
                }
                else if(BattleData.Instance.AllLoc[target, 0] < x){
                    if(!((BattleData.Instance.AllLoc[0,0] == x - 1 && BattleData.Instance.AllLoc[0,1] == y) || (BattleData.Instance.AllLoc[1,0]== x - 1 && BattleData.Instance.AllLoc[1,1] == y))){
                        BattleData.Instance.AllLoc[2, 0] -= 1;
                        BattleData.Instance.Player_Knight3.loc[0] -= 1;
                    }
                    
                }
                else{
                    if(BattleData.Instance.AllLoc[target, 1] > y){
                        if(!((BattleData.Instance.AllLoc[0,0] == x && BattleData.Instance.AllLoc[0,1] == y + 1) || (BattleData.Instance.AllLoc[1,0] == x && BattleData.Instance.AllLoc[1,1] == y + 1))){
                            BattleData.Instance.AllLoc[2, 1] += 1;
                            BattleData.Instance.Player_Knight3.loc[1] += 1;
                        }
                    }
                    else{
                        if(!((BattleData.Instance.AllLoc[0,0] == x && BattleData.Instance.AllLoc[0,1]== y - 1) || (BattleData.Instance.AllLoc[1,0] == x && BattleData.Instance.AllLoc[1,1] == y - 1))){
                            BattleData.Instance.AllLoc[2, 1] -= 1;
                            BattleData.Instance.Player_Knight3.loc[1] -= 1;
                        }
                        
                    }
                }
                break;

            case 3:
                x = BattleData.Instance.AllLoc[3, 0];
                y = BattleData.Instance.AllLoc[3, 1];
                for(int i = 0; i < 3; i++){
                    int tx = BattleData.Instance.AllLoc[i, 0];
                    int ty = BattleData.Instance.AllLoc[i, 1];
                    if(tx == -1){
                        continue;
                    }
                    if (Mathf.Abs(x - tx) + Mathf.Abs(x - y) <= min){
                        min = Mathf.Abs(x - tx) + Mathf.Abs(x - y);
                        target = i;
                    }
                }
                if(BattleData.Instance.AllLoc[target, 0] > x){
                    if(!((BattleData.Instance.AllLoc[4,0] == x + 1 && BattleData.Instance.AllLoc[4,1] == y) || (BattleData.Instance.AllLoc[5,0] == x + 1 && BattleData.Instance.AllLoc[5,1] == y))){
                        BattleData.Instance.AllLoc[3, 0] += 1;
                        BattleData.Instance.Enemy_Knight1.loc[0] += 1;
                    }
                    
                }
                else if(BattleData.Instance.AllLoc[target, 0] < x){
                    if(!((BattleData.Instance.AllLoc[4,0] == x - 1 && BattleData.Instance.AllLoc[4,1] == y) || (BattleData.Instance.AllLoc[5,0] == x - 1 && BattleData.Instance.AllLoc[5,1] == y))){
                        BattleData.Instance.AllLoc[3, 0] -= 1;
                        BattleData.Instance.Enemy_Knight1.loc[0] -= 1;
                    }
                    
                }
                else{
                    if(BattleData.Instance.AllLoc[target, 1] > y){
                        if(!((BattleData.Instance.AllLoc[4,0] == x && BattleData.Instance.AllLoc[4,1] == y + 1) || (BattleData.Instance.AllLoc[5,0] == x && BattleData.Instance.AllLoc[5,1] == y + 1))){
                            BattleData.Instance.AllLoc[3, 1] += 1;
                            BattleData.Instance.Enemy_Knight1.loc[1] += 1;
                        }
                    }
                    else{
                        if(!((BattleData.Instance.AllLoc[4,0] == x && BattleData.Instance.AllLoc[4,1] == y - 1) || (BattleData.Instance.AllLoc[5,0] == x && BattleData.Instance.AllLoc[5,1] == y - 1))){
                            BattleData.Instance.AllLoc[3, 1] -= 1;
                            BattleData.Instance.Enemy_Knight1.loc[1] -= 1;
                        }
                        
                    }
                }
                break;
            case 4:
                x = BattleData.Instance.AllLoc[4, 0];
                y = BattleData.Instance.AllLoc[4, 1];
                for(int i = 0; i < 3; i++){
                    int tx = BattleData.Instance.AllLoc[i, 0];
                    int ty = BattleData.Instance.AllLoc[i, 1];
                    if(tx == -1){
                        continue;
                    }
                    if (Mathf.Abs(x - tx) + Mathf.Abs(x - y) <= min){
                        min = Mathf.Abs(x - tx) + Mathf.Abs(x - y);
                        target = i;
                    }
                }
                if(BattleData.Instance.AllLoc[target, 0] > x){
                    if(!((BattleData.Instance.AllLoc[3,0] == x + 1 && BattleData.Instance.AllLoc[3,1] == y) || (BattleData.Instance.AllLoc[5,0] == x + 1 && BattleData.Instance.AllLoc[5,1] == y))){
                        BattleData.Instance.AllLoc[4, 0] += 1;
                        BattleData.Instance.Enemy_Knight2.loc[0] += 1;
                    }
                    
                }
                else if(BattleData.Instance.AllLoc[target, 0] < x){
                    if(!((BattleData.Instance.AllLoc[3,0] == x - 1 && BattleData.Instance.AllLoc[3,1] == y) || (BattleData.Instance.AllLoc[5,0] == x - 1 && BattleData.Instance.AllLoc[5,1] == y))){
                        BattleData.Instance.AllLoc[4, 0] -= 1;
                        BattleData.Instance.Enemy_Knight2.loc[0] -= 1;
                    }
                    
                }
                else{
                    if(BattleData.Instance.AllLoc[target, 1] > y){
                        if(!((BattleData.Instance.AllLoc[3,0] == x && BattleData.Instance.AllLoc[3,1] == y + 1) || (BattleData.Instance.AllLoc[5,0] == x && BattleData.Instance.AllLoc[5,1] == y + 1))){
                            BattleData.Instance.AllLoc[4, 1] += 1;
                            BattleData.Instance.Enemy_Knight2.loc[1] += 1;
                        }
                    }
                    else{
                        if(!((BattleData.Instance.AllLoc[3,0] == x && BattleData.Instance.AllLoc[3,1] == y - 1) || (BattleData.Instance.AllLoc[5,0] == x && BattleData.Instance.AllLoc[5,1] == y - 1))){
                            BattleData.Instance.AllLoc[4, 1] -= 1;
                            BattleData.Instance.Enemy_Knight2.loc[1] -= 1;
                        }
                        
                    }
                }
                
                break;
            case 5:
                x = BattleData.Instance.AllLoc[5, 0];
                y = BattleData.Instance.AllLoc[5, 1];
                for(int i = 0; i < 3; i++){
                    int tx = BattleData.Instance.AllLoc[i, 0];
                    int ty = BattleData.Instance.AllLoc[i, 1];
                    if(tx == -1){
                        continue;
                    }
                    if (Mathf.Abs(x - tx) + Mathf.Abs(x - y) <= min){
                        min = Mathf.Abs(x - tx) + Mathf.Abs(x - y);
                        target = i;
                    }
                }
                if(BattleData.Instance.AllLoc[target, 0] > x){
                    if(!((BattleData.Instance.AllLoc[3,0] == x + 1 && BattleData.Instance.AllLoc[3,1] == y) || (BattleData.Instance.AllLoc[4,0] == x + 1 && BattleData.Instance.AllLoc[4,1] == y))){
                        BattleData.Instance.AllLoc[5, 0] += 1;
                        BattleData.Instance.Enemy_Knight3.loc[0] += 1;
                    }
                    
                }
                else if(BattleData.Instance.AllLoc[target, 0] < x){
                    if(!((BattleData.Instance.AllLoc[3,0] == x - 1 && BattleData.Instance.AllLoc[3,1] == y) || (BattleData.Instance.AllLoc[4,0] == x - 1 && BattleData.Instance.AllLoc[4,1] == y))){
                        BattleData.Instance.AllLoc[5, 0] -= 1;
                        BattleData.Instance.Enemy_Knight3.loc[0] -= 1;
                    }
                    
                }
                else{
                    if(BattleData.Instance.AllLoc[target, 1] > y){
                        if(!((BattleData.Instance.AllLoc[3,0] == x && BattleData.Instance.AllLoc[3,1] == y + 1) || (BattleData.Instance.AllLoc[4,0] == x && BattleData.Instance.AllLoc[4,1] == y + 1))){
                            BattleData.Instance.AllLoc[5, 1] += 1;
                            BattleData.Instance.Enemy_Knight3.loc[1] += 1;
                        }
                    }
                    else{
                        if(!((BattleData.Instance.AllLoc[3,0] == x && BattleData.Instance.AllLoc[3,1] == y - 1) || (BattleData.Instance.AllLoc[4,0] == x && BattleData.Instance.AllLoc[4,1] == y - 1))){
                            BattleData.Instance.AllLoc[5, 1] -= 1;
                            BattleData.Instance.Enemy_Knight3.loc[1] -= 1;
                        }
                        
                    }
                }
                
                break;
        }
    }

    static void CheckAlive()
    {
        if(BattleData.Instance.Player_Knight1.CurrentHp <= 0){
            BattleData.Instance.PlayerAlive[0] = false;
            BattleData.Instance.AllAlive[0] = false;
        }
        if(BattleData.Instance.Player_Knight2.CurrentHp <= 0){
            BattleData.Instance.PlayerAlive[1] = false;
            BattleData.Instance.AllAlive[1] = false;
        }
        if(BattleData.Instance.Player_Knight3.CurrentHp <= 0){
            BattleData.Instance.PlayerAlive[2] = false;
            BattleData.Instance.AllAlive[2] = false;
        }
        if(BattleData.Instance.Enemy_Knight1.CurrentHp <= 0){
            BattleData.Instance.EnemyAlive[0] = false;
            BattleData.Instance.AllAlive[3] = false;
        }
        if(BattleData.Instance.Enemy_Knight2.CurrentHp <= 0){
            BattleData.Instance.EnemyAlive[1] = false;
            BattleData.Instance.AllAlive[4] = false;
        }
        if(BattleData.Instance.Enemy_Knight3.CurrentHp <= 0){
            BattleData.Instance.EnemyAlive[2] = false;
            BattleData.Instance.AllAlive[5] = false;
        }

        bool AllDie = true;

        for(int i = 0; i < 3; i++){
            if(BattleData.Instance.EnemyAlive[i]){
                AllDie = false;
                break;
            }
        }

        if(AllDie){
            BattleData.Instance.end = true;
            BattleData.Instance.result = true;
        }

        if(!AllDie){
            AllDie = true;
            for(int i = 0; i < 3; i++){
                if(BattleData.Instance.PlayerAlive[i]){
                    AllDie = false;
                    break;
                }
            }

            if(AllDie){
                BattleData.Instance.end = true;
                BattleData.Instance.result = false;
            }
        }
    }
}
