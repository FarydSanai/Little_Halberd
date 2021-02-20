using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class CharacterGround : SubComponent
    {
        public GroundData groundData;

        [SerializeField]
        private LayerMask layerMask;

        private void Start()
        {
            groundData = new GroundData
            {
                IsGrounded = Grounded,
            };
            subComponentProcessor.groundData = groundData;
            subComponentProcessor.ArrSubComponents[(int)SubComponentType.CHARACTER_GROUND] = this;
        }
        public override void OnFixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnUpdate()
        {
            throw new System.NotImplementedException();
        }
        private bool Grounded()
        {
            float dist = 0.1f;
            RaycastHit2D raycastHit = Physics2D.BoxCast(control.boxCollider.bounds.center, control.boxCollider.bounds.size, 0f, Vector2.down, dist, layerMask);

            Color rayColor;

            if (raycastHit.collider != null)
            {
                rayColor = Color.green;
            }
            else
            {
                rayColor = Color.red;
            }

            DrawRaycastHit(dist, rayColor);

            return raycastHit.collider != null;
        }

        private void DrawRaycastHit(float dist, Color rayColor)
        {
            Debug.DrawRay(control.boxCollider.bounds.center + new Vector3(control.boxCollider.bounds.extents.x, 0),
              Vector2.down * (control.boxCollider.bounds.extents.y + dist),
              rayColor);
            Debug.DrawRay(control.boxCollider.bounds.center - new Vector3(control.boxCollider.bounds.extents.x, 0),
                          Vector2.down * (control.boxCollider.bounds.extents.y + dist),
                          rayColor);
            Debug.DrawRay(control.boxCollider.bounds.center - new Vector3(control.boxCollider.bounds.extents.x, control.boxCollider.bounds.extents.y + dist),
                          Vector2.right * (control.boxCollider.bounds.size.x),
                          rayColor);
        }
    }
}