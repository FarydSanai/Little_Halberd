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
        public float ActivateDistance = 25f;
        public float PathUpdateTimer = 0.5f;

        [Header("Physics")]
        public float Speed = 200f;
        public float NextPointDistance = 4f;
        public float JumpNodeRequirement = 0.8f;

        public float JumpModifier = 0.3f;
        public float JumpCheckOffset = 0.1f;

        [Header("Custom")]
        public bool FollowEnabled = true;
        public bool JumpEnabled = true;
        public bool DirUpdateEnabled = true;

        private Path path;
        private int currentWayPoint = 0;
        private bool IsGrounded = false;
        private Seeker seeker;
        private Rigidbody2D rigid;
        private CharacterControl control;

        private void Start()
        {
            seeker = this.GetComponent<Seeker>();
            rigid = this.GetComponent<Rigidbody2D>();
            control = this.GetComponent<CharacterControl>();

            InvokeRepeating("UpdatePath", 0f, PathUpdateTimer);
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

            //Direction
            Vector2 dir = ((Vector2)path.vectorPath[currentWayPoint] - rigid.position);

            //Movement
            if (dir.x > 0.5f)
            {
                control.MoveRight = true;
                control.MoveLeft = false;
            }
            else if (dir.x < -0.5f)
            {
                control.MoveLeft = true;
                control.MoveRight = false;
            }
            else
            {
                control.MoveLeft = false;
                control.MoveRight = false;
            }

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

            float dist = Vector2.Distance(rigid.position, path.vectorPath[currentWayPoint]);
            if (dist < NextPointDistance)
            {
                currentWayPoint++;
            }   

        }

        private bool TargetInDistance()
        {
            return Vector3.Distance(rigid.position, Target.transform.position) < ActivateDistance;
        }

        private void OnPathComplete(Path p)
        {
            if (!p.error)
            {
                path = p;
                currentWayPoint = 0;
            }
        }
    }
}