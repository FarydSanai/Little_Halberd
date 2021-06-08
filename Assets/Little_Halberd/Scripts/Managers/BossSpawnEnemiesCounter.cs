using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class BossSpawnEnemiesCounter : Singleton<BossSpawnEnemiesCounter>
    {
        public int MaxEnemiesNumber;
        public int CurrentEnemiesNumber;
        private void Awake()
        {
            MaxEnemiesNumber = 3;
        }
        public bool AllowSpawnEnemy()
        {
            if (CurrentEnemiesNumber < MaxEnemiesNumber)
            {
                return true;
            }
            return false;
        }
    }
}