using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class DividingPost : MonoBehaviour
    {
        [SerializeField] private GameObject ClosedBlock;
        [SerializeField] private bool ClosedFromStart;
        private void Start()
        {
            FoliageMeshHelper.EnableMeshForGrassPath(this);

            if (ClosedFromStart)
            {
                ClosedBlock.SetActive(true);
            }
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
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 500f);
        }
    }
}
