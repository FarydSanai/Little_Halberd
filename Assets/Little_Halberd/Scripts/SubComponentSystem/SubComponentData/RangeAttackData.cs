using UnityEngine;

namespace LittleHalberd
{
    [System.Serializable]
    public class RangeAttackData
    {
        public float Acceleration;
        public float AngleInDegrees;

        public Transform Target;
        public Transform SpawnPoint;

        public ObjectType projectileType;

        public float TimeBtwAttacks;
        public float StartTimeBtwAttacks;

        public delegate void CharacterAction();
        public CharacterAction AimTarget;
        public CharacterAction ProcessRangeAtatck;

        public delegate bool GetBool();
        public GetBool RangeAttackReset;

    }
}