using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataStruct
{
    [System.Serializable]
    public class Knight1
    {
        public int MaxHP = 150;
        public int CurrentHp;
        public int damage = 20;
        public int[] loc = new int[2];
    }
    [System.Serializable]
    public class Knight2
    {
        public int MaxHP = 120;
        public int CurrentHp;
        public int damage = 15;
        public int[] loc = new int[2];
    }
    [System.Serializable]
    public class Knight3
    {
        public int MaxHP = 80;
        public int CurrentHp;
        public int damage = 10;
        public int[] loc = new int[2];
    }
}
