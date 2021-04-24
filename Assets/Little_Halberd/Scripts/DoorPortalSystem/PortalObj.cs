using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class PortalObj : MonoBehaviour
    {
        [SerializeField] private PortalObj TargetPortal;

        private bool ResetPortal = true;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (TargetPortal != null && TargetPortal != this)
            {
                if (this.ResetPortal && TargetPortal.ResetPortal)
                {
                    other.transform.position = TargetPortal.transform.position;
                    this.ResetPortal = false;
                    TargetPortal.ResetPortal = false;
                }
            }
        }
        private IEnumerator OnTriggerExit2D(Collider2D collision)
        {
            if (TargetPortal != null && TargetPortal != this)
            {
                this.ResetPortal = true;

                yield return new WaitForSeconds(1f);

                TargetPortal.ResetPortal = true;
            }
        }
    }
}
