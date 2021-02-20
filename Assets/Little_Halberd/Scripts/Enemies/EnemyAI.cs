using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace LittleHalberd
{
    public class EnemyAI : MonoBehaviour
    {
        [Header("Pathfinding")]
        public Transform Target;
        public float ActivateDistance = 600f;
        public float DeactivateDistance = 2.5f;
        public float PathUpdateTimer = 0.5f;

        [Header("Physics")]
        public float NextPointDistance = 8f;
        public float JumpNodeRequirement = 0.8f;

        [Header("Custom options")]
        public bool FollowEnabled = true;
        public bool JumpEnabled = true;
        public bool DirUpdateEnabled = true;

        private Path path;
        private int currentWayPoint = 0;
        private bool IsGrounded = false;
        private Seeker seeker;
        private Rigidbody2D rigid;
        private CharacterControl control;

        private const string UpdatePathFunc = "UpdatePath";

        private void Start()
        {
            seeker = this.GetComponent<Seeker>();
            rigid = this.GetComponent<Rigidbody2D>();
            control = this.GetComponent<CharacterControl>();

            InvokeRepeating(UpdatePathFunc, 0f, PathUpdateTimer);
        }
        private void FixedUpdate()
        {
            if (TargetInDistance() && FollowEnabled)
            {
                PathFollow();
            }
        }
        private void UpdatePath()
        {
            if (TargetInDistance() && FollowEnabled && seeker.IsDone())
            {
                seeker.StartPath(rigid.position, Target.position, OnPathComplete);
            }
        }
        private void PathFollow()
        {             
            if (path == null)
            {
                return;
            }
            if (currentWayPoint >= path.vectorPath.Count)
            {
                return;
            }

            IsGrounded = control.GROUND_DATA.IsGrounded();

            //Set direction
            Vector2 dir = ((Vector2)path.vectorPath[currentWayPoint] - rigid.position);

            //Movement
            MoveToTarget(dir);

            //Jump
            if (JumpEnabled && IsGrounded)
            {
                if (dir.y > JumpNodeRequirement)
                {
                    control.Jump = true;
                }
                else
                {
                    control.Jump = false;
                }

            }

            //Death
            if (control.DAMAGE_DATA.isDead)
            {
                CancelInvoke(UpdatePathFunc);
            }

            Vector2 dist = rigid.position - (Vector2)path.vectorPath[currentWayPoint];
            if (Vector2.SqrMagnitude(dist) < NextPointDistance)
            {
                currentWayPoint++;
            }   
        }
        private bool TargetInDistance()
        {
            Vector2 dist = rigid.position - (Vector2)Target.transform.position;
            return Vector3.SqrMagnitude(dist) < ActivateDistance;
        }
        private void OnPathComplete(Path p)
        {
            if (!p.error)
            {
                path = p;
                currentWayPoint = 0;
            }
        }
        private void MoveToTarget(Vector2 dir)
        {
            if (dir.x > DeactivateDistance)
            {
                control.MoveRight = true;
                control.MoveLeft = false;
            }
            else if (dir.x < (-DeactivateDistance))
            {
                control.MoveLeft = true;
                control.MoveRight = false;
            }
            else
            {
                control.MoveLeft = false;
                control.MoveRight = false;
            }
        }
    }
}