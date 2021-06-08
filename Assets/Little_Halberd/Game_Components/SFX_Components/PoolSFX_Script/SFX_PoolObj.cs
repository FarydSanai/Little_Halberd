using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class SFX_PoolObj : MonoBehaviour, IPooledObject
    {
        [SerializeField] ObjectType Type;
        public ObjectType objectType => Type;

        private void OnEnable()
        {
            StartCoroutine(_DeactivateSelf());
        }
        private IEnumerator _DeactivateSelf()
        {
            AudioSource audioSource = this.GetComponent<AudioSource>();
            
            while(true && audioSource != null)
            {
                yield return new WaitForSeconds(0.5f);
                if (!audioSource.isPlaying)
                {
                    PoolObjectLoader.Instance.DestroyObject(this.gameObject);
                    break;
                }
            }
        }
    }
}