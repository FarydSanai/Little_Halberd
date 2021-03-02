using System;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public enum ObjectType
    {
        GROUND,
        LEVEL_PART,
        ENEMY,
    }
    public class PoolObjectLoader : MonoBehaviour
    {
        public static PoolObjectLoader Instance;

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
        public void DestroyObject(GameObject obj)
        {
            PoolsDic[obj.GetComponent<IPooledObject>().objectType].Objects.Enqueue(obj);
            obj.SetActive(false);
        }
    }
}


