using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class DividingPost : MonoBehaviour
    {
        [SerializeField] private GameObject ClosedBlock;
        [SerializeField] private bool ClosedFromStart;
        private void OnEnable()
        {
            FoliageMeshHelper.EnableMeshForGrassPath(this);
        }
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
            if (other.gameObject.layer == CustomLayers.Instance.GetLayer(LH_Layer.Player))
            {
                yield return new WaitForSeconds(0.3f);
                PoolObjectLoader.Instance.GetObject(ObjectType.VFX_DEBRIS,
                                                    ClosedBlock.transform.position - new Vector3(0f, 1f, 0f),
                                                    Quaternion.identity);
                ClosedBlock.SetActive(true);
                this.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}
