using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class CharacterManager : Singleton<CharacterManager>
    {
        public List<CharacterControl> Characters = new List<CharacterControl>();

        public CharacterControl GetPlayableCharacter()
        {
            foreach (CharacterControl control in Characters)
            {
                if (control.subComponentProcessor.ArrSubComponents[(int)SubComponentType.MANUAL_INPUT] != null)
                {
                    Debug.Log("Return control");
                    return control;
                }
            }
            return null;
        }
    }
}