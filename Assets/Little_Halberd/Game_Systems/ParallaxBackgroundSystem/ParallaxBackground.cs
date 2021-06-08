using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class ParallaxBackground : MonoBehaviour
    {
        [SerializeField] private Vector2 parallaxEffectMultiplier;
        [SerializeField] private bool infiniteHorizontal;
        [SerializeField] private bool infiniteVertical;

        [SerializeField] private Transform cameraTransform;
        private Vector3 lastCameraPosition;
        private float textureUnitSizeX;
        private float textureUnitSizeY;

        void Start()
        {
            //cameraTransform = Camera.main.transform;
            lastCameraPosition = cameraTransform.position;

            Sprite sprite = this.GetComponent<SpriteRenderer>().sprite;
            Texture2D texture = sprite.texture;

            textureUnitSizeX = (texture.width / sprite.pixelsPerUnit) * transform.localScale.x;
            textureUnitSizeY = (texture.height / sprite.pixelsPerUnit) * transform.localScale.y;
        }

        private void LateUpdate()
        {
            Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

            this.transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x,
                                                   deltaMovement.y * parallaxEffectMultiplier.y, 0f);

            lastCameraPosition = cameraTransform.position;

            if (infiniteHorizontal)
            {
                if (Mathf.Abs(cameraTransform.position.x - this.transform.position.x) >= textureUnitSizeX)
                {
                    float offsetPositionX = (cameraTransform.position.x - this.transform.position.x) % textureUnitSizeX;
                    this.transform.position = new Vector3(cameraTransform.position.x + offsetPositionX,
                                                          this.transform.position.y, 0f);
                }
            }

            if (infiniteVertical)
            {
                if (Mathf.Abs(cameraTransform.position.y - this.transform.position.y) >= textureUnitSizeY + 10f)
                {
                    float offsetPositionY = (cameraTransform.position.y - this.transform.position.y) % textureUnitSizeY;
                    this.transform.position = new Vector3(this.transform.position.x,
                                                          cameraTransform.position.y + offsetPositionY, 0f);
                }
            }

        }
    }
}

