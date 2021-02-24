using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Linq;

namespace LittleHalberd
{
    public enum AIState
    {
        PATROL_AREA,
        CHASE_PLAYER,
        ATTACK_PLAYER,
    }
    public class EnemyAI : MonoBehaviour
    {

        private const string UpdatePathFunc = "UpdatePath";
        private const string GroundLayerName = "Ground";

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
        public AIState AICurrentState;
        public bool FollowEnabled = true;
        public bool DirUpdateEnabled = true;

        [Header("Patrol options")]
        private List<ContactPoint2D> contacts = new List<ContactPoint2D>();
        private bool changePatrolDir = true;
        private Collider2D groundCollider;
        private int GroundLayer;

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
            targetControl = Target.GetComponent<CharacterControl>();
            AICurrentState = AIState.PATROL_AREA;
            GroundLayer = LayerMask.NameToLayer(GroundLayerName);

            InvokeRepeating(UpdatePathFunc, 0f, PathUpdateTimer);            
        }
        private void FixedUpdate()
        {
            switch (AICurrentState)
            {
                case AIState.PATROL_AREA:
                    {
                        PatrolArea();
                    }
                    break;
                case AIState.CHASE_PLAYER:
                    break;
                case AIState.ATTACK_PLAYER:
                    break;
                default:
                    break;
            }
            //if (TargetInDistance() && FollowEnabled)
            //{
            //    PathFollow();
            //}
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
        private void AttackTarget()
        {
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
        }
        private void PatrolArea()
        {
            int contactsNumber = rigid.GetContacts(contacts);

            if (contactsNumber > 0)
            {
                if (groundCollider == null)
                {
                    groundCollider = contacts.Find(c => c.collider.gameObject.layer == GroundLayer).collider;
                }

                if (groundCollider != null)
                {
                    AIPatrolPlatform(groundCollider);
                }
            }
        }
        private void AIPatrolPlatform(Collider2D groundCollider) 
        {
            if (control.transform.position.x > groundCollider.bounds.min.x && changePatrolDir)
            {
                control.MoveLeft = true;
                control.MoveRight = false;
            }
            else if (changePatrolDir)
            {
                changePatrolDir = false;
            }

            if (control.transform.position.x < groundCollider.bounds.max.x && !changePatrolDir)
            {
                control.MoveRight = true;
                control.MoveLeft = false;
            }
            else if (!changePatrolDir)
            {
                changePatrolDir = true;
            }
        }
    }
}