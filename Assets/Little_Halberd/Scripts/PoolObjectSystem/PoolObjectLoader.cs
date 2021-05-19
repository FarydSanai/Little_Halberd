using System;
using System.Collections.Generic;
using UnityEngine;
using LittleHalberd.InspectorUI;

namespace LittleHalberd
{
    public enum ObjectType
    {
        GROUND,

        LEVEL_PART_1,
        LEVEL_PART_2,
        LEVEL_PART_3,
        LEVEL_PART_4,
        LEVEL_PART_5,
        LEVEL_PART_6,
        LEVEL_PART_7,
        
        GOLEM_GREY,
        GOLEM_WOOD,
        GOLEM_GREEN,
        MINOTAUR,
        REAPER_GREEN,
        WRAITH_BLACK,
        WRAITH_GREEN,

        PUMPKIN_BOMB,

        VFX_HIT,
        VFX_EXPLODE_PUMPKIN,

    }
    public class PoolObjectLoader : MonoBehaviour
    {
        public static PoolObjectLoader Instance;

        public List<PoolObjectInfo> LevelPartsInfo;
        [ColorSpacer(30, 3, 300, 252, 0, 185)]

        public List<PoolObjectInfo> EnemyTypeInfo;
        [ColorSpacer(30, 3, 300, 252, 248, 0)]

        public List<PoolObjectInfo> VFX_TypeInfo;
        [ColorSpacer(30, 3, 300, 252, 0, 0)]

        [SerializeField]
        private List<PoolObjectInfo> PoolObjectsInfo;

        private Dictionary<ObjectType, Pool> PoolsDic;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            InitPool();
        }

        private void InitPool()
        {
            PoolsDic = new Dictionary<ObjectType, Pool>();

            PoolObjectsInfo.AddRange(EnemyTypeInfo);
            PoolObjectsInfo.AddRange(LevelPartsInfo);
            PoolObjectsInfo.AddRange(VFX_TypeInfo);

            GameObject temp = new GameObject();

            foreach (PoolObjectInfo objInfo in PoolObjectsInfo)
            {
                GameObject objContainer = Instantiate(temp, transform, false);
                objContainer.name = objInfo.objectType.ToString();

                PoolsDic[objInfo.objectType] = new Pool(objContainer.transform);

                for (int i = 0; i < objInfo.StartCount; i++)
                {
                    GameObject obj = InstantiateObject(objInfo.objectType, objContainer.transform);
                    PoolsDic[objInfo.objectType].Objects.Enqueue(obj);
                }
            }

            Destroy(temp);
        }
        private GameObject InstantiateObject(ObjectType type, Transform parent)
        {
            GameObject instObj = Instantiate(PoolObjectsInfo.Find(o => o.objectType == type).objectPrefab, parent);
            instObj.SetActive(false);
            return instObj;
        }
        public GameObject GetObject(ObjectType type)
        {
            GameObject obj;

            if (PoolsDic[type].Objects.Count > 0)
            {
                obj = PoolsDic[type].Objects.Dequeue();
            }
            else
            {
                obj = InstantiateObject(type, PoolsDic[type].Container);
            }
            obj.SetActive(true);

            return obj;
        }
        public GameObject GetObject(ObjectType type, Vector3 spawnPos, Quaternion startRot)
        {
            GameObject obj;

            if (PoolsDic[type].Objects.Count > 0)
            {
                obj = PoolsDic[type].Objects.Dequeue();
            }
            else
            {
                obj = InstantiateObject(type, PoolsDic[type].Container);
            }

            obj.transform.position = spawnPos;
            obj.transform.rotation = startRot;
            obj.SetActive(true);

            return obj;
        }
        public void DestroyObject(GameObject obj)
        {
            PoolsDic[obj.GetComponent<IPooledObject>().objectType].Objects.Enqueue(obj);
            obj.SetActive(false);
        }
    }
}


