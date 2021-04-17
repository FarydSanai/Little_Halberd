using System.Collections.Generic;

namespace LittleHalberd
{
    public class CharacterManager : Singleton<CharacterManager>
    {
        public List<CharacterControl> Characters = new List<CharacterControl>();
        public CharacterControl PlayableCharacter;
        public CharacterControl GetPlayableCharacter()
        {
            if (PlayableCharacter == null)
            {
                foreach (CharacterControl control in Characters)
                {
                    if (control.subComponentProcessor.ArrSubComponents[(int)SubComponentType.MANUAL_INPUT] != null)
                    {
                        return control;
                    }
                }
            }
            return PlayableCharacter;
        }
        public bool CharacterIsPlayable(CharacterControl control)
        {
            if (control.subComponentProcessor.ArrSubComponents[(int)SubComponentType.MANUAL_INPUT] != null)
            {
                return true;
            }
            return false;
        }
    }
}