using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class HealthPoint : MonoBehaviour, IPooledObject
    {
        [SerializeField] private float HealPoints;

        [SerializeField] private ObjectType Type;
        public ObjectType objectType => Type;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == CustomLayers.Instance.GetLayer(LH_Layer.Player))
            {
                CharacterControl control = other.gameObject.GetComponent<CharacterControl>();
                if (control != null)
                {
                    control.DAMAGE_DATA.CurrentHP += HealPoints;
                    if (control.DAMAGE_DATA.CurrentHP > control.CharacterMaxHP)
                    {
                        control.DAMAGE_DATA.CurrentHP = control.CharacterMaxHP;
                    }
                    control.HEALTH_BAR_DATA.ChangeHealthBar(HealPoints);

                    PoolObjectLoader.Instance.GetObject(ObjectType.VFX_PICKUP_HEART,
                                                        this.transform.position + new Vector3(0f, 1f, 0f),
                                                        Quaternion.identity);

                    PoolObjectLoader.Instance.DestroyObject(this.gameObject);
                }

            }
        }
    }
}