using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class PatrolPlatform : IPatrolPlatform
    {
        private List<ContactPoint2D> contacts;
        private bool changePatrolDir;
        private Collider2D groundCollider;
        private int GroundLayer;
        public CharacterControl control;
        public PatrolPlatform(CharacterControl control)
        {
            this.control = control;
            this.contacts = new List<ContactPoint2D>();
            this.changePatrolDir = true;
            this.groundCollider = null;
            this.GroundLayer = LayerMask.NameToLayer("Ground");
        }

        public void PatrolArea()
        {
            if (control.GROUND_DATA.IsGrounded())
            {
                int contactsNumber = control.RIGID_BODY.GetContacts(contacts);
                if (groundCollider == null)
                {
                    groundCollider = contacts.Find(c =>
                                                   c.collider.gameObject.layer == GroundLayer).collider;
                }

                if (groundCollider != null)
                {
                    AIPatrolPlatform(groundCollider);
                }
            }
        }
        private void AIPatrolPlatform(Collider2D groundCollider)
        {
            if (changePatrolDir && control.transform.position.x > groundCollider.bounds.min.x)
            {
                control.MoveLeft = true;
                control.MoveRight = false;
            }
            else if (changePatrolDir)
            {
                changePatrolDir = false;
            }

            if (!changePatrolDir && control.transform.position.x < groundCollider.bounds.max.x)
            {
                control.MoveRight = true;
                control.MoveLeft = false;
                return;
            }
            else if (!changePatrolDir)
            {
                changePatrolDir = true;
            }
        }
    }
}