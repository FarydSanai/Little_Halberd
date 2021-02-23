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
        public float DeactivateDistance = 4f;
        public float PathUpdateTimer = 0.5f;
        public float MoveDirDist = 0.5f;

        [Header("Follow options")]
        public float NextPointDistance = 1.8f;
        public float JumpNodeRequirement = 0.8f;

        [Header("Target components")]
        private CharacterControl targetControl;

        [Header("Custom options")]
        public bool FollowEnabled = true;
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
            targetControl = Target.GetComponent<CharacterControl>();

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
            JumpPlatform(dir);

            //Attack
            if (ReachedTarget())
            {
                control.Attack = true;

                if (targetControl.DAMAGE_DATA.isDead)
                {
                    control.Attack = false;
                }
            }
            else
            {
                control.Attack = false;
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
            if (ReachedTarget())
            {
                control.MoveLeft = false;
                control.MoveRight = false;
                return;
            }
            if (dir.x > MoveDirDist)
            {
                control.MoveRight = true;
                control.MoveLeft = false;
            }
            else if (dir.x < (-MoveDirDist))
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
        private bool ReachedTarget()
        {
            Vector2 distToTarget = rigid.position - (Vector2)Target.transform.position;
            if (Vector2.SqrMagnitude(distToTarget) < DeactivateDistance)
            {
                return true;
            }
            return false;
        }
        private void JumpPlatform(Vector2 dir)
        {
            if (dir.y > JumpNodeRequirement)
            {
                if (IsGrounded)
                {
                    control.Jump = true;
                }
            }
            if (dir.y <= 0f)
            {
                control.Jump = false;
            }
        }
    }
}