using UnityEngine;

namespace LittleHalberd
{
    [System.Serializable]
    public enum MenuWindowType
    {
        MainMenu,
        ControlKeys,
        TryAgain,
        Ending,
    }
    public class MenuWindowEnum : MonoBehaviour
    {
        public MenuWindowType type;
    }
}
