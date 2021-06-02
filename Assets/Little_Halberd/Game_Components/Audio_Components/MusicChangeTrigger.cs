using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class MusicChangeTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {

            if (other.gameObject.GetComponent<CharacterControl>() ==
                CharacterManager.Instance.PlayableCharacter)

            {
                BackgroundMusic.Instance.SetBossAudioClip();
            }
        }
    }
}