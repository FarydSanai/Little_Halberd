using UnityEngine;
using UnityEngine.SceneManagement;

namespace LittleHalberd
{
    public class MenuWindowController : MonoBehaviour
    {
        public bool MainMenuWindowIsActive = true;
        [SerializeField] private GameObject MainMenuWindowObj;
        [SerializeField] private GameObject ControlKeysWindowObj;
        public void StartGame()
        {
            SceneManager.LoadScene("Level_1");
        }
        public void SetMenuWindow()
        {
            if (MainMenuWindowIsActive)
            {
                MainMenuWindowObj.SetActive(false);
                MainMenuWindowIsActive = false;
                ControlKeysWindowObj.SetActive(true);
            }
            else
            {
                MainMenuWindowObj.SetActive(true);
                MainMenuWindowIsActive = true;
                ControlKeysWindowObj.SetActive(false);
            }
        }
    }
}