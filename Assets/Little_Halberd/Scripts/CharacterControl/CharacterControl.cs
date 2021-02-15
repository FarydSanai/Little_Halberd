using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public enum MoveDirection
    {
        Right,
        Left,
    }
    public class CharacterControl : MonoBehaviour
    {
        public bool MoveRight;
        public bool MoveLeft;
        public bool Run;
        public bool Jump;
        public bool Attack;

        public Animator characterAnimator;
        public BoxCollider2D boxCollider;

        private Rigidbody2D rigid;

        //Temp
        CharacterAttack attack;
        public Rigidbody2D RIGID_BODY
        {
            get
            {
                if (rigid == null)
                {
                    rigid = this.GetComponent<Rigidbody2D>();
                }
                return rigid;
            }
        }
        public AttackData ATTACK_DATA => subComponentProcessor.attackData;
        public DamageData DAMAGE_DATA => subComponentProcessor.damageData;

        [Header("SubComponents")]
        public SubComponentProcessor subComponentProcessor;

        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider2D>();
            subComponentProcessor = GetComponentInChildren<SubComponentProcessor>();
            attack = GetComponent<CharacterAttack>();
            
        }
        private void Update()
        {
            subComponentProcessor.UpdateSubComponents();
        }
        private void FixedUpdate()
        {
            subComponentProcessor.FixedUpdateSubComponents();
        }
        public void InitCharacterStates(Animator animator)
        {
            CharacterState[] statesArr = animator.GetBehaviours<CharacterState>();

            for (int i = 0; i < statesArr.Length; i++)
            {
                statesArr[i].control = this;
            }
        }
        public Vector3 GetPosition()
        {
            return this.gameObject.transform.position;
        }
    }
}