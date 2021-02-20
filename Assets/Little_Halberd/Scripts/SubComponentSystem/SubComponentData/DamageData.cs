namespace LittleHalberd
{
    [System.Serializable]
    public class DamageData
    {
        public float CurrentHP;
        public bool isDead;

        public delegate void CharacterAction(float damage);
        public CharacterAction TakeDamage;
    }
}