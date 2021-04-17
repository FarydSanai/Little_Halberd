using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class MovePlatform : MonoBehaviour
    {
        [Header("Travel Path")]
        public Transform WayPointsParent;

        [Header("Move options")]
        [SerializeField] private float travelTimeBetweenPoints = 5f;
        [SerializeField] private float endPointStallTime = 3f;
        [SerializeField] private float midPointStallTime = 1f;
        [Range(0.05f, 0f)]
        [SerializeField] public float MoveSpeed = 0.01f;

        bool movingForward = true;

        public bool LoopTrack = false;
        public bool StopAtEndPointOnLoop = false;

        public int CurrentPointIndex = 0;

        private float StoppingDistThres = 0.0025f;
        private float t = 0f;
        private float dwellTimer = 0f;

        private Transform characterTransformParent;

        private Vector3 StartPoint;
        private Vector3 TargetPoint;

        private List<Vector3> wayPoints = new List<Vector3>();

        private void Start()
        {
            for (int i = 0; i < WayPointsParent.childCount; i++)
            {
                wayPoints.Add(WayPointsParent.GetChild(i).position);
            }

            if (wayPoints.Count < 1)
            {
                Debug.LogError("Set some points!");
                this.enabled = false;
            }

            CurrentPointIndex = 0;

            StartPoint = GetWayPoint();
            SetNextWayPointIndex();
            TargetPoint = GetWayPoint();

            transform.position = StartPoint;

        }
        private void FixedUpdate()
        {
            if (Vector3.SqrMagnitude(TargetPoint - this.transform.position) <= StoppingDistThres)
            {
                dwellTimer += Time.deltaTime;
                bool trackContinue = false;

                if (IsAtEndPoint())
                {
                    if (LoopTrack && !StopAtEndPointOnLoop &&
                        CurrentPointIndex == (wayPoints.Count - 1))
                    {
                        trackContinue = true;
                    }
                    if (dwellTimer >= endPointStallTime)
                    {
                        trackContinue = true;
                    }
                }
                else
                {
                    if (dwellTimer >= midPointStallTime)
                    {
                        trackContinue = true;
                    }
                }

                if (trackContinue)
                {
                    StartPoint = TargetPoint;
                    SetNextWayPointIndex();
                    TargetPoint = GetWayPoint();

                    t = 0f;
                    dwellTimer = 0f;
                }

                return;
            }
            t += MoveSpeed;
            transform.position = Vector3.Lerp(StartPoint, TargetPoint, t);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.GetComponent<CharacterControl>() ==
                CharacterManager.Instance.GetPlayableCharacter())
            {
                other.transform.parent = this.transform;
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            other.transform.parent = null;
        }
        private bool IsAtEndPoint()
        {
            if (CurrentPointIndex == 0 ||
                CurrentPointIndex == (wayPoints.Count - 1))
            {
                return true;
            }
            return false;
        }

        private Vector3 GetWayPoint()
        {
            return wayPoints[CurrentPointIndex];
        }
        private void SetNextWayPointIndex()
        {
            if (!LoopTrack)
            {
                if (movingForward)
                {
                    CurrentPointIndex++;
                    if (CurrentPointIndex >= wayPoints.Count)
                    {
                        CurrentPointIndex = wayPoints.Count - 2;
                        movingForward = false;
                    }
                }
                else
                {
                    CurrentPointIndex--;

                    if (CurrentPointIndex < 0)
                    {
                        CurrentPointIndex = 1;
                        movingForward = true;
                    }
                }
            }
            else
            {
                CurrentPointIndex++;
                if (CurrentPointIndex >= wayPoints.Count)
                {
                    CurrentPointIndex = 0;
                }
            }
        }
        private void OnDrawGizmos()
        {
            if (WayPointsParent == null)
            {
                return;
            }

            Gizmos.color = Color.red;

            for (int i = 0; i < WayPointsParent.childCount; i++)
            {
                Gizmos.DrawSphere(WayPointsParent.GetChild(i).position, 0.3f);

                int nextWaypoint = i + 1;

                if (nextWaypoint >= WayPointsParent.childCount)
                {
                    break;
                }

                Gizmos.DrawLine(WayPointsParent.GetChild(i).position,
                                WayPointsParent.GetChild(nextWaypoint).position);
            }
        }
    }
}
