using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class GroundGenerator : MonoBehaviour
    {
        private const string groundEndPoint = "GroundEndPoint";
        private const float GroundSpawnDistance = 1000f;

        [SerializeField] private Transform GroundStart;
        [SerializeField] private CharacterControl control;
        [SerializeField] private int MaxCountBeforeDestroy;

        private Vector3 lastGroundEndPoint;
        private List<Transform> GroundParts = new List<Transform>();

        private void Awake()
        {
            lastGroundEndPoint = GroundStart.Find(groundEndPoint).position;
            GroundParts.Add(GroundStart);
        }

        private void Update()
        {
            Vector3 dist = control.transform.position - lastGroundEndPoint;

            if (Vector3.SqrMagnitude(dist) < GroundSpawnDistance)
            {
                SpawnGround(lastGroundEndPoint);

                if (GroundParts.Count >= MaxCountBeforeDestroy)
                {
                    DestroyGround(GroundParts[0]);
                }
            }
        }
        private void SpawnGround(Vector3 position)
        {
            Transform groundTransform = PoolObjectLoader.Instance.GetObject(ObjectType.GROUND).transform;
            groundTransform.position = position;

            lastGroundEndPoint = groundTransform.Find(groundEndPoint).position;
            GroundParts.Add(groundTransform);
        }
        private void DestroyGround(Transform ground)
        {
            PoolObjectLoader.Instance.DestroyObject(ground.gameObject);
            GroundParts.RemoveAt(0);
        }
    }
}