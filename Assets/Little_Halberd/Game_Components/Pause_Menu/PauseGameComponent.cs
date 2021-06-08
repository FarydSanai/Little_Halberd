using UnityEngine;

namespace LittleHalberd
{
    public class PauseGameComponent : MonoBehaviour
    {
        [SerializeField] private MenuWindowController Menu;
        private bool GameIsPaused;
        public bool GAME_IS_PAUSED
        {
            get
            {
                return GameIsPaused;
            }
        }
        private bool GameIsEnd = false;

        public static PauseGameComponent Instance;
        private void Awake()
        {
            Instance = this;
        }
        public void SetGamePause()
        {
            if (!GameIsEnd)
            {
                if (GameIsPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
        private void PauseGame()
        {
            Time.timeScale = 0f;
            GameIsPaused = true;
            AudioListener.pause = true;
            Menu.gameObject.SetActive(true);
        }
        private void ResumeGame()
        {
            Time.timeScale = 1f;
            GameIsPaused = false;
            AudioListener.pause = false;
            Menu.gameObject.SetActive(false);
        }
        public void SetTryAgainWindow()
        {
            Menu.SetMenuWindow(MenuWindowType.TryAgain);
        }
        public void SetEndingWindow()
        {
            Menu.SetMenuWindow(MenuWindowType.Ending);
        }
        public void SetEndGame(bool gameIsEnd)
        {
            GameIsEnd = gameIsEnd;
        }
    }
}