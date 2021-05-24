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
        //[SerializeField] private float travelTimeBetweenPoints = 5f;
        [SerializeField] private float endPointStallTime = 3f;
        [SerializeField] private float midPointStallTime = 1f;

        [Range(5f, 0f)]
        [SerializeField] private float MoveSpeed = 1f;

        [SerializeField] private bool LoopTrack = false;
        [SerializeField] private bool StopAtEndPointOnLoop = false;

        private bool movingForward = true;
        private int CurrentPointIndex = 0;
        private Vector3 StartPoint;
        private Vector3 TargetPoint;
        private List<Vector3> wayPoints = new List<Vector3>();

        //Calculate options
        private const float StoppingDistThres = 0.0025f;
        private float LerpValue = 0f;
        private float dwellTimer = 0f;

        private void Start()
        {
            InitMoveOptions();
        }
        private void FixedUpdate()
        {
            CalculateMoving();
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.gameObject.layer == CustomLayers.Instance.GetLayer(LH_Layer.Projectile))
            {
                return;
            }
            if (other.transform.GetComponent<CharacterControl>() ==
                CharacterManager.Instance.PlayableCharacter)
            {
                other.transform.parent = this.transform;
            }
            if (other.transform.gameObject.layer == CustomLayers.Instance.GetLayer(LH_Layer.Enemy))
            {
                other.transform.parent = this.transform;
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.transform.gameObject.layer == CustomLayers.Instance.GetLayer(LH_Layer.Projectile))
            {
                return;
            }
            if (other.gameObject != null && other.transform.parent != null)
            {
                other.transform.parent = null;
            }
        }
        private void InitMoveOptions()
        {
            for (int i = 0; i < WayPointsParent.childCount; i++)
            {
                wayPoints.Add(WayPointsParent.GetChild(i).position);
            }

            if (wayPoints.Count < 1)
            {
                //Debug.LogError("Set some points!");
                this.enabled = false;
            }

            CurrentPointIndex = 0;

            StartPoint = GetWayPoint();
            SetNextWayPointIndex();
            TargetPoint = GetWayPoint();

            transform.position = StartPoint;
        }
        private void CalculateMoving()
        {
            if (Vector3.SqrMagnitude(TargetPoint - this.transform.position) <= StoppingDistThres)
            {
                dwellTimer += Time.fixedDeltaTime;
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

                    LerpValue = 0f;
                    dwellTimer = 0f;
                }

                return;
            }

            LerpValue += (MoveSpeed / 100f);
            transform.position = Vector3.Lerp(StartPoint, TargetPoint, LerpValue);
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
