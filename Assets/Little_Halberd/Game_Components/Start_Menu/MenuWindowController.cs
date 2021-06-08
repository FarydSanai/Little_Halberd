using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LittleHalberd
{
    public enum LittleHalberdScenes
    {
        Level_0,
        Level_1,
    }
    [System.Serializable]
    public struct MenuWindow
    {
        public MenuWindowType WindowType;
        public GameObject WindowObj;
    }
    public class MenuWindowController : MonoBehaviour
    {
        public List<MenuWindow> MenuWindows = new List<MenuWindow>();
        [Space(20f)]
        public GameObject CurrentActiveWindow;

        private Dictionary<MenuWindowType, GameObject> WindowDict = new Dictionary<MenuWindowType, GameObject>();
        private void Awake()
        {
            foreach (MenuWindow w in MenuWindows)
            {
                WindowDict.Add(w.WindowType, w.WindowObj);
            }
        }
        private void OnEnable()
        {
            CurrentActiveWindow = WindowDict[MenuWindowType.MainMenu];
            CurrentActiveWindow.SetActive(true);
        }
        public void StartGame()
        {
            if (PauseGameComponent.Instance != null)
            {
                PauseGameComponent.Instance.SetEndGame(false);
            }
            SceneManager.LoadScene(LittleHalberdScenes.Level_1.ToString());
        }
        public void ChangeSceneToMainMenu()
        {
            PauseGameComponent.Instance.SetEndGame(false);
            if (PauseGameComponent.Instance.GAME_IS_PAUSED)
            {
                PauseGameComponent.Instance.SetGamePause();
            }
            SceneManager.LoadScene(LittleHalberdScenes.Level_0.ToString());
        }
        public void SetIntroScene()
        {
            PauseGameComponent.Instance.SetEndGame(false);
            SceneManager.LoadScene(LittleHalberdScenes.Level_0.ToString());
        }
        public void SetMenuWindow(MenuWindowEnum mw)
        {
            CurrentActiveWindow.SetActive(false);
            CurrentActiveWindow = WindowDict[mw.type];
            CurrentActiveWindow.SetActive(true);
        }
        public void SetMenuWindow(MenuWindowType type)
        {
            if (CurrentActiveWindow != null)
            {
                CurrentActiveWindow.SetActive(false);
            }
            CurrentActiveWindow = WindowDict[type];
            CurrentActiveWindow.SetActive(true);
        }
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}