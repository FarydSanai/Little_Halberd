using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class RangeAttackSubComp : SubComponent
    {
        public RangeAttackData RangeAttackData;

        //Temp
        //Equals Sqrt(Projectile's RigidBody.gravityScale)
        public float GravityMultiplier = 1.4f;
        private void Start()
        {
            RangeAttackData = new RangeAttackData
            {
                Acceleration = Physics2D.gravity.y,
                AngleInDegrees = 0f,
                Target = null,
                SpawnPoint = null,
                projectileType = ObjectType.PUMPKIN_BOMB,
                AimTarget = AimTarget,
                ProcessRangeAtatck = ProcessRangeAttack,
                TimeBtwAttacks = 0f,
                StartTimeBtwAttacks = 0f,
                RangeAttackReset = RangeAttackReset,
            };
            subComponentProcessor.rangeAttackData = RangeAttackData;
            subComponentProcessor.ArrSubComponents[(int)SubComponentType.RANGE_ATTACK] = this;
        }
        public override void OnUpdate()
        {
        }

        public override void OnFixedUpdate()
        {
            //throw new System.NotImplementedException();
        }
        private void AimTarget()
        {
            if (RangeAttackData.Target == null)
            {
                return;
            }
            Vector2 dir = (Vector2)RangeAttackData.Target.position - (Vector2)control.transform.position;
            
            if (dir.x < 0f)
            {
                control.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
            else
            {
                control.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            //Debug.DrawRay((Vector2)this.transform.position, dir, Color.red);
        }
        private void ProcessRangeAttack()
        {
            GameObject obj = PoolObjectLoader.Instance.GetObject(RangeAttackData.projectileType,
                                                                 RangeAttackData.SpawnPoint.position,
                                                                 Quaternion.identity);

            float speed = CalculateBallistic();
            obj.GetComponent<Rigidbody2D>().velocity = RangeAttackData.SpawnPoint.right * speed;
            obj.GetComponent<Rigidbody2D>().AddTorque(50f);
        }
        private float CalculateBallistic()
        {
            Vector2 dir = (Vector2)RangeAttackData.Target.position - (Vector2)RangeAttackData.SpawnPoint.position;

            float x = dir.magnitude;
            float y = dir.y;
            float AngleInRad = RangeAttackData.AngleInDegrees * Mathf.Deg2Rad;

            float v2 = (RangeAttackData.Acceleration * x * x) / (2 * (y - Mathf.Tan(AngleInRad) * x) *
                                      Mathf.Pow(Mathf.Cos(AngleInRad), 2));

            float v = Mathf.Sqrt(Mathf.Abs(v2)) * GravityMultiplier;

            return v;
        }
        private bool RangeAttackReset()
        {
            //if (RangeAttackData.TimeBtwAttacks <= 0f)
            //{
            //    RangeAttackData.TimeBtwAttacks = RangeAttackData.StartTimeBtwAttacks;
            //    return true;
            //}
            //else
            //{
            //    RangeAttackData.TimeBtwAttacks -= Time.fixedDeltaTime;
            //    return false;
            //}

            if (Time.time > RangeAttackData.TimeBtwAttacks)
            {
                RangeAttackData.TimeBtwAttacks = Time.time + RangeAttackData.StartTimeBtwAttacks;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
