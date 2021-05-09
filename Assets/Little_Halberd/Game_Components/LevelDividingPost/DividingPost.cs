using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class DividingPost : MonoBehaviour
    {
        [SerializeField] private GameObject ClosedBlock; 
        private void Start()
        {
            FoliageMeshHelper.EnableMeshForGrassPath(this);
        }

        private IEnumerator OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                yield return new WaitForSeconds(0.5f);
                ClosedBlock.SetActive(true);
            }
        }
        private void OnTriggerStay2D(Collider2D other)
        {
            other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * 5000f;
        }
    }
}
