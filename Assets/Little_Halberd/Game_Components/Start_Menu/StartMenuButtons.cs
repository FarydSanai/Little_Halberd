using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LittleHalberd
{
    public class StartMenuButtons : MonoBehaviour
    {
        public bool StartMenuIsActive = true;
        [SerializeField] private GameObject StartMenuObj;
        [SerializeField] private GameObject ControlKeysObj;
        public void StartGame()
        {
            SceneManager.LoadScene("Level_1");
        }
        public void SetMenuWindow()
        {
            if (StartMenuIsActive)
            {
                StartMenuObj.SetActive(false);
                StartMenuIsActive = false;
                ControlKeysObj.SetActive(true);
            }
            else
            {
                StartMenuObj.SetActive(true);
                StartMenuIsActive = true;
                ControlKeysObj.SetActive(false);
            }
        }
    }
}