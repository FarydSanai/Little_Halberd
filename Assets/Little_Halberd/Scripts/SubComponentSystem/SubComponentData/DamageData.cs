namespace LittleHalberd
{
    [System.Serializable]
    public class DamageData
    {
        public float CurrentHP;

        public bool isDead;
        public bool AttackerIsLeft;
        public bool AttackerIsRight;
        public bool isDamaging;

        public delegate void CharacterAction(float damage);
        public CharacterAction TakeDamage;

        public delegate void CharacterStateAction(CharacterControl control);
        public CharacterStateAction ProcessDeath;
    }
}