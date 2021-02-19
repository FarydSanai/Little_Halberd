using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class LevelGenerator : MonoBehaviour
    {
        private const string endPointObjName = "EndPoint";
        private float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 10000f;

        [SerializeField] private CharacterControl control;
        [SerializeField] private Transform levelPartStart;
        [SerializeField] private List<Transform> levelPartList; 

        private Vector3 lastEndPosition;

        private void Awake()
        {
            lastEndPosition = levelPartStart.Find(endPointObjName).position;

            int startLevelPartsCount = 3;

            for (int i = 0; i < startLevelPartsCount; i++)
            {
                SpawnLevelPart();
            }
        }
        private void Update()
        {
            Vector3 dist = control.GetPosition() - lastEndPosition;
            if (Vector3.SqrMagnitude(dist) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
            {
                SpawnLevelPart();
            }
        }

        private void SpawnLevelPart()
        {
            int rand = Random.Range(0, levelPartList.Count);
            Transform levelPartTransform = SpawnLevelPart(levelPartList[rand], lastEndPosition);

            lastEndPosition = levelPartTransform.Find(endPointObjName).position;
        }
        private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
        {
            Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);

            return levelPartTransform;
        }
    }
}
