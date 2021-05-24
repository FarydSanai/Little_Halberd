using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

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
        RANGE_ATTACK,
    }
    public class CharacterAI : SubComponent
    {
        [Header("Follow options")]
        public float NextPointDistance;
        public float JumpNodeRequireDist;

        [Header("Enemy type options")]
        [SerializeField]
        private AIState InitialState = AIState.PATROL_AREA;
        public bool FollowEnabled = true;
        [SerializeField]
        private EnemyType EnemyType = EnemyType.MELEE;
        private bool BossRage;

        [Header("Patrol platform")]
        private IPatrolPlatform patrolPlatform;

        public CharacterAIData characterAIData;
        private void Start()
        {
            patrolPlatform = new PatrolPlatform(control);

            characterAIData = new CharacterAIData
            {
                NextPointDistance = this.NextPointDistance,
                JumpNodeRequireDist = this.JumpNodeRequireDist,
                EnemyType = this.EnemyType,
                AICurrentState = this.InitialState,
                FollowEnabled = this.FollowEnabled,

                targetControl = CharacterManager.Instance.PlayableCharacter,
            };

            InitAIBehaviour(EnemyType);

            subComponentProcessor.characterAIData = characterAIData;
            subComponentProcessor.ArrSubComponents[(int)SubComponentType.CHARACTER_AI] = this;
        }
        public override void OnUpdate()
        {
            //throw new System.NotImplementedException();
        }

        public override void OnFixedUpdate()
        {
            characterAIData.ProcessAIBehaviour();
        }
        private void MeleeMobBehaviour()
        {
            switch (characterAIData.AICurrentState)
            {
                case AIState.IDLE_STATE:
                    {
                        if (TargetInDistance() && FollowEnabled)
                        {
                            characterAIData.AICurrentState = AIState.CHASE_PLAYER;
                        }
                    }
                    break;
                case AIState.PATROL_AREA:
                    {
                        patrolPlatform.PatrolArea();

                        if (TargetInDistance() && FollowEnabled)
                        {
                            characterAIData.AICurrentState = AIState.CHASE_PLAYER;
                        }
                    }
                    break;
                case AIState.CHASE_PLAYER:
                    {
                        PathFollow();
                        if (ReachedTarget())
                        {
                            characterAIData.AICurrentState = AIState.ATTACK_PLAYER;
                        }
                    }
                    break;
                case AIState.ATTACK_PLAYER:
                    {
                        AttackTarget();
                        if (!ReachedTarget())
                        {
                            characterAIData.AICurrentState = AIState.CHASE_PLAYER;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        private void RangeMobBehavoiur()
        {
            switch (characterAIData.AICurrentState)
            {
                case AIState.IDLE_STATE:
                    {
                        if (TargetInDistance())
                        {
                            characterAIData.AICurrentState = AIState.ATTACK_PLAYER;
                        }
                    }
                    break;
                case AIState.ATTACK_PLAYER:
                    {
                        if (TargetInDistance())
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
                        else
                        {
                            characterAIData.AICurrentState = AIState.IDLE_STATE;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        private void BossMobBehaviour()
        {
            switch (characterAIData.AICurrentState)
            {
                case AIState.IDLE_STATE:
                    {
                        if (TargetInDistance() && FollowEnabled)
                        {
                            characterAIData.AICurrentState = AIState.CHASE_PLAYER;
                        }
                    }
                    break;
                case AIState.CHASE_PLAYER:
                    {
                        CheckBossRage(subComponentProcessor.damageData.CurrentHP);
                        RangeAttackBoss();
                        PathFollow();
                        if (ReachedTarget())
                        {
                            characterAIData.AICurrentState = AIState.ATTACK_PLAYER;
                        }
                    }
                    break;
                case AIState.ATTACK_PLAYER:
                    {
                        CheckBossRage(subComponentProcessor.damageData.CurrentHP);
                        AttackTarget();
                        if (!ReachedTarget())
                        {
                            characterAIData.AICurrentState = AIState.CHASE_PLAYER;
                        }
                    }
                    break;
                //case AIState.RANGE_ATTACK:
                //    {
                //        RangeAttackBoss(control.characterAnimator.GetCurrentAnimatorStateInfo(0));
                //        //SetNextState(AIState.CHASE_PLAYER, control.characterAnimator.GetCurrentAnimatorStateInfo(0));
                //    }
                //    break;
                default:
                    break;
            }
        }
        private void RangeAttackBoss()
        {
            if (control.RANGE_ATTACK_DATA.RangeAttackReset())
            {
                control.RANGE_ATTACK_DATA.AimTarget();
                control.RangeAttack = true;
            }
            else
            {
                control.RangeAttack = false;
            }
            
        }
        private void InitAIBehaviour(EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.MELEE:
                    {
                        characterAIData.ProcessAIBehaviour = MeleeMobBehaviour;
                    }
                    break;
                case EnemyType.RANGE:
                    {
                        characterAIData.ProcessAIBehaviour = RangeMobBehavoiur;
                    }
                    break;
                case EnemyType.BOSS:
                    {
                        characterAIData.ProcessAIBehaviour = BossMobBehaviour;
                    }
                    break;
                default:
                    break;
            }
        }
        private void PathFollow()
        {             
            if (control.PATHFINDER_DATA.path == null)
            {
                return;
            }
            if (control.PATHFINDER_DATA.currentWayPoint >= control.PATHFINDER_DATA.path.vectorPath.Count)
            {
                return;
            }
            control.Attack = false;

            //Set direction
            Vector2 dir = ((Vector2)control.PATHFINDER_DATA.path.vectorPath[control.PATHFINDER_DATA.currentWayPoint] -
                            control.RIGID_BODY.position);

            //Movement
            MoveToTarget(dir);

            //Jump
            JumpPlatform(dir);

            //Death
            if (control.DAMAGE_DATA.isDead)
            {
                StopCoroutine(control.PATHFINDER_DATA.UpdatePathRoutine);
            }

            Vector2 dist = control.RIGID_BODY.position -
                           (Vector2)control.PATHFINDER_DATA.path.vectorPath[control.PATHFINDER_DATA.currentWayPoint];

            if (Vector2.SqrMagnitude(dist) < NextPointDistance)
            {
                control.PATHFINDER_DATA.currentWayPoint++;
            }   
        }
        private bool TargetInDistance()
        {
            Vector2 dist = control.RIGID_BODY.position - (Vector2)control.PATHFINDER_DATA.Target.position;
            return Vector3.SqrMagnitude(dist) < control.PATHFINDER_DATA.ActivateDistance;
        }
        private void MoveToTarget(Vector2 dir)
        {
            if (!control.Jump && !control.GROUND_DATA.IsGrounded())
            {
                control.MoveLeft = false;
                control.MoveRight = false;
                return;
            }
            if (ReachedTarget())
            {
                control.MoveLeft = false;
                control.MoveRight = false;
                return;
            }
            if (dir.x > control.PATHFINDER_DATA.MoveDirDist)
            {
                control.MoveRight = true;
                control.MoveLeft = false;
            }
            else if (dir.x < (control.PATHFINDER_DATA.MoveDirDist))
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
            Vector2 distToTarget = control.RIGID_BODY.position -
                                   (Vector2)control.PATHFINDER_DATA.Target.position;
            if (Vector2.SqrMagnitude(distToTarget) < control.PATHFINDER_DATA.ReachedDist)
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
                if (control.GROUND_DATA.IsGrounded())
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
            if (!characterAIData.targetControl.DAMAGE_DATA.isDead && 
                !control.DAMAGE_DATA.isDead)
            {
                control.Attack = true;
            }
            else
            {
                control.Attack = false;
            }
        }
        private void CheckBossRage(float bossHP)
        {
            if (!BossRage && bossHP <= control.CharacterMaxHP / 2f)
            {
                BossRage = true;
                Rage face = control.gameObject.GetComponentInChildren<Rage>();
                face.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f);
            }
        }
    }
}