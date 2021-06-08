using UnityEngine;
using Random = UnityEngine.Random;

namespace LittleHalberd
{
    public class Projectile : MonoBehaviour, IPooledObject
    {
        [SerializeField] private ObjectType ExplodeVFX;
        [SerializeField] private ObjectType RepelVFX;
        [SerializeField] private float ProjectileDamage = 100f;
        [SerializeField] private bool IsSpawnEnemy;
        private int bitMask = (1 << 3) | (1 << 0);

        [SerializeField] private ObjectType Type;
        public ObjectType objectType => Type;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer != bitMask)
            {
                SetDamage(other);
                PoolObjectLoader.Instance.GetObject(ExplodeVFX,
                                                    this.transform.position,
                                                    Quaternion.identity);
                if (IsSpawnEnemy)
                {
                    SpawnEnemy();
                }
                PoolObjectLoader.Instance.GetObject(ObjectType.SFX_HIT_PROJECTILE);
                PoolObjectLoader.Instance.DestroyObject(this.gameObject);
            }
        }
        private void SetDamage(Collision2D other)
        {
            CharacterControl otherControl = other.gameObject.GetComponent<CharacterControl>();
            if (otherControl != null)
            {
                float dir = (other.gameObject.transform.position - this.transform.position).x;

                if (dir > 0f)
                {
                    otherControl.DAMAGE_DATA.AttackerIsLeft = true;
                }
                else
                {
                    otherControl.DAMAGE_DATA.AttackerIsRight = true;
                }
                otherControl.DAMAGE_DATA.TakeDamage(ProjectileDamage);
            }
        }
        private void SpawnEnemy()
        {
            int rand = Random.Range(0, PoolObjectLoader.Instance.EnemyTypeInfo.Count);
            //Debug.Log(rand);
            GameObject enemy = PoolObjectLoader.Instance.GetObject(
                                       PoolObjectLoader.Instance.EnemyTypeInfo[rand].objectType,
                                       this.transform.position + new Vector3(0f, 3f, 0f),
                                       Quaternion.identity);
            //enemy.transform.position = this.transform.position + new Vector3(0f, 3f, 0f);

            enemy.GetComponent<CharacterControl>().isSpawned = true;
            BossSpawnEnemiesCounter.Instance.CurrentEnemiesNumber++;
        }
    }
}