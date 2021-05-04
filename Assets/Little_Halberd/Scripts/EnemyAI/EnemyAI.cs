﻿using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using LittleHalberd.EditorUI;

namespace LittleHalberd
{
    public enum EnemyType
    {
        MELEE,
        RANGE,
        BOSS,
    }
    public enum AIState
    {
        PATROL_AREA,
        CHASE_PLAYER,
        ATTACK_PLAYER,
        IDLE_STATE,
    }
    public class EnemyAI : MonoBehaviour
    {

        private const string UpdatePathFunc = "UpdatePath";
        private const string GroundLayerName = "Ground";

        [Header("Pathfinding")]
        public Transform Target;
        public float ActivateDistance = 600f;
        public float ReachedDist = 8f;
        public float PathUpdateTimer = 0.5f;
        public float MoveDirDist = 0.5f;

        [Header("Follow options")]
        public float NextPointDistance = 1.8f;
        public float JumpNodeRequireDist = 0.8f;

        [Header("Target components")]
        private CharacterControl targetControl;

        [Header("Custom options")]
        [SerializeField] private AIState InitialState = AIState.PATROL_AREA; 
        [SerializeField] private bool FollowEnabled = true;
        [SerializeField] private EnemyType EnemyType = EnemyType.MELEE;

        [Header("Patrol options")]
        private List<ContactPoint2D> contacts = new List<ContactPoint2D>();
        private bool changePatrolDir = true;
        private Collider2D groundCollider;
        private int GroundLayer;

        private Path path;
        private int currentWayPoint = 0;
        private bool IsGrounded;
        private Seeker seeker;
        private Rigidbody2D rigid;
        private CharacterControl control;
        private AIState AICurrentState;

        public delegate void AIBehaviour();
        private AIBehaviour ProcessAIBehaviour;
        private void Start()
        {
            Target = CharacterManager.Instance.GetPlayableCharacter().transform;
            seeker = this.GetComponent<Seeker>();
            rigid = this.GetComponent<Rigidbody2D>();
            control = this.GetComponent<CharacterControl>();
            targetControl = Target.GetComponent<CharacterControl>();
            AICurrentState = InitialState;
            GroundLayer = LayerMask.NameToLayer(GroundLayerName);

            InvokeRepeating(UpdatePathFunc, 0f, PathUpdateTimer);

            InitAIBehaviour(EnemyType);
        }
        private void FixedUpdate()
        {
            ProcessAIBehaviour();
        }
        private void MeleeMobBehaviour()
        {
            switch (AICurrentState)
            {
                case AIState.IDLE_STATE:
                    {
                        if (TargetInDistance() && FollowEnabled)
                        {
                            AICurrentState = AIState.CHASE_PLAYER;
                        }
                    }
                    break;
                case AIState.PATROL_AREA:
                    {
                        PatrolArea();

                        if (TargetInDistance() && FollowEnabled)
                        {
                            AICurrentState = AIState.CHASE_PLAYER;
                        }
                    }
                    break;
                case AIState.CHASE_PLAYER:
                    {
                        PathFollow();
                        if (ReachedTarget())
                        {
                            AICurrentState = AIState.ATTACK_PLAYER;
                        }
                    }
                    break;
                case AIState.ATTACK_PLAYER:
                    {
                        AttackTarget();
                        if (!ReachedTarget())
                        {
                            AICurrentState = AIState.CHASE_PLAYER;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        private void RangeMobBehavoiur()
        {
            switch (AICurrentState)
            {
                case AIState.IDLE_STATE:
                    {
                        if (control.RANGE_ATTACK_DATA.RangeAttackReset())
                        {
                            control.RangeAttack = true;
                        }
                        else
                        {
                            control.RangeAttack = false;
                        }
                        control.RANGE_ATTACK_DATA.AimTarget();
                    }
                    break;
                default:
                    break;
            }
        }
        private void InitAIBehaviour(EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.MELEE:
                    {
                        ProcessAIBehaviour = MeleeMobBehaviour;
                    }
                    break;
                case EnemyType.RANGE:
                    {
                        ProcessAIBehaviour = RangeMobBehavoiur;
                    }
                    break;
                case EnemyType.BOSS:
                    break;
                default:
                    break;
            }
        }
        private void UpdatePath()
        {
            if (AICurrentState == AIState.CHASE_PLAYER)
            {
                if (FollowEnabled && seeker.IsDone())
                {
                    seeker.StartPath(rigid.position, Target.position, OnPathComplete);
                }
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
            control.Attack = false;

            //Set direction
            Vector2 dir = ((Vector2)path.vectorPath[currentWayPoint] - rigid.position);

            //Movement
            MoveToTarget(dir);

            //Jump
            JumpPlatform(dir);

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
            Vector2 dist = rigid.position - (Vector2)Target.position;
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
            Vector2 distToTarget = rigid.position - (Vector2)Target.position;
            if (Vector2.SqrMagnitude(distToTarget) < ReachedDist)
            {
                return true;
            }
            return false;
        }
        private void JumpPlatform(Vector2 dir)
        {
            if (ReachedTarget())
            {
                control.Jump = false;
                return;
            }
            if (dir.y > JumpNodeRequireDist)
            {
                if (IsGrounded)
                {
                    control.Jump = true;
                }
            }
            else
            {
                control.Jump = false;
            }
        }
        private void AttackTarget()
        {       
            if (!targetControl.DAMAGE_DATA.isDead)
            {
                control.Attack = true;
            }
            else
            {
                control.Attack = false;
            }
        }
        private void PatrolArea()
        {
            IsGrounded = control.GROUND_DATA.IsGrounded();
            if (IsGrounded)
            {
                int contactsNumber = rigid.GetContacts(contacts);
                if (groundCollider == null)
                {
                    groundCollider = contacts.Find(c =>
                                                   c.collider.gameObject.layer == GroundLayer).collider;
                }

                if (groundCollider != null)
                {
                    AIPatrolPlatform(groundCollider);
                }
            }
        }
        private void AIPatrolPlatform(Collider2D groundCollider) 
        {
            if (changePatrolDir && control.transform.position.x > groundCollider.bounds.min.x)
            {
                control.MoveLeft = true;
                control.MoveRight = false;
            }
            else if (changePatrolDir)
            {
                changePatrolDir = false;
            }

            if (!changePatrolDir && control.transform.position.x < groundCollider.bounds.max.x)
            {
                control.MoveRight = true;
                control.MoveLeft = false;
                return;
            }
            else if (!changePatrolDir)
            {
                changePatrolDir = true;
            }
        }
    }
}