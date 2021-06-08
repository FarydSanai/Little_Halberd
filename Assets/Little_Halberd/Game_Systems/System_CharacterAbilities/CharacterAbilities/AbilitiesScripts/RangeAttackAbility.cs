using UnityEngine;
using Random = UnityEngine.Random;

namespace LittleHalberd
{
    [CreateAssetMenu(fileName = "New ability", menuName = "LittleHalberd/Ability/RangeAttackAbility")]
    public class RangeAttackAbility : CharacterAbility
    {
        [Header("Data Settings")]
        public float AngleInDegrees;
        public ObjectType ProjectileType;
        [Header("Time settings")]
        public float StartTimeBtwAttacks;
        public int MaxEnemiesSpawn;
        public int EnemiesNumber;

        private bool IsReset;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            InitRangeAttackData(characterState);
            IsReset = true;
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (IsReset)
            {
                if (ProjectileType == ObjectType.PUMPKIN_BOMB_SPAWN &&
                    !BossSpawnEnemiesCounter.Instance.AllowSpawnEnemy())
                {
                    IsReset = false;
                    return;         
                }
                characterState.RANGE_ATTACK_DATA.ProcessRangeAtatck();
                IsReset = false;
            }
        }
        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
        private void InitRangeAttackData(CharacterState characterState)
        {
            characterState.RANGE_ATTACK_DATA.Target = CharacterManager.Instance.PlayableCharacter.transform;
            characterState.RANGE_ATTACK_DATA.SpawnPoint = characterState.ATTACK_DATA.AttackPoint;
            characterState.RANGE_ATTACK_DATA.AngleInDegrees = AngleInDegrees;
            characterState.RANGE_ATTACK_DATA.projectileType = ProjectileType;

            float rand = Random.Range(StartTimeBtwAttacks, StartTimeBtwAttacks + 1f);
            characterState.RANGE_ATTACK_DATA.StartTimeBtwAttacks = rand;

            if (ProjectileType == ObjectType.PUMPKIN_BOMB_SPAWN)
            {
                characterState.RANGE_ATTACK_DATA.MaxEnemiesSpawn = MaxEnemiesSpawn;
            }
        }
    }
}
