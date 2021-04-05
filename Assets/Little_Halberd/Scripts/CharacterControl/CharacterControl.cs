using UnityEngine;

namespace LittleHalberd
{
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
        public MovingData MOVING_DATA => subComponentProcessor.movingData;
        public GroundData GROUND_DATA => subComponentProcessor.groundData;
        public AttackData ATTACK_DATA => subComponentProcessor.attackData;
        public DamageData DAMAGE_DATA => subComponentProcessor.damageData;

        [Header("SubComponents")]
        public SubComponentProcessor subComponentProcessor;

        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider2D>();
            subComponentProcessor = GetComponentInChildren<SubComponentProcessor>();

            RegisterCharacter();            
        }
        private void OnEnable()
        {
            InitCharacterStates(characterAnimator);
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
        private void RegisterCharacter()
        {
            if (!CharacterManager.Instance.Characters.Contains(this))
            {
                CharacterManager.Instance.Characters.Add(this);
            }
            if (this.GetComponentInChildren<ManualInput>() != null)
            {
                CharacterManager.Instance.PlayableCharacter = this;
            }
        }
    }
}