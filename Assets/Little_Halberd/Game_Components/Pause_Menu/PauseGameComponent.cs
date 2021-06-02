using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LittleHalberd
{
    public class PauseGameComponent : MonoBehaviour
    {
        [SerializeField] private MenuWindowController Menu;
        private bool GameIsPaused;

        public static PauseGameComponent Instance;
        private void Awake()
        {
            Instance = this;
        }
        public void SetGamePause()
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

    }
}