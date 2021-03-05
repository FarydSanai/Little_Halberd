using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class LevelGenerator : MonoBehaviour
    {
        private const string endPointObjName = "EndPoint";
        private float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 1000f;

        [SerializeField] private CharacterControl control;
        [SerializeField] private Transform levelPartStart;
        [SerializeField] private List<Transform> levelPartList = new List<Transform>(); 

        private Vector3 lastEndPosition;

        private void Awake()
        {
            lastEndPosition = levelPartStart.Find(endPointObjName).position;
        }
        private void Update()
        {
            Vector3 dist = control.transform.position - lastEndPosition;
            if (Vector3.SqrMagnitude(dist) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
            {
                SpawnLevelPart();
            }

            if (levelPartList.Count >= 7)
            {
                PoolObjectLoader.Instance.DestroyObject(levelPartList[0].gameObject);
                levelPartList.RemoveAt(0);
            }
        }

        private void SpawnLevelPart()
        {
            //int rand = Random.Range(0, levelPartList.Count);
            Transform levelPartTransform = SpawnLevelPartCurrent();

            lastEndPosition = levelPartTransform.Find(endPointObjName).position;

            levelPartList.Add(levelPartTransform);
        }
        private Transform SpawnLevelPartCurrent()
        {
            //Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
            Transform levelPartTransform = PoolObjectLoader.Instance.GetObject(ObjectType.LEVEL_PART_1).transform;
            levelPartTransform.position = lastEndPosition + new Vector3(15f, 0f, 0f);

            return levelPartTransform;
        }
    }
}
