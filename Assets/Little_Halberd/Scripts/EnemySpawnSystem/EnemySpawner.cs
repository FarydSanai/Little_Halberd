using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    [System.Serializable]
    public struct PooledEnemy
    {
        public ObjectType EnemyType;
        public int Count;
    }
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private List<PooledEnemy> SpawnEnemyTypes;

        private Coroutine EnemySpawnRoutine;
        private void Start()
        {
            if (EnemySpawnRoutine != null)
            {
                StopCoroutine(EnemySpawnRoutine);
            }
            else
            {
                EnemySpawnRoutine = StartCoroutine(_EnemySpawn());
            }
        }
        private IEnumerator _EnemySpawn()
        {
            foreach (PooledEnemy enemy in SpawnEnemyTypes)
            {
                for (int i = 0; i < enemy.Count; i++)
                {
                    yield return new WaitForSeconds(1.5f);

                    SpawnEnemy(enemy.EnemyType);
                }
            }

            StopCoroutine(EnemySpawnRoutine);
        }
        private void SpawnEnemy(ObjectType enemyType)
        {
            Transform enemy = PoolObjectLoader.Instance.GetObject(enemyType).transform;
            enemy.position = this.gameObject.transform.position;
        }

    }
}
