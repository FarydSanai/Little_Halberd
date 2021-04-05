using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Foliage;

namespace LittleHalberd
{
    public class LevelGenerator : MonoBehaviour
    {
        private const string endPointObjName = "EndPoint";
        private float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 1000f;

        [SerializeField] private CharacterControl control;
        [SerializeField] private Transform levelPartStart;
        [SerializeField] private List<Transform> levelPartList = new List<Transform>();
        [SerializeField] private int MaxLevelPartsCount = 5;

        private Vector3 lastEndPosition;


        private void Awake()
        {
            lastEndPosition = levelPartStart.Find(endPointObjName).position;
        }
        private void Start()
        {
            StartCoroutine(_ScanGraphs());
        }
        private void Update()
        {
            Vector3 dist = control.transform.position - lastEndPosition;
            if (Vector3.SqrMagnitude(dist) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
            {
                SpawnLevelPart();
            }

            if (levelPartList.Count >= MaxLevelPartsCount)
            {
                PoolObjectLoader.Instance.DestroyObject(levelPartList[0].gameObject);
                levelPartList.RemoveAt(0);
            }
        }

        IEnumerator _ScanGraphs()
        {
            while(true)
            {
                AstarPath.active.Scan();
                //Debug.Log("Scan graphs");
                yield return new WaitForSeconds(3f);
            }
        }

        private void SpawnLevelPart()
        {
            int rand = Random.Range(0, PoolObjectLoader.Instance.LevelPartsInfo.Count);

            Transform levelPartTransform = SpawnLevelPart(PoolObjectLoader.Instance.LevelPartsInfo[rand].objectType);

            //lastEndPosition = levelPartTransform.Find(endPointObjName).position;
            lastEndPosition = levelPartTransform.position + new Vector3(50f, 0f, 0f);

            levelPartList.Add(levelPartTransform);
            //levelPartTransform.gameObject.GetComponent<Foliage2D_Path>();
        }
        private Transform SpawnLevelPart(ObjectType levelPartType)
        {
            Transform levelPartTransform = PoolObjectLoader.Instance.GetObject(levelPartType).transform;
            levelPartTransform.position = lastEndPosition;

            return levelPartTransform;
        }
    }
}
