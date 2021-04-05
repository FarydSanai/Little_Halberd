using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LittleHalberd.EditorUI;

namespace LittleHalberd
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<ObjectType> SpawnEnemyTypes;

        [ColorSpacer(30, 3, 300, 252, 248, 0)]

        [SerializeField] private int EnemyMaxCount;

        private int EnemyCurrentCount = 0;
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
            while (EnemyCurrentCount < EnemyMaxCount)
            {
                int rand = Random.Range(0, SpawnEnemyTypes.Count);

                yield return new WaitForSeconds(1.5f);

                SpawnEnemy(SpawnEnemyTypes[rand]);
                EnemyCurrentCount++;
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
